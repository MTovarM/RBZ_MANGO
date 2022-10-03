namespace RiderbikeZone.Formularios
{
    using RiderbikeZone.Modelos;
    using RiderbikeZone.Servicios;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public partial class EditarCliente : Form
    {
        #region Propiedades
        public bool IsNewClient { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public ClientNew Cliente { get; set; }
        public int ANI { get; set; }
        #endregion

        #region Constructor
        public EditarCliente(ClientNew _cliente, string _email, string _clave, string _nombre)
        {
            Cliente = new ClientNew() { Identificacion = new _Identificacion(), Ubicacion = new _UbicacionNew()};

            InitializeComponent();
            Email = _email;
            Clave = _clave;
            ANI = 0;
            if (_nombre == "nuevo_cliente")
            {
                IsNewClient = true;
            }
            else
            {
                IsNewClient = false;
                Nombre = _nombre;
                this.Cliente = _cliente;
                Cedula_NumericUpDown.Value = Convert.ToInt32(this.Cliente.Identificacion.Numero);
                Nombre_TextBox.Text = _cliente.Nombre;
                IsDetal_ComboBox.Text = _cliente.IsDetal ? "Detal" : "Por Mayor";
                TipoPersona_ComboBox.Text = _cliente.IsPersonaNatural ? "Natural" : "Juridica";
                TipoIdentificacion_ComboBox.Text = _cliente.Identificacion.Tipo;

                if (_cliente.Identificacion.DigitoVe == -1)
                {
                    Digito_Label.Visible = false;
                    Digito_NumericUpDown.Visible = false;
                }
                else
                {
                    Digito_NumericUpDown.Value = _cliente.Identificacion.DigitoVe;
                }
                Sexo_ComboBox.Text = _cliente.IsMan ? "Masculino" : "Femenino";

                Direccion_TextBox.Text = _cliente.Ubicacion.Direccion;
                Ciudad_TextBox.Text = _cliente.Ubicacion.Ciudad;
                Departamento_ComboBox.Text = _cliente.Ubicacion.Departamento;
                Contacto_TextBox.Text = _cliente.Contacto;
                Origen_ComboBox.Text = _cliente.Origen;
                Pago_ComboBox.Text = _cliente.Pago;
                Listo_Button.Enabled = false;
            }
        }
        #endregion

        #region Métodos
        private void Volver_IconPictureBox_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private async void Listo_Button_Click(object sender, EventArgs e)
        {
            Animacion_Panel.Visible = true;
            Animacion_Timer.Enabled = true;
            ClientNew c = new ClientNew();
            _Identificacion ide = new _Identificacion();
            _UbicacionNew ubi = new _UbicacionNew();
            
            try
            {
                DateTime _fecha = DateTime.Now;
                List<string> Mes = new List<string>()
                {"ene","feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic"};
                string Fecha = _fecha.Day + "/" + Mes[_fecha.Month - 1] + "/" + _fecha.Year;
                c.Fecha = Fecha;
                c.Nombre = Nombre_TextBox.Text.ToUpper();
                c.IsDetal = IsDetal_ComboBox.Text == "Detal" ? true : false;
                c.IsPersonaNatural = TipoPersona_ComboBox.Text == "Natural" ? true : false;
                ide.Tipo = TipoIdentificacion_ComboBox.Text;
                if (Digito_Label.Visible) ide.DigitoVe = Convert.ToInt32(Digito_NumericUpDown.Value);
                else ide.DigitoVe = -1;
                c.IsMan = Sexo_ComboBox.Text == "Masculino" ? true : false;
                ubi.Departamento = Departamento_ComboBox.Text;
                ubi.Direccion = Direccion_TextBox.Text;
                ubi.Ciudad = Ciudad_TextBox.Text;
                c.Contacto = Contacto_TextBox.Text;
                c.Origen = Origen_ComboBox.Text;
                c.Pago = Pago_ComboBox.Text;
                ide.Numero = Cedula_NumericUpDown.Value.ToString();
                c.Identificacion = ide;
                c.Ubicacion = ubi;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Por favor complete los espacios vacios.", "Espacios vacios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SFirebase FB = new SFirebase()
            {
                Email = this.Email,
                Clave = this.Clave
            };
            try
            {
                var t = await FB.LoginWithEmail(false);
                if (t == null) throw new Exception();
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error de conexión.\nIntente recargando la página.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Animacion_Panel.Visible = false;
                Animacion_Timer.Enabled = false;
                return;
            }
            if (IsNewClient) 
            {
                await FB.Guardar("UCliente", c);

                MessageBox.Show("Cliente agregado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            else
            {
                await FB.DeleteClientNew(Nombre);
                await FB.Guardar("UCliente", c);

                MessageBox.Show("Cliente modificado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        private void Cambio(object sender, EventArgs e)
        {
            Listo_Button.Enabled = true;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void PanelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Animacion_Timer_Tick(object sender, EventArgs e)
        {
            int _cambio = -5;
            switch (ANI)
            {
                case 0:
                    CL11.Location = new Point(CL11.Location.X, CL11.Location.Y - _cambio);
                    CL1.Location = new Point(CL1.Location.X, CL1.Location.Y + _cambio);
                    ANI++;
                    break;
                case 1:
                    CL1.Location = new Point(CL1.Location.X, CL1.Location.Y - _cambio);
                    CL2.Location = new Point(CL2.Location.X, CL2.Location.Y + _cambio);
                    ANI++;
                    break;
                case 2:
                    CL2.Location = new Point(CL2.Location.X, CL2.Location.Y - _cambio);
                    CL3.Location = new Point(CL3.Location.X, CL3.Location.Y + _cambio);
                    ANI++;
                    break;
                case 3:
                    CL3.Location = new Point(CL3.Location.X, CL3.Location.Y - _cambio);
                    CL4.Location = new Point(CL4.Location.X, CL4.Location.Y + _cambio);
                    ANI++;
                    break;
                case 4:
                    CL4.Location = new Point(CL4.Location.X, CL4.Location.Y - _cambio);
                    CL5.Location = new Point(CL5.Location.X, CL5.Location.Y + _cambio);
                    ANI++;
                    break;
                case 5:
                    CL5.Location = new Point(CL5.Location.X, CL5.Location.Y - _cambio);
                    CL6.Location = new Point(CL6.Location.X, CL6.Location.Y + _cambio);
                    ANI++;
                    break;
                case 6:
                    CL6.Location = new Point(CL6.Location.X, CL6.Location.Y - _cambio);
                    CL7.Location = new Point(CL7.Location.X, CL7.Location.Y + _cambio);
                    ANI++;
                    break;
                case 7:
                    CL7.Location = new Point(CL7.Location.X, CL7.Location.Y - _cambio);
                    CL8.Location = new Point(CL8.Location.X, CL8.Location.Y + _cambio);
                    ANI++;
                    break;
                case 8:
                    CL8.Location = new Point(CL8.Location.X, CL8.Location.Y - _cambio);
                    CL9.Location = new Point(CL9.Location.X, CL9.Location.Y + _cambio);
                    ANI++;
                    break;
                case 9:
                    CL9.Location = new Point(CL9.Location.X, CL9.Location.Y - _cambio);
                    CL10.Location = new Point(CL10.Location.X, CL10.Location.Y + _cambio);
                    ANI++;
                    break;
                case 10:
                    CL10.Location = new Point(CL10.Location.X, CL10.Location.Y - _cambio);
                    CL11.Location = new Point(CL11.Location.X, CL11.Location.Y + _cambio);
                    ANI = 0;
                    break;
            }
        }
        #endregion
    }
}
