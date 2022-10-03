namespace RiderbikeZone.Formularios
{
    using RiderbikeZone.Modelos;
    using RiderbikeZone.Servicios;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    public partial class Vender : Form
    {
        #region Propiedades
        public List<int> Ganancias { get; set; }
        public List<int> Unidadess { get; set; }
        public List<int> Valores { get; set; }
        public List<int> ValoresCUTemp { get; set; }
        public string EmailT { get; set; }
        public string ClaveT { get; set; }
        public List<string> Tipos { get; set; }
        public List<string> Marcas { get; set; }
        public int StockAceptableMin { get; set; }
        public int StockAceptableMax { get; set; }
        public List<Producto> Productos { get; set; }
        public Producto ProductoTemp { get; set; }
        public List<Producto> ProductosCarrito { get; set; }
        public List<Producto> ProductosBusqueda { get; set; }
        public List<ClientNew> Clientes { get; set; }
        public int Total { get; set; }
        public int PrecioSugerido { get; set; }
        #endregion

        #region Constructor
        public Vender(string _email, string _clave)
        {
            Ganancias = new List<int>();
            PrecioSugerido = 0;
            Total = 0;
            EmailT = _email;
            ClaveT = _clave;
            StockAceptableMin = 3;
            StockAceptableMax = 6;
            Tipos = new List<string>();
            Marcas = new List<string>();
            Clientes = new List<ClientNew>();
            ProductosCarrito = new List<Producto>();
            Productos = new List<Producto>();
            Unidadess = new List<int>();
            Valores = new List<int>();
            ValoresCUTemp = new List<int>();
            InitializeComponent();
            Obtener();
        }
        #endregion

        #region Métodos
        public async void Obtener()
        {
            if (ListaProductos_DataGridView != null) ListaProductos_DataGridView.Rows.Clear();
            Productos.Clear();
            SFirebase FB = new SFirebase
            {
                Email = EmailT,
                Clave = ClaveT
            };
            try
            {
                var t = await FB.LoginWithEmail(false);
                if (t == null) throw new Exception();
                var Productoss = await FB.ObtenerProductos("Inventario");
                var Clientess = await FB.ObtenerClientNew();
                foreach (var cli in Clientess.Where(n => !string.IsNullOrEmpty(n.Nombre)).ToList()) Clientes.Add(cli);
                foreach (var pro in Productoss.Where(c => c.Unidades != 0).ToList()) Productos.Add(pro);
                Llenar_DataGridView(Productos);
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error de conexión.\nIntente recargando la página.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void Llenar_DataGridView(List<Producto> _productos)
        {
            try
            {
                ListaProductos_DataGridView.Rows.Clear();
                foreach (var D in _productos)
                {
                    int i = ListaProductos_DataGridView.Rows.Add();

                    ListaProductos_DataGridView.Rows[i].Cells[0].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    ListaProductos_DataGridView.Rows[i].Cells[1].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    ListaProductos_DataGridView.Rows[i].Cells[2].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    ListaProductos_DataGridView.Rows[i].Cells[3].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);

                    ListaProductos_DataGridView.Rows[i].DefaultCellStyle.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    ListaProductos_DataGridView.Rows[i].Cells[0].Value = D.Codigo;
                    ListaProductos_DataGridView.Rows[i].Cells[1].Value = D.Nombre;
                    ListaProductos_DataGridView.Rows[i].Cells[3].Value = D.PrecioVenta.ToString("N0");

                    if (D.Unidades > StockAceptableMax)
                    {
                        ListaProductos_DataGridView.Rows[i].Cells[2].Style.BackColor = Color.DarkGreen;
                        ListaProductos_DataGridView.Rows[i].Cells[2].Style.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    if (D.Unidades < StockAceptableMin)
                    {
                        ListaProductos_DataGridView.Rows[i].Cells[2].Style.BackColor = Color.DarkRed; ;
                        ListaProductos_DataGridView.Rows[i].Cells[2].Style.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    if (D.Unidades <= StockAceptableMax && D.Unidades >= StockAceptableMin)
                    {
                        ListaProductos_DataGridView.Rows[i].Cells[2].Style.BackColor = Color.DarkOrange;
                        ListaProductos_DataGridView.Rows[i].Cells[2].Style.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    ListaProductos_DataGridView.Rows[i].Cells[2].Value = D.Unidades;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar la página, intente abriendo la página de nuevo.", "Error al cargar la página", MessageBoxButtons.OK);
                return;
            }
        }

        private void BuscarCod_TextBox_TextChanged(object sender, EventArgs e)
        {
            ProductosBusqueda = Productos.Where(p => p.Codigo.ToLower().IndexOf(BuscarCod_TextBox.Text.ToLower()) != -1).ToList();
            Llenar_DataGridView(ProductosBusqueda);
        }

        private void BuscarNom_TextBox_TextChanged(object sender, EventArgs e)
        {
            ProductosBusqueda = Productos.Where(p => p.Nombre.ToLower().IndexOf(BuscarNom_TextBox.Text.ToLower()) != -1).ToList();
            Llenar_DataGridView(ProductosBusqueda);
        }

        private void AñadirCarrito_Button_Click(object sender, EventArgs e)
        {
            if (ValorPro_NumericUpDown.Value == 0)
            {
                MessageBox.Show("Ingrese el valor del producto.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ValorUnidad_NumericUpDown.Value == 0)
            {
                MessageBox.Show("Ingrese el valor por unidad del producto.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToInt32(ValorUnidad_NumericUpDown.Value) < ProductoTemp.PrecioCompra)
            {
                DialogResult d = MessageBox.Show("El precio de compra es mayor al precio de venta, si continua\n" +
                                "podrían haber errores en el balance." +
                                "¿Desea continuar?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (d == DialogResult.No) return;
            }
            int _unidadesSolicitadas = Convert.ToInt32(Unidades_NumericUpDown.Value);
            int _valorTotalProducto = Convert.ToInt32(ValorPro_NumericUpDown.Value);
            Valores.Add(_valorTotalProducto);
            Unidadess.Add(_unidadesSolicitadas);
            ValoresCUTemp.Add(Convert.ToInt32(ValorUnidad_NumericUpDown.Value));

            ProductoTemp.UnidadesVendidas += _unidadesSolicitadas;
            ProductoTemp.Unidades -= _unidadesSolicitadas;
            int _ganancia = Convert.ToInt32(ValorUnidad_NumericUpDown.Value) * _unidadesSolicitadas;
            int _restarAGanancia = ProductoTemp.PrecioCompra * _unidadesSolicitadas;
            ProductoTemp.Ganancia += _ganancia - _restarAGanancia;
            Ganancias.Add(_ganancia - _restarAGanancia);

            ProductosCarrito.Add(ProductoTemp);
            Total += _valorTotalProducto;
            Total_Label.Text = "$ " + Total.ToString("N0") + " COP";
            PrecioSugerido = 0;
            PrecioSugerido_Label.Text = string.Empty;
            Carrito_DataGridView.Rows.Add(ProductoTemp.Codigo, ProductoTemp.Nombre,
                _unidadesSolicitadas, _valorTotalProducto, _ganancia - _restarAGanancia);
            Carrito_DataGridView.Rows[Carrito_DataGridView.Rows.Count - 1].DefaultCellStyle.Font =
                new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            for (int i = 0; i < ListaProductos_DataGridView.Rows.Count; i++)
            {
                if (ProductoTemp.Codigo == ListaProductos_DataGridView.Rows[i].Cells[0].Value.ToString()) ListaProductos_DataGridView.Rows.RemoveAt(i);
            }
            Vender_Button.Enabled = true;
            Cotizar_IconButton.Enabled = true;
            Cancelar_Button.Enabled = true;
            Productos.Remove(ProductoTemp);
            Codigo_Label.Text = string.Empty;
            Nombre_TextBox.Text = string.Empty;
            Unidades_NumericUpDown.Minimum = 0;
            Unidades_NumericUpDown.Value = 1;
            ValorUnidad_NumericUpDown.Value = 0;
            ValorPro_NumericUpDown.Value = 0;
            Unidades_NumericUpDown.Enabled = false;
            ValorUnidad_NumericUpDown.Enabled = false;
            ValorPro_NumericUpDown.Enabled = false;
            AñadirCarrito_Button.Enabled = false;
            ProductoTemp = null;
            BuscarCod_TextBox.Text = string.Empty;
            BuscarNom_TextBox.Text = string.Empty;
        }

        private void Vender_Button_Click(object sender, EventArgs e)
        {
            AnimacionClienteStart();
            int ganancias = 0;
            foreach (var g in Ganancias) ganancias += g;
            if (ganancias <= 0)
            {
                DialogResult d = MessageBox.Show("La ganancia de la venta tiene un valor negativo, revise el valor de los productos.", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }
            Cliente _cliente = new Cliente(Clientes, true, ProductosCarrito, EmailT, ClaveT, Total, Carrito_DataGridView, Unidadess, Valores, ValoresCUTemp, Ganancias);
            DialogResult D = _cliente.ShowDialog();
            if (D == DialogResult.Yes)
            {
                Carrito_DataGridView.Rows.Clear();
                ProductosCarrito.Clear();
                Total = 0;
                Total_Label.Text = "$                 0  COP";
                Vender_Button.Enabled = false;
                Cancelar_Button.Enabled = false;
                Cotizar_IconButton.Enabled = false;
                AñadirCarrito_Button.Enabled = false;
                Ganancias.Clear();
                Unidadess.Clear();
                Valores.Clear();
                ValoresCUTemp.Clear();
                Clientes.Clear();
                Obtener();
                AnimacionClienteFinished();
            }
            else if (D == DialogResult.Abort)
            {
                AnimacionClienteFinished();
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error en la conexión.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AnimacionClienteFinished();
            }

            AnimacionClienteFinished();
        }

        private void Cancelar_Button_Click(object sender, EventArgs e)
        {
            Unidades_NumericUpDown.Enabled = false;
            ValorUnidad_NumericUpDown.Enabled = false;
            ValorPro_NumericUpDown.Enabled = false;
            Unidades_NumericUpDown.Value = 1;
            ValorUnidad_NumericUpDown.Value = 0;
            ValorPro_NumericUpDown.Value = 0;
            AñadirCarrito_Button.Enabled = false;
            ProductoTemp = null;
            for (int i = 0; i < Carrito_DataGridView.Rows.Count; i++)
            {
                int _unidadesVendidas = Convert.ToInt32(Carrito_DataGridView.Rows[i].Cells[2].Value.ToString());
                ProductosCarrito[i].Unidades += _unidadesVendidas;
                ProductosCarrito[i].UnidadesVendidas -= _unidadesVendidas;
                Productos.Add(ProductosCarrito[i]);
            }
            Total = 0; ;
            Total_Label.Text = "$                 0  COP";
            ProductosCarrito.Clear();
            Carrito_DataGridView.Rows.Clear();
            Vender_Button.Enabled = false;
            Cotizar_IconButton.Enabled = false;
            Cancelar_Button.Enabled = false;
            Llenar_DataGridView(Productos);
        }

        private void ListaProductos_DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Unidades_NumericUpDown.Enabled = true;
            ValorUnidad_NumericUpDown.Enabled = true;
            AñadirCarrito_Button.Enabled = true;

            try
            {
                foreach (var p in Productos)
                {
                    if (p.Codigo == ListaProductos_DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString())
                    {
                        ProductoTemp = p;
                        break;
                    }
                }
                PrecioSugerido = ProductoTemp.PrecioVenta;
                PrecioSugerido_Label.Text = "Valor sugerido: $ " + PrecioSugerido.ToString("N0") + " COP";
                Codigo_Label.Text = ProductoTemp.Codigo;
                Nombre_TextBox.Text = ProductoTemp.Nombre;
                Unidades_NumericUpDown.Maximum = ProductoTemp.Unidades;
                Unidades_NumericUpDown.Minimum = 1;
                Unidades_NumericUpDown.Value = 1;
                ValorUnidad_NumericUpDown.Value = ProductoTemp.PrecioVenta;
                ValorPro_NumericUpDown.Value = ProductoTemp.PrecioVenta;
            }
            catch (Exception ex)
            {
                Unidades_NumericUpDown.Enabled = false;
                ValorUnidad_NumericUpDown.Enabled = false;
                ValorUnidad_NumericUpDown.Enabled = false;
                AñadirCarrito_Button.Enabled = false;
                ValorPro_NumericUpDown.Enabled = false;
                return;
            }
        }

        private void Carrito_DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            var aa = Carrito_DataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
            Total -= Convert.ToInt32(Carrito_DataGridView.Rows[e.RowIndex].Cells[3].Value.ToString());
            Valores.RemoveAt(e.RowIndex);
            Unidadess.RemoveAt(e.RowIndex);

            Total_Label.Text = "$ " + Total.ToString("N0") + " COP";
            int _unidadesVendidas = Convert.ToInt32(Carrito_DataGridView.Rows[e.RowIndex].Cells[2].Value.ToString());
            ProductosCarrito[e.RowIndex].UnidadesVendidas -= _unidadesVendidas;
            ProductosCarrito[e.RowIndex].Unidades += _unidadesVendidas;
            Productos.Add(ProductosCarrito[e.RowIndex]);
            ListaProductos_DataGridView.Rows.Add(ProductosCarrito[e.RowIndex].Codigo,
                ProductosCarrito[e.RowIndex].Nombre, ProductosCarrito[e.RowIndex].Unidades,
                ProductosCarrito[e.RowIndex].PrecioVenta);
            ListaProductos_DataGridView.Rows[ListaProductos_DataGridView.Rows.Count - 1].DefaultCellStyle.Font =
                new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            if (ProductosCarrito[e.RowIndex].Unidades > StockAceptableMax) ListaProductos_DataGridView.Rows[ListaProductos_DataGridView.Rows.Count - 1].Cells[2].Style.BackColor = Color.DarkGreen;
            if (ProductosCarrito[e.RowIndex].Unidades < StockAceptableMin) ListaProductos_DataGridView.Rows[ListaProductos_DataGridView.Rows.Count - 1].Cells[2].Style.BackColor = Color.DarkRed; ;
            if (ProductosCarrito[e.RowIndex].Unidades <= StockAceptableMax && ProductosCarrito[e.RowIndex].Unidades >= StockAceptableMin) ListaProductos_DataGridView.Rows[ListaProductos_DataGridView.Rows.Count - 1].Cells[2].Style.BackColor = Color.DarkOrange;
            ProductosCarrito.RemoveAt(e.RowIndex);
            Carrito_DataGridView.Rows.RemoveAt(e.RowIndex);
            if (ProductosCarrito.Count == 0)
            {
                Vender_Button.Enabled = false;
                Cotizar_IconButton.Enabled = false;
                Cancelar_Button.Enabled = false;
            }
        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            int _unidades = Convert.ToInt32(Unidades_NumericUpDown.Value);
            int _valorUnidad = Convert.ToInt32(ValorUnidad_NumericUpDown.Value);
            ValorPro_NumericUpDown.Value = _unidades * _valorUnidad;
        }

        private void AnimacionClienteStart()
        {
            Proveedor_GroupBox.Enabled = false;
            ListaProductos_GroupBox.Enabled = false;
            Carrito_GroupBox.Enabled = false;
            Vender_Button.BackColor = Color.FromArgb(100, 100, 100);
            Cancelar_Button.BackColor = Color.FromArgb(100, 100, 100);

            Vender_Button.IconColor = Color.FromArgb(100, 100, 100);
            Cancelar_Button.IconColor = Color.FromArgb(100, 100, 100);

            for (int i = 0; i <= ListaProductos_DataGridView.Rows.Count - 1; i++) ListaProductos_DataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(90, 90, 90);
            ListaProductos_DataGridView.GridColor = Color.FromArgb(90, 90, 90);
            ListaProductos_DataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(90, 90, 90);

            for (int i = 0; i <= Carrito_DataGridView.Rows.Count - 1; i++) Carrito_DataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(90, 90, 90);
            Carrito_DataGridView.GridColor = Color.FromArgb(90, 90, 90);
            Carrito_DataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(90, 90, 90);

            Carrito_DataGridView.Enabled = false;
        }

        private void AnimacionClienteFinished()
        {
            Proveedor_GroupBox.Enabled = true;
            ListaProductos_GroupBox.Enabled = true;
            Carrito_GroupBox.Enabled = true;
            Vender_Button.BackColor = Color.FromArgb(35, 175, 126);
            Cancelar_Button.BackColor = Color.DarkRed;
            Cotizar_IconButton.BackColor = Color.Gray;

            Vender_Button.IconColor = Color.White;
            Cotizar_IconButton.IconColor = Color.White;
            Cancelar_Button.IconColor = Color.White;

            for (int i = 0; i <= ListaProductos_DataGridView.Rows.Count - 1; i++) ListaProductos_DataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.White;
            ListaProductos_DataGridView.GridColor = Color.White;
            ListaProductos_DataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            for (int i = 0; i <= Carrito_DataGridView.Rows.Count - 1; i++) Carrito_DataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.White;
            Carrito_DataGridView.GridColor = Color.White;
            Carrito_DataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            Carrito_DataGridView.Enabled = true;
        }

        private void PrecioSugerido_Label_DoubleClick(object sender, EventArgs e)
        {
            ValorUnidad_NumericUpDown.Value = PrecioSugerido;
        }

        private void Cotizar_Button_Click(object sender, EventArgs e)
        {
            AnimacionClienteStart();
            int ganancias = 0;
            foreach (var g in Ganancias) ganancias += g;
            if (ganancias <= 0)
            {
                DialogResult d = MessageBox.Show("La ganancia de la venta tiene un valor negativo, revise el valor de los productos.", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }
            Cliente _cliente = new Cliente(Clientes, false, ProductosCarrito, EmailT, ClaveT, Total, Carrito_DataGridView, Unidadess, Valores, ValoresCUTemp, Ganancias);
            DialogResult D = _cliente.ShowDialog();
            if (D == DialogResult.Yes)
            {
                Carrito_DataGridView.Rows.Clear();
                ProductosCarrito.Clear();
                Total = 0;
                Total_Label.Text = "$                 0  COP";
                Vender_Button.Enabled = false;
                Cotizar_IconButton.Enabled = false;
                Cancelar_Button.Enabled = false;
                AñadirCarrito_Button.Enabled = false;
                Ganancias.Clear();
                Unidadess.Clear();
                Valores.Clear();
                ValoresCUTemp.Clear();
                Clientes.Clear();
                Obtener();
                AnimacionClienteFinished();
            }
            else if (D == DialogResult.Abort)
            {
                AnimacionClienteFinished();
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error al generar la cotización. Intentelo de nuevo.", "Error creando archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AnimacionClienteFinished();
            }

            AnimacionClienteFinished();
        }
        #endregion
    }
}
