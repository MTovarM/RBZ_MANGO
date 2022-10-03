namespace RiderbikeZone.Formularios
{
    using RiderbikeZone.Servicios;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public partial class RegistrarUsuario : Form
    {
        #region Propiedades
        public int ANI { get; private set; }
        public string Email { get; private set; }
        public string Clave { get; private set; }
        #endregion

        #region Constructor
        public RegistrarUsuario()
        {
            ANI = 0;
            InitializeComponent();
        }
        #endregion

        #region Métodos
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void PanelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Volver_IconPictureBox_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private async void Registrar_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Email_TextBox.Text))
            {
                MessageBox.Show("Ingrese el correo del nuevo usuario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(ConfirmarEmail_TextBox.Text))
            {
                MessageBox.Show("Confirme el correo del nuevo usuario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(Clave_TextBox.Text))
            {
                MessageBox.Show("Ingrese la clave del nuevo usuario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(ConfirmarClave_TextBox.Text))
            {
                MessageBox.Show("Confirme la clave del nuevo usuario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Email_TextBox.Text != ConfirmarEmail_TextBox.Text)
            {
                MessageBox.Show("Los correos no coinciden.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ConfirmarEmail_TextBox.Text = string.Empty;
                return;
            }
            if (Clave_TextBox.Text != ConfirmarClave_TextBox.Text)
            {
                MessageBox.Show("Las claves no coinciden no coinciden.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ConfirmarClave_TextBox.Text = string.Empty;
                return;
            }

            Animacion_Timer.Enabled = true;
            Animacion2_Panel.Visible = true;
            Email = Email_TextBox.Text;
            Clave = Clave_TextBox.Text;
            SFirebase FB = new SFirebase()
            {
                Email = this.Email,
                Clave = this.Clave
            };
            try
            {
                var t = await FB.LoginWithEmail(true);
                if (t == null) throw new Exception();
                SEscribirTXT TXT = new SEscribirTXT();
                Animacion_Timer.Enabled = false;
                Animacion2_Panel.Visible = false;
                MessageBox.Show("Usuario registrado con éxito.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            catch (Exception)
            {
                Clave = string.Empty;
                MessageBox.Show("Ha ocurrido un error de conexión. Intente de nuevo\no pruebe cambiando el usuario.",
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Animacion_Timer.Enabled = false;
                Animacion2_Panel.Visible = false;
                return;
            }
        }

        private void Animacion_Timer_Tick(object sender, EventArgs e)
        {
            int _cambio = -5;
            switch (ANI)
            {
                case 0:
                    CL13.Location = new Point(CL13.Location.X, CL13.Location.Y - _cambio);
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
                    ANI++;
                    break;
                case 11:
                    CL11.Location = new Point(CL11.Location.X, CL11.Location.Y - _cambio);
                    CL12.Location = new Point(CL12.Location.X, CL12.Location.Y + _cambio);
                    ANI++;
                    break;
                case 12:
                    CL12.Location = new Point(CL12.Location.X, CL12.Location.Y - _cambio);
                    CL13.Location = new Point(CL13.Location.X, CL13.Location.Y + _cambio);
                    ANI = 0;
                    break;
            }
        }

        private void Email_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (Email_TextBox.Text != ConfirmarEmail_TextBox.Text) CorreosMal_Label.Visible = true;
            else CorreosMal_Label.Visible = false;
        }

        private void Clave_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (Clave_TextBox.Text != ConfirmarClave_TextBox.Text) ClavesMal_Label.Visible = true;
            else ClavesMal_Label.Visible = false;
        }
        #endregion
    }
}
