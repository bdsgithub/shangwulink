namespace WinformTest
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnChange = new System.Windows.Forms.Button();
            this.txtFirst = new System.Windows.Forms.TextBox();
            this.txtSecond = new System.Windows.Forms.TextBox();
            this.btn_ll = new System.Windows.Forms.Button();
            this.txt_ll = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(141, 58);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(75, 23);
            this.btnChange.TabIndex = 0;
            this.btnChange.Text = "转换";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // txtFirst
            // 
            this.txtFirst.Location = new System.Drawing.Point(12, 12);
            this.txtFirst.Multiline = true;
            this.txtFirst.Name = "txtFirst";
            this.txtFirst.Size = new System.Drawing.Size(880, 40);
            this.txtFirst.TabIndex = 1;
            // 
            // txtSecond
            // 
            this.txtSecond.Location = new System.Drawing.Point(12, 101);
            this.txtSecond.Multiline = true;
            this.txtSecond.Name = "txtSecond";
            this.txtSecond.Size = new System.Drawing.Size(880, 62);
            this.txtSecond.TabIndex = 2;
            // 
            // btn_ll
            // 
            this.btn_ll.Location = new System.Drawing.Point(176, 178);
            this.btn_ll.Name = "btn_ll";
            this.btn_ll.Size = new System.Drawing.Size(75, 23);
            this.btn_ll.TabIndex = 3;
            this.btn_ll.Text = "浏览";
            this.btn_ll.UseVisualStyleBackColor = true;
            this.btn_ll.Click += new System.EventHandler(this.btn_ll_Click);
            // 
            // txt_ll
            // 
            this.txt_ll.Location = new System.Drawing.Point(12, 221);
            this.txt_ll.Multiline = true;
            this.txt_ll.Name = "txt_ll";
            this.txt_ll.Size = new System.Drawing.Size(880, 82);
            this.txt_ll.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 423);
            this.Controls.Add(this.txt_ll);
            this.Controls.Add(this.btn_ll);
            this.Controls.Add(this.txtSecond);
            this.Controls.Add(this.txtFirst);
            this.Controls.Add(this.btnChange);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.TextBox txtFirst;
        private System.Windows.Forms.TextBox txtSecond;
        private System.Windows.Forms.Button btn_ll;
        private System.Windows.Forms.TextBox txt_ll;
    }
}

