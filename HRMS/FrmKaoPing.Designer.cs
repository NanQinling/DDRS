namespace DDRS
{
    partial class FrmKaoPing
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNotSubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvKaoPing = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbKaoPing = new System.Windows.Forms.GroupBox();
            this.txtJieShiShuoMing = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtKaoPingYiJian = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtKaoPingDeFen = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtJi = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtQin = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtNeng = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtDe = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtBuMen = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBeiZhu = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHnbh = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKaoPing)).BeginInit();
            this.gbKaoPing.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnNotSubmit);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dgvKaoPing);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.gbKaoPing);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(704, 447);
            this.panel1.TabIndex = 25;
            // 
            // btnNotSubmit
            // 
            this.btnNotSubmit.Location = new System.Drawing.Point(445, 21);
            this.btnNotSubmit.Name = "btnNotSubmit";
            this.btnNotSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnNotSubmit.TabIndex = 24;
            this.btnNotSubmit.Text = "未报送人员";
            this.btnNotSubmit.UseVisualStyleBackColor = true;
            this.btnNotSubmit.Click += new System.EventHandler(this.btnNotSubmit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 36);
            this.label1.TabIndex = 3;
            this.label1.Text = "修改考评数据";
            // 
            // dgvKaoPing
            // 
            this.dgvKaoPing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvKaoPing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKaoPing.Location = new System.Drawing.Point(11, 267);
            this.dgvKaoPing.Name = "dgvKaoPing";
            this.dgvKaoPing.RowTemplate.Height = 23;
            this.dgvKaoPing.Size = new System.Drawing.Size(680, 171);
            this.dgvKaoPing.TabIndex = 1;
            this.dgvKaoPing.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKaoPing_CellClick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(526, 21);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gbKaoPing
            // 
            this.gbKaoPing.Controls.Add(this.txtJieShiShuoMing);
            this.gbKaoPing.Controls.Add(this.label6);
            this.gbKaoPing.Controls.Add(this.txtKaoPingYiJian);
            this.gbKaoPing.Controls.Add(this.label19);
            this.gbKaoPing.Controls.Add(this.txtKaoPingDeFen);
            this.gbKaoPing.Controls.Add(this.label20);
            this.gbKaoPing.Controls.Add(this.txtJi);
            this.gbKaoPing.Controls.Add(this.label11);
            this.gbKaoPing.Controls.Add(this.txtQin);
            this.gbKaoPing.Controls.Add(this.label22);
            this.gbKaoPing.Controls.Add(this.txtNeng);
            this.gbKaoPing.Controls.Add(this.label23);
            this.gbKaoPing.Controls.Add(this.txtDe);
            this.gbKaoPing.Controls.Add(this.label14);
            this.gbKaoPing.Controls.Add(this.txtBuMen);
            this.gbKaoPing.Controls.Add(this.label4);
            this.gbKaoPing.Controls.Add(this.txtBeiZhu);
            this.gbKaoPing.Controls.Add(this.label5);
            this.gbKaoPing.Controls.Add(this.txtName);
            this.gbKaoPing.Controls.Add(this.label3);
            this.gbKaoPing.Controls.Add(this.txtHnbh);
            this.gbKaoPing.Controls.Add(this.label2);
            this.gbKaoPing.Location = new System.Drawing.Point(11, 59);
            this.gbKaoPing.Name = "gbKaoPing";
            this.gbKaoPing.Size = new System.Drawing.Size(680, 202);
            this.gbKaoPing.TabIndex = 0;
            this.gbKaoPing.TabStop = false;
            this.gbKaoPing.Text = "[考评数据]";
            // 
            // txtJieShiShuoMing
            // 
            this.txtJieShiShuoMing.Location = new System.Drawing.Point(118, 95);
            this.txtJieShiShuoMing.Multiline = true;
            this.txtJieShiShuoMing.Name = "txtJieShiShuoMing";
            this.txtJieShiShuoMing.Size = new System.Drawing.Size(556, 71);
            this.txtJieShiShuoMing.TabIndex = 56;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(17, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 48);
            this.label6.TabIndex = 57;
            this.label6.Text = "具体解释说明：                 （200字以内）";
            // 
            // txtKaoPingYiJian
            // 
            this.txtKaoPingYiJian.BackColor = System.Drawing.SystemColors.Info;
            this.txtKaoPingYiJian.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtKaoPingYiJian.Location = new System.Drawing.Point(574, 69);
            this.txtKaoPingYiJian.Name = "txtKaoPingYiJian";
            this.txtKaoPingYiJian.ReadOnly = true;
            this.txtKaoPingYiJian.Size = new System.Drawing.Size(101, 21);
            this.txtKaoPingYiJian.TabIndex = 54;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(494, 73);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 55;
            this.label19.Text = "考评意见：";
            // 
            // txtKaoPingDeFen
            // 
            this.txtKaoPingDeFen.BackColor = System.Drawing.SystemColors.Info;
            this.txtKaoPingDeFen.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtKaoPingDeFen.Location = new System.Drawing.Point(574, 42);
            this.txtKaoPingDeFen.Name = "txtKaoPingDeFen";
            this.txtKaoPingDeFen.ReadOnly = true;
            this.txtKaoPingDeFen.Size = new System.Drawing.Size(101, 21);
            this.txtKaoPingDeFen.TabIndex = 52;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(494, 46);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(65, 12);
            this.label20.TabIndex = 53;
            this.label20.Text = "考评得分：";
            // 
            // txtJi
            // 
            this.txtJi.Location = new System.Drawing.Point(360, 69);
            this.txtJi.Name = "txtJi";
            this.txtJi.Size = new System.Drawing.Size(101, 21);
            this.txtJi.TabIndex = 50;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(259, 73);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 12);
            this.label11.TabIndex = 51;
            this.label11.Text = "绩（最高25分）：";
            // 
            // txtQin
            // 
            this.txtQin.Location = new System.Drawing.Point(118, 69);
            this.txtQin.Name = "txtQin";
            this.txtQin.Size = new System.Drawing.Size(101, 21);
            this.txtQin.TabIndex = 48;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(17, 73);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(101, 12);
            this.label22.TabIndex = 49;
            this.label22.Text = "勤（最高25分）：";
            // 
            // txtNeng
            // 
            this.txtNeng.Location = new System.Drawing.Point(360, 42);
            this.txtNeng.Name = "txtNeng";
            this.txtNeng.Size = new System.Drawing.Size(101, 21);
            this.txtNeng.TabIndex = 46;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(259, 46);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(101, 12);
            this.label23.TabIndex = 47;
            this.label23.Text = "能（最高25分）：";
            // 
            // txtDe
            // 
            this.txtDe.Location = new System.Drawing.Point(118, 42);
            this.txtDe.Name = "txtDe";
            this.txtDe.Size = new System.Drawing.Size(101, 21);
            this.txtDe.TabIndex = 44;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(17, 46);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(101, 12);
            this.label14.TabIndex = 45;
            this.label14.Text = "德（最高25分）：";
            // 
            // txtBuMen
            // 
            this.txtBuMen.BackColor = System.Drawing.SystemColors.Info;
            this.txtBuMen.Location = new System.Drawing.Point(118, 17);
            this.txtBuMen.Name = "txtBuMen";
            this.txtBuMen.ReadOnly = true;
            this.txtBuMen.Size = new System.Drawing.Size(101, 21);
            this.txtBuMen.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "部门：";
            // 
            // txtBeiZhu
            // 
            this.txtBeiZhu.Location = new System.Drawing.Point(118, 170);
            this.txtBeiZhu.Multiline = true;
            this.txtBeiZhu.Name = "txtBeiZhu";
            this.txtBeiZhu.Size = new System.Drawing.Size(556, 25);
            this.txtBeiZhu.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "备    注：";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.SystemColors.Info;
            this.txtName.Location = new System.Drawing.Point(574, 17);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(101, 21);
            this.txtName.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(494, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "姓名：";
            // 
            // txtHnbh
            // 
            this.txtHnbh.BackColor = System.Drawing.SystemColors.Info;
            this.txtHnbh.Location = new System.Drawing.Point(360, 17);
            this.txtHnbh.Name = "txtHnbh";
            this.txtHnbh.ReadOnly = true;
            this.txtHnbh.Size = new System.Drawing.Size(101, 21);
            this.txtHnbh.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "人员编号：";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(607, 21);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭窗口";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmKaoPing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(706, 450);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "FrmKaoPing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改考评数据";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKaoPing)).EndInit();
            this.gbKaoPing.ResumeLayout(false);
            this.gbKaoPing.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvKaoPing;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gbKaoPing;
        private System.Windows.Forms.TextBox txtJieShiShuoMing;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtKaoPingYiJian;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtKaoPingDeFen;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtJi;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtQin;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtNeng;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtDe;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtBuMen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBeiZhu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHnbh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNotSubmit;
    }
}