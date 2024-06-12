namespace Management_System.PAL
{
    partial class UserControlStats
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.chartMonthlyProfit = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cmbYearSelect = new System.Windows.Forms.ComboBox();
            this.lblYearSelect = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvTopSell = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.lblYearSelect1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartMonthlyProfit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopSell)).BeginInit();
            this.SuspendLayout();
            // 
            // chartMonthlyProfit
            // 
            chartArea1.AxisX.Interval = 1D;
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            chartArea1.BorderColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.chartMonthlyProfit.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartMonthlyProfit.Legends.Add(legend1);
            this.chartMonthlyProfit.Location = new System.Drawing.Point(12, 150);
            this.chartMonthlyProfit.Name = "chartMonthlyProfit";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Revenue";
            this.chartMonthlyProfit.Series.Add(series1);
            this.chartMonthlyProfit.Size = new System.Drawing.Size(885, 478);
            this.chartMonthlyProfit.TabIndex = 0;
            this.chartMonthlyProfit.Text = "chart1";
            this.chartMonthlyProfit.MouseEnter += new System.EventHandler(this.chartMonthlyProfit_MouseEnter);
            // 
            // cmbYearSelect
            // 
            this.cmbYearSelect.FormattingEnabled = true;
            this.cmbYearSelect.Location = new System.Drawing.Point(776, 261);
            this.cmbYearSelect.Name = "cmbYearSelect";
            this.cmbYearSelect.Size = new System.Drawing.Size(121, 24);
            this.cmbYearSelect.TabIndex = 1;
            this.cmbYearSelect.SelectedIndexChanged += new System.EventHandler(this.cmbYearSelect_SelectedIndexChanged);
            // 
            // lblYearSelect
            // 
            this.lblYearSelect.AutoSize = true;
            this.lblYearSelect.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblYearSelect.Location = new System.Drawing.Point(452, 119);
            this.lblYearSelect.Name = "lblYearSelect";
            this.lblYearSelect.Size = new System.Drawing.Size(33, 28);
            this.lblYearSelect.TabIndex = 5;
            this.lblYearSelect.Text = "{?}";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(254, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(192, 28);
            this.label4.TabIndex = 4;
            this.label4.Text = "Monthly Revenue of ";
            // 
            // dgvTopSell
            // 
            this.dgvTopSell.AllowUserToAddRows = false;
            this.dgvTopSell.AllowUserToDeleteRows = false;
            this.dgvTopSell.AllowUserToResizeColumns = false;
            this.dgvTopSell.AllowUserToResizeRows = false;
            this.dgvTopSell.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopSell.BackgroundColor = System.Drawing.Color.White;
            this.dgvTopSell.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvTopSell.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(67)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(67)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTopSell.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTopSell.ColumnHeadersHeight = 35;
            this.dgvTopSell.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTopSell.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3});
            this.dgvTopSell.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTopSell.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTopSell.EnableHeadersVisualStyles = false;
            this.dgvTopSell.Location = new System.Drawing.Point(932, 150);
            this.dgvTopSell.MultiSelect = false;
            this.dgvTopSell.Name = "dgvTopSell";
            this.dgvTopSell.ReadOnly = true;
            this.dgvTopSell.RowHeadersWidth = 51;
            this.dgvTopSell.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvTopSell.RowTemplate.Height = 30;
            this.dgvTopSell.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopSell.ShowCellErrors = false;
            this.dgvTopSell.ShowCellToolTips = false;
            this.dgvTopSell.ShowEditingIcon = false;
            this.dgvTopSell.ShowRowErrors = false;
            this.dgvTopSell.Size = new System.Drawing.Size(644, 444);
            this.dgvTopSell.TabIndex = 6;
            this.dgvTopSell.MouseEnter += new System.EventHandler(this.dgvTopSell_MouseEnter);
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Product_Name";
            this.Column2.HeaderText = "Product";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Total_Sold";
            this.Column3.HeaderText = "Sold Quantity";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1102, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 28);
            this.label1.TabIndex = 4;
            this.label1.Text = "Top 10 Sold Products of";
            // 
            // lblYearSelect1
            // 
            this.lblYearSelect1.AutoSize = true;
            this.lblYearSelect1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblYearSelect1.Location = new System.Drawing.Point(1329, 119);
            this.lblYearSelect1.Name = "lblYearSelect1";
            this.lblYearSelect1.Size = new System.Drawing.Size(33, 28);
            this.lblYearSelect1.TabIndex = 5;
            this.lblYearSelect1.Text = "{?}";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(685, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(298, 46);
            this.label2.TabIndex = 4;
            this.label2.Text = "Notable Statistics";
            // 
            // UserControlStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvTopSell);
            this.Controls.Add(this.lblYearSelect1);
            this.Controls.Add(this.lblYearSelect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbYearSelect);
            this.Controls.Add(this.chartMonthlyProfit);
            this.Name = "UserControlStats";
            this.Size = new System.Drawing.Size(1672, 632);
            ((System.ComponentModel.ISupportInitialize)(this.chartMonthlyProfit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopSell)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartMonthlyProfit;
        private System.Windows.Forms.ComboBox cmbYearSelect;
        private System.Windows.Forms.Label lblYearSelect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvTopSell;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblYearSelect1;
        private System.Windows.Forms.Label label2;
    }
}
