namespace DeMariaTeste.UI.Forms.Relatorios
{
    partial class FormRelatorioOS
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ComboBox cboCliente;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.CheckBox chkDe;
        private System.Windows.Forms.DateTimePicker dtDe;
        private System.Windows.Forms.CheckBox chkAte;
        private System.Windows.Forms.DateTimePicker dtAte;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.Button btnExportarPdf;
        private System.Windows.Forms.Label lblResumo;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;

        private void InitializeComponent()
        {
            this.lblCliente = new System.Windows.Forms.Label();
            this.cboCliente = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.chkDe = new System.Windows.Forms.CheckBox();
            this.dtDe = new System.Windows.Forms.DateTimePicker();
            this.chkAte = new System.Windows.Forms.CheckBox();
            this.dtAte = new System.Windows.Forms.DateTimePicker();
            this.btnGerar = new System.Windows.Forms.Button();
            this.btnExportarPdf = new System.Windows.Forms.Button();
            this.lblResumo = new System.Windows.Forms.Label();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(10, 14);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(42, 13);
            this.lblCliente.TabIndex = 0;
            this.lblCliente.Text = "Cliente:";
            // 
            // cboCliente
            // 
            this.cboCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCliente.Location = new System.Drawing.Point(60, 11);
            this.cboCliente.Name = "cboCliente";
            this.cboCliente.Size = new System.Drawing.Size(241, 21);
            this.cboCliente.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(317, 14);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Status:";
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Location = new System.Drawing.Point(360, 11);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(121, 21);
            this.cboStatus.TabIndex = 3;
            // 
            // chkDe
            // 
            this.chkDe.AutoSize = true;
            this.chkDe.Location = new System.Drawing.Point(497, 13);
            this.chkDe.Name = "chkDe";
            this.chkDe.Size = new System.Drawing.Size(43, 17);
            this.chkDe.TabIndex = 4;
            this.chkDe.Text = "De:";
            // 
            // dtDe
            // 
            this.dtDe.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDe.Location = new System.Drawing.Point(541, 11);
            this.dtDe.Name = "dtDe";
            this.dtDe.Size = new System.Drawing.Size(112, 20);
            this.dtDe.TabIndex = 5;
            // 
            // chkAte
            // 
            this.chkAte.AutoSize = true;
            this.chkAte.Location = new System.Drawing.Point(665, 13);
            this.chkAte.Name = "chkAte";
            this.chkAte.Size = new System.Drawing.Size(45, 17);
            this.chkAte.TabIndex = 6;
            this.chkAte.Text = "Ate:";
            // 
            // dtAte
            // 
            this.dtAte.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtAte.Location = new System.Drawing.Point(711, 11);
            this.dtAte.Name = "dtAte";
            this.dtAte.Size = new System.Drawing.Size(112, 20);
            this.dtAte.TabIndex = 7;
            // 
            // btnGerar
            // 
            this.btnGerar.Location = new System.Drawing.Point(10, 46);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(103, 26);
            this.btnGerar.TabIndex = 8;
            this.btnGerar.Text = "Gerar";
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // btnExportarPdf
            // 
            this.btnExportarPdf.Location = new System.Drawing.Point(120, 46);
            this.btnExportarPdf.Name = "btnExportarPdf";
            this.btnExportarPdf.Size = new System.Drawing.Size(103, 26);
            this.btnExportarPdf.TabIndex = 9;
            this.btnExportarPdf.Text = "Exportar PDF";
            this.btnExportarPdf.Click += new System.EventHandler(this.btnExportarPdf_Click);
            // 
            // lblResumo
            // 
            this.lblResumo.AutoSize = true;
            this.lblResumo.Location = new System.Drawing.Point(240, 52);
            this.lblResumo.Name = "lblResumo";
            this.lblResumo.Size = new System.Drawing.Size(0, 13);
            this.lblResumo.TabIndex = 10;
            // 
            // reportViewer
            // 
            this.reportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer.Location = new System.Drawing.Point(10, 84);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(943, 446);
            this.reportViewer.TabIndex = 11;
            // 
            // FormRelatorioOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 557);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.cboCliente);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.chkDe);
            this.Controls.Add(this.dtDe);
            this.Controls.Add(this.chkAte);
            this.Controls.Add(this.dtAte);
            this.Controls.Add(this.btnGerar);
            this.Controls.Add(this.btnExportarPdf);
            this.Controls.Add(this.lblResumo);
            this.Controls.Add(this.reportViewer);
            this.Name = "FormRelatorioOS";
            this.Text = "Relatorio de OS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
