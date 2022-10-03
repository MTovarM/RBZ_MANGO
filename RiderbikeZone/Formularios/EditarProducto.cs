namespace RiderbikeZone.Formularios
{
    using RiderbikeZone.Modelos;
    using RiderbikeZone.Servicios;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public partial class EditarProducto : Form
    {
        #region Propiedades
        public List<int> ValoresAnteriores { get; set; }
        public bool CambiarNumeric { get; set; }
        public bool IsValorCompra { get; set; }
        public int veces { get; set; }
        public Producto Producto { get; set; }
        public string EmailT { get; set; }
        public string ClaveT { get; set; }
        public int UnidadesExistentes { get; set; }
        public int InversionExistente { get; set; }
        public List<string> Marcas { get; set; }
        public SEscribirTXT TXT { get; set; }
        public List<string> Tipos { get; set; }
        public int NuevaInversion { get; set; }
        public int PrecioCompra { get; set; }
        public int PrecioSug { get; set; }
        public double IVA { get; set; }
        public double GANANCIA { get; set; }
        public int ValorSinIVA { get; set; }
        public int ANI { get; set; }
        #endregion

        #region Constructor
        public EditarProducto(Producto _producto, int _unidades,string _email, string _clave, List<string> _tipos, List<string> _marcas, int _inversion)
        {
            InitializeComponent();
            ValoresAnteriores = new List<int>();
            for (int yy = 0; yy <= 5; yy++) ValoresAnteriores.Add(0);
            CambiarNumeric = false;
            IsValorCompra = true;
            veces = 0;
            ANI = 0;
            Producto = _producto;
            Tipos = _tipos;
            InversionExistente = _inversion;
            UnidadesExistentes = _unidades;
            PrecioCompra = Producto.PrecioCompra;
            PrecioSug = Producto.PrecioVenta;
            Marcas = _marcas;
            TXT = new SEscribirTXT();
            GANANCIA = Convert.ToDouble(TXT.Leer("GANANCIA.txt"));
            IVA = Convert.ToDouble(TXT.Leer("IVA.txt"));
            NuevaInversion = 0;
            EmailT = _email;
            ClaveT = _clave;

            ToolTip.SetToolTip(Unidades_NumericUpDown, "Unidades actuales en stock");
            ToolTip.SetToolTip(ValorCompra_NumericUpDown, "Precio con IVA de " + IVA * 100 + "%");
            ToolTip.SetToolTip(Inversion_NumericUpDown, "Valor acumulado de TODAS las unidades compradas");
            ToolTip.SetToolTip(PrecioSugerido_Label, "Precio sugerido con ganancia de " + GANANCIA * 100 + "%");
            if (Producto.Ganancia < 0) Producto.Ganancia = -Producto.Ganancia; 
            Ganancia_NumericUpDown.Value = Producto.Ganancia;
            Codigo_Label.Text = Producto.Codigo;
            Nombre_TextBox.Text = Producto.Nombre;
            Anotaciones_TextBox.Text = Producto.Nota;
            Unidades_NumericUpDown.Value = Producto.Unidades;
            //Unidades_NumericUpDown.Minimum = Producto.Unidades;
            UnidadesVendidas_NumericUpDown.Value = Producto.UnidadesVendidas;
            PrecioVenta_NumericUpDown.Value = Producto.PrecioVenta;
            ValorSinIVA_NumericUpDown.Value = Producto.PrecioAntesIVA;
            ValorCompra_NumericUpDown.Value = Producto.PrecioCompra;
            Inversion_NumericUpDown.Value = Producto.Inversion;
            PrecioSugerido_Label.Text = "Valor sugerido: $ " + PrecioSug.ToString("N0") + " COP";
            NombreProv_TextBox.Text = Producto.Origen.Nombre;
            Contacto_TextBox.Text = Producto.Origen.Contacto;
            foreach (var Tipo in Tipos) Tipo_ComboBox.Items.Add(Tipo);
            foreach (var Marca in Marcas) Marca_ComboBox.Items.Add(Marca);
            Tipo_ComboBox.Text = Producto.Tipo;
            Marca_ComboBox.Text = Producto.Marca;
            Listo_Button.Enabled = false;
        }
        #endregion

        #region Métodos
        private async void Listo_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (PrecioVenta_NumericUpDown.Value == 0)
                {
                    MessageBox.Show("Ingrese un precio de venta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                this.Producto.Ganancia = Convert.ToInt32(Ganancia_NumericUpDown.Value);
                this.Producto.PrecioCompra = Convert.ToInt32(ValorCompra_NumericUpDown.Value);
                this.Producto.UnidadesVendidas = Convert.ToInt32(UnidadesVendidas_NumericUpDown.Value);
                this.Producto.Inversion = Convert.ToInt32(Inversion_NumericUpDown.Value);
                this.Producto.Unidades = Convert.ToInt32(Unidades_NumericUpDown.Value);
                this.Producto.PrecioVenta = Convert.ToInt32(PrecioVenta_NumericUpDown.Value);
                this.Producto.Tipo = Tipo_ComboBox.Text;
                this.Producto.Marca = Marca_ComboBox.Text;
                this.Producto.Nombre = Nombre_TextBox.Text;
                this.Producto.Nota = Anotaciones_TextBox.Text;
                this.Producto.PrecioCompra = PrecioCompra;
                this.Producto.PrecioAntesIVA = ValorSinIVA;

                Proveedor Pro = new Proveedor()
                {
                    Nombre = NombreProv_TextBox.Text,
                    Contacto = Contacto_TextBox.Text
                };
                Producto.Origen = Pro;
            }
            catch (Exception)
            {
                MessageBox.Show("Ingrese valores válidos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                DialogResult D = MessageBox.Show("¿Esta seguro que quiere modificar el producto?",
                    "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (D == DialogResult.Yes)
                {
                    Animacion_Panel.Visible = true;
                    Animacion_Timer.Enabled = true;
                    Listo_Button.Enabled = false;
                    SFirebase FB = new SFirebase();
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
                    await FB.BorrarProducto(Producto.Codigo);
                    await FB.Guardar("Inventario", Producto);

                    DateTime _fecha = DateTime.Now;
                    List<string> Mes = new List<string>()
                    { "ene","feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic" };
                    string Fechas = _fecha.Day + "/" + Mes[_fecha.Month - 1] + "/" + _fecha.Year;
                    Compra c = new Compra()
                    {
                        Fecha = Fechas,
                        ListaProductos = new List<Producto>() { Producto },
                        Proveedor = new Proveedor() { Nombre = "Se añadieron unidades", Contacto = " "},
                        Total = NuevaInversion
                    };
                    await FB.Guardar("Compras", c);
                }
                else return;
            }
            catch (Exception)
            {
                Animacion_Panel.Visible = false;
                Animacion_Timer.Enabled = false;
                MessageBox.Show("No se ha podido modificar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Producto actualizado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void Volver_IconPictureBox_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
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

        private void ValoresModificados(object sender, EventArgs e)
        {
            /*
             *******************************************
             *MIRAR LOS EVENTOS PARA QUE SOLO CAMBIE CUANDO INTERACTUA EL HUMANO* 
             * */

            Listo_Button.Enabled = true;
            if (veces > 6)// && CambioValor())
            {
                var t2 = sender as NumericUpDown;
                if (t2 == null) return;
                var n = t2.Name;

                PrecioCompra = Convert.ToInt32(ValorCompra_NumericUpDown.Value);
                ValorSinIVA_NumericUpDown.Value = Convert.ToInt32(PrecioCompra/( 1 + IVA));
                if (n == "ValorSinIVA_NumericUpDown" || n == "ValorCompra_NumericUpDown") 
                {
                    PrecioVenta_NumericUpDown.Value = Convert.ToInt32(PrecioCompra + PrecioCompra * GANANCIA);
                    PrecioSug = Convert.ToInt32(PrecioCompra + PrecioCompra * GANANCIA);
                    PrecioSugerido_Label.Text = "Valor sugerido: $" + PrecioSug.ToString("N0") + " COP";
                }
                if (UnidadesExistentes != UnidadesVendidas_NumericUpDown.Value)
                {
                    int unidadesAniadidas = Convert.ToInt32(Unidades_NumericUpDown.Value - UnidadesExistentes);
                    NuevaInversion_Label.Text = "Nueva inversión: $" + (ValorCompra_NumericUpDown.Value * unidadesAniadidas).ToString("N0") + " COP";
                    Inversion_NumericUpDown.Value = InversionExistente + (ValorCompra_NumericUpDown.Value*unidadesAniadidas);
                }
            }
            ////if (veces > 6)// && CambioValor())
            ////{
            ////    var t2 = sender as NumericUpDown;
            ////    if (t2 == null) return;
            ////    var n = t2.Name;
            ////    if (n == "ValorCompra_NumericUpDown") IsValorCompra = true;
            ////    else if (n == "ValorSinIVA_NumericUpDown") IsValorCompra = false;
            ////    int _valorSinIVA;
            ////    _valorSinIVA = Convert.ToInt32(ValorSinIVA_NumericUpDown.Value);
            ////    //PrecioCompra = Convert.ToInt32(_valorSinIVA + _valorSinIVA * IVA);
            ////    if (IsValorCompra && CambiarNumeric)
            ////    {
            ////        int valor_compra = Convert.ToInt32(ValorCompra_NumericUpDown.Value);
            ////        var valor_compra_sIVA = Convert.ToInt32(ValorCompra_NumericUpDown.Value) * IVA;
            ////        _valorSinIVA = valor_compra - Convert.ToInt32(valor_compra_sIVA);
            ////        PrecioCompra = Convert.ToInt32(ValorCompra_NumericUpDown.Value);
            ////        PrecioVenta_NumericUpDown.Value = Convert.ToInt32(PrecioCompra + PrecioCompra * GANANCIA);
            ////        CambiarNumeric = false;
            ////        ValorSinIVA_NumericUpDown.Value = _valorSinIVA;
            ////    }
            ////    else if (!IsValorCompra && CambiarNumeric)
            ////    {
            ////        _valorSinIVA = Convert.ToInt32(ValorSinIVA_NumericUpDown.Value);
            ////        PrecioCompra = Convert.ToInt32(_valorSinIVA + _valorSinIVA * IVA);
            ////        PrecioVenta_NumericUpDown.Value = Convert.ToInt32(PrecioCompra + PrecioCompra * GANANCIA); ;
            ////        CambiarNumeric = false;
            ////        ValorCompra_NumericUpDown.Value = PrecioCompra;
            ////    }


            ////    //NuevaInversion = (Convert.ToInt32(Unidades_NumericUpDown.Value - UnidadesExistentes)) * PrecioCompra;
            ////    NuevaInversion = (Convert.ToInt32(Unidades_NumericUpDown.Value - UnidadesExistentes)) * Convert.ToInt32(ValorCompra_NumericUpDown.Value);
            ////    Producto.Inversion = InversionExistente + NuevaInversion;
            ////    PrecioSug = Convert.ToInt32(PrecioCompra + PrecioCompra * GANANCIA);
            ////    Inversion_NumericUpDown.Value = Producto.Inversion;
            ////    PrecioSugerido_Label.Text = "Valor sugerido: $" + PrecioSug.ToString("N0") + " COP";
            ////    NuevaInversion_Label.Text = "Nueva inversión: $" + NuevaInversion.ToString("N0") + " COP";
            ////    ValorSinIVA = _valorSinIVA;
            ////    //ValoresAnteriores[0] = Convert.ToInt32( Unidades_NumericUpDown.Value);
            ////    //ValoresAnteriores[1] = Convert.ToInt32( UnidadesVendidas_NumericUpDown);
            ////    //ValoresAnteriores[2] = Convert.ToInt32( ValorSinIVA_NumericUpDown.Value);
            ////    //ValoresAnteriores[3] = Convert.ToInt32( ValorCompra_NumericUpDown.Value);
            ////    //ValoresAnteriores[4] = Convert.ToInt32( PrecioVenta_NumericUpDown.Value);
            ////    //ValoresAnteriores[5] = Convert.ToInt32( Inversion_NumericUpDown.Value);
            ////}
            //var y = e;
            veces++;
        }

        public bool CambioValor()
        {
            if (ValoresAnteriores.Count > 0)
            {
                if (ValoresAnteriores[0] == Unidades_NumericUpDown.Value) return false;
                if (ValoresAnteriores[1] == UnidadesVendidas_NumericUpDown.Value) return false;
                if (ValoresAnteriores[2] == ValorSinIVA_NumericUpDown.Value) return false;
                if (ValoresAnteriores[3] == ValorCompra_NumericUpDown.Value) return false;
                if (ValoresAnteriores[4] == PrecioVenta_NumericUpDown.Value) return false;
                if (ValoresAnteriores[5] == Inversion_NumericUpDown.Value) return false;
            }
            return true;
        }

        private void PrecioSugerido_Label_DoubleClick(object sender, EventArgs e)
        {
            PrecioVenta_NumericUpDown.Value = PrecioSug;
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

        private void Focus_control(object sender, EventArgs e)
        {
            CambiarNumeric = true;
        }
        #endregion

        private void Focus_controlK(object sender, KeyEventArgs e)
        {
            CambiarNumeric = true;
        }

        private void Focus_controlM(object sender, MouseEventArgs e)
        {
            CambiarNumeric = true;
        }
    }
}
