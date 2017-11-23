namespace WindowsFormsApplication1
{
    partial class FarmNReprogramming
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
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.referencesaedskifteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saedskifteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jordbundstypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arealtypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nLeachKgNhaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nLeachmgNlDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nLeachKgNhatotalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nLeachmgNltotalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.farmNDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonCalculateExisting = new System.Windows.Forms.Button();
            this.buttonCalculateApplication = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.farmNDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(472, 460);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.referencesaedskifteDataGridViewTextBoxColumn,
            this.saedskifteDataGridViewTextBoxColumn,
            this.jordbundstypeDataGridViewTextBoxColumn,
            this.arealtypeDataGridViewTextBoxColumn,
            this.nLeachKgNhaDataGridViewTextBoxColumn,
            this.nLeachmgNlDataGridViewTextBoxColumn,
            this.nLeachKgNhatotalDataGridViewTextBoxColumn,
            this.nLeachmgNltotalDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.farmNDataBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 345);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(843, 89);
            this.dataGridView1.TabIndex = 2;
            // 
            // referencesaedskifteDataGridViewTextBoxColumn
            // 
            this.referencesaedskifteDataGridViewTextBoxColumn.DataPropertyName = "Referencesaedskifte";
            this.referencesaedskifteDataGridViewTextBoxColumn.HeaderText = "Referencesaedskifte";
            this.referencesaedskifteDataGridViewTextBoxColumn.Name = "referencesaedskifteDataGridViewTextBoxColumn";
            // 
            // saedskifteDataGridViewTextBoxColumn
            // 
            this.saedskifteDataGridViewTextBoxColumn.DataPropertyName = "Saedskifte";
            this.saedskifteDataGridViewTextBoxColumn.HeaderText = "Saedskifte";
            this.saedskifteDataGridViewTextBoxColumn.Name = "saedskifteDataGridViewTextBoxColumn";
            // 
            // jordbundstypeDataGridViewTextBoxColumn
            // 
            this.jordbundstypeDataGridViewTextBoxColumn.DataPropertyName = "Jordbundstype";
            this.jordbundstypeDataGridViewTextBoxColumn.HeaderText = "Jordbundstype";
            this.jordbundstypeDataGridViewTextBoxColumn.Name = "jordbundstypeDataGridViewTextBoxColumn";
            // 
            // arealtypeDataGridViewTextBoxColumn
            // 
            this.arealtypeDataGridViewTextBoxColumn.DataPropertyName = "Arealtype";
            this.arealtypeDataGridViewTextBoxColumn.HeaderText = "Arealtype";
            this.arealtypeDataGridViewTextBoxColumn.Name = "arealtypeDataGridViewTextBoxColumn";
            // 
            // nLeachKgNhaDataGridViewTextBoxColumn
            // 
            this.nLeachKgNhaDataGridViewTextBoxColumn.DataPropertyName = "NLeach_KgN_ha";
            this.nLeachKgNhaDataGridViewTextBoxColumn.HeaderText = "NLeach_KgN_ha";
            this.nLeachKgNhaDataGridViewTextBoxColumn.Name = "nLeachKgNhaDataGridViewTextBoxColumn";
            // 
            // nLeachmgNlDataGridViewTextBoxColumn
            // 
            this.nLeachmgNlDataGridViewTextBoxColumn.DataPropertyName = "NLeach_mgN_l";
            this.nLeachmgNlDataGridViewTextBoxColumn.HeaderText = "NLeach_mgN_l";
            this.nLeachmgNlDataGridViewTextBoxColumn.Name = "nLeachmgNlDataGridViewTextBoxColumn";
            // 
            // nLeachKgNhatotalDataGridViewTextBoxColumn
            // 
            this.nLeachKgNhatotalDataGridViewTextBoxColumn.DataPropertyName = "NLeach_KgN_ha_total";
            this.nLeachKgNhatotalDataGridViewTextBoxColumn.HeaderText = "NLeach_KgN_ha_total";
            this.nLeachKgNhatotalDataGridViewTextBoxColumn.Name = "nLeachKgNhatotalDataGridViewTextBoxColumn";
            // 
            // nLeachmgNltotalDataGridViewTextBoxColumn
            // 
            this.nLeachmgNltotalDataGridViewTextBoxColumn.DataPropertyName = "NLeach_mgN_l_total";
            this.nLeachmgNltotalDataGridViewTextBoxColumn.HeaderText = "NLeach_mgN_l_total";
            this.nLeachmgNltotalDataGridViewTextBoxColumn.Name = "nLeachmgNltotalDataGridViewTextBoxColumn";
            // 
            // farmNDataBindingSource
            // 
            //this.farmNDataBindingSource.DataSource = typeof(WindowsFormsApplication1.LocalFarmNWebService.FarmNData);
            //this.farmNDataBindingSource.CurrentChanged += new System.EventHandler(this.farmNDataBindingSource_CurrentChanged);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(12, 35);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(843, 105);
            this.dataGridView2.TabIndex = 3;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(12, 169);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(843, 150);
            this.dataGridView3.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Gødningsliste";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Arealliste";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 440);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "BeregnNudrift";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 470);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "BeregnAnsoegt";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(780, 450);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "createFarm";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonCalculateExisting
            // 
            this.buttonCalculateExisting.Location = new System.Drawing.Point(320, 440);
            this.buttonCalculateExisting.Name = "buttonCalculateExisting";
            this.buttonCalculateExisting.Size = new System.Drawing.Size(126, 23);
            this.buttonCalculateExisting.TabIndex = 10;
            this.buttonCalculateExisting.Text = "CalculateExisting";
            this.buttonCalculateExisting.UseVisualStyleBackColor = true;
            this.buttonCalculateExisting.Click += new System.EventHandler(this.buttonCalculateExisting_Click);
            // 
            // buttonCalculateApplication
            // 
            this.buttonCalculateApplication.Location = new System.Drawing.Point(320, 470);
            this.buttonCalculateApplication.Name = "buttonCalculateApplication";
            this.buttonCalculateApplication.Size = new System.Drawing.Size(126, 23);
            this.buttonCalculateApplication.TabIndex = 11;
            this.buttonCalculateApplication.Text = "CalculateApplication";
            this.buttonCalculateApplication.UseVisualStyleBackColor = true;
            this.buttonCalculateApplication.Click += new System.EventHandler(this.buttonCalculateApplication_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(163, 440);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(128, 23);
            this.button4.TabIndex = 12;
            this.button4.Text = "BeregnNudriftLocal";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(163, 470);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(128, 23);
            this.button5.TabIndex = 13;
            this.button5.Text = "BeregnAnsoegt";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // FarmNReprogramming
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 530);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.buttonCalculateApplication);
            this.Controls.Add(this.buttonCalculateExisting);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Name = "FarmNReprogramming";
            this.Text = "ReferenceFarmNTest";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.farmNDataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn referencesaedskifteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn saedskifteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn jordbundstypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn arealtypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nLeachKgNhaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nLeachmgNlDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nLeachKgNhatotalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nLeachmgNltotalDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource farmNDataBindingSource;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button buttonCalculateExisting;
        private System.Windows.Forms.Button buttonCalculateApplication;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}

