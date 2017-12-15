using ICSharpCode.TextEditor.Document;

namespace Snail.Collector.IDE
{
    partial class FrmMain
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.txtScript = new ICSharpCode.TextEditor.TextEditorControl();
            this.btnRun = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "地址:";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(80, 12);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(843, 25);
            this.txtUrl.TabIndex = 2;
            // 
            // txtScript
            // 
            this.txtScript.IsReadOnly = false;
            this.txtScript.Location = new System.Drawing.Point(15, 53);
            this.txtScript.Name = "txtScript";
            this.txtScript.Size = new System.Drawing.Size(989, 366);
            this.txtScript.TabIndex = 4;
            this.txtScript.Text = "function parse(uri) {\r\n    // 自定义请求头\r\n    http.headers.add(\"client\",\"Snail_Collec" +
    "tor\");\r\n\r\n    // 获取文档\r\n    var doc = http.getDoc(uri);\r\n\r\n    // 返回成功\r\n    retur" +
    "n 1;\r\n}";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(929, 13);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 6;
            this.btnRun.Text = "运行脚本";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // txtResult
            // 
            this.txtResult.BackColor = System.Drawing.Color.White;
            this.txtResult.Location = new System.Drawing.Point(15, 425);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(989, 299);
            this.txtResult.TabIndex = 7;
            this.txtResult.Text = "";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 736);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.txtScript);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(1024, 768);
            this.Name = "FrmMain";
            this.Text = "IDE";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUrl;
        protected ICSharpCode.TextEditor.TextEditorControl txtScript;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.RichTextBox txtResult;
    }
}

