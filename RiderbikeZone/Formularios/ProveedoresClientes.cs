namespace RiderbikeZone.Formularios
{
    using RiderbikeZone.Modelos;
    using RiderbikeZone.Servicios;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    public partial class ProveedoresClientes : Form
    {
        #region Propiedades
        const string RutaBackUp = @"C:\MANGO\BackUp\";
        public string Email { get; set; }
        public string Clave { get; set; }
        public List<Proveedor> Proveedores { get; set; }
        public List<ClientNew> Clientes { get; set; }
        public List<ClientNew> ProductosBusqueda { get; set; }
        public ContextMenuStrip menu { get; set; }
        public int RowIndex { get; set; }
        #endregion

        #region Constructor
        public ProveedoresClientes(string _email, string _clave)
        {
            ProductosBusqueda = new List<ClientNew>();
            Email = _email;
            Clave = _clave;
            InitializeComponent();
            Obtener();
        }
        #endregion

        #region Métodos
        public async void Obtener()
        {
            if (this.Clientes != null) Clientes.Clear();
            if (this.Proveedores != null) Proveedores.Clear();
            Clientes = new List<ClientNew>();
            Proveedores = new List<Proveedor>();
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
            var _proveedores = await FB.ObtenerProveedores();
            var _clientes = await FB.ObtenerClientNew();
            foreach (var c in _proveedores) Proveedores.Add(c);
            foreach (var v in _clientes) Clientes.Add(v);
            ///////////////////////////////////


            //////////////////////////////////
            //foreach (var item in Clientes)
            //{
            //    item.Nombre = item.Nombre.ToUpper();
            //    var dato = item.Ubicacion.Departamento;
            //    aa = 0;
            //    foreach (var i in departamentoss)
            //    {
            //        if (dato.Contains(i))
            //        {
            //            item.Ubicacion.Departamento = depar[aa];
            //            item.Ubicacion.Ciudad = dato;
            //            break;
            //        }
            //        aa++;
            //    }
            //    cli++;
            //}
            //////////////////////////////////////
            /////
            //await FB.DeleteAllString("ClientNew");
            //foreach (var i in Clientes)
            //{
            //    var ide = new _Identificacion()
            //    {
            //        Tipo = "CC-Cédula Ciudadanía",
            //        Numero = i.Identificacion.Numero,
            //        DigitoVe = -1
            //    };
            //    string iOrigen = "";
            //    var _origen = i.Origen == "Otro" ? iOrigen = "Punto Fisico" : iOrigen = i.Origen;

            //    try
            //    {
            //        var t = new ClientNew()
            //        {
            //            Fecha = i.Fecha,
            //            IsDetal = true,
            //            IsPersonaNatural = true,
            //            Nombre = i.Nombre,
            //            Identificacion = ide,
            //            Ubicacion = i.Ubicacion,
            //            Productos = i.Productos,
            //            Origen = iOrigen,
            //            Contacto = i.Contacto,
            //            IsMan = true,
            //            Pago = "Pagos digitales"
            //        };
            //        await FB.Guardar("ClientNew", t);
            //    }
            //    catch (Exception)
            //    {
            //        var y = 0;
            //    }
            //}

            //List<string> bus = new List<string>()
            //{
            //    "Amaz",
            //    "Anti",
            //    "Arau",
            //    "Atla",
            //    "Bogo",
            //    "Boli",
            //    "Boya",
            //    "Cald",
            //    "Caqu",
            //    "Casa",
            //    "Cauc",
            //    "Cesa",
            //    "Choc",
            //    "Cord",
            //    "Cund",
            //    "Guai",
            //    "Guaj",
            //    "Guav",
            //    "Huil",
            //    "Magd",
            //    "Meta",
            //    "Nari",
            //    "Nort",
            //    "Putu",
            //    "Quin",
            //    "Risa",
            //    "Andr",
            //    "Sant",
            //    "Sucr",
            //    "Toli",
            //    "Vall",
            //    "Vaup",
            //    "Vich"
            //};
            //List<string> Depar = new List<string>()
            //{
            //    "Amazonas",
            //    "Antioquia",
            //    "Arauca",
            //    "Atlantico",
            //    "Bogota",
            //    "Bolivar",
            //    "Boyaca",
            //    "Caldas",
            //    "Caqueta",
            //    "Casanare",
            //    "Cauca",
            //    "Cesar",
            //    "Choco",
            //    "Cordoba",
            //    "Cundinamarca",
            //    "Guainia",
            //    "Guajira",
            //    "Guaviare",
            //    "Huila",
            //    "Magdalena",
            //    "Meta",
            //    "Nariño",
            //    "Norte de Santander",
            //    "Putumayo",
            //    "Quindio",
            //    "Risaralda",
            //    "San Andres",
            //    "Santander",
            //    "Sucre",
            //    "Tolima",
            //    "Valle del Cauca",
            //    "Vaupes",
            //    "Vichada"
            //};

            //int cyu = 0;
            //foreach (var it in Clientes)
            //{
            //    it.Nombre = it.Nombre.ToUpper();
            //    cyu = 0;
            //    foreach (var busq in bus)
            //    {
            //        if (it.Ubicacion.Ciudad.Contains(busq))
            //        {
            //            it.Ubicacion.Departamento = Depar[cyu];
            //        }
            //        cyu++;
            //    }
            //}
            //foreach (var item in Clientes)
            //{
            //    await FB.DeleteClientNew(item.Nombre);
            //    await FB.Guardar("UCliente", item);
            //}
            ///////////////////////////////////
            LlenarTablas(Clientes);

        }

        private void BuscarCod_TextBox_TextChanged(object sender, EventArgs e)
        {
            var t = BuscarCod_TextBox.Text.Replace(" ", "");
            ProductosBusqueda = Clientes.Where(p => p.Identificacion.Numero.IndexOf(t.ToLower()) != -1).ToList();
            LlenarTablas(ProductosBusqueda);
        }

        private void BuscarNom_TextBox_TextChanged(object sender, EventArgs e)
        {
            ProductosBusqueda = Clientes.Where(p => p.Nombre.ToLower().IndexOf(BuscarNom_TextBox.Text.ToLower()) != -1).ToList();
            LlenarTablas(ProductosBusqueda);
        }

        private void LlenarTablas(List<ClientNew> _Ncliente)
        {
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
                        if(c.Productos != null) foreach (var p in c.Productos) _productos += p.Codigo + "|" + p.Nombre + "|" + p.Unidades + p.Valor + "\n";

                        Clientes_DataGridView.Rows[i].Cells[0].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                        Clientes_DataGridView.Rows[i].Cells[1].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                        Clientes_DataGridView.Rows[i].Cells[2].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                        Clientes_DataGridView.Rows[i].Cells[3].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                        Clientes_DataGridView.Rows[i].Cells[4].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                        Clientes_DataGridView.Rows[i].Cells[5].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                        Clientes_DataGridView.Rows[i].Cells[6].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                        Clientes_DataGridView.Rows[i].Cells[7].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                        Clientes_DataGridView.Rows[i].Cells[8].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                        Clientes_DataGridView.Rows[i].Cells[9].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                        Clientes_DataGridView.Rows[i].Cells[10].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                        Clientes_DataGridView.Rows[i].Cells[11].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                        Clientes_DataGridView.Rows[i].Cells[12].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                        Clientes_DataGridView.Rows[i].Cells[13].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);
                        Clientes_DataGridView.Rows[i].Cells[13].Style.SelectionBackColor = Color.FromArgb(0, 167, 180);

                        Clientes_DataGridView.Rows[i].DefaultCellStyle.Font = new Font("Century Gothic", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
                        Clientes_DataGridView.Rows[i].Cells[0].Value = c.Fecha;
                        Clientes_DataGridView.Rows[i].Cells[1].Value = c.IsPersonaNatural ? "Natural" : "Juridica";
                        Clientes_DataGridView.Rows[i].Cells[2].Value = c.Nombre;
                        Clientes_DataGridView.Rows[i].Cells[3].Value = c.Identificacion.Tipo;
                        Clientes_DataGridView.Rows[i].Cells[4].Value = c.Identificacion.Numero;
                        Clientes_DataGridView.Rows[i].Cells[5].Value = c.Identificacion.DigitoVe == -1 ? "" : c.Identificacion.DigitoVe.ToString();
                        Clientes_DataGridView.Rows[i].Cells[6].Value = c.IsMan == true? "Masculino" : "Femenino";
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

        private void Clientes_DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void Clientes_DataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    Clientes_DataGridView.CurrentCell = Clientes_DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
                catch (Exception)
                {
                    return;
                }
                menu = new ContextMenuStrip();
                menu.Items.Add("Editar").Name = "_editar";
                menu.Items.Add("Eliminar").Name = "_eliminar";
                Clientes_DataGridView.Rows[e.RowIndex].Selected = true;
                RowIndex = e.RowIndex;
                Clientes_DataGridView.CurrentCell = Clientes_DataGridView.Rows[e.RowIndex].Cells[0];
                menu.Show(Clientes_DataGridView, e.Location);
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
                        //AnimacioStart();
                        //var ProBu = Productos.Where(p => p.Codigo.Equals(ListaProductos_DataGridView.Rows[RowIndex].Cells[0].Value.ToString().ToLower())).ToList();
                        //Producto _pro = ProBu[0];
                        //EditarProducto EP = new EditarProducto(_pro, _pro.Unidades, EmailT, ClaveT, Tipos, Marcas, _pro.Inversion);
                        //DialogResult diag = EP.ShowDialog();
                        //if (diag == DialogResult.Yes)
                        //{
                        //    Obtener();
                        //}

                        var _cliente = Clientes.Where(m => m.Nombre == Clientes_DataGridView.Rows[RowIndex].Cells[2].Value.ToString()).ToList();
                        EditarCliente EC = new EditarCliente(_cliente[0], Email, Clave, this.Clientes[RowIndex].Nombre);
                        DialogResult d = EC.ShowDialog();
                        if (d == DialogResult.Yes) Obtener();

                        //AnimacionFinish();
                    }
                    catch (Exception)
                    {
                        //AnimacionFinish();
                        return;
                    }
                    break;
                case "_eliminar":
                    menu.Visible = false;
                    var tt = Clientes_DataGridView.Rows[RowIndex].Cells[2].Value;
                    DialogResult D = MessageBox.Show("¿Esta seguro que quiere eliminar el cliente\n"
                        + tt.ToString() + "?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (D == DialogResult.Yes)
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
                                var t = await FB.LoginWithEmail(false);
                                if (t == null) throw new Exception();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Ha ocurrido un error de conexión.\nIntente recargando la página.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            await FB.DeleteClientNew(Clientes_DataGridView.Rows[RowIndex].Cells[2].Value.ToString());
                            MessageBox.Show("El cliente a sido eliminado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void HacerBK_Button_Click(object sender, EventArgs e)
        {
            DateTime _fecha = DateTime.Now;
            Random rnd = new Random();
            int num = rnd.Next(1000, 9999);
            List<string> Mes = new List<string>()
            { "ene","feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic" };
            string Dir = RutaBackUp + _fecha.Day + "_" + Mes[_fecha.Month - 1] + "_" + _fecha.Year + "BackUp_Clientes" + num;
            SCrearXLS XLS = new SCrearXLS();
            //XLS.EscribirExcel(Clientes_DataGridView, Dir);
            XLS.ExportarDataGridViewExcel(Clientes_DataGridView, Dir);
            MessageBox.Show("Copia de seguridad de clientes creada.", "Copia creada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        private void Aniadir_Button_Click(object sender, EventArgs e)
        {
            try
            {
                EditarCliente EC = new EditarCliente(new ClientNew(), Email, Clave, "nuevo_cliente");
                DialogResult d = EC.ShowDialog();
                if (d == DialogResult.Yes) Obtener();
            }
            catch (Exception)
            {
                return;
            }
        }

        private void AnimacionFinish()
        {
            for (int i = 0; i <= Clientes_DataGridView.Rows.Count - 1; i++) Clientes_DataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.White;
            Clientes_DataGridView.GridColor = Color.FromArgb(50, 50, 50);
            Clientes_DataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            BuscarCod_TextBox.Text = string.Empty;
            BuscarNom_TextBox.Text = string.Empty;
            Proveedor_GroupBox.Enabled = true;
        }

        private void AnimacioStart()
        {
            for (int i = 0; i <= Clientes_DataGridView.Rows.Count - 1; i++) Clientes_DataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(90, 90, 90);
            Clientes_DataGridView.GridColor = Color.FromArgb(90, 90, 90);
            Clientes_DataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(90, 90, 90);
            Proveedor_GroupBox.Enabled = false;
        }
        #endregion
    }
}
