﻿using ICSharpCode.TextEditor.Document;
using Snail.Collector.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snail.Collector.IDE
{
    public partial class FrmMain : Form
    {      
        public FrmMain()
        {           
            InitializeComponent();
            InitStyle();
            this.tool_Run.Click += toolRun_Click;
            this.tool_new.Click += toolNew_Click;
            this.tool_Save.Click += toolSave_Click;
            this.tool_Open.Click += toolOpen_Click;
            this.KeyPreview = true;
            this.KeyDown += FrmMain_KeyDown;
            ErrorMananger.Instance.OnOccursError += Instance_OnOccursError;
        }
      
        private void InitStyle()
        {
            this.txtResult.ReadOnly = true;
            this.txtResult.BackColor = Color.FromArgb(230, 231, 232);          
        }

        #region 事件

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            // 保存
            if (e.KeyCode == Keys.S && e.Control)
            {
                e.Handled = true;
                toolSave_Click(sender, e);
                return;
            }

            // 运行
            if (e.KeyCode == Keys.F5)
            {
                e.Handled = true;
                this.toolRun_Click(sender, e);
            }
        }

        private async void toolRun_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.editor.Value))
            {
                return;
            }
            InitResult();
            this.SetResult(true, "正在执行...", true);
            try
            {
                using (var tester = new TaskTester())
                {
                    tester.AddObj("log", this);
                    try
                    {
                        await tester.RunAsync(this.editor.Value);
                    }
                    catch (Exception ex)
                    {
                        this.SetResult(false, ex.ToString(), true);
                    }
                }
            }
            catch (Exception ex)
            {
                this.SetResult(false, ex.ToString(), true);
            }
            this.SetResult(true, "执行结束.", true);
        }

        private void toolNew_Click(object sender, EventArgs e)
        {
            this.editor.AddFile();
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            this.editor.Save();
        }

        private void toolOpen_Click(object sender, EventArgs e)
        {
            this.editor.Open();
        }

        private void Instance_OnOccursError(object sender, ErrorEventArgs e)
        {
            SetResult(false, e.Message, true);
        }

        #endregion

        #region 私有成员

        private string ReadDefaultScript()
        {
            try
            {
                var path = Snail.IO.PathUnity.GetFullPath("Default.js");
                if (string.IsNullOrEmpty(path))
                {
                    return null;
                }
                return Snail.IO.FileUnity.ReadStringAsync(path, Encoding.UTF8).ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                // todo: 记录日志
            }
            return null;
        }

        private void SetResult(bool success, object content, bool append)
        {
            if (content == null)
            {
                return;
            }
            if (this.txtResult.InvokeRequired)
            {
                this.txtResult.BeginInvoke(new MethodInvoker(() =>
                {
                    this.SetResult(success, content, append);
                }));
                return;
            }
            if (!success)
            {                
                this.txtResult.ForeColor = Color.Red;
            }
            if (!append)
            {
                this.txtResult.Text = content.ToString();
            }
            else
            {
                this.txtResult.AppendText(content.ToString() + "\r\n");
            }
        }

        private void InitResult()
        {
            this.txtResult.ForeColor = Color.Black;
            this.txtResult.Text = "";
        }       

        #endregion

        #region 导出脚本

        public void debug(object content)
        {
            this.SetResult(true, content, true);
        }

        public void error(object content)
        {
            this.SetResult(false, content, true);
        }

        #endregion
    }
}