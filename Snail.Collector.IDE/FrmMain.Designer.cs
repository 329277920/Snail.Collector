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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_new = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_Run = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.labUser = new System.Windows.Forms.Label();
            this.editor = new Snail.Collector.IDE.Editor();
            this.txtStat = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LabStatus = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.tool_Run});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_new,
            this.tool_Open,
            this.tool_Save,
            this.tool_SaveAs});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // tool_new
            // 
            this.tool_new.Name = "tool_new";
            this.tool_new.Size = new System.Drawing.Size(125, 26);
            this.tool_new.Text = "新建";
            // 
            // tool_Open
            // 
            this.tool_Open.Name = "tool_Open";
            this.tool_Open.Size = new System.Drawing.Size(125, 26);
            this.tool_Open.Text = "打开";
            // 
            // tool_Save
            // 
            this.tool_Save.Name = "tool_Save";
            this.tool_Save.Size = new System.Drawing.Size(125, 26);
            this.tool_Save.Text = "保存";
            // 
            // tool_SaveAs
            // 
            this.tool_SaveAs.Name = "tool_SaveAs";
            this.tool_SaveAs.Size = new System.Drawing.Size(125, 26);
            this.tool_SaveAs.Text = "另存为";
            // 
            // tool_Run
            // 
            this.tool_Run.Name = "tool_Run";
            this.tool_Run.Size = new System.Drawing.Size(49, 20);
            this.tool_Run.Text = "运行";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 546);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(792, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.editor);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtStat);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.txtResult);
            this.splitContainer1.Size = new System.Drawing.Size(792, 522);
            this.splitContainer1.SplitterDistance = 365;
            this.splitContainer1.TabIndex = 11;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtUser);
            this.groupBox2.Controls.Add(this.labUser);
            this.groupBox2.Location = new System.Drawing.Point(3, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(362, 43);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "配置";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(83, 15);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(100, 25);
            this.txtUser.TabIndex = 15;
            this.txtUser.Text = "1";
            // 
            // labUser
            // 
            this.labUser.AutoSize = true;
            this.labUser.Location = new System.Drawing.Point(17, 20);
            this.labUser.Name = "labUser";
            this.labUser.Size = new System.Drawing.Size(60, 15);
            this.labUser.TabIndex = 14;
            this.labUser.Text = "客户端:";
            // 
            // editor
            // 
            this.editor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editor.Location = new System.Drawing.Point(0, 54);
            this.editor.Name = "editor";
            this.editor.Size = new System.Drawing.Size(365, 468);
            this.editor.TabIndex = 11;
            // 
            // txtStat
            // 
            this.txtStat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStat.BackColor = System.Drawing.Color.White;
            this.txtStat.Location = new System.Drawing.Point(2, 55);
            this.txtStat.Name = "txtStat";
            this.txtStat.ReadOnly = true;
            this.txtStat.Size = new System.Drawing.Size(421, 121);
            this.txtStat.TabIndex = 10;
            this.txtStat.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.LabStatus);
            this.groupBox1.Location = new System.Drawing.Point(2, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(421, 43);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "状态";
            // 
            // LabStatus
            // 
            this.LabStatus.AutoSize = true;
            this.LabStatus.Location = new System.Drawing.Point(6, 21);
            this.LabStatus.Name = "LabStatus";
            this.LabStatus.Size = new System.Drawing.Size(55, 15);
            this.LabStatus.TabIndex = 0;
            this.LabStatus.Text = "label1";
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.BackColor = System.Drawing.Color.White;
            this.txtResult.Location = new System.Drawing.Point(2, 181);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(421, 340);
            this.txtResult.TabIndex = 8;
            this.txtResult.Text = "";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 568);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FrmMain";
            this.Text = "IDE";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tool_new;
        private System.Windows.Forms.ToolStripMenuItem tool_Open;
        private System.Windows.Forms.ToolStripMenuItem tool_Save;
        private System.Windows.Forms.ToolStripMenuItem tool_SaveAs;
        private System.Windows.Forms.ToolStripMenuItem tool_Run;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Editor editor;
        private System.Windows.Forms.RichTextBox txtResult;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label labUser;
        private System.Windows.Forms.Label LabStatus;
        private System.Windows.Forms.RichTextBox txtStat;
    }
}

