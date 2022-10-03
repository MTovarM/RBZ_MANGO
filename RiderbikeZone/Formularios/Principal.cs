namespace RiderbikeZone
{
    using FontAwesome.Sharp;
    using RiderbikeZone.Formularios;
    using RiderbikeZone.Servicios;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public partial class FormPrincipal : Form
    {
        #region Properties
        private IconButton Actual_Button { get; set; }
        private Panel Animacion_Panel { get; set; }
        private Form ChildForm { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public bool IsReady { get; set; }
        public int ANI { get; private set; }
        public List<Color> Colores { get; set; }
        #endregion

        #region Constructor
        public FormPrincipal()
        {
            ANI = 0;
            InitializeComponent();
            DefinirColores();
            SEscribirTXT TXT = new SEscribirTXT();
            if (File.Exists("EMAIL.txt"))
            {
                Email = TXT.Leer("EMAIL.txt");
                Email_TextBox.Text = Email;
            }
            if (File.Exists("CLAVE.txt"))
            {
                Clave = TXT.Leer("CLAVE.txt");
                Clave_TextBox.Text = Clave;
            }
            DeshabilitarSubmenus();
            Animacion_Panel = new Panel();
            Animacion_Panel.Size = new Size(7, 55);
            Menu_Panel.Controls.Add(Animacion_Panel);
            string DirectoriosRuta = @"C:\MANGO";
            if (!Directory.Exists(DirectoriosRuta))
            {
                DirectoryInfo D1 = Directory.CreateDirectory(DirectoriosRuta);
                DirectoryInfo D3 = Directory.CreateDirectory(DirectoriosRuta + @"\BackUp");
                DirectoryInfo D4 = Directory.CreateDirectory(DirectoriosRuta + @"\Archivos");
                DirectoryInfo D5 = Directory.CreateDirectory(DirectoriosRuta + @"\Archivos\Cotizaciones");
                DirectoryInfo D6 = Directory.CreateDirectory(DirectoriosRuta + @"\Archivos\Ventas");
                DirectoryInfo D7 = Directory.CreateDirectory(DirectoriosRuta + @"\Archivos\Adjuntos");
            }
            ModificarMenu();
            IsReady = false;
        }
        #endregion

        #region Métodos
        private void DefinirColores()
        {
            //35, 175, 126
            Colores = new List<Color>();
            //NARANJA
            Colores.Add(Color.FromArgb(227, 109, 52));//#E36D34
            //AMARILLO
            Colores.Add(Color.FromArgb(242, 193, 73));//#F2C149
            //AZUL
            Colores.Add(Color.FromArgb(0, 167, 180));//#00A7B4
            //BLANCO
            Colores.Add(Color.FromArgb(250, 250, 241));//#FAFAF1

        }

        private void OpenChildForm(Form _childForm)
        {
            if (ChildForm != null)
            {
                ChildForm.Close();
            }
            ChildForm = _childForm;
            _childForm.TopLevel = false;
            _childForm.FormBorderStyle = FormBorderStyle.None;
            _childForm.Dock = DockStyle.Fill;
            this.Controls.Add(_childForm);
            this.Tag = _childForm;
            _childForm.BringToFront();
            _childForm.Show();
            Titulo_Label.Text = _childForm.Text;
            Menu_Panel.Enabled = false;
            Animacion1.Enabled = true;
        }

        public void DeshabilitarSubmenus()
        {
            ComprarVenderPanel.Visible = false;
        }

        public void MostrarSubmenu_Animacion(Panel _panel)
        {
            if (_panel.Visible == false)
            {
                OcultarSubmenu_Animacion();
                _panel.Visible = true;
            }
            else _panel.Visible = false;

        }

        public void OcultarSubmenu_Animacion()
        {
            if (ComprarVenderPanel.Visible == true) ComprarVenderPanel.Visible = false;
        }

        private void ActivarButton(object _senderBtn, Color _color)
        {
            if (_senderBtn != null)
            {
                DeshabilitarButton();
                Actual_Button = (IconButton)_senderBtn;
                Actual_Button.BackColor = Color.FromArgb(80, 80, 80);
                Actual_Button.ForeColor = _color;
                Actual_Button.TextAlign = ContentAlignment.MiddleCenter;
                Actual_Button.IconColor = _color;
                Actual_Button.TextImageRelation = TextImageRelation.TextBeforeImage;
                Actual_Button.ImageAlign = ContentAlignment.MiddleRight;
                //Left border button
                Animacion_Panel.BackColor = _color;
                Animacion_Panel.Location = new Point(0, Actual_Button.Location.Y);
                Animacion_Panel.Visible = true;
                Animacion_Panel.BringToFront();
                //Animacion pestaña seleccionada
                IconoEncabezado_PictureBox.IconChar = Actual_Button.IconChar;
                IconoEncabezado_PictureBox.IconColor = _color;
                Titulo_Label.Text = Actual_Button.Text;
            }
        }

        private void DeshabilitarButton()
        {
            if (Actual_Button != null)
            {
                Actual_Button.BackColor = Color.FromArgb(55, 55, 55);
                Actual_Button.ForeColor = Color.White;
                Actual_Button.TextAlign = ContentAlignment.MiddleCenter;
                Actual_Button.IconColor = Color.White;
                Actual_Button.TextImageRelation = TextImageRelation.ImageBeforeText;
                Actual_Button.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void ComprarVender_Button_Click(object sender, EventArgs e)
        {
            MostrarSubmenu_Animacion(ComprarVenderPanel);
            ActivarButton(sender, Colores[2]);
        }

        private void Comprar_Button_Click(object sender, EventArgs e)
        {
            if (File.Exists("GANANCIA.txt"))
            {
                OpenChildForm(new Comprar(Email, Clave));
            }
            else OpenChildForm(new Ajustes(true));
            ModificarMenu();
            OcultarSubmenu_Animacion();
        }

        private void Vender_Button_Click(object sender, EventArgs e)
        {
            if (File.Exists("GANANCIA.txt"))
            {
                OpenChildForm(new Vender(Email, Clave));
            }
            else OpenChildForm(new Ajustes(true));
            ModificarMenu();
            OcultarSubmenu_Animacion();
        }

        private void Inventario_Button_Click(object sender, EventArgs e)
        {
            if (File.Exists("GANANCIA.txt"))
            {
                ActivarButton(sender, Colores[2]);
                OcultarSubmenu_Animacion();
                OpenChildForm(new Inventario(Email, Clave));
            }
            else OpenChildForm(new Ajustes(true));
            ModificarMenu();
        }

        private void GenerarCotizacion_Button_Click(object sender, EventArgs e)
        {
            if (File.Exists("GANANCIA.txt"))
            {
                ActivarButton(sender, Colores[2]);
                OcultarSubmenu_Animacion();
                OpenChildForm(new Cotizacion(Email, Clave));
            }
            else OpenChildForm(new Ajustes(true));
            ModificarMenu();
        }

        private void Balance_Button_Click(object sender, EventArgs e)
        {
            if (File.Exists("GANANCIA.txt"))
            {
                ActivarButton(sender, Colores[2]);
                OcultarSubmenu_Animacion();
                OpenChildForm(new Balance(Email, Clave));
            }
            else OpenChildForm(new Ajustes(true));
            ModificarMenu();
        }

        private void Estadisticas_Button_Click(object sender, EventArgs e)
        {
            if (File.Exists("GANANCIA.txt"))
            {
                ActivarButton(sender, Colores[2]);
                OcultarSubmenu_Animacion();
                OpenChildForm(new Estadisticas(Email, Clave));
            }
            else OpenChildForm(new Ajustes(true));
            ModificarMenu();
        }

        private void GestorArchivos_Button_Click(object sender, EventArgs e)
        {
            if (File.Exists("GANANCIA.txt"))
            {
                ActivarButton(sender, Colores[2]);
                OcultarSubmenu_Animacion();
                OpenChildForm(new GestorArchivos(Email, Clave));
            }
            else OpenChildForm(new Ajustes(true));
            ModificarMenu();
        }

        private void ClientesProveedores_Button_Click(object sender, EventArgs e)
        {
            if (File.Exists("GANANCIA.txt"))
            {
                ActivarButton(sender, Colores[2]);
                OcultarSubmenu_Animacion();
                OpenChildForm(new ProveedoresClientes(Email, Clave));
            }
            else OpenChildForm(new Ajustes(true));
            ModificarMenu();
        }

        private void Ajustes_IconButton_Click(object sender, EventArgs e)
        {
            ActivarButton(sender, Colores[2]);
            OpenChildForm(new Ajustes(false));
            ModificarMenu();
        }

        private void Home_PictureBox_Click(object sender, EventArgs e)
        {
            DeshabilitarButton();
            Animacion_Panel.Visible = false;
            IconoEncabezado_PictureBox.IconChar = IconChar.Home;
            IconoEncabezado_PictureBox.IconColor = Colores[2];
            Titulo_Label.Text = "Inicio";
            this.Controls.Remove(ChildForm);
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

        private void Minimizar_IconPictureBox_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Cerrar_IconPictureBox_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("¿Desea cerrar Riderbike Zone?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (d == DialogResult.Yes) Application.Exit();
        }

        private void Minimizar_IconPictureBox_MouseLeave(object sender, EventArgs e)
        {
            Cerrar_IconPictureBox.BackColor = Color.FromArgb(30, 30, 30);
            Minimizar_IconPictureBox.BackColor = Color.FromArgb(30, 30, 30);
        }

        private void Minimizar_IconPictureBox_MouseEnter(object sender, EventArgs e)
        {
            Minimizar_IconPictureBox.BackColor = Color.FromArgb(70, 70, 70);
        }

        private void Cerrar_IconPictureBox_MouseEnter(object sender, EventArgs e)
        {
            Cerrar_IconPictureBox.BackColor = Colores[0];
        }

        private void Animacion1_Tick(object sender, EventArgs e)
        {
            //Menu_Panel.Enabled = true;
            Animacion1.Enabled = false;
        }

        private void Menu_IconPictureBox_Click(object sender, EventArgs e)
        {
            if (Menu_Panel.Width == 202) ModificarMenu();
            else RestaurarMenu();
        }

        public void RestaurarMenu()
        {
            Menu_Panel.Enabled = true;
            Menu_IconPictureBox.IconChar = IconChar.ArrowLeft;
            Home_PictureBox.Width = 181;
            ComprarVender_Button.Text = "Comprar/Vender";
            Inventario_Button.Text = "Inventario";
            GenerarCotizacion_Button.Text = "Generar\nCotización";
            Balance_Button.Text = "Balance";
            Estadisticas_Button.Text = "Estadísticas";
            GestorArchivos_Button.Text = "Gestor de\narchivos";
            Ajustes_IconButton.Text = "Ajustes";
            ClientesProveedores_Button.Text = "Clientes/\nProveedores";
            Menu_Panel.Width = 202;
        }

        public void ModificarMenu()
        {
            Menu_IconPictureBox.IconChar = IconChar.Bars;
            int _newWidth = 70;
            Home_PictureBox.Width = _newWidth - 10;
            ComprarVender_Button.Text = string.Empty;
            Inventario_Button.Text = string.Empty;
            GenerarCotizacion_Button.Text = string.Empty;
            Balance_Button.Text = string.Empty;
            Estadisticas_Button.Text = string.Empty;
            GestorArchivos_Button.Text = string.Empty;
            Ajustes_IconButton.Text = string.Empty;
            ClientesProveedores_Button.Text = string.Empty;
            Menu_Panel.Width = _newWidth;
            Menu_Panel.Enabled = false;
        }

        private async void Entrar_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Email_TextBox.Text))
            {
                MessageBox.Show("Ingrese un usuario.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(Clave_TextBox.Text))
            {
                MessageBox.Show("Ingrese una contraseña.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Animacion_Timer.Enabled = true;
            Animacion2_Panel.Visible = true;
            Email = Email_TextBox.Text;
            Clave = Clave_TextBox.Text;
            SFirebase FB = new SFirebase()
            {
                Email = this.Email,
                Clave = this.Clave
            };
            try
            {
                var t = await FB.LoginWithEmail(false);
                if (t == null) throw new Exception();
                SEscribirTXT TXT = new SEscribirTXT();
                if (Recordar_CheckBox.Checked)
                {
                    TXT.Escribirs("EMAIL.txt", Email);
                    TXT.Escribirs("CLAVE.txt", Clave);
                }
                else
                {
                    TXT.Eliminar("EMAIL.txt");
                    TXT.Eliminar("CLAVE.txt");
                }
                Login_Label.Visible = false;
                Login_Panel.Visible = false;
                Menu_IconPictureBox.Enabled = true;
                IconoEncabezado_PictureBox.Visible = true;
                Titulo_Label.Visible = true;
                TitleBar_Panel.Height = 78;
                Entrar_Button.Enabled = false;
                Registrar_IconButton.Enabled = false;
                Recordar_CheckBox.Enabled = false;
                Animacion_Timer.Enabled = false;
                Animacion2_Panel.Visible = false;
                Login_Label.Visible = false;
            }
            catch (Exception)
            {
                Clave = string.Empty;
                Clave_TextBox.Text = Clave;
                MessageBox.Show("El usuario y/o contraseña no se encuetran registrados.\nSi el error persiste cierre el programa.",
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Animacion_Timer.Enabled = false;
                Animacion2_Panel.Visible = false;
                return;
            }
        }

        private void Registrar_IconButton_Click(object sender, EventArgs e)
        {
            RegistrarUsuario RU = new RegistrarUsuario();
            RU.ShowDialog();
        }

        private void Animacion_Timer_Tick(object sender, EventArgs e)
        {
            int _cambio = -5;
            switch (ANI)
            {
                case 0:
                    CL13.Location = new Point(CL13.Location.X, CL13.Location.Y - _cambio);
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
                    ANI++;
                    break;
                case 11:
                    CL11.Location = new Point(CL11.Location.X, CL11.Location.Y - _cambio);
                    CL12.Location = new Point(CL12.Location.X, CL12.Location.Y + _cambio);
                    ANI++;
                    break;
                case 12:
                    CL12.Location = new Point(CL12.Location.X, CL12.Location.Y - _cambio);
                    CL13.Location = new Point(CL13.Location.X, CL13.Location.Y + _cambio);
                    ANI = 0;
                    break;
            }
        }

        private void Menu_IconPictureBox_MouseEnter(object sender, EventArgs e)
        {
            Menu_IconPictureBox.IconColor = Colores[2];

        }

        private void Menu_IconPictureBox_MouseLeave(object sender, EventArgs e)
        {
            Menu_IconPictureBox.IconColor = Color.White;
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void iconPictureBox1_MouseEnter(object sender, EventArgs e)
        {
            Maximizar_IconPictureBox.BackColor = Color.FromArgb(70, 70, 70);
        }

        private void iconPictureBox1_MouseLeave(object sender, EventArgs e)
        {
            Maximizar_IconPictureBox.BackColor = Color.FromArgb(30, 30, 30);
        }
        #endregion
    }
}
