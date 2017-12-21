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
        private RunStatus runStatus;

        private RunSetting runSetting;

        private Snail.Sync.Parallel tasks;

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
            TaskErrorMananger.Instance.OnOccursError += Instance_OnOccursError;
            this.LabStatus.Text = "";
            this.runStatus = new RunStatus();
            this.runSetting = new RunSetting();
            StartWatch();
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
            System.Net.Http.HttpClient ss;
            
            if (this.runStatus.Running)
            {
                return;
            }
            if (!this.editor.Save())
            {
                return;
            }           
            this.LabStatus.Text = "";
            InitResult();
            this.SetRunSetting();
            this.SetResult(true, "正在执行...", true);
            this.runStatus.Init();
            var scriptFile = this.editor.SelectedItem.BindFile.FullName;
            await new System.Threading.Tasks.TaskFactory().StartNew(() =>
            {
                try
                {
                    this.runStatus.Running = true;
                    using (tasks = new Snail.Sync.Parallel(this.runSetting.UserCount))
                    {
                        tasks.ForEach<int>(new int[this.runSetting.UserCount], (item) =>
                        {
                            try
                            {
                                using (var invoker = new TaskInvoker(scriptFile))
                                {
                                    invoker.AddHostObj("log", this);
                                    for (var i = 0; i < this.runSetting.RequestCount; i++)
                                    {
                                        var runResult = false;
                                        try
                                        {
                                            runResult = invoker.Run();
                                        }
                                        catch (Exception ex)
                                        {
                                            this.SetResult(false, ex.ToString(), true);
                                            runResult = false;
                                        }
                                        SafeSetRunStatus(status =>
                                        {
                                            status.TotalReqCount++;
                                            if (runResult)
                                            {
                                                status.SuccessReqCount++;
                                            }
                                            else
                                            {
                                                status.ErrorReqCount++;
                                            }
                                        });
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                this.SetResult(false, ex.ToString(), true);
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    this.SetResult(false, ex.ToString(), true);
                }
                finally
                {
                    this.SetResult(true, "执行结束.", true);
                    this.runStatus.Running = false;
                }
            });                   
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

        private void SetRunSetting()
        {
            lock (this)
            {
                this.runSetting.UserCount = 1;
                this.runSetting.RequestCount = 1;
                if (int.TryParse(this.txtUser.Text.Trim(), out int userCount))
                {
                    this.runSetting.UserCount = userCount;
                }
                if (int.TryParse(this.txtReqCount.Text.Trim(), out int reqCount))
                {
                    this.runSetting.RequestCount = reqCount;
                }
            }
        }

        private void SafeSetRunStatus(Action<RunStatus> func)
        {
            lock (this)
            {
                func(this.runStatus);
            }
        }

        private void StartWatch()
        {
            new System.Threading.Tasks.TaskFactory().StartNew(() =>
            {
                while (true)
                {
                    if (!this.runStatus.Running)
                    {
                        continue;
                    }
                    try
                    {
                        this.SafeSetRunStatus((status) => { status.UserCount = this.tasks.RunningTaskCount; });
                        var content = new StringBuilder();
                        content.AppendFormat("用户数:{0},", runStatus.UserCount);
                        content.AppendFormat("请求数:{0},", runStatus.TotalReqCount);
                        content.AppendFormat("成功数:{0},", runStatus.SuccessReqCount);
                        content.AppendFormat("失败数:{0},", runStatus.ErrorReqCount);
                        this.LabStatus.BeginInvoke(new MethodInvoker(() =>
                        {
                            this.LabStatus.Text = content.ToString();
                        }));
                    }
                    catch { }
                    System.Threading.Thread.Sleep(500);
                }
            });
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
