
namespace RiderbikeZone.Formularios
{
    partial class RegistrarUsuario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrarUsuario));
            this.Animacion_Timer = new System.Windows.Forms.Timer(this.components);
            this.Volver_IconPictureBox = new FontAwesome.Sharp.IconPictureBox();
            this.ConfirmarEmail_TextBox = new System.Windows.Forms.TextBox();
            this.Codigo_Label = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Email_TextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Clave_TextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ConfirmarClave_TextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Registrar_Button = new FontAwesome.Sharp.IconButton();
            this.Animacion2_Panel = new System.Windows.Forms.Panel();
            this.CL13 = new System.Windows.Forms.Label();
            this.CL12 = new System.Windows.Forms.Label();
            this.CL11 = new System.Windows.Forms.Label();
            this.CL10 = new System.Windows.Forms.Label();
            this.CL9 = new System.Windows.Forms.Label();
            this.CL8 = new System.Windows.Forms.Label();
            this.CL7 = new System.Windows.Forms.Label();
            this.CL2 = new System.Windows.Forms.Label();
            this.CL3 = new System.Windows.Forms.Label();
            this.CL4 = new System.Windows.Forms.Label();
            this.CL5 = new System.Windows.Forms.Label();
            this.CL6 = new System.Windows.Forms.Label();
            this.CL1 = new System.Windows.Forms.Label();
            this.CorreosMal_Label = new System.Windows.Forms.Label();
            this.ClavesMal_Label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Volver_IconPictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.Animacion2_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Animacion_Timer
            // 
            this.Animacion_Timer.Interval = 150;
            this.Animacion_Timer.Tick += new System.EventHandler(this.Animacion_Timer_Tick);
            // 
            // Volver_IconPictureBox
            // 
            this.Volver_IconPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Volver_IconPictureBox.IconChar = FontAwesome.Sharp.IconChar.ArrowLeft;
            this.Volver_IconPictureBox.IconColor = System.Drawing.Color.White;
            this.Volver_IconPictureBox.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Volver_IconPictureBox.IconSize = 27;
            this.Volver_IconPictureBox.Location = new System.Drawing.Point(0, 0);
            this.Volver_IconPictureBox.Name = "Volver_IconPictureBox";
            this.Volver_IconPictureBox.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.Volver_IconPictureBox.Size = new System.Drawing.Size(38, 27);
            this.Volver_IconPictureBox.TabIndex = 0;
            this.Volver_IconPictureBox.TabStop = false;
            this.Volver_IconPictureBox.Click += new System.EventHandler(this.Volver_IconPictureBox_Click);
            // 
            // ConfirmarEmail_TextBox
            // 
            this.ConfirmarEmail_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ConfirmarEmail_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConfirmarEmail_TextBox.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfirmarEmail_TextBox.ForeColor = System.Drawing.Color.White;
            this.ConfirmarEmail_TextBox.Location = new System.Drawing.Point(52, 188);
            this.ConfirmarEmail_TextBox.Name = "ConfirmarEmail_TextBox";
            this.ConfirmarEmail_TextBox.Size = new System.Drawing.Size(375, 19);
            this.ConfirmarEmail_TextBox.TabIndex = 2;
            this.ConfirmarEmail_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ConfirmarEmail_TextBox.TextChanged += new System.EventHandler(this.Email_TextBox_TextChanged);
            // 
            // Codigo_Label
            // 
            this.Codigo_Label.AutoSize = true;
            this.Codigo_Label.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Codigo_Label.ForeColor = System.Drawing.Color.White;
            this.Codigo_Label.Location = new System.Drawing.Point(102, 38);
            this.Codigo_Label.Name = "Codigo_Label";
            this.Codigo_Label.Size = new System.Drawing.Size(270, 38);
            this.Codigo_Label.TabIndex = 109;
            this.Codigo_Label.Text = "Registrar usuario";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panel1.Controls.Add(this.Volver_IconPictureBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(485, 27);
            this.panel1.TabIndex = 105;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelTitleBar_MouseDown);
            // 
            // Email_TextBox
            // 
            this.Email_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Email_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Email_TextBox.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Email_TextBox.ForeColor = System.Drawing.Color.White;
            this.Email_TextBox.Location = new System.Drawing.Point(52, 127);
            this.Email_TextBox.Name = "Email_TextBox";
            this.Email_TextBox.Size = new System.Drawing.Size(375, 19);
            this.Email_TextBox.TabIndex = 1;
            this.Email_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(212, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 20);
            this.label2.TabIndex = 106;
            this.label2.Text = "Email";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(180, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 20);
            this.label1.TabIndex = 124;
            this.label1.Text = "Confirmar email";
            // 
            // Clave_TextBox
            // 
            this.Clave_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Clave_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Clave_TextBox.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clave_TextBox.ForeColor = System.Drawing.Color.White;
            this.Clave_TextBox.Location = new System.Drawing.Point(52, 250);
            this.Clave_TextBox.Name = "Clave_TextBox";
            this.Clave_TextBox.Size = new System.Drawing.Size(375, 19);
            this.Clave_TextBox.TabIndex = 3;
            this.Clave_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(192, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 20);
            this.label4.TabIndex = 127;
            this.label4.Text = "Contraseña";
            // 
            // ConfirmarClave_TextBox
            // 
            this.ConfirmarClave_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ConfirmarClave_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConfirmarClave_TextBox.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfirmarClave_TextBox.ForeColor = System.Drawing.Color.White;
            this.ConfirmarClave_TextBox.Location = new System.Drawing.Point(52, 311);
            this.ConfirmarClave_TextBox.Name = "ConfirmarClave_TextBox";
            this.ConfirmarClave_TextBox.Size = new System.Drawing.Size(375, 19);
            this.ConfirmarClave_TextBox.TabIndex = 4;
            this.ConfirmarClave_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ConfirmarClave_TextBox.UseSystemPasswordChar = true;
            this.ConfirmarClave_TextBox.TextChanged += new System.EventHandler(this.Clave_TextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(158, 288);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 20);
            this.label3.TabIndex = 128;
            this.label3.Text = "Confirmar contraseña";
            // 
            // Registrar_Button
            // 
            this.Registrar_Button.BackColor = System.Drawing.Color.Gray;
            this.Registrar_Button.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Registrar_Button.FlatAppearance.BorderSize = 0;
            this.Registrar_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Registrar_Button.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.Registrar_Button.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Registrar_Button.ForeColor = System.Drawing.Color.White;
            this.Registrar_Button.IconChar = FontAwesome.Sharp.IconChar.None;
            this.Registrar_Button.IconColor = System.Drawing.Color.White;
            this.Registrar_Button.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Registrar_Button.IconSize = 1;
            this.Registrar_Button.Location = new System.Drawing.Point(119, 375);
            this.Registrar_Button.Name = "Registrar_Button";
            this.Registrar_Button.Rotation = 0D;
            this.Registrar_Button.Size = new System.Drawing.Size(242, 39);
            this.Registrar_Button.TabIndex = 5;
            this.Registrar_Button.Text = "    Registrar usuario";
            this.Registrar_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Registrar_Button.UseVisualStyleBackColor = false;
            this.Registrar_Button.Click += new System.EventHandler(this.Registrar_Button_Click);
            // 
            // Animacion2_Panel
            // 
            this.Animacion2_Panel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Animacion2_Panel.Controls.Add(this.CL13);
            this.Animacion2_Panel.Controls.Add(this.CL12);
            this.Animacion2_Panel.Controls.Add(this.CL11);
            this.Animacion2_Panel.Controls.Add(this.CL10);
            this.Animacion2_Panel.Controls.Add(this.CL9);
            this.Animacion2_Panel.Controls.Add(this.CL8);
            this.Animacion2_Panel.Controls.Add(this.CL7);
            this.Animacion2_Panel.Controls.Add(this.CL2);
            this.Animacion2_Panel.Controls.Add(this.CL3);
            this.Animacion2_Panel.Controls.Add(this.CL4);
            this.Animacion2_Panel.Controls.Add(this.CL5);
            this.Animacion2_Panel.Controls.Add(this.CL6);
            this.Animacion2_Panel.Controls.Add(this.CL1);
            this.Animacion2_Panel.Location = new System.Drawing.Point(52, 364);
            this.Animacion2_Panel.Name = "Animacion2_Panel";
            this.Animacion2_Panel.Size = new System.Drawing.Size(388, 61);
            this.Animacion2_Panel.TabIndex = 129;
            this.Animacion2_Panel.Visible = false;
            // 
            // CL13
            // 
            this.CL13.AutoSize = true;
            this.CL13.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CL13.ForeColor = System.Drawing.Color.White;
            this.CL13.Location = new System.Drawing.Point(325, 11);
            this.CL13.Name = "CL13";
            this.CL13.Size = new System.Drawing.Size(23, 32);
            this.CL13.TabIndex = 13;
            this.CL13.Text = ".";
            // 
            // CL12
            // 
            this.CL12.AutoSize = true;
            this.CL12.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CL12.ForeColor = System.Drawing.Color.White;
            this.CL12.Location = new System.Drawing.Point(311, 16);
            this.CL12.Name = "CL12";
            this.CL12.Size = new System.Drawing.Size(23, 32);
            this.CL12.TabIndex = 12;
            this.CL12.Text = ".";
            // 
            // CL11
            // 
            this.CL11.AutoSize = true;
            this.CL11.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CL11.ForeColor = System.Drawing.Color.White;
            this.CL11.Location = new System.Drawing.Point(282, 16);
            this.CL11.Name = "CL11";
            this.CL11.Size = new System.Drawing.Size(38, 32);
            this.CL11.TabIndex = 11;
            this.CL11.Text = "O";
            // 
            // CL10
            // 
            this.CL10.AutoSize = true;
            this.CL10.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CL10.ForeColor = System.Drawing.Color.White;
            this.CL10.Location = new System.Drawing.Point(258, 16);
            this.CL10.Name = "CL10";
            this.CL10.Size = new System.Drawing.Size(34, 32);
            this.CL10.TabIndex = 10;
            this.CL10.Text = "D";
            // 
            // CL9
            // 
            this.CL9.AutoSize = true;
            this.CL9.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CL9.ForeColor = System.Drawing.Color.White;
            this.CL9.Location = new System.Drawing.Point(231, 16);
            this.CL9.Name = "CL9";
            this.CL9.Size = new System.Drawing.Size(35, 32);
            this.CL9.TabIndex = 9;
            this.CL9.Text = "N";
            // 
            // CL8
            // 
            this.CL8.AutoSize = true;
            this.CL8.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CL8.ForeColor = System.Drawing.Color.White;
            this.CL8.Location = new System.Drawing.Point(204, 16);
            this.CL8.Name = "CL8";
            this.CL8.Size = new System.Drawing.Size(35, 32);
            this.CL8.TabIndex = 7;
            this.CL8.Text = "A";
            // 
            // CL7
            // 
            this.CL7.AutoSize = true;
            this.CL7.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CL7.ForeColor = System.Drawing.Color.White;
            this.CL7.Location = new System.Drawing.Point(180, 16);
            this.CL7.Name = "CL7";
            this.CL7.Size = new System.Drawing.Size(31, 32);
            this.CL7.TabIndex = 6;
            this.CL7.Text = "R";
            // 
            // CL2
            // 
            this.CL2.AutoSize = true;
            this.CL2.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CL2.ForeColor = System.Drawing.Color.White;
            this.CL2.Location = new System.Drawing.Point(68, 16);
            this.CL2.Name = "CL2";
            this.CL2.Size = new System.Drawing.Size(29, 32);
            this.CL2.TabIndex = 5;
            this.CL2.Text = "E";
            // 
            // CL3
            // 
            this.CL3.AutoSize = true;
            this.CL3.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CL3.ForeColor = System.Drawing.Color.White;
            this.CL3.Location = new System.Drawing.Point(90, 16);
            this.CL3.Name = "CL3";
            this.CL3.Size = new System.Drawing.Size(38, 32);
            this.CL3.TabIndex = 4;
            this.CL3.Text = "G";
            // 
            // CL4
            // 
            this.CL4.AutoSize = true;
            this.CL4.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CL4.ForeColor = System.Drawing.Color.White;
            this.CL4.Location = new System.Drawing.Point(122, 16);
            this.CL4.Name = "CL4";
            this.CL4.Size = new System.Drawing.Size(23, 32);
            this.CL4.TabIndex = 3;
            this.CL4.Text = "I";
            // 
            // CL5
            // 
            this.CL5.AutoSize = true;
            this.CL5.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CL5.ForeColor = System.Drawing.Color.White;
            this.CL5.Location = new System.Drawing.Point(139, 16);
            this.CL5.Name = "CL5";
            this.CL5.Size = new System.Drawing.Size(29, 32);
            this.CL5.TabIndex = 2;
            this.CL5.Text = "S";
            // 
            // CL6
            // 
            this.CL6.AutoSize = true;
            this.CL6.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CL6.ForeColor = System.Drawing.Color.White;
            this.CL6.Location = new System.Drawing.Point(162, 16);
            this.CL6.Name = "CL6";
            this.CL6.Size = new System.Drawing.Size(26, 32);
            this.CL6.TabIndex = 1;
            this.CL6.Text = "T";
            // 
            // CL1
            // 
            this.CL1.AutoSize = true;
            this.CL1.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CL1.ForeColor = System.Drawing.Color.White;
            this.CL1.Location = new System.Drawing.Point(43, 16);
            this.CL1.Name = "CL1";
            this.CL1.Size = new System.Drawing.Size(31, 32);
            this.CL1.TabIndex = 0;
            this.CL1.Text = "R";
            // 
            // CorreosMal_Label
            // 
            this.CorreosMal_Label.AutoSize = true;
            this.CorreosMal_Label.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CorreosMal_Label.ForeColor = System.Drawing.Color.Brown;
            this.CorreosMal_Label.Location = new System.Drawing.Point(171, 207);
            this.CorreosMal_Label.Name = "CorreosMal_Label";
            this.CorreosMal_Label.Size = new System.Drawing.Size(139, 16);
            this.CorreosMal_Label.TabIndex = 130;
            this.CorreosMal_Label.Text = "Los correos no coinciden";
            this.CorreosMal_Label.Visible = false;
            // 
            // ClavesMal_Label
            // 
            this.ClavesMal_Label.AutoSize = true;
            this.ClavesMal_Label.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClavesMal_Label.ForeColor = System.Drawing.Color.Brown;
            this.ClavesMal_Label.Location = new System.Drawing.Point(173, 333);
            this.ClavesMal_Label.Name = "ClavesMal_Label";
            this.ClavesMal_Label.Size = new System.Drawing.Size(138, 16);
            this.ClavesMal_Label.TabIndex = 131;
            this.ClavesMal_Label.Text = "Las claves no coinciden";
            this.ClavesMal_Label.Visible = false;
            // 
            // RegistrarUsuario
            // 
            this.AcceptButton = this.Registrar_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(485, 499);
            this.Controls.Add(this.ClavesMal_Label);
            this.Controls.Add(this.CorreosMal_Label);
            this.Controls.Add(this.Animacion2_Panel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ConfirmarClave_TextBox);
            this.Controls.Add(this.Clave_TextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConfirmarEmail_TextBox);
            this.Controls.Add(this.Codigo_Label);
            this.Controls.Add(this.Registrar_Button);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Email_TextBox);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegistrarUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RegistrarUsuario";
            ((System.ComponentModel.ISupportInitialize)(this.Volver_IconPictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.Animacion2_Panel.ResumeLayout(false);
            this.Animacion2_Panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer Animacion_Timer;
        private FontAwesome.Sharp.IconPictureBox Volver_IconPictureBox;
        private System.Windows.Forms.TextBox ConfirmarEmail_TextBox;
        private System.Windows.Forms.Label Codigo_Label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox Email_TextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Clave_TextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ConfirmarClave_TextBox;
        private System.Windows.Forms.Label label3;
        private FontAwesome.Sharp.IconButton Registrar_Button;
        private System.Windows.Forms.Panel Animacion2_Panel;
        private System.Windows.Forms.Label CL13;
        private System.Windows.Forms.Label CL12;
        private System.Windows.Forms.Label CL11;
        private System.Windows.Forms.Label CL10;
        private System.Windows.Forms.Label CL9;
        private System.Windows.Forms.Label CL8;
        private System.Windows.Forms.Label CL7;
        private System.Windows.Forms.Label CL2;
        private System.Windows.Forms.Label CL3;
        private System.Windows.Forms.Label CL4;
        private System.Windows.Forms.Label CL5;
        private System.Windows.Forms.Label CL6;
        private System.Windows.Forms.Label CL1;
        private System.Windows.Forms.Label CorreosMal_Label;
        private System.Windows.Forms.Label ClavesMal_Label;
    }
}