namespace RiderbikeZone.Formularios
{
    using RiderbikeZone.Servicios;
    using System;
    using System.IO;
    using System.Windows.Forms;

    public partial class Ajustes : Form
    {
        #region Propiedades
        public SEscribirTXT TXT { get; set; }
        public double GANANCIA { get; set; }
        public double IVA { get; set; }
        public int CONSECUTIVO { get; set; }
        public string Vendedor { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string AcEconomica { get; set; }
        public string AcEconomicaCheck { get; set; }
        public int MinStock { get; set; }
        public int MaxStock { get; set; }
        #endregion

        #region Contructor
        public Ajustes(bool _IsFirstTime)
        {
            GANANCIA = 1;
            IVA = 1;
            CONSECUTIVO = 1;
            MinStock = 3;
            MaxStock = 5;
            Vendedor = "Vendedor";
            Telefono = string.Empty;
            Correo = string.Empty;
            AcEconomicaCheck = "false";
            InitializeComponent();
            if (_IsFirstTime) PrimeraVez();
            AllTimes();
        }
        #endregion

        #region Métodos
        private void AllTimes()
        {
            Obtener();
        }

        private void Obtener()
        {
            TXT = new SEscribirTXT();
            var _ganancia = File.Exists("GANANCIA.txt") ? GANANCIA = (Convert.ToDouble(TXT.Leer("GANANCIA.txt")) * 100) : GANANCIA = 1;
            var _iva = File.Exists("IVA.txt") ? IVA = (Convert.ToDouble(TXT.Leer("IVA.txt")) * 100) : IVA = 1;
            var _minStock = File.Exists("MINSTOCK.txt") ? MinStock = Convert.ToInt32(TXT.Leer("MINSTOCK.txt")) : MinStock = 3;
            var _maxStock = File.Exists("MAXSTOCK.txt") ? MaxStock = Convert.ToInt32(TXT.Leer("MAXSTOCK.txt")) : MaxStock = 5;
            var _consecutivo = File.Exists("CONSECUTIVO.txt") ? CONSECUTIVO = Convert.ToInt32(TXT.Leer("CONSECUTIVO.txt")) : CONSECUTIVO = 1;
            var _vendedor = File.Exists("VENDEDOR.txt") ? Vendedor = TXT.Leer("VENDEDOR.txt") : Vendedor = "Vendedor";
            var _telefono = File.Exists("TELEFONO.txt") ? Telefono = TXT.Leer("TELEFONO.txt") : Telefono = string.Empty;
            var _correo = File.Exists("CORREO.txt") ? Correo = TXT.Leer("CORREO.txt") : Correo = string.Empty;
            var _acEconomica = File.Exists("ACECONOMICA.txt") ? AcEconomica = TXT.Leer("ACECONOMICA.txt") : AcEconomica = string.Empty;
            var _acEconomicaCheck = File.Exists("ACECONOMICAC.txt") ? AcEconomicaCheck = TXT.Leer("ACECONOMICAC.txt") : AcEconomicaCheck = "false";
            Ganancia_NumericUpDown.Value = Convert.ToInt32(GANANCIA);
            IVA_NumericUpDown.Value = Convert.ToInt32(IVA);
            MinStock_NumericUpDown.Value = MinStock;
            MaxStock_NumericUpDown.Value = MaxStock;
            Consecutivo_NumericUpDown.Value = CONSECUTIVO;
            NombreVendedor_TextBox.Text = Vendedor;
            Telefono_TextBox.Text = Telefono;
            Correo_TextBox.Text = Correo;
            ActividadEconomica_TextBox.Text = AcEconomica;
            ActividadEconomica_RadioButton.Checked = Convert.ToBoolean(AcEconomicaCheck);
        }

        private void PrimeraVez()
        {
            MessageBox.Show("Complete TODOS los espacios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void Listo_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(Ganancia_NumericUpDown.Value) == 1)
                {
                    MessageBox.Show("Ingrese el porcentaje de las ganancias.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (Convert.ToDouble(IVA_NumericUpDown.Value) == 1)
                {
                    MessageBox.Show("Ingrese el porcentaje del IVA.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (MinStock_NumericUpDown.Value > MaxStock_NumericUpDown.Value)
                {
                    MessageBox.Show("El stock mínimo debe ser menor o igual al stock máximo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                string _mensaje = "\nGanancia = \t\t" + Ganancia_NumericUpDown.Value + "%" +
                                  "\nIVA      = \t\t\t" + IVA_NumericUpDown.Value + "%" +
                                  "\nConsecutivo = \t\t" + Consecutivo_NumericUpDown.Value +
                                  "\nStock mínimo = \t\t" + MinStock_NumericUpDown.Value +
                                  "\nStock máximo = \t\t" + MaxStock_NumericUpDown.Value +
                                  "\nVendedor = \t\t" + NombreVendedor_TextBox.Text +
                                  "\nTeléfono = \t\t" + Telefono_TextBox.Text +
                                  "\nCorreo   = \t\t" + Correo_TextBox.Text;
                if (!ActividadEconomica_RadioButton.Checked) _mensaje += "\nActividad económica = \tDesactivada";
                else
                {
                    _mensaje += "\nActividad económica = \t\t" + ActividadEconomica_TextBox.Text;
                }

                DialogResult d = MessageBox.Show("¿Desea guardar los siguientes parámetros?.\n" + _mensaje, "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == DialogResult.Yes)
                {
                    var _ganancia = Convert.ToDouble(Ganancia_NumericUpDown.Value) / 100;
                    TXT.Escribirs("GANANCIA.txt", _ganancia.ToString());
                    var _iva = Convert.ToDouble(IVA_NumericUpDown.Value) / 100;
                    TXT.Escribirs("IVA.txt", _iva.ToString());
                    var _consecutivo = Convert.ToInt32(Consecutivo_NumericUpDown.Value);
                    TXT.Escribirs("CONSECUTIVO.txt", _consecutivo.ToString());
                    var _minStock = Convert.ToInt32(MinStock_NumericUpDown.Value);
                    TXT.Escribirs("MINSTOCK.txt", _minStock.ToString());
                    var _maxStock = Convert.ToInt32(MaxStock_NumericUpDown.Value);
                    TXT.Escribirs("MAXSTOCK.txt", _maxStock.ToString());
                    if (!string.IsNullOrEmpty(NombreVendedor_TextBox.Text)) TXT.Escribirs("VENDEDOR.txt", NombreVendedor_TextBox.Text);
                    else TXT.Escribirs("VENDEDOR.txt", "Vendedor");
                    TXT.Escribirs("TELEFONO.txt", Telefono_TextBox.Text);
                    TXT.Escribirs("CORREO.txt", Correo_TextBox.Text);
                    TXT.Escribirs("ACECONOMICA.txt", ActividadEconomica_TextBox.Text);
                    if (!ActividadEconomica_RadioButton.Checked) TXT.Escribirs("ACECONOMICAC.txt", "false");
                    else TXT.Escribirs("ACECONOMICAC.txt", "true");
                }
                else return;
                MessageBox.Show("Parámetros actualizados con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error, intenta de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ActividadEconomica_RadioButton_Click(object sender, EventArgs e)
        {
        }
        #endregion
    }
}
