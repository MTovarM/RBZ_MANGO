
namespace RiderbikeZone.Formularios
{
    partial class Subiendo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Subiendo));
            this.panel1 = new System.Windows.Forms.Panel();
            this.SubirProductos_ProgressBar = new System.Windows.Forms.ProgressBar();
            this.PorcentajePB_Label = new System.Windows.Forms.Label();
            this.SubiendoPB_Label = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(498, 5);
            this.panel1.TabIndex = 4;
            // 
            // SubirProductos_ProgressBar
            // 
            this.SubirProductos_ProgressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(175)))), ((int)(((byte)(126)))));
            this.SubirProductos_ProgressBar.Location = new System.Drawing.Point(29, 16);
            this.SubirProductos_ProgressBar.Name = "SubirProductos_ProgressBar";
            this.SubirProductos_ProgressBar.Size = new System.Drawing.Size(396, 37);
            this.SubirProductos_ProgressBar.Step = 100;
            this.SubirProductos_ProgressBar.TabIndex = 32;
            // 
            // PorcentajePB_Label
            // 
            this.PorcentajePB_Label.AutoSize = true;
            this.PorcentajePB_Label.Font = new System.Drawing.Font("Century Gothic", 13F);
            this.PorcentajePB_Label.ForeColor = System.Drawing.Color.White;
            this.PorcentajePB_Label.Location = new System.Drawing.Point(431, 25);
            this.PorcentajePB_Label.Name = "PorcentajePB_Label";
            this.PorcentajePB_Label.Size = new System.Drawing.Size(34, 21);
            this.PorcentajePB_Label.TabIndex = 34;
            this.PorcentajePB_Label.Text = "0%";
            // 
            // SubiendoPB_Label
            // 
            this.SubiendoPB_Label.AutoSize = true;
            this.SubiendoPB_Label.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.SubiendoPB_Label.ForeColor = System.Drawing.Color.White;
            this.SubiendoPB_Label.Location = new System.Drawing.Point(141, 84);
            this.SubiendoPB_Label.Name = "SubiendoPB_Label";
            this.SubiendoPB_Label.Size = new System.Drawing.Size(0, 20);
            this.SubiendoPB_Label.TabIndex = 33;
            this.SubiendoPB_Label.Tag = "";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(0, 108);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(498, 5);
            this.panel2.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(191, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 20);
            this.label1.TabIndex = 36;
            this.label1.Tag = "";
            this.label1.Text = "Subiendo ....";
            // 
            // Subiendo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(498, 113);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.SubirProductos_ProgressBar);
            this.Controls.Add(this.PorcentajePB_Label);
            this.Controls.Add(this.SubiendoPB_Label);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Subiendo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Subiendo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar SubirProductos_ProgressBar;
        private System.Windows.Forms.Label PorcentajePB_Label;
        private System.Windows.Forms.Label SubiendoPB_Label;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
    }
}