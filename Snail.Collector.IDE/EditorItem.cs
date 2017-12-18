using ICSharpCode.TextEditor.Document;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snail.Collector.IDE
{
    /// <summary>
    /// 编辑控件子项
    /// </summary>
    public class EditorItem
    {
        /// <summary>
        /// 获取是否绑定脚本文件
        /// </summary>
        public bool IsBindFile { get; protected set; }

        /// <summary>
        /// 获取脚本文件是否被修改
        /// </summary>
        public bool IsModify { get; protected set; }

        /// <summary>
        /// 获取当前的脚本文件
        /// </summary>
        public FileInfo BindFile { get; protected set; }

        /// <summary>
        /// 获取编辑器
        /// </summary>
        public ICSharpCode.TextEditor.TextEditorControl EditorControl { get; protected set; }

        /// <summary>
        /// 获取或设置该对象加载的TabPage
        /// </summary>
        public TabPage TagPage { get; set; }

        /// <summary>
        /// 在文本发生变更时触发
        /// </summary>
        public event EventHandler OnTextChanged;

        public EditorItem()
        {
            this.EditorControl = new ICSharpCode.TextEditor.TextEditorControl();
            this.EditorControl.Dock = DockStyle.Fill;
            this.EditorControl.Encoding = System.Text.Encoding.Default;
            this.EditorControl.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");           
            this.EditorControl.TextChanged += EditorControl_TextChanged;
        }

        private void EditorControl_TextChanged(object sender, EventArgs e)
        {
            if (this.IsModify)
            {
                return;
            }
            this.IsModify = true;
            this.OnTextChanged?.Invoke(this, e);
        }

        public void Bind(string file)
        {
            this.IsBindFile = true;
            this.BindFile = new FileInfo(file);
            this.IsModify = false;
        }

        public string Value
        {
            get
            {
                return this.EditorControl.Text;
            }
            set
            {
                this.EditorControl.Text = value;
            }
        }       
    }
}
