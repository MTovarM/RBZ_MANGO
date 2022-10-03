namespace RiderbikeZone.Formularios
{
    using RiderbikeZone.Modelos;
    using RiderbikeZone.Servicios;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public partial class Comprar : Form
    {
        ///######COSAS QUE HACEN FALTA
        /// 1. Colocar "el cuadro en rojo" cuando este vacio

        #region Propiedades
        public double IVA { get; set; }
        public double GANANCIA { get; set; }
        public string EmailT { get; set; }
        public string ClaveT { get; set; }
        public int TInversion { get; set; }
        public int TValorCompra { get; set; }
        public int TValorSinIVA { get; set; }
        public int TValorVenta { get; set; }
        public int Total { get; set; }
        public int ANI { get; set; }
        public bool ComBoxFlat { get; set; }
        public bool ProductosDTXT { get; set; }
        public List<string> Tipos { get; set; }
        public List<string> Marcas { get; set; }
        public List<string> Codigos { get; set; }
        public List<Producto> Productos { get; set; }
        public List<Producto> ProductosSinUnid { get; set; }
        public SEscribirTXT TXT { get; set; }
        #endregion

        #region Constructor
        public Comprar(string _email, string _clave)
        {
            ANI = 0;
            TValorSinIVA = 0;
            TXT = new SEscribirTXT();
            GANANCIA = Convert.ToDouble(TXT.Leer("GANANCIA.txt"));
            IVA = Convert.ToDouble(TXT.Leer("IVA.txt"));
            ProductosDTXT = false;
            EmailT = _email;
            ClaveT = _clave;
            ProductosSinUnid = new List<Producto>();
            Productos = new List<Producto>();
            Codigos = new List<string>();
            Tipo_ComboBox = new ComboBox();
            Marca_ComboBox = new ComboBox();
            Total = 0;
            InitializeComponent();
            Obtener();
            ComBoxFlat = true;
        }
        #endregion

        #region Métodos
        public async void Obtener()
        {
            if (File.Exists("ProductosNoAgregados.txt")) Informacion_IconPictureBox.Visible = true;
            if (ListaProductos_DataGridView != null) ListaProductos_DataGridView.Rows.Clear();
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
                Aniadir_Button.Enabled = false;
                MessageBox.Show("Ha ocurrido un error de conexión.\nIntente recargando la página.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var Tipos = await FB.ObtenerMarcasTipos("Tipos");
            var Marcas = await FB.ObtenerMarcasTipos("Marcas");
            foreach (var Tipo in Tipos) if (Tipo != null) Tipo_ComboBox.Items.Add(Tipo);
            foreach (var Marca in Marcas) if (Marca != null) Marca_ComboBox.Items.Add(Marca);
            var Productoss = await FB.ObtenerProductos("Inventario");
            foreach (var Cod in Productoss) Codigos.Add(Cod.Codigo.ToLower());
            if (Codigos.Count == 0) MessageBox.Show("No existe ningún producto en inventario.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Aniadir_Button.Enabled = true;
        }

        private void CambioValor(object sender, EventArgs e)
        {
            int ValorCompra = Convert.ToInt32(ValorSIVA_NumericUpDown.Value);
            if (ValorSinIVA_CheckBox.Checked)
            {
                Valor_Label.Text = "Precio sin\n    IVA: ";
                TValorCompra = Convert.ToInt32(ValorCompra + ValorCompra * IVA);
                TInversion = TValorCompra * Convert.ToInt32(Unidades_NumericUpDown.Value);
                ValorCompra_Label.Text = "$ " + TValorCompra.ToString("N0") + " COP";
                TValorVenta = Convert.ToInt32(TValorCompra + TValorCompra * GANANCIA);
                PrecioVenta_Label.Text = "$ " + TValorVenta.ToString("N0") + " COP";
                Inversion_Label.Text = "$ " + TInversion.ToString("N0") + " COP";
                TValorSinIVA = Convert.ToInt32(ValorSIVA_NumericUpDown.Value);
                PrecioCompra_Label.Text = "Precio\ncompra: ";
            }
            else
            {
                Valor_Label.Text = "Precio\ncompra:";
                TValorCompra = Convert.ToInt32(ValorCompra);
                TInversion = TValorCompra * Convert.ToInt32(Unidades_NumericUpDown.Value);
                ValorCompra_Label.Text = "$ " + TValorCompra.ToString("N0") + " COP";
                TValorVenta = Convert.ToInt32(TValorCompra + TValorCompra * GANANCIA);
                PrecioVenta_Label.Text = "$ " + TValorVenta.ToString("N0") + " COP";
                Inversion_Label.Text = "$ " + TInversion.ToString("N0") + " COP";
                TValorSinIVA = Convert.ToInt32(ValorCompra / (1 + IVA));
                PrecioCompra_Label.Text = "Precio sin\n    IVA: ";
                ValorCompra_Label.Text = "$ " + TValorSinIVA.ToString("N0") + " COP";
            }
        }

        private void ListaProductos_DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Producto Pro = Productos[e.RowIndex];
                DialogResult Dialogo = MessageBox.Show("¿Desea quitar el producto "
                        + Pro.Codigo + " - " + Pro.Nombre + " ?",
                       "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Dialogo == DialogResult.No) return;
                Total -= Pro.Inversion;
                if (Pro.Unidades == 0) ProductosSinUnid.Remove(Pro);
                Productos.RemoveAt(e.RowIndex);
                ListaProductos_DataGridView.Rows.RemoveAt(e.RowIndex);
            }
        }

        //#######SE UTILIZA CUANDO HAY UN ESPACIO QUE HACE FALTA LLENAR (CUADROS ROJOS)
        public void TextBox_Vacio(object sender, EventArgs e)
        {
            //#####Cambiar color de textBox cuando esta vacio
            //TextBox TB = (TextBox) sender;
            //if (string.IsNullOrEmpty(TB.Text))
            //{
            //    TB.BackColor = Color.FromArgb(((int)(((byte)(100)))), 0, 0);
            //    //sender = (object)TB;
            //}
            //if (string.IsNullOrEmpty(TB.Text))
            //{
            //    TB.BackColor = Color.FromArgb(((int)(((byte)(100)))), 0, 0);
            //    //sender = (object)TB;
            //}
        }
        //######################################################################

        private void Aniadir_Button_ClickAsync(object sender, EventArgs e)
        {
            Comprar_Button.Enabled = true;
            Cancelar_Button.Enabled = true;
            if (Codigos.Contains(Codigo_TextBox.Text.ToLower()))
            {
                MessageBox.Show("El código de producto " + "\"" + Codigo_TextBox.Text + "\"" + "\nya se encuentra en el inventario",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(Codigo_TextBox.Text)
                || string.IsNullOrEmpty(Nombre_TextBox.Text))
            {
                MessageBox.Show("Los espacios de código o nombre están vacios",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Tipo_ComboBox.Text == "Agregar ..." || Marca_ComboBox.Text == "Agregar ..."
                || string.IsNullOrEmpty(Tipo_ComboBox.Text) || string.IsNullOrEmpty(Marca_ComboBox.Text))
            {
                MessageBox.Show("Seleccione un tipo y/o una marca para el producto",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ValorSIVA_NumericUpDown.Value == 0)
            {
                MessageBox.Show("Indique el precio SIN IVA del producto.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Producto P = new Producto()
            {
                Codigo = Codigo_TextBox.Text,
                Nombre = Nombre_TextBox.Text,
                Nota = Anotaciones_TextBox.Text,
                Marca = Marca_ComboBox.Text,
                Tipo = Tipo_ComboBox.Text,
                PrecioAntesIVA = TValorSinIVA,
                Unidades = Convert.ToInt32(Unidades_NumericUpDown.Value),
                PrecioCompra = TValorCompra,
                Inversion = TInversion,
                PrecioVenta = TValorVenta,
                UnidadesVendidas = 0,
                Ganancia = 0
            };
            Total += TInversion;
            Total_Label.Text = "$" + Total.ToString("N0") + " COP";
            Productos.Add(P);
            if (P.Unidades == 0) ProductosSinUnid.Add(P);
            ListaProductos_DataGridView.Rows.Add(P.Codigo, P.Nombre, P.Unidades, P.Inversion.ToString("N0"));
            Reset(true);
            Informacion_IconPictureBox.Visible = false;
            ProductosDTXT = false;
        }

        private async void Comprar_Button_Click(object sender, EventArgs e)
        {
            if (ProductosSinUnid.Count != 0)
            {
                string PSUS = string.Empty;
                int t = 0;
                foreach (var PSU in ProductosSinUnid)
                {
                    try
                    {
                        if ((t % 2) == 0) PSUS += PSU.Nombre.Substring(0, 7) + "\t\t";
                        else PSUS += PSU.Nombre.Substring(0, 7) + "\n";
                    }
                    catch (Exception)
                    {
                        if ((t % 2) == 0) PSUS += PSU.Nombre + "\t\t";
                        else PSUS += PSU.Nombre + "\n";
                    }
                    t++;
                }
                DialogResult Men = MessageBox.Show("Los siguientes productos no tienen unidades:\n\n"
                                + PSUS
                                + "\n\n¿Desea agregarlos?",
                    "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Men == DialogResult.No) return;
            }

            string PS = string.Empty;
            int tt = 0;
            foreach (var P in Productos)
            {
                try
                {
                    if ((tt % 2) == 0) PS += P.Nombre.Substring(0, 7) + "\t\t";
                    else PS += P.Nombre.Substring(0, 7) + "\n";
                }
                catch (Exception)
                {
                    if ((tt % 2) == 0) PS += P.Nombre + "\t\t";
                    else PS += P.Nombre + "\n";
                }
                tt++;
            }
            DialogResult RR = MessageBox.Show("Lista de Productos:\n\n"
                + PS
                + "\n\n¿Desea agregarlos?",
                "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (RR == DialogResult.No) return;
            Animacion_Panel.Visible = true;
            Animacion_Timer.Enabled = true;
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
            Proveedor Proveedorr;
            if (string.IsNullOrEmpty(NombreProv_TextBox.Text) || string.IsNullOrEmpty(Contacto_TextBox.Text))
            {
                if (NombreProv_TextBox.Enabled)
                {
                    DialogResult Dialogo = MessageBox.Show("¿Quiere dejar los datos del proveedor en blanco?",
                    "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (Dialogo == DialogResult.No) return;
                    else
                    {
                        Proveedorr = new Proveedor()
                        {
                            Nombre = string.Empty,
                            Contacto = string.Empty
                        };
                        foreach (var Pro in Productos) if (Proveedorr != null) Pro.Origen = Proveedorr;
                    }
                }
                else
                {
                    Proveedorr = new Proveedor()
                    {
                        Nombre = string.Empty,
                        Contacto = string.Empty
                    };
                }
            }
            else
            {
                Proveedorr = new Proveedor()
                {
                    Nombre = NombreProv_TextBox.Text,
                    Contacto = Contacto_TextBox.Text
                };
                await FB.Guardar("Proveedores", Proveedorr);
                foreach (var Pro in Productos) if (Proveedorr != null) Pro.Origen = Proveedorr;
            }
            Comprar_Button.Enabled = false;
            Aniadir_Button.Enabled = false;
            Cancelar_Button.Enabled = false;
            Tipos = new List<string>();
            Marcas = new List<string>();
            foreach (var M in Marca_ComboBox.Items) if (M != null && !string.IsNullOrEmpty(M.ToString())) Marcas.Add(M.ToString());
            foreach (var T in Tipo_ComboBox.Items) if (T != null && !string.IsNullOrEmpty(T.ToString())) Tipos.Add(T.ToString());
            Tipos.RemoveAt(0);
            Marcas.RemoveAt(0);
            await FB.DeleteAllString("Tipos");
            await FB.DeleteAllString("Marcas");

            await FB.Guardar("Tipos", Tipos);
            await FB.Guardar("Marcas", Marcas);

            if (ProductosDTXT)
            {
                SEscribirTXT Txtr = new SEscribirTXT();
                Txtr.Eliminar("ProductosNoAgregados.txt");
            }
            Subiendo UP = new Subiendo(EmailT, ClaveT, Productos, Total, Proveedorr);
            DialogResult DG = UP.ShowDialog();
            if (DG == DialogResult.OK)
            {
                Total = 0;
                Reset(false);
                MessageBox.Show("Inventario agregado con exito.",
                       "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Animacion_Panel.Visible = false;
                Animacion_Timer.Enabled = false;
                return;
            }
            else
            {
                Reset(false);
                SEscribirTXT Txt = new SEscribirTXT();
                string TxtContenido = string.Empty;
                PS = string.Empty;
                foreach (var Pr in UP.ProductosNoEnviados)
                {
                    TxtContenido += Pr.Codigo + ";" +
                        Pr.Inversion + ";" +
                        Pr.Marca + ";" +
                        Pr.Nombre + ";" +
                        Pr.Nota + ";" +
                        Pr.Origen.Nombre + ";" +
                        Pr.Origen.Contacto + ";" +
                        Pr.PrecioAntesIVA + ";" +
                        Pr.PrecioCompra + ";" +
                        Pr.PrecioVenta + ";" +
                        Pr.Tipo + ";" +
                        Pr.Unidades + ";";
                    Txt.Escribir("ProductosNoAgregados.txt", TxtContenido);
                    Productos.Add(Pr);
                    Total += Pr.Inversion;
                    TxtContenido = string.Empty;
                    try
                    {
                        PS += Pr.Nombre.Substring(0, 18) + "\n";
                    }
                    catch (Exception)
                    {
                        PS += Pr.Nombre + "\n";
                    }
                }
                MessageBox.Show("Ha ocurrido un error de conexión, los siguientes\nproductos no fueron agregados:\n\n"
                + PS
                + "\nPor favor intente de nuevo.",
                "Error de econexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (File.Exists("ProductosNoAgregados.txt")) Informacion_IconPictureBox.Visible = true;
                Animacion_Panel.Visible = false;
                Animacion_Timer.Enabled = false;
            }
            Animacion_Panel.Visible = false;
            Animacion_Timer.Enabled = false;
        }

        private void Cancelar_Button_Click(object sender, EventArgs e)
        {
            Reset(false);
        }

        public void Reset(bool _aniadir)
        {
            Codigo_TextBox.Text = string.Empty;
            Nombre_TextBox.Text = string.Empty;
            Anotaciones_TextBox.Text = string.Empty;
            ValorSIVA_NumericUpDown.Value = 0;
            Unidades_NumericUpDown.Value = 0;
            ValorCompra_Label.Text = "$                            COP";
            Inversion_Label.Text = "$                            COP";
            if (!_aniadir)
            {
                if (File.Exists("ProductosNoAgregados.txt")) Informacion_IconPictureBox.Visible = true;
                ListaProductos_DataGridView.Rows.Clear();
                Total_Label.Text = "$                       COP";
                Comprar_Button.Enabled = false;
                Cancelar_Button.Enabled = false;
                ProductosSinUnid.Clear();
                Productos.Clear();
                Total = 0;
                TInversion = 0;
                TValorCompra = 0;
            }
            Marca_ComboBox.SelectedIndex = -1;
            Marca_ComboBox.Text = string.Empty;
            Tipo_ComboBox.SelectedIndex = -1;
            Tipo_ComboBox.Text = string.Empty;
            Aniadir_Button.Enabled = true;
            NombreProv_TextBox.Enabled = true;
            Contacto_TextBox.Enabled = true;
            Nombre_TextBox.Enabled = true;
            Codigo_TextBox.Enabled = true;
            Marca_ComboBox.Enabled = true;
            Tipo_ComboBox.Enabled = true;
            Cancelar_Button.Enabled = true;
        }

        private void ComboBox_TextChanged(object sender, EventArgs e)
        {
            string _label = "Marca";
            if (Marca_ComboBox.Text == "Agregar ...")
            {
                AgregarCambiar AC = new AgregarCambiar(_label);
                DialogResult DG = AC.ShowDialog();
                if (DG == DialogResult.Cancel)
                {
                    ComBoxFlat = false;
                    Marca_ComboBox.SelectedIndex = -1;
                    return;
                }
                else
                {
                    List<string> mm = new List<string>();
                    foreach (var M in Marca_ComboBox.Items) mm.Add(M.ToString().ToLower());
                    if (mm.Contains(AC.Valor.ToLower()))
                    {
                        MessageBox.Show("El tipo \"" + AC.Valor + "\" ya existe, agregue\nuno nuevo o seleccione uno existente.",
                            "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Marca_ComboBox.SelectedIndex = -1;
                        return;
                    }
                    string Mensaje = string.Empty;
                    Mensaje = "Se ha agregado una nueva marca.";
                    Marca_ComboBox.Items.Add(AC.Valor);
                    ComBoxFlat = false;
                    Marca_ComboBox.SelectedIndex = -1;
                    //Marca_ComboBox.Text = AC.Valor;
                    MessageBox.Show(Mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else { }
        }

        private void ComboBox_TextChangedTIpo(object sender, EventArgs e)
        {
            string _label = "Tipo";
            if (Tipo_ComboBox.Text == "Agregar ...")
            {
                AgregarCambiar AC = new AgregarCambiar(_label);
                DialogResult DG = AC.ShowDialog();
                if (DG == DialogResult.Cancel)
                {
                    ComBoxFlat = false;
                    Tipo_ComboBox.SelectedIndex = -1;
                    return;
                }
                else
                {
                    List<string> mm = new List<string>();
                    foreach (var M in Tipo_ComboBox.Items) mm.Add(M.ToString().ToLower());
                    if (mm.Contains(AC.Valor.ToLower()))
                    {
                        MessageBox.Show("El tipo \"" + AC.Valor + "\" ya existe, agregue\nuno nuevo o seleccione uno existente.",
                            "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Tipo_ComboBox.SelectedIndex = -1;
                        return;
                    }
                    string Mensaje = string.Empty;
                    Mensaje = "Se ha agregado un nuevo tipo.";
                    Tipo_ComboBox.Items.Add(AC.Valor);
                    ComBoxFlat = false;
                    Tipo_ComboBox.SelectedIndex = -1;
                    MessageBox.Show(Mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else { }
        }

        private void Informacion_IconPictureBox_Click(object sender, EventArgs e)
        {
            DialogResult Dialogo = MessageBox.Show("Existen productos que no se han subido\n" +
                   "¿Desea agregar estos productos?",
                   "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Dialogo == DialogResult.No) return;
            else
            {
                List<string> _marca = new List<string>();
                List<string> _tipo = new List<string>();
                foreach (var M in Marca_ComboBox.Items) _marca.Add(M.ToString().ToLower());
                foreach (var M in Tipo_ComboBox.Items) _tipo.Add(M.ToString().ToLower());

                Aniadir_Button.Enabled = false;
                NombreProv_TextBox.Enabled = false;
                Contacto_TextBox.Enabled = false;
                Nombre_TextBox.Enabled = false;
                Codigo_TextBox.Enabled = false;
                Marca_ComboBox.Enabled = false;
                Tipo_ComboBox.Enabled = false;

                Comprar_Button.Enabled = true;
                Cancelar_Button.Enabled = true;
                SEscribirTXT Txt = new SEscribirTXT();
                var TxtContenido = Txt.Leer("ProductosNoAgregados.txt");
                string[] Contenido = TxtContenido.Split('?');
                string MensajeT = string.Empty;
                string MensajeM = string.Empty;
                string MensajeP = string.Empty;
                int t = 0;
                foreach (var C in Contenido)
                {
                    string[] Pro = C.Split(';');
                    if (Pro.Length <= 10) break;
                    Proveedor Prov = new Proveedor()
                    {
                        Nombre = Pro[5],
                        Contacto = Pro[6]
                    };
                    string CodigoP = string.Empty;
                    if (C == Contenido[0]) CodigoP = Pro[0];
                    else CodigoP = Pro[0].Remove(0, 2);

                    if (!Codigos.Contains(CodigoP.ToLower()))
                    {
                        Producto P = new Producto()
                        {
                            Codigo = CodigoP,
                            Inversion = Convert.ToInt32(Pro[1]),
                            Marca = Pro[2],
                            Nombre = Pro[3],
                            Nota = Pro[4],
                            Origen = Prov,
                            PrecioAntesIVA = Convert.ToInt32(Pro[7]),
                            PrecioCompra = Convert.ToInt32(Pro[8]),
                            PrecioVenta = Convert.ToInt32(Pro[9]),
                            Tipo = Pro[10],
                            Unidades = Convert.ToInt32(Pro[11])
                        };

                        Productos.Add(P);
                        Total += Convert.ToInt32(Pro[2]);
                        if (P.Unidades == 0) ProductosSinUnid.Add(P);
                        ListaProductos_DataGridView.Rows.Add(P.Codigo, P.Nombre, P.Unidades, P.Inversion);
                        if (!_marca.Contains(Pro[2].ToLower()))
                        {
                            Marca_ComboBox.Items.Add(Pro[2]);
                            MensajeM += Pro[2] + "\n";
                        }
                        if (!_tipo.Contains(Pro[10].ToLower()))
                        {
                            Tipo_ComboBox.Items.Add(Pro[10]);
                            MensajeT += Pro[10] + "\n";
                        }
                    }
                    else
                    {
                        if ((t % 2) == 0)
                        {
                            if (CodigoP.Length >= 8) MensajeP += CodigoP + "\n";
                            else MensajeP += CodigoP + "\t\t";
                        }
                        else MensajeP += CodigoP + "\n";
                        t++;
                    }
                }

                if (!string.IsNullOrEmpty(MensajeT))
                {
                    MessageBox.Show("Se han agregado los siguientes tipos:\n\n" + MensajeT, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (!string.IsNullOrEmpty(MensajeM))
                {
                    MessageBox.Show("Se han agregado las siguientes marcas:\n\n" + MensajeM, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (!string.IsNullOrEmpty(MensajeP))
                {
                    SEscribirTXT Txtr = new SEscribirTXT();
                    Txtr.Eliminar("ProductosNoAgregados.txt");
                    string TxtrContenido = string.Empty;
                    foreach (var Pr in Productos)
                    {
                        TxtContenido += Pr.Codigo + ";" +
                         Pr.Inversion + ";" +
                         Pr.Marca + ";" +
                         Pr.Nombre + ";" +
                         Pr.Nota + ";" +
                         Pr.Origen.Nombre + ";" +
                         Pr.Origen.Contacto + ";" +
                         Pr.PrecioAntesIVA + ";" +
                         Pr.PrecioCompra + ";" +
                         Pr.PrecioVenta + ";" +
                         Pr.Tipo + ";" +
                         Pr.Unidades + ";";
                        Txt.Escribir("ProductosNoAgregados.txt", TxtContenido);
                    }
                    MessageBox.Show("Los siguientes productos NO han sido agregados\n"
                              + "porque ya existen en el inventario:\n\n" + MensajeP, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Aniadir_Button.Enabled = true;
                    NombreProv_TextBox.Enabled = true;
                    Contacto_TextBox.Enabled = true;
                    Nombre_TextBox.Enabled = true;
                    Codigo_TextBox.Enabled = true;
                    Marca_ComboBox.Enabled = true;
                    Tipo_ComboBox.Enabled = true;
                    Informacion_IconPictureBox.Visible = false;
                    return;
                }
                Total_Label.Text = "$" + Total + " COP";
                Informacion_IconPictureBox.Visible = false;
                ProductosDTXT = true;
            }
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
