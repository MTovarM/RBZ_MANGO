namespace RiderbikeZone.Formularios
{
    partial class Inventario
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inventario));
            this.panel5 = new System.Windows.Forms.Panel();
            this.ListaProductos_DataGridView = new System.Windows.Forms.DataGridView();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Marca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unidades = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unidadesVendidas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioSinIVA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValorCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Inversion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ganancia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Anotaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Proveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContactoProv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel6 = new System.Windows.Forms.Panel();
            this.Proveedor_GroupBox = new System.Windows.Forms.GroupBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Marca_RadioButton = new System.Windows.Forms.RadioButton();
            this.Tipo_RadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.BuscarCod_TextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.BuscarNom_TextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListaProductos_DataGridView)).BeginInit();
            this.Proveedor_GroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.ListaProductos_DataGridView);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.Proveedor_GroupBox);
            this.panel5.Location = new System.Drawing.Point(10, 10);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1106, 544);
            this.panel5.TabIndex = 9;
            // 
            // ListaProductos_DataGridView
            // 
            this.ListaProductos_DataGridView.AllowUserToAddRows = false;
            this.ListaProductos_DataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(175)))), ((int)(((byte)(126)))));
            this.ListaProductos_DataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ListaProductos_DataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListaProductos_DataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ListaProductos_DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(175)))), ((int)(((byte)(126)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListaProductos_DataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ListaProductos_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListaProductos_DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.Nombre,
            this.Marca,
            this.Tipo,
            this.Unidades,
            this.unidadesVendidas,
            this.PrecioSinIVA,
            this.ValorCompra,
            this.PrecioVenta,
            this.Inversion,
            this.ganancia,
            this.Anotaciones,
            this.Proveedor,
            this.ContactoProv});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(175)))), ((int)(((byte)(126)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListaProductos_DataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.ListaProductos_DataGridView.EnableHeadersVisualStyles = false;
            this.ListaProductos_DataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ListaProductos_DataGridView.Location = new System.Drawing.Point(0, 111);
            this.ListaProductos_DataGridView.Name = "ListaProductos_DataGridView";
            this.ListaProductos_DataGridView.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(175)))), ((int)(((byte)(126)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListaProductos_DataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.ListaProductos_DataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 9.5F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(175)))), ((int)(((byte)(126)))));
            this.ListaProductos_DataGridView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.ListaProductos_DataGridView.Size = new System.Drawing.Size(1106, 433);
            this.ListaProductos_DataGridView.StandardTab = true;
            this.ListaProductos_DataGridView.TabIndex = 8;
            this.ListaProductos_DataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListaProductos_DataGridView_CellDoubleClick);
            this.ListaProductos_DataGridView.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ListaProductos_DataGridView_CellMouseUp);
            // 
            // Codigo
            // 
            this.Codigo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Codigo.HeaderText = "Código";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Nombre
            // 
            this.Nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            this.Nombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Nombre.Width = 5;
            // 
            // Marca
            // 
            this.Marca.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Marca.HeaderText = "Marca";
            this.Marca.Name = "Marca";
            this.Marca.ReadOnly = true;
            this.Marca.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Marca.Width = 5;
            // 
            // Tipo
            // 
            this.Tipo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Tipo.HeaderText = "Tipo";
            this.Tipo.Name = "Tipo";
            this.Tipo.ReadOnly = true;
            this.Tipo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Tipo.Width = 5;
            // 
            // Unidades
            // 
            this.Unidades.HeaderText = "Unidades Actuales";
            this.Unidades.Name = "Unidades";
            this.Unidades.ReadOnly = true;
            this.Unidades.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // unidadesVendidas
            // 
            this.unidadesVendidas.HeaderText = "Unidades Vendidas";
            this.unidadesVendidas.Name = "unidadesVendidas";
            this.unidadesVendidas.ReadOnly = true;
            this.unidadesVendidas.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PrecioSinIVA
            // 
            this.PrecioSinIVA.HeaderText = "Precio sin IVA";
            this.PrecioSinIVA.Name = "PrecioSinIVA";
            this.PrecioSinIVA.ReadOnly = true;
            this.PrecioSinIVA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PrecioSinIVA.Width = 130;
            // 
            // ValorCompra
            // 
            this.ValorCompra.HeaderText = "Precio compra c/u";
            this.ValorCompra.Name = "ValorCompra";
            this.ValorCompra.ReadOnly = true;
            this.ValorCompra.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ValorCompra.Width = 130;
            // 
            // PrecioVenta
            // 
            this.PrecioVenta.HeaderText = "Precio venta c/u";
            this.PrecioVenta.Name = "PrecioVenta";
            this.PrecioVenta.ReadOnly = true;
            this.PrecioVenta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PrecioVenta.Width = 130;
            // 
            // Inversion
            // 
            this.Inversion.HeaderText = "Inversión";
            this.Inversion.Name = "Inversion";
            this.Inversion.ReadOnly = true;
            this.Inversion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Inversion.Width = 130;
            // 
            // ganancia
            // 
            this.ganancia.HeaderText = "Ganancia";
            this.ganancia.Name = "ganancia";
            this.ganancia.ReadOnly = true;
            // 
            // Anotaciones
            // 
            this.Anotaciones.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Anotaciones.HeaderText = "Anotaciones";
            this.Anotaciones.Name = "Anotaciones";
            this.Anotaciones.ReadOnly = true;
            this.Anotaciones.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Anotaciones.Width = 5;
            // 
            // Proveedor
            // 
            this.Proveedor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Proveedor.HeaderText = "Proveedor";
            this.Proveedor.Name = "Proveedor";
            this.Proveedor.ReadOnly = true;
            this.Proveedor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Proveedor.Width = 130;
            // 
            // ContactoProv
            // 
            this.ContactoProv.HeaderText = "Contacto";
            this.ContactoProv.Name = "ContactoProv";
            this.ContactoProv.ReadOnly = true;
            this.ContactoProv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 101);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1106, 10);
            this.panel6.TabIndex = 7;
            // 
            // Proveedor_GroupBox
            // 
            this.Proveedor_GroupBox.Controls.Add(this.panel10);
            this.Proveedor_GroupBox.Controls.Add(this.panel9);
            this.Proveedor_GroupBox.Controls.Add(this.panel8);
            this.Proveedor_GroupBox.Controls.Add(this.panel7);
            this.Proveedor_GroupBox.Controls.Add(this.groupBox1);
            this.Proveedor_GroupBox.Controls.Add(this.label1);
            this.Proveedor_GroupBox.Controls.Add(this.BuscarCod_TextBox);
            this.Proveedor_GroupBox.Controls.Add(this.label13);
            this.Proveedor_GroupBox.Controls.Add(this.BuscarNom_TextBox);
            this.Proveedor_GroupBox.Controls.Add(this.label12);
            this.Proveedor_GroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.Proveedor_GroupBox.Font = new System.Drawing.Font("Century Gothic", 13F, System.Drawing.FontStyle.Bold);
            this.Proveedor_GroupBox.ForeColor = System.Drawing.Color.White;
            this.Proveedor_GroupBox.Location = new System.Drawing.Point(0, 0);
            this.Proveedor_GroupBox.Name = "Proveedor_GroupBox";
            this.Proveedor_GroupBox.Size = new System.Drawing.Size(1106, 101);
            this.Proveedor_GroupBox.TabIndex = 6;
            this.Proveedor_GroupBox.TabStop = false;
            this.Proveedor_GroupBox.Text = "Buscar producto";
            // 
            // panel10
            // 
            this.panel10.Location = new System.Drawing.Point(400, 44);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(35, 51);
            this.panel10.TabIndex = 37;
            // 
            // panel9
            // 
            this.panel9.Location = new System.Drawing.Point(152, 51);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(10, 43);
            this.panel9.TabIndex = 36;
            // 
            // panel8
            // 
            this.panel8.Location = new System.Drawing.Point(159, 86);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(259, 10);
            this.panel8.TabIndex = 35;
            // 
            // panel7
            // 
            this.panel7.Location = new System.Drawing.Point(158, 51);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(255, 10);
            this.panel7.TabIndex = 34;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Marca_RadioButton);
            this.groupBox1.Controls.Add(this.Tipo_RadioButton);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.ForeColor = System.Drawing.Color.Transparent;
            this.groupBox1.Location = new System.Drawing.Point(160, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 47);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            // 
            // Marca_RadioButton
            // 
            this.Marca_RadioButton.AutoSize = true;
            this.Marca_RadioButton.Checked = true;
            this.Marca_RadioButton.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.Marca_RadioButton.Location = new System.Drawing.Point(129, 17);
            this.Marca_RadioButton.Name = "Marca_RadioButton";
            this.Marca_RadioButton.Size = new System.Drawing.Size(77, 24);
            this.Marca_RadioButton.TabIndex = 4;
            this.Marca_RadioButton.TabStop = true;
            this.Marca_RadioButton.Text = "Marca";
            this.Marca_RadioButton.UseVisualStyleBackColor = true;
            this.Marca_RadioButton.CheckedChanged += new System.EventHandler(this.Marca_RadioButton_CheckedChanged);
            // 
            // Tipo_RadioButton
            // 
            this.Tipo_RadioButton.AutoSize = true;
            this.Tipo_RadioButton.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.Tipo_RadioButton.Location = new System.Drawing.Point(18, 16);
            this.Tipo_RadioButton.Name = "Tipo_RadioButton";
            this.Tipo_RadioButton.Size = new System.Drawing.Size(55, 24);
            this.Tipo_RadioButton.TabIndex = 3;
            this.Tipo_RadioButton.Text = "Tipo";
            this.Tipo_RadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.label1.Location = new System.Drawing.Point(32, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 20);
            this.label1.TabIndex = 32;
            this.label1.Text = "Ordenar por:";
            // 
            // BuscarCod_TextBox
            // 
            this.BuscarCod_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.BuscarCod_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BuscarCod_TextBox.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuscarCod_TextBox.ForeColor = System.Drawing.Color.White;
            this.BuscarCod_TextBox.Location = new System.Drawing.Point(123, 26);
            this.BuscarCod_TextBox.Name = "BuscarCod_TextBox";
            this.BuscarCod_TextBox.Size = new System.Drawing.Size(167, 19);
            this.BuscarCod_TextBox.TabIndex = 1;
            this.BuscarCod_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.BuscarCod_TextBox.TextChanged += new System.EventHandler(this.BuscarCod_TextBox_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.label13.Location = new System.Drawing.Point(33, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 20);
            this.label13.TabIndex = 30;
            this.label13.Text = "Código:";
            // 
            // BuscarNom_TextBox
            // 
            this.BuscarNom_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.BuscarNom_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BuscarNom_TextBox.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuscarNom_TextBox.ForeColor = System.Drawing.Color.White;
            this.BuscarNom_TextBox.Location = new System.Drawing.Point(409, 24);
            this.BuscarNom_TextBox.Name = "BuscarNom_TextBox";
            this.BuscarNom_TextBox.Size = new System.Drawing.Size(477, 19);
            this.BuscarNom_TextBox.TabIndex = 2;
            this.BuscarNom_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.BuscarNom_TextBox.TextChanged += new System.EventHandler(this.BuscarNom_TextBox_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.label12.Location = new System.Drawing.Point(324, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 20);
            this.label12.TabIndex = 28;
            this.label12.Text = "Nombre:";
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(1116, 10);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(10, 544);
            this.panel4.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 10);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 544);
            this.panel3.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 554);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1126, 10);
            this.panel2.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1126, 10);
            this.panel1.TabIndex = 5;
            // 
            // Inventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(1126, 564);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Inventario";
            this.Text = "Inventario";
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ListaProductos_DataGridView)).EndInit();
            this.Proveedor_GroupBox.ResumeLayout(false);
            this.Proveedor_GroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox Proveedor_GroupBox;
        private System.Windows.Forms.TextBox BuscarCod_TextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox BuscarNom_TextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Marca_RadioButton;
        private System.Windows.Forms.RadioButton Tipo_RadioButton;
        private System.Windows.Forms.DataGridView ListaProductos_DataGridView;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Marca;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unidades;
        private System.Windows.Forms.DataGridViewTextBoxColumn unidadesVendidas;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioSinIVA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValorCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Inversion;
        private System.Windows.Forms.DataGridViewTextBoxColumn ganancia;
        private System.Windows.Forms.DataGridViewTextBoxColumn Anotaciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn Proveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContactoProv;
    }
}