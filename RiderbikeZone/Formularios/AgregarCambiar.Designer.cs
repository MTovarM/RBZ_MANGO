
namespace RiderbikeZone.Formularios
{
    partial class AgregarCambiar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregarCambiar));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Volver_IconPictureBox = new FontAwesome.Sharp.IconPictureBox();
            this.Label = new System.Windows.Forms.Label();
            this.Listo_Button = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Volver_IconPictureBox)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel1.Controls.Add(this.Volver_IconPictureBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(533, 27);
            this.panel1.TabIndex = 3;
            // 
            // Volver_IconPictureBox
            // 
            this.Volver_IconPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
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
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.Label.ForeColor = System.Drawing.Color.White;
            this.Label.Location = new System.Drawing.Point(0, 0);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(98, 20);
            this.Label.TabIndex = 4;
            this.Label.Text = "XXXXXXXXX: ";
            // 
            // Listo_Button
            // 
            this.Listo_Button.BackColor = System.Drawing.Color.Gray;
            this.Listo_Button.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Listo_Button.FlatAppearance.BorderSize = 0;
            this.Listo_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Listo_Button.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.Listo_Button.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Listo_Button.ForeColor = System.Drawing.Color.White;
            this.Listo_Button.IconChar = FontAwesome.Sharp.IconChar.None;
            this.Listo_Button.IconColor = System.Drawing.Color.White;
            this.Listo_Button.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Listo_Button.IconSize = 1;
            this.Listo_Button.Location = new System.Drawing.Point(215, 76);
            this.Listo_Button.Name = "Listo_Button";
            this.Listo_Button.Rotation = 0D;
            this.Listo_Button.Size = new System.Drawing.Size(108, 30);
            this.Listo_Button.TabIndex = 9;
            this.Listo_Button.Text = "   Listo";
            this.Listo_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Listo_Button.UseVisualStyleBackColor = false;
            this.Listo_Button.Click += new System.EventHandler(this.Listo_Button_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.TextBox);
            this.panel2.Controls.Add(this.Label);
            this.panel2.Location = new System.Drawing.Point(12, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(490, 28);
            this.panel2.TabIndex = 10;
            // 
            // TextBox
            // 
            this.TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.TextBox.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox.ForeColor = System.Drawing.Color.White;
            this.TextBox.Location = new System.Drawing.Point(98, 0);
            this.TextBox.Name = "TextBox";
            this.TextBox.Size = new System.Drawing.Size(389, 19);
            this.TextBox.TabIndex = 1;
            this.TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AgregarCambiar
            // 
            this.AcceptButton = this.Listo_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(533, 118);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Listo_Button);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AgregarCambiar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AgregarCambiar";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelTitleBar_MouseDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Volver_IconPictureBox)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconPictureBox Volver_IconPictureBox;
        private System.Windows.Forms.Label Label;
        private FontAwesome.Sharp.IconButton Listo_Button;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox TextBox;
    }
}