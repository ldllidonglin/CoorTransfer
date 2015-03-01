namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_BeTransCoor = new System.Windows.Forms.ComboBox();
            this.lb_Step5 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.cb_ToTransCoor = new System.Windows.Forms.ComboBox();
            this.bt_SaveFile = new System.Windows.Forms.Button();
            this.lb_Step4 = new System.Windows.Forms.Label();
            this.lb_Step3 = new System.Windows.Forms.Label();
            this.lb_Step2 = new System.Windows.Forms.Label();
            this.cb_FieldLat = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_FiledLng = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_Step1 = new System.Windows.Forms.Label();
            this.bt_DoTransfer = new System.Windows.Forms.Button();
            this.BD2SHSelctFile = new System.Windows.Forms.Button();
            this.lb_SaveFilePath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "result.csv";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(215, 122);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 38;
            this.label8.Text = "转";
            // 
            // cb_BeTransCoor
            // 
            this.cb_BeTransCoor.FormattingEnabled = true;
            this.cb_BeTransCoor.Items.AddRange(new object[] {
            "BD-09",
            "GCJ-02",
            "WGS-84",
            "Web mercator",
            "Shanghai"});
            this.cb_BeTransCoor.Location = new System.Drawing.Point(69, 115);
            this.cb_BeTransCoor.Name = "cb_BeTransCoor";
            this.cb_BeTransCoor.Size = new System.Drawing.Size(121, 20);
            this.cb_BeTransCoor.TabIndex = 37;
            this.cb_BeTransCoor.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // lb_Step5
            // 
            this.lb_Step5.AutoSize = true;
            this.lb_Step5.Location = new System.Drawing.Point(9, 153);
            this.lb_Step5.Name = "lb_Step5";
            this.lb_Step5.Size = new System.Drawing.Size(53, 12);
            this.lb_Step5.TabIndex = 36;
            this.lb_Step5.Text = "第五步：";
            // 
            // cb_ToTransCoor
            // 
            this.cb_ToTransCoor.FormattingEnabled = true;
            this.cb_ToTransCoor.Location = new System.Drawing.Point(258, 115);
            this.cb_ToTransCoor.Name = "cb_ToTransCoor";
            this.cb_ToTransCoor.Size = new System.Drawing.Size(121, 20);
            this.cb_ToTransCoor.TabIndex = 39;
            // 
            // bt_SaveFile
            // 
            this.bt_SaveFile.Location = new System.Drawing.Point(66, 80);
            this.bt_SaveFile.Name = "bt_SaveFile";
            this.bt_SaveFile.Size = new System.Drawing.Size(95, 23);
            this.bt_SaveFile.TabIndex = 35;
            this.bt_SaveFile.Text = "选择保存路径";
            this.bt_SaveFile.UseVisualStyleBackColor = true;
            this.bt_SaveFile.Click += new System.EventHandler(this.bt_SaveFileClick);
            // 
            // lb_Step4
            // 
            this.lb_Step4.AutoSize = true;
            this.lb_Step4.Location = new System.Drawing.Point(9, 115);
            this.lb_Step4.Name = "lb_Step4";
            this.lb_Step4.Size = new System.Drawing.Size(53, 12);
            this.lb_Step4.TabIndex = 34;
            this.lb_Step4.Text = "第四步：";
            // 
            // lb_Step3
            // 
            this.lb_Step3.AutoSize = true;
            this.lb_Step3.Location = new System.Drawing.Point(7, 80);
            this.lb_Step3.Name = "lb_Step3";
            this.lb_Step3.Size = new System.Drawing.Size(53, 12);
            this.lb_Step3.TabIndex = 33;
            this.lb_Step3.Text = "第三步：";
            // 
            // lb_Step2
            // 
            this.lb_Step2.AutoSize = true;
            this.lb_Step2.Location = new System.Drawing.Point(7, 48);
            this.lb_Step2.Name = "lb_Step2";
            this.lb_Step2.Size = new System.Drawing.Size(53, 12);
            this.lb_Step2.TabIndex = 32;
            this.lb_Step2.Text = "第二步：";
            // 
            // cb_FieldLat
            // 
            this.cb_FieldLat.FormattingEnabled = true;
            this.cb_FieldLat.Location = new System.Drawing.Point(357, 45);
            this.cb_FieldLat.Name = "cb_FieldLat";
            this.cb_FieldLat.Size = new System.Drawing.Size(121, 20);
            this.cb_FieldLat.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(274, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 28;
            this.label2.Text = "选择纬度字段:";
            // 
            // cb_FiledLng
            // 
            this.cb_FiledLng.FormattingEnabled = true;
            this.cb_FiledLng.Location = new System.Drawing.Point(147, 45);
            this.cb_FiledLng.Name = "cb_FiledLng";
            this.cb_FiledLng.Size = new System.Drawing.Size(121, 20);
            this.cb_FiledLng.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "选择经度字段:";
            // 
            // lb_Step1
            // 
            this.lb_Step1.AutoSize = true;
            this.lb_Step1.Location = new System.Drawing.Point(7, 16);
            this.lb_Step1.Name = "lb_Step1";
            this.lb_Step1.Size = new System.Drawing.Size(53, 12);
            this.lb_Step1.TabIndex = 31;
            this.lb_Step1.Text = "第一步：";
            // 
            // bt_DoTransfer
            // 
            this.bt_DoTransfer.Location = new System.Drawing.Point(66, 148);
            this.bt_DoTransfer.Name = "bt_DoTransfer";
            this.bt_DoTransfer.Size = new System.Drawing.Size(75, 23);
            this.bt_DoTransfer.TabIndex = 30;
            this.bt_DoTransfer.Text = "GO";
            this.bt_DoTransfer.UseVisualStyleBackColor = true;
            this.bt_DoTransfer.Click += new System.EventHandler(this.bt_DoTransfer_Click);
            // 
            // BD2SHSelctFile
            // 
            this.BD2SHSelctFile.Location = new System.Drawing.Point(69, 11);
            this.BD2SHSelctFile.Name = "BD2SHSelctFile";
            this.BD2SHSelctFile.Size = new System.Drawing.Size(75, 23);
            this.BD2SHSelctFile.TabIndex = 25;
            this.BD2SHSelctFile.Text = "选择文件";
            this.BD2SHSelctFile.UseVisualStyleBackColor = true;
            this.BD2SHSelctFile.Click += new System.EventHandler(this.BD2SHSelctFile_Click);
            // 
            // lb_SaveFilePath
            // 
            this.lb_SaveFilePath.AutoSize = true;
            this.lb_SaveFilePath.Location = new System.Drawing.Point(203, 85);
            this.lb_SaveFilePath.Name = "lb_SaveFilePath";
            this.lb_SaveFilePath.Size = new System.Drawing.Size(65, 12);
            this.lb_SaveFilePath.TabIndex = 40;
            this.lb_SaveFilePath.Text = "保存路径：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 192);
            this.Controls.Add(this.lb_SaveFilePath);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cb_BeTransCoor);
            this.Controls.Add(this.lb_Step5);
            this.Controls.Add(this.cb_ToTransCoor);
            this.Controls.Add(this.bt_SaveFile);
            this.Controls.Add(this.lb_Step4);
            this.Controls.Add(this.lb_Step3);
            this.Controls.Add(this.lb_Step2);
            this.Controls.Add(this.cb_FieldLat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cb_FiledLng);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lb_Step1);
            this.Controls.Add(this.bt_DoTransfer);
            this.Controls.Add(this.BD2SHSelctFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "坐标转换";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cb_BeTransCoor;
        private System.Windows.Forms.Label lb_Step5;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ComboBox cb_ToTransCoor;
        private System.Windows.Forms.Button bt_SaveFile;
        private System.Windows.Forms.Label lb_Step4;
        private System.Windows.Forms.Label lb_Step3;
        private System.Windows.Forms.Label lb_Step2;
        private System.Windows.Forms.ComboBox cb_FieldLat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_FiledLng;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_Step1;
        private System.Windows.Forms.Button bt_DoTransfer;
        private System.Windows.Forms.Button BD2SHSelctFile;
        private System.Windows.Forms.Label lb_SaveFilePath;
    }
}

