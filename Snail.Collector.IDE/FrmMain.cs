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
        private RunSetting runSetting;

        private SnailCore.Sync.Parallel tasks;

        private TaskStatistics stat;

        private bool isRun = false;

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
            this.LabStatus.Text = "";            
            this.runSetting = new RunSetting();
            TaskErrorMananger.Instance.OnOccursError += Instance_OnOccursError;
            StartWatch();
            this.stat = new TaskStatistics();
        }
      
        private void InitStyle()
        {
            this.txtResult.ReadOnly = true;
            this.txtResult.BackColor = Color.FromArgb(230, 231, 232);
            this.txtStat.ReadOnly = true;
            this.txtStat.BackColor = Color.FromArgb(230, 231, 232);
            this.txtResult.Text = "";
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
            if (this.isRun)
            {
                return;
            }
            if (!this.editor.Save())
            {
                return;
            }
            if (this.isRun)
            {
                return;
            }
            this.txtResult.Text = "";
            this.txtResult.ForeColor = Color.Black;
            this.isRun = true;
            this.SetRunSetting();                                    
            this.stat.Start();
            this.LabStatus.Text = "正在执行...";
            var scriptFile = this.editor.SelectedItem.BindFile.FullName;
            var source = new TaskSource();
            await new System.Threading.Tasks.TaskFactory().StartNew(() =>
            {
                try
                {
                    using (tasks = new SnailCore.Sync.Parallel(this.runSetting.UserCount))
                    {
                        tasks.ForEach<int>(new int[this.runSetting.UserCount], (item) =>
                        {
                            try
                            {
                                // 启动一个用户客户端
                                using (var invoker = new TaskInvoker(scriptFile))
                                {
                                    invoker.AddHostObj("console", this);
                                    invoker.AddHostObj("log", this);
                                    invoker.AddHostObj("stat", this.stat);
                                    invoker.AddHostObj("source", source);
                                    this.stat.addUser();
                                    var runResult = false;
                                    try
                                    {
                                        runResult = invoker.Run();
                                    }
                                    catch (Exception ex)
                                    {
                                        this.SetResult(false, ex.ToString() + "\r\n", true);
                                        runResult = false;
                                    }
                                    this.stat.addUser(-1);
                                }
                            }
                            catch (Exception ex)
                            {
                                this.SetResult(false, ex.ToString() + "\r\n", true);
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    this.SetResult(false, ex.ToString() + "\r\n", true);
                }
                finally
                {
                    this.isRun = false;
                    this.LabStatus.BeginInvoke(new MethodInvoker(() =>
                    {
                        this.LabStatus.Text = "已结束";
                    }));
                    SetRunStatus();
                }
            });
            source.Dispose();
            this.isRun = false;
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
                var path = SnailCore.IO.PathUnity.GetFullPath("Default.js");
                if (string.IsNullOrEmpty(path))
                {
                    return null;
                }
                return SnailCore.IO.FileUnity.ReadStringAsync(path, Encoding.UTF8).ConfigureAwait(false).GetAwaiter().GetResult();
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
                this.txtResult.AppendText(content.ToString());
            }
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
            }
        }       

        private void StartWatch()
        {
            new System.Threading.Tasks.TaskFactory().StartNew(() =>
            {
                while (true)
                {
                    if (!this.isRun)
                    {
                        System.Threading.Thread.Sleep(1000);
                        continue;
                    }
                    SetRunStatus();
                    System.Threading.Thread.Sleep(1000);
                }
            });
        }

        private void SetRunStatus()
        {
            try
            {                                           
                this.txtStat.BeginInvoke(new MethodInvoker(() =>
                {
                    var content = new StringBuilder();
                    var totalTime = this.stat.TotalTimeSpan;
                    content.AppendFormat("当前客户端:{0},总请求:{1},成功:{2},失败:{3},并发:{4},总耗时:{5}\r\n",
                        this.stat.TotalUser,
                        this.stat.TotalReq,
                        this.stat.TotalReqSuccess,
                        this.stat.TotalReqError,
                        this.stat.Concurrent,
                        string.Format("{0}:{1}:{2}", totalTime.Minutes, totalTime.Seconds, totalTime.Milliseconds));
                    this.stat.each(item =>
                    {
                        content.AppendFormat("{0},总请求:{1},成功:{2},失败:{3},最大:{4},最小:{5},当前并发:{6}\r\n",
                            item.Uri,
                            item.TotalReq,
                            item.TotalReqSuccess,
                            item.TotalReqError,
                            item.MaxTime.Minutes * 60000 + item.MaxTime.Seconds * 1000 + item.MaxTime.Milliseconds,
                            item.MinTime.Minutes * 60000 + item.MinTime.Seconds * 1000 + item.MinTime.Milliseconds,
                            item.Concurrent);
                    });
                    this.txtStat.Text = content.ToString();
                }));
            }
            catch { }
        }

        /// <summary>
        /// 设置结束
        /// </summary>
        private void SetStop()
        {
            this.isRun = false;
            this.LabStatus.BeginInvoke(new MethodInvoker(() =>
            {
                this.LabStatus.Text = "已结束";
            }));
            SetRunStatus();
            this.stat.Stop();
        }

        #endregion

        #region 导出脚本

        public void writeLine(object content)
        {
            this.SetResult(true, content + "\r\n", true);
        }

        public void write(object content)
        {
            this.SetResult(true, content, true);
        }

        public void info(object content)
        {
            TaskErrorMananger.Instance.Info("task", content.ToString() + "\r\n");
        }

        public void error(object content)
        {
            TaskErrorMananger.Instance.Error("task", content.ToString() + "\r\n");
        }

        #endregion
    }
}
