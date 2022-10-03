namespace RiderbikeZone.Formularios
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public partial class AgregarCambiar : Form
    {
        ///######COSAS QUE HACEN FALTA
        /// 1. Colocar "el cuadro en rojo" cuando este vacio
        /// 

        #region Propiedades
        public string Valor { get; set; }
        #endregion

        #region Constructor
        public AgregarCambiar(string _label)
        {
            InitializeComponent();
            Label.Text = _label + ":  ";
        }

        #endregion

        #region Métodos
        private void Listo_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox.Text))
            {
                MessageBox.Show("No deje espacios en blanco",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Valor = TextBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Volver_IconPictureBox_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void PanelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion
    }
}
