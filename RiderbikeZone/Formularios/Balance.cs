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

    public partial class Balance : Form
    {
        #region Propiedades
        public string EmailT { get; set; }
        public string ClaveT { get; set; }
        public List<VentaNew> Ventas { get; set; }
        public List<Compra> Compras { get; set; }
        public List<Producto> Productos { get; set; }
        public List<Proveedor> Proveedores { get; set; }
        public List<ClientNew> Clientes { get; set; }
        public int TotalCompras { get; set; }
        public int TotalVentas { get; set; }
        public int Total { get; set; }
        public int TotalVentasRealizadas { get; set; }
        public int VentasRealizadasMes { get; set; }
        public int TotalUnidadesVendidas { get; set; }
        public int UnidadesVendidasLastMes { get; set; }
        public ContextMenuStrip menu { get; set; }
        public int RowIndex { get; set; }
        public int TotalGanancia { get; private set; }
        #endregion

        #region Constructor
        public Balance(string _email, string _clave)
        {
            Proveedores = new List<Proveedor>();
            Clientes = new List<ClientNew>();
            UnidadesVendidasLastMes = 0;
            TotalUnidadesVendidas = 0;
            VentasRealizadasMes = 0;
            TotalVentasRealizadas = 0;
            TotalCompras = 0;
            TotalVentas = 0;
            TotalGanancia = 0;
            Total = 0;
            EmailT = _email;
            ClaveT = _clave;
            Ventas = new List<VentaNew>();
            Compras = new List<Compra>();
            Productos = new List<Producto>();
            InitializeComponent();
            Obtener();

        }
        #endregion

        #region Métodos
        public async void Obtener()
        {
            if (Compras_DataGridView != null) Compras_DataGridView.Rows.Clear();
            //if (Ventas_DataGridView != null) Ventas_DataGridView.Rows.Clear(); #########Ventas_DataGridView#######
            SFirebase FB = new SFirebase
            {
                Email = EmailT,
                Clave = ClaveT
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
            var _proveedores = await FB.ObtenerProveedores();
            var _compras = await FB.ObtenerCompras();
            var _ventas = await FB.ObtenerVentasNew();
            var _productos = await FB.ObtenerProductos("Inventario");
            var _clientes = await FB.ObtenerClientNew();
            foreach (var c in _proveedores) Proveedores.Add(c);
            foreach (var v in _clientes) Clientes.Add(v);
            foreach (var c in _compras) Compras.Add(c);
            foreach (var v in _ventas) Ventas.Add(v);
            foreach (var p in _productos) Productos.Add(p);
            LLenarCompras(Productos);
            //LLenarVentas(Productos);
            LlenarOtrosDatos();
            Total_Label.Text = "$ " + Total.ToString("N0") + " COP";
        }

        //public void LLenarCompras(List<Compra> _compras)
        //{
        //    try
        //    {
        //        Compras_DataGridView.Rows.Clear();
        //        foreach (var c in _compras)
        //        {
        //            int i = Compras_DataGridView.Rows.Add();

        //            Compras_DataGridView.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //            Compras_DataGridView.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        //            Compras_DataGridView.Rows[i].DefaultCellStyle.Font = new Font("Century Gothic", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
        //            Compras_DataGridView.Rows[i].Cells[0].Value = c.Fecha;
        //            if (string.IsNullOrEmpty(c.Proveedor.Nombre)) Compras_DataGridView.Rows[i].Cells[1].Value = "Proveedor";
        //            else Compras_DataGridView.Rows[i].Cells[1].Value = c.Proveedor.Nombre;
        //            Compras_DataGridView.Rows[i].Cells[2].Value = "$" + c.Total.ToString("N0");
        //            TotalCompras += c.Total;
        //        }
        //        Total -= TotalCompras;
        //        TotalCompras_Label.Text = "$ " + TotalCompras.ToString("N0") + " COP";
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("Error al cargar la página, intente abriendo la página de nuevo.", "Error al cargar la página", MessageBoxButtons.OK);
        //        return;
        //    }
        //}

        public void LLenarCompras(List<Producto> _productos)
        {
            try
            {
                Compras_DataGridView.Rows.Clear();
                foreach (var p in _productos)
                {
                    int i = Compras_DataGridView.Rows.Add();

                    Compras_DataGridView.Rows[i].Cells[0].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    Compras_DataGridView.Rows[i].Cells[1].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    Compras_DataGridView.Rows[i].Cells[2].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    Compras_DataGridView.Rows[i].Cells[3].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    Compras_DataGridView.Rows[i].Cells[4].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    Compras_DataGridView.Rows[i].Cells[5].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);

                    Compras_DataGridView.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Compras_DataGridView.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Compras_DataGridView.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Compras_DataGridView.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Compras_DataGridView.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Compras_DataGridView.Rows[i].DefaultCellStyle.Font = new Font("Century Gothic", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    Compras_DataGridView.Rows[i].Cells[0].Value = p.Codigo;
                    //if (string.IsNullOrEmpty(c.Proveedor.Nombre)) Compras_DataGridView.Rows[i].Cells[1].Value = "Proveedor";
                    //else Compras_DataGridView.Rows[i].Cells[1].Value = p.Nombre;
                    Compras_DataGridView.Rows[i].Cells[1].Value = p.Nombre;
                    Compras_DataGridView.Rows[i].Cells[2].Value = "$" + p.Inversion.ToString("N0");
                    Compras_DataGridView.Rows[i].Cells[3].Value = "$" + (p.UnidadesVendidas * p.PrecioVenta).ToString("N0");
                    Compras_DataGridView.Rows[i].Cells[4].Value = p.UnidadesVendidas;
                    Compras_DataGridView.Rows[i].Cells[5].Value = "$" + p.Ganancia.ToString("N0");
                    TotalCompras += p.Inversion;
                    TotalVentas += p.PrecioVenta * p.UnidadesVendidas;
                    TotalGanancia += p.Ganancia;
                }
                Total += TotalVentas;
                Total -= TotalCompras;
                TotalCompras_Label.Text = "$ " + TotalCompras.ToString("N0") + " COP";
                TotalVentas_Label.Text = "$ " + TotalVentas.ToString("N0") + " COP";
                TotalGanancia_Label.Text = "$ " + TotalGanancia.ToString("N0") + " COP";
                TotalVentasRealizadas = Ventas.Count;
                TotalVentasRealizadas_Label.Text = TotalVentasRealizadas.ToString("N0");
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar la página, intente abriendo la página de nuevo.", "Error al cargar la página", MessageBoxButtons.OK);
                return;
            }
        }

        //public void LLenarVentas(List<VentaNew> _ventas)
        //{
        //    try
        //    {
        //        Ventas_DataGridView.Rows.Clear();
        //        foreach (var v in _ventas)
        //        {
        //            int i = Ventas_DataGridView.Rows.Add();
        //            Ventas_DataGridView.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //            Ventas_DataGridView.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        //            Ventas_DataGridView.Rows[i].DefaultCellStyle.Font = new Font("Century Gothic", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
        //            Ventas_DataGridView.Rows[i].Cells[0].Value = v.Fecha;
        //            if (string.IsNullOrEmpty(v.ClientNew.Nombre)) Ventas_DataGridView.Rows[i].Cells[1].Value = "Cliente";
        //            else Ventas_DataGridView.Rows[i].Cells[1].Value = v.ClientNew.Nombre;
        //            Ventas_DataGridView.Rows[i].Cells[2].Value = "$" + v.Total.ToString("N0");
        //            TotalVentas += v.Total;
        //            TotalVentasRealizadas++;
        //        }
        //        Total += TotalVentas;
        //        TotalVentas_Label.Text = "$ " + TotalVentas.ToString("N0") + " COP";
        //        TotalVentasRealizadas_Label.Text = TotalVentasRealizadas.ToString("N0");
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("Error al cargar la página, intente abriendo la página de nuevo.", "Error al cargar la página", MessageBoxButtons.OK);
        //        return;
        //    }
        //}

        //public void LLenarVentas(List<Producto> _produtos)
        //{
        //    try
        //    {
        //        Ventas_DataGridView.Rows.Clear();
        //        foreach (var p in _produtos)
        //        {
        //            int i = Ventas_DataGridView.Rows.Add();
        //            Ventas_DataGridView.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //            Ventas_DataGridView.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        //            Ventas_DataGridView.Rows[i].DefaultCellStyle.Font = new Font("Century Gothic", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
        //            Ventas_DataGridView.Rows[i].Cells[0].Value = p.Codigo;
        //            //if (string.IsNullOrEmpty(v.ClientNew.Nombre)) Ventas_DataGridView.Rows[i].Cells[1].Value = "Cliente";
        //            //else Ventas_DataGridView.Rows[i].Cells[1].Value = v.ClientNew.Nombre;
        //            Ventas_DataGridView.Rows[i].Cells[1].Value = p.Nombre;
        //            Ventas_DataGridView.Rows[i].Cells[2].Value = "$" + p.Ganancia.ToString("N0");
        //            TotalVentas += p.Ganancia;
        //            TotalVentasRealizadas++;
        //        }
        //        Total += TotalVentas;
        //        TotalVentas_Label.Text = "$ " + TotalVentas.ToString("N0") + " COP";
        //        TotalVentasRealizadas_Label.Text = TotalVentasRealizadas.ToString("N0");
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("Error al cargar la página, intente abriendo la página de nuevo.", "Error al cargar la página", MessageBoxButtons.OK);
        //        return;
        //    }
        //}

        public void LlenarOtrosDatos()
        {
            DateTime _fecha = DateTime.Now;
            List<string> Mes = new List<string>()
            {"ene","feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic"};
            string _mess = Mes[_fecha.Month - 1];
            string _mes = string.Empty; ;
            string _anio = _fecha.Year.ToString();
            if (_mess == "ene")
            {
                _mes = "dic";
                var _year = Convert.ToInt32(_anio) - 1;
                _anio = _year.ToString();
            }
            else _mes = Mes[_fecha.Month - 2];

            //Ventas último Mes
            var _productoXmes = Ventas.Where(v => v.Fecha.Contains(_mes));
            var _productoLastMes = _productoXmes.Where(v => v.Fecha.Contains(_anio)).ToList();
            VentasRealizadasMes = _productoLastMes.Count;
            VentasUltimoMes_Label.Text = VentasRealizadasMes.ToString("N0");

            //Total Productos vendidos
            foreach (var p in Productos) TotalUnidadesVendidas += p.UnidadesVendidas;
            ProductosVendidos_Label.Text = TotalUnidadesVendidas.ToString("N0");

            //Productos Vendidos Último Mes
            foreach (var v in _productoLastMes)
            {
                foreach (var p in v.ListaProductos) UnidadesVendidasLastMes += p.UnidadesVendidas;
            }
            ProductosVendidosMes_Label.Text = UnidadesVendidasLastMes.ToString("N0");

        }

        private void ListaProductos_DataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    //Ventas_DataGridView.CurrentCell = Ventas_DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];#########Ventas_DataGridView#######
                }
                catch (Exception)
                {
                    return;
                }
                menu = new ContextMenuStrip();
                menu.Items.Add("Realizar devolución").Name = "_devolucion";
                menu.Items.Add("Generar factura").Name = "_generarFactura";
                //Ventas_DataGridView.Rows[e.RowIndex].Selected = true;#########Ventas_DataGridView#######
                RowIndex = e.RowIndex;
                //Ventas_DataGridView.CurrentCell = Ventas_DataGridView.Rows[e.RowIndex].Cells[0];#########Ventas_DataGridView#######
                //menu.Show(Ventas_DataGridView, e.Location); #########Ventas_DataGridView#######
                menu.Show(Cursor.Position);
                menu.ItemClicked += new ToolStripItemClickedEventHandler(Clic_ContextMenuStrip);
            }
        }

        private async void Clic_ContextMenuStrip(object sender, ToolStripItemClickedEventArgs e)
        {
            var _venta = Ventas[RowIndex];

            if (e.ClickedItem.Name == "_devolucion")
            {
                string nom;
                var x = string.IsNullOrEmpty(_venta.ClientNew.Nombre) ? nom = string.Empty : nom = " a " + _venta.ClientNew.Nombre;

                string _mensaje = "¿Quiere realizar la devolución de la venta realizada el\n" + _venta.Fecha
                    + nom + "?\nTenga en cuenta que se modificarán además del balance, los siguiente productos:\n\n";
                foreach (var vl in _venta.ListaProductos)
                {
                    if (vl.Nombre.Length > 35) _mensaje += vl.Codigo + " - " + vl.Nombre.Substring(0, 34) + "\n";
                    else _mensaje += vl.Codigo + " - " + vl.Nombre + "\n";
                }
                DialogResult diag = MessageBox.Show(_mensaje, "Devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (diag == DialogResult.Yes)
                {
                    SFirebase FB = new SFirebase
                    {
                        Email = EmailT,
                        Clave = ClaveT
                    };
                    try
                    {
                        var tyu = await FB.LoginWithEmail(false);
                        if (tyu == null) throw new Exception();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ha ocurrido un error de conexión.\nIntente recargando la página.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return
                    ;
                    }
                    try
                    {
                        var _ventas = await FB.ObtenerVentasNew();
                        var t = FB.Ventas.ToList();
                        var _indexVentas = Ventas.IndexOf(_venta);

                        var bs = Clientes.Where(n => n.Nombre == _venta.ClientNew.Nombre).ToList();
                        if (bs.Count != 0) await FB.DeleteClientNew(_venta.ClientNew.Nombre);
                        if (_indexVentas < t.Count) await FB.BorrarVentaKey(t[_indexVentas]);
                        List<Producto> _productosModificados = new List<Producto>();
                        foreach (var v in _venta.ListaProductos)
                        {
                            var bus = Productos.Where(c => c.Codigo == v.Codigo).ToList();
                            Producto _pro = new Producto();
                            if (bus.Count != 0)
                            {
                                _pro = bus[0];
                                int _unidades = v.UnidadesVendidas;
                                _pro.UnidadesVendidas -= _unidades;
                                _pro.Unidades += _unidades;
                                int restarGanancia = (v.PrecioVenta - v.PrecioCompra) * _unidades;
                                _pro.Ganancia -= restarGanancia;
                                _productosModificados.Add(_pro);
                            }
                        }
                        foreach (var Pro in _productosModificados)
                        {
                            var Produc = Productos.Where(P => P.Codigo == Pro.Codigo).ToList();
                            if (Produc.Count != 0)
                            {
                                await FB.BorrarProducto(Pro.Codigo);
                                await FB.Guardar("Inventario", Pro);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ha ocurrido un problema de conexión, intente de nuevo por favor.",
                            "Devolucón NO realizada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else return;
                Limpiar();
                Obtener();
                MessageBox.Show("Venta eliminada con éxito, se han modificado los productos correspondientes,\n" +
                                "si el producto fue previamente eliminado no se realizarán cambios en el\n" +
                                "inventario.", "Devolucón exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (e.ClickedItem.Name == "_generarFactura")
            {
                try
                {
                    List<int> Unidadess = new List<int>(), Valores = new List<int>();
                    foreach (var i in _venta.ListaProductos)
                    {
                        Unidadess.Add(i.UnidadesVendidas);
                        Valores.Add(i.PrecioVenta);
                    }
                    int _consecutivo = 0220;
                    SCrearPDF PDF = new SCrearPDF();
                    PDF.CrearPDF("VENTA", _consecutivo.ToString(), _venta.ClientNew.Nombre,
                        _venta.ClientNew.Identificacion.Numero, _venta.ClientNew.Ubicacion.Direccion, _venta.ClientNew.Ubicacion.Ciudad,
                        _venta.ClientNew.Contacto, _venta.Fecha, _venta.Observaciones,
                        "Luis C. Pulido", _venta.FormaPago, _venta.ListaProductos,_venta.Total,
                        "321 2471283", "luiscarlospulido14@gmail.com", Unidadess, Valores, 0.19);
                    MessageBox.Show("Factura de venta se guardada en " + @"C:\MANGO\Archivos\Ventas", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al generar la factura, intente e nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        public void Limpiar()
        {
            Ventas.Clear();
            Clientes.Clear();
            Productos.Clear();
            Compras.Clear();
            UnidadesVendidasLastMes = 0;
            TotalUnidadesVendidas = 0;
            VentasRealizadasMes = 0;
            TotalVentasRealizadas = 0;
            TotalCompras = 0;
            TotalVentas = 0;
            Total = 0;
        }

        private void Compras_DataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private async void ClicCompras_ContextMenuStrip(object sender, ToolStripItemClickedEventArgs e)
        {
            {
                var _compra = Compras[RowIndex];
                string nom;
                var x = string.IsNullOrEmpty(_compra.Proveedor.Nombre) ? nom = string.Empty : nom = " a " + _compra.Proveedor.Nombre;

                string _mensaje = "¿Quiere realizar la devolución de la compra realizada el\n" + _compra.Fecha
                    + nom + "?\nTenga en cuenta que se modificarán además del balance, los siguiente productos:\n\n";
                foreach (var cl in _compra.ListaProductos)
                {
                    if (cl.Nombre.Length > 35) _mensaje += cl.Codigo + " - " + cl.Nombre.Substring(0, 34) + "\n";
                    else _mensaje += cl.Codigo + " - " + cl.Nombre + "\n";
                }
                DialogResult diag = MessageBox.Show(_mensaje, "Devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (diag == DialogResult.Yes)
                {
                    SFirebase FB = new SFirebase
                    {
                        Email = EmailT,
                        Clave = ClaveT
                    };
                    try
                    {
                        var tyu = await FB.LoginWithEmail(false);
                        if (tyu == null) throw new Exception();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ha ocurrido un error de conexión.\nIntente recargando la página.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return
                    ;
                    }
                    try
                    {
                        var _ventas = await FB.ObtenerCompras();
                        var t = FB.Compras.ToList();
                        var _indexVentas = Compras.IndexOf(_compra);

                        var bs = Proveedores.Where(n => n.Nombre == _compra.Proveedor.Nombre).ToList();
                        if (bs.Count != 0) await FB.BorrarProveedor(_compra.Proveedor.Nombre);
                        if (_indexVentas < t.Count) await FB.BorrarCompraKey(t[_indexVentas]);
                        List<Producto> _productosModificados = new List<Producto>();
                        foreach (var v in _compra.ListaProductos)
                        {
                            var bus = Productos.Where(c => c.Codigo == v.Codigo).ToList();
                            Producto _pro = new Producto();
                            if (bus.Count != 0)
                            {
                                _pro = bus[0];
                                int _unidades = v.Unidades;
                                _pro.Unidades -= _unidades;
                                int restarGanancia = (v.PrecioVenta - v.PrecioCompra) * _unidades;
                                _pro.Ganancia -= restarGanancia;
                                _productosModificados.Add(_pro);
                            }
                        }
                        foreach (var Pro in _productosModificados)
                        {
                            var Produc = Productos.Where(P => P.Codigo == Pro.Codigo).ToList();
                            if (Produc.Count != 0)
                            {
                                if (Pro.Unidades == 0) await FB.BorrarProducto(Pro.Codigo);
                                else
                                {
                                    await FB.BorrarProducto(Pro.Codigo);
                                    await FB.Guardar("Inventario", Pro);
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ha ocurrido un problema de conexión, intente de nuevo por favor.",
                            "Devolucón NO realizada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else return;
                Limpiar();
                Obtener();
                MessageBox.Show("Compra eliminada con éxito, se han modificado los productos correspondientes,\n" +
                                "si el producto fue previamente eliminado no se realizarán cambios en el\n" +
                                "inventario.", "Devolucón exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
    }
}
