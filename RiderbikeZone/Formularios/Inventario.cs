namespace RiderbikeZone.Formularios
{
    using RiderbikeZone.Modelos;
    using RiderbikeZone.Servicios;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class Inventario : Form
    {
        /// 
        /// 1. Agregar unidades por TIPOS/MARCAS en un nuevo Form
        /// 

        #region Propiedades
        public string EmailT { get; set; }
        public string ClaveT { get; set; }
        public List<Producto> Productos { get; set; }
        public List<Producto> ProductosBusqueda { get; set; }
        public List<string> Tipos { get; set; }
        public List<string> Marcas { get; set; }
        public List<Color> Colores { get; set; }
        public int RowIndex { get; set; }
        public ContextMenuStrip menu { get; set; }
        public int StockAceptableMin { get; set; }
        public int StockAceptableMax { get; set; }
        public SEscribirTXT TXT { get; set; }

        delegate void delegado(object o);
        BindingSource bs;
        int globalCounter = 100;    
        #endregion

        #region Constructor
        public Inventario(string _email, string _clave)
        {
            InitializeComponent();
            TXT = new SEscribirTXT();
            StockAceptableMin = Convert.ToInt32(TXT.Leer("MINSTOCK.txt"));
            StockAceptableMax = Convert.ToInt32(TXT.Leer("MAXSTOCK.txt"));
            EmailT = _email;
            ClaveT = _clave;
            Tipos = new List<string>();
            Marcas = new List<string>();
            Productos = new List<Producto>();
            CrearColores();
            Obtener();
        }
        #endregion

        #region Métodos
        public void CrearColores()
        {
            Colores = new List<Color>();
            Colores.Add(Color.FromArgb(123, 125, 125));
            Colores.Add(Color.FromArgb(31, 97, 141));
            Colores.Add(Color.FromArgb(24, 106, 59));
            Colores.Add(Color.FromArgb(66, 73, 73));
            Colores.Add(Color.FromArgb(27, 38, 49));
            Colores.Add(Color.FromArgb(17, 120, 100));
        }

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
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error de conexión.\nIntente recargando la página.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            var Tiposs = await FB.ObtenerMarcasTipos("Tipos");
            foreach (var Tipo in Tiposs) if (Tipo != null) Tipos.Add(Tipo);
            var Marcass = await FB.ObtenerMarcasTipos("Marcas");
            foreach (var Marca in Marcass) if (Marca != null) Marcas.Add(Marca);
            var Productoss = await FB.ObtenerProductos("Inventario");
            foreach (var pro in Productoss) Productos.Add(pro);

            int temp = 0;
            foreach (var item in Productos) temp += item.Ganancia;
            //{
            //    var t = (item.Unidades + item.UnidadesVendidas) * item.PrecioCompra;
            //    if (t != item.Inversion)
            //    {
            //        item.Inversion = t;
            //        //await FB.BorrarProducto(item.Codigo);
            //        //await FB.Guardar("Inventario", item);
            //    }
            //}
            Llenar_DataGridView(Productos);
            //Llenar_DataGridViewBase(Productos);
            //Task b = new Task(() =>
            //{
            //    var _productos = Productos;
            //    try
            //    {
            //        ListaProductos_DataGridView.Rows.Clear();
            //        List<Producto> ProOrdenados = new List<Producto>();
            //        if (Marca_RadioButton.Checked) ProOrdenados = _productos.OrderBy(p => p.Marca).ToList();
            //        else ProOrdenados = _productos.OrderBy(p => p.Tipo).ToList();
            //        string LastValue = string.Empty;
            //        int Index = 0;
            //        foreach (var D in ProOrdenados)
            //        {
            //            int i = ListaProductos_DataGridView.Rows.Add();
            //        }
            //    }
            //    catch (Exception ty)
            //    {
            //        MessageBox.Show("Error al cargar la página, intente abriendo la página de nuevo.", "Error al cargar la página", MessageBoxButtons.OK);
            //        return;
            //    }
            //});
            //b.Start();
            //await b;
            //Task h = new Task(() => {
            //    var _productos = Productos;
            //    try
            //    {
            //        int i = 0;
            //        List<Producto> ProOrdenados = new List<Producto>();
            //        if (Marca_RadioButton.Checked) ProOrdenados = _productos.OrderBy(p => p.Marca).ToList();
            //        else ProOrdenados = _productos.OrderBy(p => p.Tipo).ToList();
            //        string LastValue = string.Empty;
            //        int Index = 0;
            //        int limit = ProOrdenados.Count / 2;
            //        for (int ind = 0; ind < limit; ind++)
            //        {
            //            if (Marca_RadioButton.Checked)
            //            {
            //                if (LastValue != ProOrdenados[ind].Marca)
            //                {
            //                    var Itndex = Index < Colores.Count - 1 ? Index++ : Index = 0;
            //                    LastValue = ProOrdenados[ind].Marca;
            //                }
            //            }
            //            else
            //            {
            //                if (LastValue != ProOrdenados[ind].Tipo)
            //                {
            //                    var Itndex = Index < Colores.Count - 1 ? Index++ : Index = 0;
            //                    LastValue = ProOrdenados[ind].Tipo;
            //                }
            //            }
            //            ListaProductos_DataGridView.Rows[i].DefaultCellStyle.BackColor = Colores[Index];
            //            ListaProductos_DataGridView.Rows[i].DefaultCellStyle.Font = new Font("Century Gothic", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            //            ListaProductos_DataGridView.Rows[i].Cells[0].Value = ProOrdenados[ind].Codigo;
            //            ListaProductos_DataGridView.Rows[i].Cells[1].Value = ProOrdenados[ind].Nombre;
            //            ListaProductos_DataGridView.Rows[i].Cells[2].Value = ProOrdenados[ind].Marca;
            //            ListaProductos_DataGridView.Rows[i].Cells[3].Value = ProOrdenados[ind].Tipo;
            //            ListaProductos_DataGridView.Rows[i].DefaultCellStyle.BackColor = Colores[Index];
            //            ListaProductos_DataGridView.Rows[i].Cells[5].Value = ProOrdenados[ind].UnidadesVendidas;
            //            ListaProductos_DataGridView.Rows[i].Cells[6].Value = ProOrdenados[ind].PrecioAntesIVA.ToString("N0");
            //            ListaProductos_DataGridView.Rows[i].Cells[7].Value = ProOrdenados[ind].PrecioCompra.ToString("N0");
            //            ListaProductos_DataGridView.Rows[i].Cells[8].Value = ProOrdenados[ind].PrecioVenta.ToString("N0");
            //            ListaProductos_DataGridView.Rows[i].Cells[9].Value = ProOrdenados[ind].Inversion.ToString("N0");
            //            ListaProductos_DataGridView.Rows[i].Cells[10].Value = ProOrdenados[ind].Ganancia.ToString("N0");
            //            ListaProductos_DataGridView.Rows[i].Cells[11].Value = ProOrdenados[ind].Nota;
            //            ListaProductos_DataGridView.Rows[i].Cells[12].Value = ProOrdenados[ind].Origen.Nombre;
            //            ListaProductos_DataGridView.Rows[i].Cells[13].Value = ProOrdenados[ind].Origen.Contacto;

            //            if (ProOrdenados[ind].Unidades > StockAceptableMax)
            //            {
            //                ListaProductos_DataGridView.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(158, 214, 169);
            //                ListaProductos_DataGridView.Rows[i].Cells[4].Style.ForeColor = Color.FromArgb(0, 89, 18);
            //                ListaProductos_DataGridView.Rows[i].Cells[4].Style.Font = new Font("Century Gothic", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            //            }
            //            if (ProOrdenados[ind].Unidades < StockAceptableMin)
            //            {
            //                ListaProductos_DataGridView.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(236, 153, 153);
            //                ListaProductos_DataGridView.Rows[i].Cells[4].Style.ForeColor = Color.DarkRed;
            //                ListaProductos_DataGridView.Rows[i].Cells[4].Style.Font = new Font("Century Gothic", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            //            }
            //            if (ProOrdenados[ind].Unidades <= StockAceptableMax && ProOrdenados[ind].Unidades >= StockAceptableMin)
            //            {
            //                ListaProductos_DataGridView.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(236, 217, 116);
            //                ListaProductos_DataGridView.Rows[i].Cells[4].Style.ForeColor = Color.FromArgb(115, 87, 6);
            //                ListaProductos_DataGridView.Rows[i].Cells[4].Style.Font = new Font("Century Gothic", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            //            }
            //            ListaProductos_DataGridView.Rows[i].Cells[4].Value = ProOrdenados[ind].Unidades;
            //            i++;
            //        }
            //    }
            //    catch (Exception ty)
            //    {
            //        MessageBox.Show("Error al cargar la página, intente abriendo la página de nuevo.", "Error al cargar la página", MessageBoxButtons.OK);
            //        return;
            //    }
            //});
            //Task h2 = new Task(()=>
            //{
            //    var _productos = Productos;
            //    try
            //    {
            //        List<Producto> ProOrdenados = new List<Producto>();
            //        if (Marca_RadioButton.Checked) ProOrdenados = _productos.OrderBy(p => p.Marca).ToList();
            //        else ProOrdenados = _productos.OrderBy(p => p.Tipo).ToList();
            //        string LastValue = string.Empty;
            //        int Index = 0;
            //        int limit = ProOrdenados.Count / 2;
            //        int i = ProOrdenados.Count;
            //        for (int ind = ProOrdenados.Count; ind >= limit; ind--)
            //        {
            //            if (Marca_RadioButton.Checked)
            //            {
            //                if (LastValue != ProOrdenados[ind].Marca)
            //                {
            //                    var Itndex = Index < Colores.Count - 1 ? Index++ : Index = 0;
            //                    LastValue = ProOrdenados[ind].Marca;
            //                }
            //            }
            //            else
            //            {
            //                if (LastValue != ProOrdenados[ind].Tipo)
            //                {
            //                    var Itndex = Index < Colores.Count - 1 ? Index++ : Index = 0;
            //                    LastValue = ProOrdenados[ind].Tipo;
            //                }
            //            }
            //            ListaProductos_DataGridView.Rows[i].DefaultCellStyle.BackColor = Colores[Index];
            //            ListaProductos_DataGridView.Rows[i].DefaultCellStyle.Font = new Font("Century Gothic", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            //            ListaProductos_DataGridView.Rows[i].Cells[0].Value = ProOrdenados[ind].Codigo;
            //            ListaProductos_DataGridView.Rows[i].Cells[1].Value = ProOrdenados[ind].Nombre;
            //            ListaProductos_DataGridView.Rows[i].Cells[2].Value = ProOrdenados[ind].Marca;
            //            ListaProductos_DataGridView.Rows[i].Cells[3].Value = ProOrdenados[ind].Tipo;
            //            ListaProductos_DataGridView.Rows[i].DefaultCellStyle.BackColor = Colores[Index];
            //            ListaProductos_DataGridView.Rows[i].Cells[5].Value = ProOrdenados[ind].UnidadesVendidas;
            //            ListaProductos_DataGridView.Rows[i].Cells[6].Value = ProOrdenados[ind].PrecioAntesIVA.ToString("N0");
            //            ListaProductos_DataGridView.Rows[i].Cells[7].Value = ProOrdenados[ind].PrecioCompra.ToString("N0");
            //            ListaProductos_DataGridView.Rows[i].Cells[8].Value = ProOrdenados[ind].PrecioVenta.ToString("N0");
            //            ListaProductos_DataGridView.Rows[i].Cells[9].Value = ProOrdenados[ind].Inversion.ToString("N0");
            //            ListaProductos_DataGridView.Rows[i].Cells[10].Value = ProOrdenados[ind].Ganancia.ToString("N0");
            //            ListaProductos_DataGridView.Rows[i].Cells[11].Value = ProOrdenados[ind].Nota;
            //            ListaProductos_DataGridView.Rows[i].Cells[12].Value = ProOrdenados[ind].Origen.Nombre;
            //            ListaProductos_DataGridView.Rows[i].Cells[13].Value = ProOrdenados[ind].Origen.Contacto;

            //            if (ProOrdenados[ind].Unidades > StockAceptableMax)
            //            {
            //                ListaProductos_DataGridView.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(158, 214, 169);
            //                ListaProductos_DataGridView.Rows[i].Cells[4].Style.ForeColor = Color.FromArgb(0, 89, 18);
            //                ListaProductos_DataGridView.Rows[i].Cells[4].Style.Font = new Font("Century Gothic", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            //            }
            //            if (ProOrdenados[ind].Unidades < StockAceptableMin)
            //            {
            //                ListaProductos_DataGridView.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(236, 153, 153);
            //                ListaProductos_DataGridView.Rows[i].Cells[4].Style.ForeColor = Color.DarkRed;
            //                ListaProductos_DataGridView.Rows[i].Cells[4].Style.Font = new Font("Century Gothic", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            //            }
            //            if (ProOrdenados[ind].Unidades <= StockAceptableMax && ProOrdenados[ind].Unidades >= StockAceptableMin)
            //            {
            //                ListaProductos_DataGridView.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(236, 217, 116);
            //                ListaProductos_DataGridView.Rows[i].Cells[4].Style.ForeColor = Color.FromArgb(115, 87, 6);
            //                ListaProductos_DataGridView.Rows[i].Cells[4].Style.Font = new Font("Century Gothic", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            //            }
            //            ListaProductos_DataGridView.Rows[i].Cells[4].Value = ProOrdenados[ind].Unidades;
            //            i--;
            //        }
            //    }
            //    catch (Exception ty)
            //    {
            //        MessageBox.Show("Error al cargar la página, intente abriendo la página de nuevo.", "Error al cargar la página", MessageBoxButtons.OK);
            //        return;
            //    }
            //});
            //h.Start();
            //h2.Start();
            //delegado d1 = new delegado(HacerAlgo1);
            //delegado d2 = new delegado(HacerAlgo2);
            //AsyncCallback cb = new AsyncCallback(Acabe);
           // d1.BeginInvoke(ListaProductos_DataGridView.DataSource, cb, null);
        }

        public void Llenar_DataGridView2(List<Producto> _productos)
        {
            try
            {
                List<Producto> ProOrdenados = new List<Producto>();
                if (Marca_RadioButton.Checked) ProOrdenados = _productos.OrderBy(p => p.Marca).ToList();
                else ProOrdenados = _productos.OrderBy(p => p.Tipo).ToList();
                string LastValue = string.Empty;
                int Index = 0;
                int limit = ProOrdenados.Count / 2;
                int i = ProOrdenados.Count;
                for (int ind = ProOrdenados.Count; ind >= limit; ind--)
                {
                    if (Marca_RadioButton.Checked)
                    {
                        if (LastValue != ProOrdenados[ind].Marca)
                        {
                            var Itndex = Index < Colores.Count - 1 ? Index++ : Index = 0;
                            LastValue = ProOrdenados[ind].Marca;
                        }
                    }
                    else
                    {
                        if (LastValue != ProOrdenados[ind].Tipo)
                        {
                            var Itndex = Index < Colores.Count - 1 ? Index++ : Index = 0;
                            LastValue = ProOrdenados[ind].Tipo;
                        }
                    }
                    ListaProductos_DataGridView.Rows[i].DefaultCellStyle.BackColor = Colores[Index];
                    ListaProductos_DataGridView.Rows[i].DefaultCellStyle.Font = new Font("Century Gothic", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    ListaProductos_DataGridView.Rows[i].Cells[0].Value = ProOrdenados[ind].Codigo;
                    ListaProductos_DataGridView.Rows[i].Cells[1].Value = ProOrdenados[ind].Nombre;
                    ListaProductos_DataGridView.Rows[i].Cells[2].Value = ProOrdenados[ind].Marca;
                    ListaProductos_DataGridView.Rows[i].Cells[3].Value = ProOrdenados[ind].Tipo;
                    ListaProductos_DataGridView.Rows[i].DefaultCellStyle.BackColor = Colores[Index];
                    ListaProductos_DataGridView.Rows[i].Cells[5].Value = ProOrdenados[ind].UnidadesVendidas;
                    ListaProductos_DataGridView.Rows[i].Cells[6].Value = ProOrdenados[ind].PrecioAntesIVA.ToString("N0");
                    ListaProductos_DataGridView.Rows[i].Cells[7].Value = ProOrdenados[ind].PrecioCompra.ToString("N0");
                    ListaProductos_DataGridView.Rows[i].Cells[8].Value = ProOrdenados[ind].PrecioVenta.ToString("N0");
                    ListaProductos_DataGridView.Rows[i].Cells[9].Value = ProOrdenados[ind].Inversion.ToString("N0");
                    ListaProductos_DataGridView.Rows[i].Cells[10].Value = ProOrdenados[ind].Ganancia.ToString("N0");
                    ListaProductos_DataGridView.Rows[i].Cells[11].Value = ProOrdenados[ind].Nota;
                    ListaProductos_DataGridView.Rows[i].Cells[12].Value = ProOrdenados[ind].Origen.Nombre;
                    ListaProductos_DataGridView.Rows[i].Cells[13].Value = ProOrdenados[ind].Origen.Contacto;

                    if (ProOrdenados[ind].Unidades > StockAceptableMax)
                    {
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(158, 214, 169);
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.ForeColor = Color.FromArgb(0, 89, 18);
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.Font = new Font("Century Gothic", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    if (ProOrdenados[ind].Unidades < StockAceptableMin)
                    {
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(236, 153, 153);
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.ForeColor = Color.DarkRed;
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.Font = new Font("Century Gothic", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    if (ProOrdenados[ind].Unidades <= StockAceptableMax && ProOrdenados[ind].Unidades >= StockAceptableMin)
                    {
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(236, 217, 116);
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.ForeColor = Color.FromArgb(115, 87, 6);
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.Font = new Font("Century Gothic", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    ListaProductos_DataGridView.Rows[i].Cells[4].Value = ProOrdenados[ind].Unidades;
                    i--;
                }
            }
            catch (Exception ty)
            {
                MessageBox.Show("Error al cargar la página, intente abriendo la página de nuevo.", "Error al cargar la página", MessageBoxButtons.OK);
                return;
            }
        }

        public void Llenar_DataGridView1(List<Producto> _productos)
        {
            try
            {
                int i = 0;
                List<Producto> ProOrdenados = new List<Producto>();
                if (Marca_RadioButton.Checked) ProOrdenados = _productos.OrderBy(p => p.Marca).ToList();
                else ProOrdenados = _productos.OrderBy(p => p.Tipo).ToList();
                string LastValue = string.Empty;
                int Index = 0;
                int limit = ProOrdenados.Count / 2;
                for(int ind = 0; ind < limit; ind++)
                {
                    if (Marca_RadioButton.Checked)
                    {
                        if (LastValue != ProOrdenados[ind].Marca)
                        {
                            var Itndex = Index < Colores.Count - 1 ? Index++ : Index = 0;
                            LastValue = ProOrdenados[ind].Marca;
                        }
                    }
                    else
                    {
                        if (LastValue != ProOrdenados[ind].Tipo)
                        {
                            var Itndex = Index < Colores.Count - 1 ? Index++ : Index = 0;
                            LastValue = ProOrdenados[ind].Tipo;
                        }
                    }
                    ListaProductos_DataGridView.Rows[i].DefaultCellStyle.BackColor = Colores[Index];
                    ListaProductos_DataGridView.Rows[i].DefaultCellStyle.Font = new Font("Century Gothic", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    ListaProductos_DataGridView.Rows[i].Cells[0].Value = ProOrdenados[ind].Codigo;
                    ListaProductos_DataGridView.Rows[i].Cells[1].Value = ProOrdenados[ind].Nombre;
                    ListaProductos_DataGridView.Rows[i].Cells[2].Value = ProOrdenados[ind].Marca;
                    ListaProductos_DataGridView.Rows[i].Cells[3].Value = ProOrdenados[ind].Tipo;
                    ListaProductos_DataGridView.Rows[i].DefaultCellStyle.BackColor = Colores[Index];
                    ListaProductos_DataGridView.Rows[i].Cells[5].Value = ProOrdenados[ind].UnidadesVendidas;
                    ListaProductos_DataGridView.Rows[i].Cells[6].Value = ProOrdenados[ind].PrecioAntesIVA.ToString("N0");
                    ListaProductos_DataGridView.Rows[i].Cells[7].Value = ProOrdenados[ind].PrecioCompra.ToString("N0");
                    ListaProductos_DataGridView.Rows[i].Cells[8].Value = ProOrdenados[ind].PrecioVenta.ToString("N0");
                    ListaProductos_DataGridView.Rows[i].Cells[9].Value = ProOrdenados[ind].Inversion.ToString("N0");
                    ListaProductos_DataGridView.Rows[i].Cells[10].Value = ProOrdenados[ind].Ganancia.ToString("N0");
                    ListaProductos_DataGridView.Rows[i].Cells[11].Value = ProOrdenados[ind].Nota;
                    ListaProductos_DataGridView.Rows[i].Cells[12].Value = ProOrdenados[ind].Origen.Nombre;
                    ListaProductos_DataGridView.Rows[i].Cells[13].Value = ProOrdenados[ind].Origen.Contacto;

                    if (ProOrdenados[ind].Unidades > StockAceptableMax)
                    {
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(158, 214, 169);
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.ForeColor = Color.FromArgb(0, 89, 18);
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.Font = new Font("Century Gothic", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    if (ProOrdenados[ind].Unidades < StockAceptableMin)
                    {
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(236, 153, 153);
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.ForeColor = Color.DarkRed;
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.Font = new Font("Century Gothic", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    if (ProOrdenados[ind].Unidades <= StockAceptableMax && ProOrdenados[ind].Unidades >= StockAceptableMin)
                    {
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(236, 217, 116);
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.ForeColor = Color.FromArgb(115, 87, 6);
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.Font = new Font("Century Gothic", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    ListaProductos_DataGridView.Rows[i].Cells[4].Value = ProOrdenados[ind].Unidades;
                    i++;
                }
            }
            catch (Exception ty)
            {
                MessageBox.Show("Error al cargar la página, intente abriendo la página de nuevo.", "Error al cargar la página", MessageBoxButtons.OK);
                return;
            }
        }

        public void Llenar_DataGridViewBase(List<Producto> _productos)
        {
            try
            {
                ListaProductos_DataGridView.Rows.Clear();
                List<Producto> ProOrdenados = new List<Producto>();
                if (Marca_RadioButton.Checked) ProOrdenados = _productos.OrderBy(p => p.Marca).ToList();
                else ProOrdenados = _productos.OrderBy(p => p.Tipo).ToList();
                string LastValue = string.Empty;
                int Index = 0;
                foreach (var D in ProOrdenados)
                {
                    int i = ListaProductos_DataGridView.Rows.Add();
                }
            }
            catch (Exception ty)
            {
                MessageBox.Show("Error al cargar la página, intente abriendo la página de nuevo.", "Error al cargar la página", MessageBoxButtons.OK);
                return;
            }
        }


        public void Llenar_DataGridView(List<Producto> _productos)
        {
            try
            {
                ListaProductos_DataGridView.Rows.Clear();
                List<Producto> ProOrdenados = new List<Producto>();
                if (Marca_RadioButton.Checked) ProOrdenados = _productos.OrderBy(p => p.Marca).ToList();
                else ProOrdenados = _productos.OrderBy(p => p.Tipo).ToList();
                string LastValue = string.Empty;
                int Index = 0;
                foreach (var D in ProOrdenados)
                {
                    int i = ListaProductos_DataGridView.Rows.Add();

                    if (Marca_RadioButton.Checked)
                    {
                        if (LastValue != D.Marca)
                        {
                            var Itndex = Index < Colores.Count - 1 ? Index++ : Index = 0;
                            LastValue = D.Marca;
                        }
                    }
                    else
                    {
                        if (LastValue != D.Tipo)
                        {
                            var Itndex = Index < Colores.Count - 1 ? Index++ : Index = 0;
                            LastValue = D.Tipo;
                        }
                    }

                    ListaProductos_DataGridView.Rows[i].Cells[0].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    ListaProductos_DataGridView.Rows[i].Cells[1].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    ListaProductos_DataGridView.Rows[i].Cells[2].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    ListaProductos_DataGridView.Rows[i].Cells[3].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    ListaProductos_DataGridView.Rows[i].Cells[4].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    ListaProductos_DataGridView.Rows[i].Cells[5].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    ListaProductos_DataGridView.Rows[i].Cells[6].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    ListaProductos_DataGridView.Rows[i].Cells[7].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    ListaProductos_DataGridView.Rows[i].Cells[8].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    ListaProductos_DataGridView.Rows[i].Cells[9].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    ListaProductos_DataGridView.Rows[i].Cells[10].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    ListaProductos_DataGridView.Rows[i].Cells[11].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    ListaProductos_DataGridView.Rows[i].Cells[12].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                    ListaProductos_DataGridView.Rows[i].Cells[13].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);

                    ListaProductos_DataGridView.Rows[i].DefaultCellStyle.BackColor = Colores[Index];
                    ListaProductos_DataGridView.Rows[i].DefaultCellStyle.Font = new Font("Century Gothic", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    ListaProductos_DataGridView.Rows[i].Cells[0].Value = D.Codigo;
                    ListaProductos_DataGridView.Rows[i].Cells[1].Value = D.Nombre;
                    ListaProductos_DataGridView.Rows[i].Cells[2].Value = D.Marca;
                    ListaProductos_DataGridView.Rows[i].Cells[3].Value = D.Tipo;
                    ListaProductos_DataGridView.Rows[i].DefaultCellStyle.BackColor = Colores[Index];
                    ListaProductos_DataGridView.Rows[i].Cells[5].Value = D.UnidadesVendidas;
                    ListaProductos_DataGridView.Rows[i].Cells[6].Value = D.PrecioAntesIVA.ToString("N0");
                    ListaProductos_DataGridView.Rows[i].Cells[7].Value = D.PrecioCompra.ToString("N0");
                    ListaProductos_DataGridView.Rows[i].Cells[8].Value = D.PrecioVenta.ToString("N0");
                    ListaProductos_DataGridView.Rows[i].Cells[9].Value = D.Inversion.ToString("N0");
                    ListaProductos_DataGridView.Rows[i].Cells[10].Value = D.Ganancia.ToString("N0");
                    ListaProductos_DataGridView.Rows[i].Cells[11].Value = D.Nota;
                    ListaProductos_DataGridView.Rows[i].Cells[12].Value = D.Origen.Nombre;
                    ListaProductos_DataGridView.Rows[i].Cells[13].Value = D.Origen.Contacto;

                    if (D.Unidades > StockAceptableMax)
                    {
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(158, 214, 169);
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.ForeColor = Color.FromArgb(0, 89, 18);
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.Font = new Font("Century Gothic", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    if (D.Unidades < StockAceptableMin)
                    {
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(236, 153, 153);
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.ForeColor = Color.DarkRed;
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.Font = new Font("Century Gothic", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    if (D.Unidades <= StockAceptableMax && D.Unidades >= StockAceptableMin)
                    {
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(236, 217, 116);
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.ForeColor = Color.FromArgb(115, 87, 6);
                        ListaProductos_DataGridView.Rows[i].Cells[4].Style.Font = new Font("Century Gothic", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    ListaProductos_DataGridView.Rows[i].Cells[4].Value = D.Unidades;
                }
            }
            catch (Exception ty)
            {
                MessageBox.Show("Error al cargar la página, intente abriendo la página de nuevo.", "Error al cargar la página", MessageBoxButtons.OK);
                return;
            }
        }


        private void BuscarCod_TextBox_TextChanged(object sender, EventArgs e)
        {
            var t = BuscarCod_TextBox.Text.Replace(" ", "");
            ProductosBusqueda = Productos.Where(p => p.Codigo.ToLower().IndexOf(t.ToLower()) != -1).ToList();
            //Llenar_DataGridView(ProductosBusqueda);
        }

        private void BuscarNom_TextBox_TextChanged(object sender, EventArgs e)
        {
            ProductosBusqueda = Productos.Where(p => p.Nombre.ToLower().IndexOf(BuscarNom_TextBox.Text.ToLower()) != -1).ToList();
            //Llenar_DataGridView(ProductosBusqueda);
        }

        private void Marca_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //Llenar_DataGridView(Productos);
            BuscarCod_TextBox.Text = string.Empty;
            BuscarNom_TextBox.Text = string.Empty;
        }

        private void ListaProductos_DataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    ListaProductos_DataGridView.CurrentCell = ListaProductos_DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
                catch (Exception)
                {
                    return;
                }
                menu = new ContextMenuStrip();
                menu.Items.Add("Editar").Name = "_editar";
                menu.Items.Add("Eliminar").Name = "_eliminar";
                ListaProductos_DataGridView.Rows[e.RowIndex].Selected = true;
                RowIndex = e.RowIndex;
                ListaProductos_DataGridView.CurrentCell = ListaProductos_DataGridView.Rows[e.RowIndex].Cells[0];
                menu.Show(ListaProductos_DataGridView, e.Location);
                menu.Show(Cursor.Position);
                menu.ItemClicked += new ToolStripItemClickedEventHandler(Clic_ContextMenuStrip);
            }
        }

        private async void Clic_ContextMenuStrip(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "_editar":
                    try
                    {
                        AnimacioStart();
                        var ProBu = Productos.Where(p => p.Codigo.Equals(ListaProductos_DataGridView.Rows[RowIndex].Cells[0].Value.ToString().ToLower())).ToList();
                        Producto _pro = ProBu[0];
                        EditarProducto EP = new EditarProducto(_pro, _pro.Unidades, EmailT, ClaveT, Tipos, Marcas, _pro.Inversion);
                        DialogResult diag = EP.ShowDialog();
                        if (diag == DialogResult.Yes)
                        {
                            Obtener();
                        }
                        AnimacionFinish();
                    }
                    catch (Exception)
                    {
                        AnimacionFinish();
                        return;
                    }
                    break;
                case "_eliminar":
                    menu.Visible = false;
                    var tt = ListaProductos_DataGridView.Rows[RowIndex].Cells[1].Value;
                    DialogResult D = MessageBox.Show("¿Esta seguro que quiere eliminar el producto\n"
                        + tt.ToString() + "\ndel inventario?" + "\n\n*Nota: Recuerde que eliminar un producto NO eliminará el registro de compra,"
                        + " por lo que la inversión realizada sobre este producto seguirá afectando "
                        + "el balance general.*", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (D == DialogResult.Yes)
                    {
                        try
                        {
                            SFirebase FB = new SFirebase()
                            {
                                Email = this.EmailT,
                                Clave = this.ClaveT
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
                            await FB.BorrarProducto(ListaProductos_DataGridView.Rows[RowIndex].Cells[0].Value.ToString());
                            MessageBox.Show("El producto fue eliminado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Obtener();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Ha ocurrido un error de conexión, intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //Llenar_DataGridView(Productos);
                            return;
                        }
                    }
                    break;
            }
        }

        private void AnimacionFinish()
        {
            for (int i = 0; i <= ListaProductos_DataGridView.Rows.Count - 1; i++) ListaProductos_DataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.White;
            ListaProductos_DataGridView.GridColor = Color.FromArgb(50, 50, 50);
            ListaProductos_DataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            BuscarCod_TextBox.Text = string.Empty;
            BuscarNom_TextBox.Text = string.Empty;
            Proveedor_GroupBox.Enabled = true;
        }

        private void AnimacioStart()
        {
            for (int i = 0; i <= ListaProductos_DataGridView.Rows.Count - 1; i++) ListaProductos_DataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(90, 90, 90);
            ListaProductos_DataGridView.GridColor = Color.FromArgb(90, 90, 90);
            ListaProductos_DataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(90, 90, 90);
            Proveedor_GroupBox.Enabled = false;
        }

        private void ListaProductos_DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Marca_RadioButton.Checked)
            {
                var _productosAEditar = Productos.Where(m => m.Marca == ListaProductos_DataGridView.Rows[e.RowIndex].Cells[2].Value.ToString()).ToList();
            }
            else
            {
                var _productosAEditar = Productos.Where(m => m.Tipo == ListaProductos_DataGridView.Rows[e.RowIndex].Cells[3].Value.ToString()).ToList();
            }
        }
        #endregion

        private async void HacerAlgo1(object p)
        {
            CheckForIllegalCrossThreadCalls = false;
            bs = p as BindingSource;

            Llenar_DataGridView1(Productos);
        }

        private void HacerAlgo2(object p)
        {
            CheckForIllegalCrossThreadCalls = false;
            bs = p as BindingSource;

            Llenar_DataGridView2(Productos);
        }

        public void Acabe(IAsyncResult ar)
        {
            ListaProductos_DataGridView.Invalidate();
        }

    }
}
