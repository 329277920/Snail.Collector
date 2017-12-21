using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snail.Collector.IDE
{
    public partial class Editor : UserControl
    {
        public event EventHandler<EditorExceptionEventArgs> OnError;

        private int newScriptCount = 0;

        private SaveFileDialog _saveFileDialog;

        private OpenFileDialog _openFileDialog;

        public TabPage SelectedTab
        {
            get
            {
                return this.tabs.SelectedTab;
            }
        }

        public EditorItem SelectedItem
        {
            get
            {
                return this.SelectedTab?.Tag as EditorItem;
            }
        }

        public string Value
        {
            get
            {
                return this.SelectedItem?.Value;
            }
        }

        public Editor()
        {
            InitializeComponent();

            this.BorderStyle = BorderStyle.None;

            this._saveFileDialog = new SaveFileDialog();
            this._saveFileDialog.Title = "选择脚本文件保存路径";
            this._saveFileDialog.AddExtension = true;
            this._saveFileDialog.CheckPathExists = true;
            this._saveFileDialog.DefaultExt = ".js";

            this._openFileDialog = new OpenFileDialog();
            this._openFileDialog.Title = "打开脚本文件";
            this._openFileDialog.AddExtension = true;
            this._openFileDialog.DefaultExt = ".js";
            this._openFileDialog.Filter = "脚本文件(*.js)|*.js";
            this._openFileDialog.CheckFileExists = true;

            this.tabs.DoubleClick += Tabs_DoubleClick;
        }      

        /// <summary>
        /// 添加一个编辑项
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool AddFile(string filePath = null)
        {
            var item = new EditorItem()
            {
                Value = this.ReadScript(filePath)
            };
            var tabPage = new TabPage() { Tag = item };
            item.TagPage = tabPage;
            if (filePath?.Length > 0)
            {
                var fullPath = Snail.IO.PathUnity.GetFullPath(filePath);
                if (string.IsNullOrEmpty(fullPath))
                {
                    this.OnError(this, new EditorExceptionEventArgs()
                    {
                        Message = string.Format("未能找到脚本文件:'{0}'.", filePath)
                    });
                    return false;
                }
                tabPage.Text = filePath.Substring(fullPath.LastIndexOf("\\") + 1);
                item.Bind(fullPath);
            }
            else
            {
                tabPage.Text = string.Format("newScript({0})", ++newScriptCount);              
            }
            this.tabs.TabPages.Add(tabPage);
            tabPage.Controls.Add(item.EditorControl);
            this.tabs.SelectedTab = tabPage;
            item.OnTextChanged += Item_OnTextChanged;
            return true;
        }

        /// <summary>
        /// 双击关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tabs_DoubleClick(object sender, EventArgs e)
        {
            if (this.SelectedTab != null)
            {
                if (this.SelectedItem != null && this.SelectedItem.IsModify)
                {
                    var rest = MessageBox.Show("是否保存修改?", "提示", MessageBoxButtons.YesNoCancel);
                    if (rest == DialogResult.Cancel)
                    {
                        return;
                    }
                    if (rest == DialogResult.Yes)
                    {
                        if (this.Save())
                        {
                            this.tabs.TabPages.Remove(this.SelectedTab);
                        }
                        return;
                    }
                }
                this.tabs.TabPages.Remove(this.SelectedTab);
            }
        }

        private void Item_OnTextChanged(object sender, EventArgs e)
        {
            var item = sender as EditorItem;
            if (item.IsModify && item.TagPage != null)
            {
                item.TagPage.Text = item.TagPage.Text + "*";
            }
        }

        public bool Open()
        {
            if (this._openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            var fullPath = this._openFileDialog.FileName;
            return this.AddFile(fullPath);            
        }

        public bool Save()
        {
            if (this.SelectedItem == null)
            {
                return false;
            }
            var fullPath = this.SelectedItem.IsBindFile ? this.SelectedItem.BindFile.FullName : "";
            if (string.IsNullOrEmpty(fullPath))
            {
                if (this._saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }
                fullPath = this._saveFileDialog.FileName;
            }
            if (string.IsNullOrEmpty(fullPath))
            {
                return false;
            }
            this.Save(fullPath, this.SelectedItem.Value);
            this.SelectedItem.Bind(fullPath);
            this.SelectedTab.Text = fullPath.Substring(fullPath.LastIndexOf("\\") + 1);
            return true;
        }

        #region 私有成员

        private string ReadScript(string filePath = null)
        {
            filePath = filePath ?? "Default.js";
            try
            {
                var path = Snail.IO.PathUnity.GetFullPath(filePath);
                if (string.IsNullOrEmpty(path))
                {
                    return null;
                }
                return Snail.IO.FileUnity.ReadStringAsync(path, Encoding.UTF8).ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, new EditorExceptionEventArgs()
                {
                    Message = "未能读取脚本文件,路径:" + filePath,
                    Ex = ex
                });
            }
            return null;
        }

        private void Save(string filePath, string value)
        {
            Snail.IO.FileUnity.Save(filePath, value, Encoding.UTF8);
        }
        #endregion
    }
}
