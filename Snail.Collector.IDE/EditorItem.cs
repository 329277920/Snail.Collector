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
        public RichTextBox EditorControl { get; protected set; }

        public EditorItem()
        {
            var txtBox = new RichTextBox();
            txtBox.Dock = DockStyle.Fill;
            this.EditorControl = txtBox;
        }

        public void Bind(string file)
        {
            this.IsBindFile = true;
            this.BindFile = new FileInfo(file);       
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
