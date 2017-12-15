using ICSharpCode.TextEditor.Document;
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
        }
       
        private async void btnRun_Click(object sender, EventArgs e)
        {
            InitResult();
            try
            {
                using (var tester = new TaskTester())
                {
                    tester.AddObj("log", this);
                    try
                    {
                        await tester.RunAsync(this.txtScript.Text.Trim(), this.txtUrl.Text.ToString().Trim());
                    }
                    catch (Exception ex)
                    {
                        this.SetResult(false, ex.ToString(), false);
                    }
                }
            }
            catch (Exception ex)
            {
                this.SetResult(false, ex.ToString(), false);
            }           
        }

        private void SetResult(bool success, string content, bool append)
        {
            if (this.txtResult.InvokeRequired)
            {
                this.txtResult.BeginInvoke(new MethodInvoker(()=> 
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
                this.txtResult.Text = content;
            }
            else
            {
                this.txtResult.AppendText(content);
            }
        }        

        private void InitResult()
        {
            this.txtResult.ForeColor = Color.Black;
            this.txtResult.Text = "";
        }

        #region 导出脚本

        public void debug(string content)
        {
            this.SetResult(true, content, true);
        }

        public void error(string content)
        {
            this.SetResult(false, content, true);
        }

        #endregion
    }
}
