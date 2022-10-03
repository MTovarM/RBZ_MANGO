namespace RiderbikeZone.Formularios
{
    using RiderbikeZone.Modelos;
    using RiderbikeZone.Servicios;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    public partial class Subiendo : Form
    {
        #region Propiedades
        public int Total { get; set; }
        public string EmailT { get; set; }
        public string ClaveT { get; set; }
        public Proveedor Prov { get; set; }
        public List<Producto> Productos { get; set; }
        public List<Producto> ProductosNoEnviados { get; set; }
        #endregion

        #region Constructor
        public Subiendo(string _email, string _clave, List<Producto> _listaProductos, int _total, Proveedor _proveedor)
        {
            Prov = _proveedor;
            Total = _total;
            ProductosNoEnviados = new List<Producto>();
            Productos = _listaProductos;
            EmailT = _email;
            ClaveT = _clave;
            InitializeComponent();
            SubirProductos();
        }
        #endregion

        #region Métodos
        public async void SubirProductos()
        {
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
                MessageBox.Show("Ha ocurrido un error de conexión.\nIntente de nuevo.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.No;
                this.Close();
            }
            int incrementoPB = Convert.ToInt32(100 / Productos.Count);
            int RestarValor = 0;
            foreach (var Prro in Productos)
            {
                try
                {
                    await FB.Guardar("Inventario", Prro);
                    if (SubirProductos_ProgressBar.Value <= 99)
                    {
                        SubirProductos_ProgressBar.Value += incrementoPB;
                        if (Prro.Nombre.Length <= 10) SubiendoPB_Label.Text = "Subiendo " + Prro.Nombre + "...";
                        else SubiendoPB_Label.Text = "Subiendo " + Prro.Nombre.Substring(0, 10) + "...";
                        PorcentajePB_Label.Text = SubirProductos_ProgressBar.Value + "%";
                    }
                }
                catch (Exception)
                {
                    ProductosNoEnviados.Add(Prro);
                    RestarValor += Prro.Inversion;
                }
            }
            SubiendoPB_Label.Text = "Subiendo Compra ...";
            if (ProductosNoEnviados.Count == 0) this.DialogResult = DialogResult.OK;
            else this.DialogResult = DialogResult.No;
            foreach (var l in ProductosNoEnviados) Productos.Remove(l);
            DateTime _fecha = DateTime.Now;
            List<string> Mes = new List<string>()
            {"ene","feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic"};
            string Fecha = _fecha.Day + "/" + Mes[_fecha.Month - 1] + "/" + _fecha.Year;
            if ((Total - RestarValor) == 0)
            {
                this.Close();
            }
            else
            {
                Compra Compras = new Compra()
                {
                    Fecha = Fecha,
                    ListaProductos = Productos,
                    Proveedor = Prov,
                    Total = Total - RestarValor
                };

                await FB.Guardar("Compras", Compras);

                this.Close();
            }
        }
        #endregion
    }
}
