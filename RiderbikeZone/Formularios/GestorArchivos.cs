namespace RiderbikeZone.Formularios
{
    using RiderbikeZone.Modelos;
    using RiderbikeZone.Servicios;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    public partial class GestorArchivos : Form
    {
        #region Propiedades
        const string RutaBackUp = @"C:\MANGO\BackUp\";
        const string RutaVentas = @"C:\MANGO\Archivos\Ventas";
        const string RutaAdjuntos = @"C:\MANGO\Archivos\Adjuntos";
        const string RutaCotizaciones = @"C:\MANGO\Archivos\Cotizaciones";
        public DataGridView ListaProductos_DataGridView { get; set; }
        public DataGridView Clientes_DataGridView { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public int ANI { get; set; }
        public List<Producto> Productos { get; set; }
        #endregion

        #region Constructor
        public GestorArchivos(string _email, string _clave)
        {
            ANI = 0;
            Productos = new List<Producto>();
            Email = _email;
            Clave = _clave;
            InitializeComponent();
            Obtener();
        }
        #endregion

        #region Métodos
        public void Obtener()
        {
            string[] _filesVentas = Directory.GetFiles(RutaVentas);
            LlenarDataGridView(Ventas_DataGridView, _filesVentas);
            string[] _filesAdjuntos = Directory.GetFiles(RutaAdjuntos);
            LlenarDataGridView(Adjuntos_DataGridView, _filesAdjuntos);
            string[] _filesCotizaciones = Directory.GetFiles(RutaCotizaciones);
            LlenarDataGridView(Cotizaciones_DataGridView, _filesCotizaciones);
        }

        public void LlenarDataGridView(DataGridView _dataGridView, string[] _elementos)
        {
            try
            {
                _dataGridView.Rows.Clear();
                foreach (var e in _elementos)
                {
                    int i = _dataGridView.Rows.Add();
                    string[] _value = e.Split('\\');
                    _dataGridView.Rows[i].Cells[0].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    _dataGridView.Rows[i].DefaultCellStyle.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    _dataGridView.Rows[i].Cells[0].Value = _value[_value.Length - 1];
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar la página, intente abriendo la página de nuevo.", "Error al cargar la página", MessageBoxButtons.OK);
                return;
            }
        }

        private void Seleccionar_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog _op = new OpenFileDialog();
            if (_op.ShowDialog() == DialogResult.OK)
            {
                string[] _archivoName = _op.FileName.Split('\\');
                File.Copy(_op.FileName, RutaAdjuntos + @"\" + _archivoName[_archivoName.Length - 1], false);
                Obtener();
            }
        }

        private void AbrirBK_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog _op = new OpenFileDialog();
            _op.InitialDirectory = RutaBackUp;
            _op.Filter = "Excel File (*.xls)|*.xls";
            if (_op.ShowDialog() == DialogResult.OK) Process.Start(_op.FileName);
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            Animacion_Panel.Visible = true;
            Animacion_Timer.Enabled = true;
            Seleccionar_Button.Enabled = false;
            Ventas_DataGridView.Enabled = false;
            Cotizaciones_DataGridView.Enabled = false;
            Adjuntos_DataGridView.Enabled = false;
            SFirebase FB = new SFirebase
            {
                Email = Email,
                Clave = Clave
            };
            List<ClientNew> Clientes = new List<ClientNew>();
            try
            {
                var t = await FB.LoginWithEmail(false);
                if (t == null) throw new Exception();
                var _clientes = await FB.ObtenerClientNew();
                foreach (var v in _clientes) Clientes.Add(v);
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error de conexión.\nIntente recargando la página.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime _fecha = DateTime.Now;
            Random rnd = new Random();
            int num = rnd.Next(1000, 9999);
            List<string> Mes = new List<string>()
            { "ene","feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic" };
            string Dir = RutaBackUp + _fecha.Day + "_" + Mes[_fecha.Month - 1] + "_" + _fecha.Year + "BackUp_Clientes" + num;
            SCrearXLS XLS = new SCrearXLS();
            //XLS.EscribirExcel(Clientes_DataGridView, Dir);
            LlenarTablas(Clientes);
            XLS.EscribirExcelClientes(Clientes_DataGridView, Dir);
            //XLS.ExportarDataGridViewExcel(Clientes_DataGridView, Dir);
            MessageBox.Show("Copia de seguridad de clientes creada.", "Copia creada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            Animacion_Panel.Visible = false;
            Animacion_Timer.Enabled = false;
            Seleccionar_Button.Enabled = true;
            Ventas_DataGridView.Enabled = true;
            Cotizaciones_DataGridView.Enabled = true;
            Adjuntos_DataGridView.Enabled = true;
        }

        private void LlenarTablas(List<ClientNew> _Ncliente)
        {
            List<string> columns = new List<string>(){ 
                "FECHA" ,
                "TIPO",
                "NOMBRE",
                "TIPO DE IDENTIFICACIÓN",
                "NÚMERO DE IDENTIFICACIÓN",
                "DÍGITO DE VERIFICACIÓN",
                "SEXO",
                "DEPARTAMENTO",
                "CIUDAD",
                "DIRECCIÓN",
                "PRODUCTOS",
                "ORIGEN",
                "TIPO DE PAGO",
                "TIPO DE COMPRADOR",
                "CONTACTO"
            };
            Clientes_DataGridView = new DataGridView();
            for (int i = 0; i < 15; i++)
            {
                Clientes_DataGridView.Columns.Add(new DataGridViewTextBoxColumn() 
                { HeaderText =  columns[i]});
            }
            try
            {
                Clientes_DataGridView.Rows.Clear();
                foreach (var c in _Ncliente)
                {
                    if (!string.IsNullOrEmpty(c.Nombre))
                    {
                        int i = Clientes_DataGridView.Rows.Add();
                        string digitoVer = "";
                        string _productos = "";
                        var t = c.Identificacion.DigitoVe == -1 ? digitoVer = "" : digitoVer = c.Identificacion.DigitoVe.ToString();
                        if (c.Productos != null) foreach (var p in c.Productos) _productos += p.Codigo + "|" + p.Nombre + "|" + p.Unidades + p.Valor + "\n";

                        Clientes_DataGridView.Rows[i].DefaultCellStyle.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
                        Clientes_DataGridView.Rows[i].Cells[0].Value = c.Fecha;
                        Clientes_DataGridView.Rows[i].Cells[1].Value = c.IsPersonaNatural ? "Natural" : "Juridica";
                        Clientes_DataGridView.Rows[i].Cells[2].Value = c.Nombre;
                        Clientes_DataGridView.Rows[i].Cells[3].Value = c.Identificacion.Tipo;
                        Clientes_DataGridView.Rows[i].Cells[4].Value = c.Identificacion.Numero;
                        Clientes_DataGridView.Rows[i].Cells[5].Value = c.Identificacion.DigitoVe == -1 ? "" : c.Identificacion.DigitoVe.ToString();
                        Clientes_DataGridView.Rows[i].Cells[6].Value = c.IsMan == true ? "Masculino" : "Femenino";
                        Clientes_DataGridView.Rows[i].Cells[7].Value = c.Ubicacion.Departamento;
                        Clientes_DataGridView.Rows[i].Cells[8].Value = c.Ubicacion.Ciudad;
                        Clientes_DataGridView.Rows[i].Cells[9].Value = c.Ubicacion.Direccion;
                        Clientes_DataGridView.Rows[i].Cells[10].Value = _productos;
                        Clientes_DataGridView.Rows[i].Cells[11].Value = c.Origen;
                        Clientes_DataGridView.Rows[i].Cells[12].Value = c.Pago;
                        Clientes_DataGridView.Rows[i].Cells[13].Value = c.IsDetal ? "Detal" : "Por Mayor";
                        Clientes_DataGridView.Rows[i].Cells[14].Value = c.Contacto;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar la página, intente abriendo la página de nuevo.", "Error al cargar la página", MessageBoxButtons.OK);
                return;
            }
        }

        private async void HacerBK_Button_Click(object sender, EventArgs e)
        {
            Animacion_Panel.Visible = true;
            Animacion_Timer.Enabled = true;
            Seleccionar_Button.Enabled = false;
            Ventas_DataGridView.Enabled = false;
            Cotizaciones_DataGridView.Enabled = false;
            Adjuntos_DataGridView.Enabled = false;
            SFirebase FB = new SFirebase
            {
                Email = Email,
                Clave = Clave
            };
            try
            {
                var t = await FB.LoginWithEmail(false);
                if (t == null) throw new Exception();
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error de conexión.\nIntente recargando la página.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Random rnd = new Random();
            int num = rnd.Next(1000, 9999);
            var _productos = await FB.ObtenerProductos("Inventario");
            foreach (var p in _productos) Productos.Add(p);
            Llenar_DataGridView(Productos);
            DateTime _fecha = DateTime.Now;
            List<string> Mes = new List<string>()
            { "ene","feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic" };
            string Dir = RutaBackUp + _fecha.Day + "_" + Mes[_fecha.Month - 1] + "_" + _fecha.Year + "BackUp" + num;
            SCrearXLS XLS = new SCrearXLS();
            XLS.EscribirExcel(ListaProductos_DataGridView, Dir);
            MessageBox.Show("Copia de seguridad creada con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Animacion_Panel.Visible = false;
            Animacion_Timer.Enabled = false;
            Seleccionar_Button.Enabled = true;
            Ventas_DataGridView.Enabled = true;
            Cotizaciones_DataGridView.Enabled = true;
            Adjuntos_DataGridView.Enabled = true;
        }

        public void Llenar_DataGridView(List<Producto> _productos)
        {
            DataGridViewTextBoxColumn Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn()
            { HeaderText = "Código"};
            DataGridViewTextBoxColumn Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn()
            { HeaderText = "Descripción" };
            DataGridViewTextBoxColumn Marca = new System.Windows.Forms.DataGridViewTextBoxColumn()
            { HeaderText = "Marca" };
            DataGridViewTextBoxColumn Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn()
            { HeaderText = "Tipo" };
            DataGridViewTextBoxColumn Unidades = new System.Windows.Forms.DataGridViewTextBoxColumn()
            { HeaderText = "Unidades actuales" };
            DataGridViewTextBoxColumn unidadesVendidas = new System.Windows.Forms.DataGridViewTextBoxColumn()
            { HeaderText = "Unidades vendidas" };
            DataGridViewTextBoxColumn PrecioSinIVA = new System.Windows.Forms.DataGridViewTextBoxColumn()
            { HeaderText = "Precio sin IVA" };
            DataGridViewTextBoxColumn ValorCompra = new System.Windows.Forms.DataGridViewTextBoxColumn()
            { HeaderText = "Precio de compra" };
            DataGridViewTextBoxColumn PrecioVenta = new System.Windows.Forms.DataGridViewTextBoxColumn()
            { HeaderText = "Precio de venta" };
            DataGridViewTextBoxColumn Inversion = new System.Windows.Forms.DataGridViewTextBoxColumn()
            { HeaderText = "Inversión" };
            DataGridViewTextBoxColumn ganancia = new System.Windows.Forms.DataGridViewTextBoxColumn()
            { HeaderText = "Ganancia" };
            DataGridViewTextBoxColumn Anotaciones = new System.Windows.Forms.DataGridViewTextBoxColumn()
            { HeaderText = "Anotaciones" };
            DataGridViewTextBoxColumn Proveedorr = new System.Windows.Forms.DataGridViewTextBoxColumn()
            { HeaderText = "Nombre del proveedor" };
            DataGridViewTextBoxColumn ContactoProv = new System.Windows.Forms.DataGridViewTextBoxColumn()
            { HeaderText = "Contacto del proveedor" };
            ListaProductos_DataGridView = new DataGridView();
            ListaProductos_DataGridView.Columns.AddRange(new DataGridViewColumn[] {
            Codigo,
            Nombre,
            Marca,
            Tipo,
            Unidades,
            unidadesVendidas,
            PrecioSinIVA,
            ValorCompra,
            PrecioVenta,
            Inversion,
            ganancia,
            Anotaciones,
            Proveedorr,
            ContactoProv});

            ListaProductos_DataGridView.Rows.Clear();
            List<Producto> ProOrdenados = new List<Producto>();
            ProOrdenados = _productos.OrderBy(p => p.Codigo).ToList();
            foreach (var D in ProOrdenados)
            {
                int i = ListaProductos_DataGridView.Rows.Add();

                ListaProductos_DataGridView.Rows[i].Cells[0].Value = D.Codigo;
                ListaProductos_DataGridView.Rows[i].Cells[1].Value = D.Nombre;
                ListaProductos_DataGridView.Rows[i].Cells[2].Value = D.Marca;
                ListaProductos_DataGridView.Rows[i].Cells[3].Value = D.Tipo;
                ListaProductos_DataGridView.Rows[i].Cells[4].Value = D.Unidades;
                ListaProductos_DataGridView.Rows[i].Cells[5].Value = D.UnidadesVendidas;
                ListaProductos_DataGridView.Rows[i].Cells[6].Value = D.PrecioAntesIVA.ToString("N0");
                ListaProductos_DataGridView.Rows[i].Cells[7].Value = D.PrecioCompra.ToString("N0");
                ListaProductos_DataGridView.Rows[i].Cells[8].Value = D.PrecioVenta.ToString("N0");
                ListaProductos_DataGridView.Rows[i].Cells[9].Value = D.Inversion.ToString("N0");
                ListaProductos_DataGridView.Rows[i].Cells[10].Value = D.Ganancia.ToString("N0");
                ListaProductos_DataGridView.Rows[i].Cells[11].Value = D.Nota;
                ListaProductos_DataGridView.Rows[i].Cells[12].Value = D.Origen.Nombre;
                ListaProductos_DataGridView.Rows[i].Cells[13].Value = D.Origen.Contacto;
            }
        }

        private void Ventas_DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1) Process.Start(RutaVentas + @"\" + Ventas_DataGridView.Rows[e.RowIndex].Cells[0].Value);
        }

        private void Adjuntos_DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1) Process.Start(RutaAdjuntos + @"\" + Adjuntos_DataGridView.Rows[e.RowIndex].Cells[0].Value);
        }

        private void Cotizaciones_dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.RowIndex != -1) Process.Start(RutaCotizaciones + @"\" + Cotizaciones_DataGridView.Rows[e.RowIndex].Cells[0].Value);
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
