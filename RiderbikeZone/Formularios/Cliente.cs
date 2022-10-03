namespace RiderbikeZone.Formularios
{
    using RiderbikeZone.Modelos;
    using RiderbikeZone.Servicios;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public partial class Cliente : Form
    {
        #region Propiedades
        public List<int> Ganancias { get; set; }
        public int ANI { get; set; }
        public List<int> Unidadess { get; set; }
        public List<int> Valores { get; set; }
        public List<int> ValoresCUTemp { get; set; }
        public int Total { get; set; }
        public string Fecha { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public List<Producto> Productos { get; set; }
        public List<ClientNew> Clientes { get; set; }
        public string TelefonoEmpresa { get; set; }
        public string CorreoEmpresa { get; set; }
        public bool IsVenta { get; set; }
        public SEscribirTXT TXT { get; set; }
        public double IVA { get; set; }
        public ContextMenuStrip menu { get; set; }
        #endregion

        #region Constructor
        public Cliente(List<ClientNew> _clientes, bool _isVenta, List<Producto> _productos,
            string _email, string _clave, int _total, DataGridView _dataGridView, List<int> _unidades, 
            List<int> _valores, List<int> _valoresCUTemp, List<int> _ganancias)
        {
            InitializeComponent();
            Ganancias = _ganancias;
            Clientes = _clientes;
            TXT = new SEscribirTXT();
            IVA = Convert.ToDouble(TXT.Leer("IVA.txt"));
            ValoresCUTemp = _valoresCUTemp;
            CorreoEmpresa = TXT.Leer("CORREO.txt");
            TelefonoEmpresa = TXT.Leer("TELEFONO.txt");
            Vendedor_TextBox.Text = TXT.Leer("VENDEDOR.txt");
            Unidadess = _unidades;
            Valores = _valores;
            Total = _total;
            IsVenta = _isVenta;
            var x = IsVenta ? Listo_Button.Enabled = false : Listo_Button.Enabled = true;
            DateTime _fecha = DateTime.Now;
            List<string> Mes = new List<string>()
            { "ene","feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic" };
            Fecha = _fecha.Day + "/" + Mes[_fecha.Month - 1] + "/" + _fecha.Year;
            Email = _email;
            Clave = _clave;
            Productos = _productos;
            Total_Label.Text = "$ " + _total.ToString("N0") + " COP";
            Fecha_Label.Text = Fecha;
            LLenar_DataGridView(_dataGridView);
        }
        #endregion

        #region Métodos
        private void Volver_IconPictureBox_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }

        public void LLenar_DataGridView(DataGridView _dataGridView)
        {
            List<string> Codigo = new List<string>();
            List<string> Nombre = new List<string>();
            List<string> Unidades = new List<string>();
            List<string> Valor = new List<string>();
            List<string> Ganancia = new List<string>();
            for (int i = 0; i < _dataGridView.Rows.Count; i++)
            {
                Codigo.Add(_dataGridView.Rows[i].Cells[0].Value.ToString());
                Nombre.Add(_dataGridView.Rows[i].Cells[1].Value.ToString());
                Unidades.Add(_dataGridView.Rows[i].Cells[2].Value.ToString());
                Valor.Add(_dataGridView.Rows[i].Cells[3].Value.ToString());
                Ganancia.Add(_dataGridView.Rows[i].Cells[4].Value.ToString());
            }
            foreach (var D in Codigo)
            {
                int i = Carrito_DataGridView.Rows.Add();

                Carrito_DataGridView.Rows[i].DefaultCellStyle.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
                Carrito_DataGridView.Rows[i].Cells[0].Value = D;
                Carrito_DataGridView.Rows[i].Cells[1].Value = Nombre[i];
                Carrito_DataGridView.Rows[i].Cells[2].Value = Unidades[i];
                Carrito_DataGridView.Rows[i].Cells[3].Value = Valor[i];
                Carrito_DataGridView.Rows[i].Cells[4].Value = Ganancia[i];
            }
        }

        private async void Listo_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Origen_ComboBox.Text))
            {
                MessageBox.Show("Por favor seleccione la plataforma de origen del cliente",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(TipoPersona_ComboBox.Text))
            {
                MessageBox.Show("Por favor seleccione el tipo de persona del cliente",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(Nombre_TextBox.Text) ||
                string.IsNullOrEmpty(Ciudad_TextBox.Text) ||
                string.IsNullOrEmpty(Contacto_TextBox.Text) ||
                string.IsNullOrEmpty(Direccion_TextBox.Text) ||
                string.IsNullOrEmpty(Observaciones_TextBox.Text) ||
                string.IsNullOrEmpty(Vendedor_TextBox.Text))
            {
                DialogResult D = MessageBox.Show("Existen espacios en blanco.\n¿Quiere dejarlos en blanco?",
                    "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (D == DialogResult.No) return;
            }
            Animacion();
            ANI = 0;
            Animacion_Panel.Visible = true;
            Animacion_Timer.Enabled = true;
            if (IsVenta)
            {
                try
                {
                SFirebase FB = new SFirebase()
                {
                    Email = this.Email,
                    Clave = this.Clave
                };
                try
                {
                        var tr = await FB.LoginWithEmail(false);
                        if (tr == null) throw new Exception();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ha ocurrido un error de conexión.\nIntente recargando la página.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //foreach (var Pro in Productos)
                    //{
                    //    await FB.BorrarProducto(Pro.Codigo);
                    //    await FB.Guardar("Inventario", Pro);
                    //}
                    List<_Product> _proLis = new List<_Product>();
                    List<Producto> _valorCambiado = Productos;
                    int t = 0;
                    foreach (var pro in _valorCambiado)
                    {
                        pro.PrecioVenta = ValoresCUTemp[t];
                        pro.UnidadesVendidas = Unidadess[t];
                        _proLis.Add(new _Product()
                        {
                            Codigo = pro.Codigo,
                            Nombre = pro.Nombre,
                            Valor = pro.PrecioVenta,
                            Unidades = pro.UnidadesVendidas
                        });
                        t++;
                    }
                    _UbicacionNew Location = new _UbicacionNew()
                    {
                        Ciudad = Ciudad_TextBox.Text.ToUpper(),
                        Departamento = Departamento_ComboBox.Text.ToUpper(),
                        Direccion = Direccion_TextBox.Text
                    };

                    _Identificacion ide = new _Identificacion()
                    {
                        Numero = Cedula_NumericUpDown.Value.ToString(),
                        Tipo = TipoIdentificacion_ComboBox.Text,
                        DigitoVe = Digito_NumericUpDown.Enabled == true ? Convert.ToInt32(Digito_NumericUpDown.Value) : -1
                    };

                    ClientNew _nCliente = new ClientNew()
                    {
                        Fecha = this.Fecha,
                        IsDetal = IsDetal_ComboBox.Text == "Detal" ? true : false,
                        IsPersonaNatural = TipoPersona_ComboBox.Text == "Natural" ? true : false,
                        Identificacion = ide,
                        Nombre = Nombre_TextBox.Text.ToUpper(),
                        IsMan = Sexo_ComboBox.Text == "Masculino",
                        Pago = Pago_ComboBox.Text,
                        Origen = Origen_ComboBox.Text,
                        Ubicacion = Location,
                        Contacto = Contacto_TextBox.Text,
                        Productos = _proLis
                    };
                    int ganancias = 0;
                    foreach (var g in Ganancias) ganancias += g;
                    VentaNew _venta = new VentaNew()
                    {
                        ClientNew = _nCliente,
                        Fecha = this.Fecha,
                        FormaPago = Pago_ComboBox.Text,
                        ListaProductos = _valorCambiado,
                        Observaciones = Observaciones_TextBox.Text,
                        Total = this.Total,
                        Ganancia = ganancias
                    };
                    //if (!string.IsNullOrEmpty(_nCliente.Nombre))
                    //{
                    //    var cont = Clientes.Where(cli => cli.Nombre == Nombre_TextBox.Text).ToList();
                    //    if (cont.Count == 0) await FB.Guardar("UCliente", _nCliente);
                    //}
                    //await FB.Guardar("UVenta", _venta);
                    try
                    {
                        var _consecutivo = Convert.ToInt32(TXT.Leer("CONSECUTIVO.txt"));
                        _consecutivo++;
                        SCrearPDF PDF = new SCrearPDF();
                        PDF.CrearPDF("VENTA", _consecutivo.ToString(), Nombre_TextBox.Text,
                            Cedula_NumericUpDown.Value.ToString("N0"), Direccion_TextBox.Text, Ciudad_TextBox.Text,
                            Contacto_TextBox.Text, Fecha, Observaciones_TextBox.Text,
                            Vendedor_TextBox.Text, Pago_ComboBox.Text, _valorCambiado, Total,
                            TelefonoEmpresa, CorreoEmpresa, Unidadess, Valores, IVA);
                        MessageBox.Show("Venta realizada con éxito. La factura de venta se guardó en " + @"C:\MANGO\Archivos\Ventas", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Venta realizada con éxito. Ocurrio un error al crear la factura.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (ganancias <= 0)
                    {
                        MessageBox.Show("Ha ocurrido un error al calcular la ganancia." +
                                        "Realice la devolución de la venta por favor.", "Error de ganancias",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
                catch (Exception)
                {
                    Animacion_Timer.Enabled = false;
                    Animacion_Panel.Visible = false;
                    MessageBox.Show("Error de conexión, intente de nuevo. Si el problema persiste reinicie el programa.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Listo_Button.Enabled = true;
                    HabilitarControles();
                    return;
                }
            }
            else
            {
                List<_Product> _proLis = new List<_Product>();
                List<Producto> _valorCambiado = Productos;
                int t = 0;
                foreach (var pro in _valorCambiado)
                {
                    pro.PrecioVenta = ValoresCUTemp[t];
                    pro.UnidadesVendidas = Unidadess[t];
                    _proLis.Add(new _Product()
                    {
                        Codigo = pro.Codigo,
                        Nombre = pro.Nombre,
                        Valor = pro.PrecioVenta,
                        Unidades = pro.UnidadesVendidas
                    });
                    t++;
                }

                SCrearPDF PDF = new SCrearPDF();
                PDF.CrearPDF("COTIZACIÓN", "0000", Nombre_TextBox.Text,
                    Cedula_NumericUpDown.Value.ToString("N0"), Direccion_TextBox.Text, Ciudad_TextBox.Text,
                    Contacto_TextBox.Text, Fecha, Observaciones_TextBox.Text,
                    Vendedor_TextBox.Text, Pago_ComboBox.Text, _valorCambiado, Total,
                    TelefonoEmpresa, CorreoEmpresa, Unidadess, Valores, IVA);
                MessageBox.Show("La cotización se guardó en " + @"C:\RiderikeZone\Archivos\Cotizaciones", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.Yes;
                this.Close();
                try
                {
                    /*
                    List<_Product> _proLis = new List<_Product>();
                    List<Producto> _valorCambiado = Productos;
                    int t = 0;
                    foreach (var pro in _valorCambiado)
                    {
                        pro.PrecioVenta = ValoresCUTemp[t];
                        pro.UnidadesVendidas = Unidadess[t];
                        _proLis.Add(new _Product()
                        {
                            Codigo = pro.Codigo,
                            Nombre = pro.Nombre,
                            Valor = pro.PrecioVenta,
                            Unidades = pro.UnidadesVendidas
                        });
                        t++;
                    }
                    
                    SCrearPDF PDF = new SCrearPDF();
                    PDF.CrearPDF("COTIZACIÓN", "0000", Nombre_TextBox.Text,
                        Cedula_NumericUpDown.Value.ToString("N0"), Direccion_TextBox.Text, Ciudad_TextBox.Text,
                        Contacto_TextBox.Text, Fecha, Observaciones_TextBox.Text,
                        Vendedor_TextBox.Text, Pago_ComboBox.Text, _valorCambiado, Total,
                        TelefonoEmpresa, CorreoEmpresa, Unidadess, Valores, IVA);
                    MessageBox.Show("La cotización se guardó en " + @"C:\RiderikeZone\Archivos\Cotizaciones", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                    */
                }
                catch (Exception h)
                {
                    Animacion_Panel.Visible = false;
                    Animacion_Timer.Enabled = false;
                    HabilitarControles();
                    MessageBox.Show("Error al guardar cotización.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
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

        private void Origen_ComboBox_TextChanged(object sender, EventArgs e)
        {
            Listo_Button.Enabled = true;
        }

        public void Animacion()
        {
            Listo_Button.Enabled = false;
            Cedula_NumericUpDown.Enabled = false;
            Nombre_TextBox.Enabled = false;
            Ciudad_TextBox.Enabled = false;
            Contacto_TextBox.Enabled = false;
            Direccion_TextBox.Enabled = false;
            Origen_ComboBox.Enabled = false;
            Observaciones_TextBox.Enabled = false;
            Vendedor_TextBox.Enabled = false;
            Pago_ComboBox.Enabled = false;

            for (int i = 0; i <= Carrito_DataGridView.Rows.Count - 1; i++) Carrito_DataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(90, 90, 90);
            Carrito_DataGridView.GridColor = Color.FromArgb(90, 90, 90);
            Carrito_DataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(90, 90, 90);
        }

        private void HabilitarControles()
        {
            Listo_Button.Enabled = true;
            Cedula_NumericUpDown.Enabled = true;
            Nombre_TextBox.Enabled = true;
            Ciudad_TextBox.Enabled = true;
            Contacto_TextBox.Enabled = true;
            Direccion_TextBox.Enabled = true;
            Origen_ComboBox.Enabled = true;
            Observaciones_TextBox.Enabled = true;
            Vendedor_TextBox.Enabled = true;
            Pago_ComboBox.Enabled = true;

            for (int i = 0; i <= Carrito_DataGridView.Rows.Count - 1; i++) Carrito_DataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.White;
            Carrito_DataGridView.GridColor = Color.White;
            Carrito_DataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
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

        private void Buscar_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Nombre_TextBox.Text)) press();
        }

        private void KeyPress_menustrip(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)8) MessageBox.Show("hola", "titulo", MessageBoxButtons.OK);
            menu.Hide();
            Nombre_TextBox.Text += e.KeyChar;
            Nombre_TextBox.Focus();
            Nombre_TextBox.Select(Nombre_TextBox.Text.Length, 0);
            Nombre_TextBox.Focus();
        }

        public void press()
        {
            var clienteBusqueda = Clientes.Where(p => p.Nombre.ToLower().IndexOf(Nombre_TextBox.Text.ToLower()) != -1).ToList();

            if (clienteBusqueda.Count >= 25) clienteBusqueda.RemoveRange(15,clienteBusqueda.Count-20);
            Listo_Button.Enabled = true;
            menu = new ContextMenuStrip();
            menu.BackColor = Color.FromArgb(180, 180, 180);
            //menu.MaximumSize = 10;
            menu.Size = new Size(menu.Size.Height, Nombre_TextBox.Size.Width);
            foreach (var v in clienteBusqueda) menu.Items.Add(v.Nombre).Name = v.Nombre;

            //menu.Items.RemoveAt(15);
            //menu.ClientSize = new Size(Width = 331, Height = 331);
            var _ubicacion = Nombre_TextBox.Location;
            _ubicacion.Y += 19;
            menu.Show(this, _ubicacion);
            menu.KeyPress += new KeyPressEventHandler(KeyPress_menustrip);
            Nombre_TextBox.Focus();
            menu.ItemClicked += new ToolStripItemClickedEventHandler(Clic_ContextMenuStrip);
        }

        private void Clic_ContextMenuStrip(object sender, ToolStripItemClickedEventArgs e)
        {
            var cli = Clientes.Where(c => c.Nombre == e.ClickedItem.Text).ToList();
            Cedula_NumericUpDown.Value = Convert.ToInt32(cli[0].Identificacion.Numero);
            TipoPersona_ComboBox.Text = cli[0].IsPersonaNatural ? "Natural": "Juridica";
            TipoIdentificacion_ComboBox.Text = cli[0].Identificacion.Tipo;
            if (cli[0].Identificacion.DigitoVe == -1)
            {
                Digito_Label.Visible = false;
                Digito_NumericUpDown.Visible = false;
            }
            else
            {
                Digito_Label.Visible = true;
                Digito_NumericUpDown.Visible = true;
                Digito_NumericUpDown.Value = cli[0].Identificacion.DigitoVe;
            }
            Nombre_TextBox.Text = cli[0].Nombre;
            Ciudad_TextBox.Text = cli[0].Ubicacion.Ciudad;
            Departamento_ComboBox.Text = cli[0].Ubicacion.Departamento;
            Contacto_TextBox.Text = cli[0].Contacto;
            Direccion_TextBox.Text = cli[0].Ubicacion.Direccion;
            Origen_ComboBox.Text = cli[0].Origen;
            Sexo_ComboBox.Text = cli[0].IsMan ? "Masculino" :"Femenino";
            menu.Hide();

        }

        private void TipoPersona_ComboBox_TextChanged(object sender, EventArgs e)
        {
            if (TipoPersona_ComboBox.Text == "Natural")
            {
                Digito_NumericUpDown.Enabled = false;
                Digito_NumericUpDown.Visible = false;
                Digito_Label.Enabled = false;
                Digito_Label.Visible = false;
            }
            else
            {
                Digito_NumericUpDown.Enabled = true;
                Digito_NumericUpDown.Visible = true;
                Digito_Label.Enabled = true;
                Digito_Label.Visible = true;
            }
        }
        #endregion
    }
}
