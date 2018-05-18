namespace OOP2.HRMS.WF
{
    partial class PayslipHRE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PayslipHRE));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.dgvPayslipHRE = new MetroFramework.Controls.MetroGrid();
            this.PayID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PayrollName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BasicSalary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HouseAllowance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Medical = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Conveyance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Addition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Deduction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NetTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSelectEmpListESHRD = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayslipHRE)).BeginInit();
            this.metroPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 251F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.metroPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 60);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(939, 531);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.metroPanel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(682, 525);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.dgvPayslipHRE);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(3, 3);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(676, 519);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // dgvPayslipHRE
            // 
            this.dgvPayslipHRE.AllowUserToAddRows = false;
            this.dgvPayslipHRE.AllowUserToDeleteRows = false;
            this.dgvPayslipHRE.AllowUserToResizeRows = false;
            this.dgvPayslipHRE.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvPayslipHRE.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPayslipHRE.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvPayslipHRE.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPayslipHRE.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPayslipHRE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPayslipHRE.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PayID,
            this.PayrollName,
            this.BasicSalary,
            this.HouseAllowance,
            this.Medical,
            this.Conveyance,
            this.Addition,
            this.Deduction,
            this.NetTotal});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPayslipHRE.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPayslipHRE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPayslipHRE.EnableHeadersVisualStyles = false;
            this.dgvPayslipHRE.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.dgvPayslipHRE.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvPayslipHRE.Location = new System.Drawing.Point(0, 0);
            this.dgvPayslipHRE.Name = "dgvPayslipHRE";
            this.dgvPayslipHRE.ReadOnly = true;
            this.dgvPayslipHRE.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPayslipHRE.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPayslipHRE.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvPayslipHRE.RowTemplate.Height = 24;
            this.dgvPayslipHRE.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPayslipHRE.Size = new System.Drawing.Size(676, 519);
            this.dgvPayslipHRE.TabIndex = 2;
            this.dgvPayslipHRE.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPayslipHRE_CellClick);
            // 
            // PayID
            // 
            this.PayID.DataPropertyName = "ID";
            this.PayID.HeaderText = "ID";
            this.PayID.Name = "PayID";
            this.PayID.ReadOnly = true;
            this.PayID.Width = 40;
            // 
            // PayrollName
            // 
            this.PayrollName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PayrollName.DataPropertyName = "PayrollName";
            this.PayrollName.HeaderText = "Payroll";
            this.PayrollName.Name = "PayrollName";
            this.PayrollName.ReadOnly = true;
            // 
            // BasicSalary
            // 
            this.BasicSalary.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BasicSalary.DataPropertyName = "BasicSalary";
            this.BasicSalary.HeaderText = "Salary";
            this.BasicSalary.Name = "BasicSalary";
            this.BasicSalary.ReadOnly = true;
            // 
            // HouseAllowance
            // 
            this.HouseAllowance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.HouseAllowance.DataPropertyName = "HouseAllowance";
            this.HouseAllowance.HeaderText = "House";
            this.HouseAllowance.Name = "HouseAllowance";
            this.HouseAllowance.ReadOnly = true;
            // 
            // Medical
            // 
            this.Medical.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Medical.DataPropertyName = "Medical";
            this.Medical.HeaderText = "Medical";
            this.Medical.Name = "Medical";
            this.Medical.ReadOnly = true;
            // 
            // Conveyance
            // 
            this.Conveyance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Conveyance.DataPropertyName = "Conveyance";
            this.Conveyance.HeaderText = "Conveyance";
            this.Conveyance.Name = "Conveyance";
            this.Conveyance.ReadOnly = true;
            // 
            // Addition
            // 
            this.Addition.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Addition.DataPropertyName = "Addition";
            this.Addition.HeaderText = "Addition";
            this.Addition.Name = "Addition";
            this.Addition.ReadOnly = true;
            // 
            // Deduction
            // 
            this.Deduction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Deduction.DataPropertyName = "Deduction";
            this.Deduction.HeaderText = "Deduction";
            this.Deduction.Name = "Deduction";
            this.Deduction.ReadOnly = true;
            // 
            // NetTotal
            // 
            this.NetTotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NetTotal.DataPropertyName = "NetTotal";
            this.NetTotal.HeaderText = "Total";
            this.NetTotal.Name = "NetTotal";
            this.NetTotal.ReadOnly = true;
            // 
            // metroPanel2
            // 
            this.metroPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel2.Controls.Add(this.groupBox1);
            this.metroPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(691, 3);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(245, 525);
            this.metroPanel2.TabIndex = 2;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelectEmpListESHRD);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox1.Location = new System.Drawing.Point(16, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 345);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "VIEW";
            // 
            // btnSelectEmpListESHRD
            // 
            this.btnSelectEmpListESHRD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(167)))), ((int)(((byte)(240)))));
            this.btnSelectEmpListESHRD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectEmpListESHRD.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectEmpListESHRD.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSelectEmpListESHRD.Location = new System.Drawing.Point(41, 137);
            this.btnSelectEmpListESHRD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectEmpListESHRD.Name = "btnSelectEmpListESHRD";
            this.btnSelectEmpListESHRD.Size = new System.Drawing.Size(142, 54);
            this.btnSelectEmpListESHRD.TabIndex = 8;
            this.btnSelectEmpListESHRD.Text = "VIEW PAYSLIP";
            this.btnSelectEmpListESHRD.UseVisualStyleBackColor = false;
            this.btnSelectEmpListESHRD.Click += new System.EventHandler(this.btnSelectEmpListESHRD_Click);
            // 
            // PayslipHRE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 611);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PayslipHRE";
            this.Text = "Payslip";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PayslipHRE_FormClosing);
            this.Load += new System.EventHandler(this.PayslipHRE_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.metroPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayslipHRE)).EndInit();
            this.metroPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroGrid dgvPayslipHRE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PayID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PayrollName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BasicSalary;
        private System.Windows.Forms.DataGridViewTextBoxColumn HouseAllowance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Medical;
        private System.Windows.Forms.DataGridViewTextBoxColumn Conveyance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Addition;
        private System.Windows.Forms.DataGridViewTextBoxColumn Deduction;
        private System.Windows.Forms.DataGridViewTextBoxColumn NetTotal;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelectEmpListESHRD;
    }
}