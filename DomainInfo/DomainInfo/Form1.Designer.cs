namespace DomainInfo
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.lstWhois = new System.Windows.Forms.ListBox();
            this.lstDNS = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbPropType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblStatusWhois = new System.Windows.Forms.Label();
            this.lblStatusDNS = new System.Windows.Forms.Label();
            this.lblStatusProp = new System.Windows.Forms.Label();
            this.lstDNSRecord = new System.Windows.Forms.ListBox();
            this.lstDNSData = new System.Windows.Forms.ListBox();
            this.lstDNSTtl = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lstPropagation = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(407, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Domain:";
            // 
            // txtDomain
            // 
            this.txtDomain.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDomain.Location = new System.Drawing.Point(459, 10);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(361, 20);
            this.txtDomain.TabIndex = 0;
            this.txtDomain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDomain_KeyDown);
            // 
            // lstWhois
            // 
            this.lstWhois.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstWhois.FormattingEnabled = true;
            this.lstWhois.Location = new System.Drawing.Point(12, 66);
            this.lstWhois.Name = "lstWhois";
            this.lstWhois.Size = new System.Drawing.Size(380, 706);
            this.lstWhois.TabIndex = 2;
            this.lstWhois.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstWhois_MouseDoubleClick);
            // 
            // lstDNS
            // 
            this.lstDNS.FormattingEnabled = true;
            this.lstDNS.Location = new System.Drawing.Point(401, 67);
            this.lstDNS.Name = "lstDNS";
            this.lstDNS.Size = new System.Drawing.Size(182, 238);
            this.lstDNS.TabIndex = 3;
            this.lstDNS.SelectedIndexChanged += new System.EventHandler(this.lstDNS_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(398, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Domain";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(398, 323);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "DNS Propagation";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Whois information";
            // 
            // cmbPropType
            // 
            this.cmbPropType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPropType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPropType.FormattingEnabled = true;
            this.cmbPropType.Items.AddRange(new object[] {
            "A",
            "MX",
            "NS",
            "CNAME",
            "SPF",
            "TXT"});
            this.cmbPropType.Location = new System.Drawing.Point(967, 315);
            this.cmbPropType.Name = "cmbPropType";
            this.cmbPropType.Size = new System.Drawing.Size(121, 21);
            this.cmbPropType.TabIndex = 8;
            this.cmbPropType.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.cmbPropType.SelectionChangeCommitted += new System.EventHandler(this.cmbPropType_SelectionChangeCommitted);
            this.cmbPropType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPropType_KeyDown);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(892, 318);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Record Type";
            // 
            // lblStatusWhois
            // 
            this.lblStatusWhois.Location = new System.Drawing.Point(213, 50);
            this.lblStatusWhois.Name = "lblStatusWhois";
            this.lblStatusWhois.Size = new System.Drawing.Size(179, 13);
            this.lblStatusWhois.TabIndex = 10;
            this.lblStatusWhois.Text = "Checking....";
            this.lblStatusWhois.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblStatusWhois.Visible = false;
            // 
            // lblStatusDNS
            // 
            this.lblStatusDNS.Location = new System.Drawing.Point(477, 51);
            this.lblStatusDNS.Name = "lblStatusDNS";
            this.lblStatusDNS.Size = new System.Drawing.Size(106, 13);
            this.lblStatusDNS.TabIndex = 11;
            this.lblStatusDNS.Text = "Checking....";
            this.lblStatusDNS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblStatusDNS.Visible = false;
            // 
            // lblStatusProp
            // 
            this.lblStatusProp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusProp.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusProp.Location = new System.Drawing.Point(494, 321);
            this.lblStatusProp.Name = "lblStatusProp";
            this.lblStatusProp.Size = new System.Drawing.Size(125, 16);
            this.lblStatusProp.TabIndex = 12;
            this.lblStatusProp.Text = "Checking....";
            this.lblStatusProp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblStatusProp.Visible = false;
            // 
            // lstDNSRecord
            // 
            this.lstDNSRecord.FormattingEnabled = true;
            this.lstDNSRecord.Location = new System.Drawing.Point(589, 66);
            this.lstDNSRecord.Name = "lstDNSRecord";
            this.lstDNSRecord.Size = new System.Drawing.Size(48, 238);
            this.lstDNSRecord.TabIndex = 13;
            this.lstDNSRecord.SelectedIndexChanged += new System.EventHandler(this.lstDNSRecord_SelectedIndexChanged);
            // 
            // lstDNSData
            // 
            this.lstDNSData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDNSData.FormattingEnabled = true;
            this.lstDNSData.Location = new System.Drawing.Point(643, 66);
            this.lstDNSData.Name = "lstDNSData";
            this.lstDNSData.Size = new System.Drawing.Size(507, 238);
            this.lstDNSData.TabIndex = 14;
            this.lstDNSData.SelectedIndexChanged += new System.EventHandler(this.lstDNSData_SelectedIndexChanged);
            this.lstDNSData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstDNSData_MouseDoubleClick);
            // 
            // lstDNSTtl
            // 
            this.lstDNSTtl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDNSTtl.FormattingEnabled = true;
            this.lstDNSTtl.Location = new System.Drawing.Point(1156, 66);
            this.lstDNSTtl.Name = "lstDNSTtl";
            this.lstDNSTtl.Size = new System.Drawing.Size(59, 238);
            this.lstDNSTtl.TabIndex = 15;
            this.lstDNSTtl.SelectedIndexChanged += new System.EventHandler(this.lstDNSTtl_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(586, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Record";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1153, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "TTL";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(643, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Data";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1091, 323);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Region";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1075, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lstPropagation
            // 
            this.lstPropagation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPropagation.FullRowSelect = true;
            this.lstPropagation.GridLines = true;
            this.lstPropagation.Location = new System.Drawing.Point(401, 339);
            this.lstPropagation.Name = "lstPropagation";
            this.lstPropagation.Size = new System.Drawing.Size(814, 434);
            this.lstPropagation.TabIndex = 22;
            this.lstPropagation.UseCompatibleStateImageBehavior = false;
            this.lstPropagation.View = System.Windows.Forms.View.List;
            this.lstPropagation.DoubleClick += new System.EventHandler(this.lstPropagation_DoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1227, 799);
            this.Controls.Add(this.lstPropagation);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lstDNSTtl);
            this.Controls.Add(this.lstDNSData);
            this.Controls.Add(this.lstDNSRecord);
            this.Controls.Add(this.lblStatusProp);
            this.Controls.Add(this.lblStatusDNS);
            this.Controls.Add(this.lblStatusWhois);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbPropType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstDNS);
            this.Controls.Add(this.lstWhois);
            this.Controls.Add(this.txtDomain);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Domain Information";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_SizeChanged);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.ListBox lstWhois;
        private System.Windows.Forms.ListBox lstDNS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbPropType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblStatusWhois;
        private System.Windows.Forms.Label lblStatusDNS;
        private System.Windows.Forms.Label lblStatusProp;
        private System.Windows.Forms.ListBox lstDNSRecord;
        private System.Windows.Forms.ListBox lstDNSData;
        private System.Windows.Forms.ListBox lstDNSTtl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView lstPropagation;
    }
}

