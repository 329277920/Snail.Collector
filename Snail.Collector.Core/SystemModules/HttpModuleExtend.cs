
using Snail.Collector.Html;
using Snail.Collector.Http;
using Snail.Collector.JSAdapter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core.SystemModules
{
    public class HttpModuleExtend : HttpModule
    {
        public virtual Element getDoc(string uri)
        {
            return new Element(this.getStr(uri));
        }        

        public override bool getFile(params dynamic[] files)
        {
            List<dynamic> allFiles = new List<dynamic>();
            var invokerContext = ContextManager.GetTaskInvokerContext();
            if (invokerContext == null)
            {
                // todo: 写入日志
                return false;
            }
            foreach (var item in files)
            {
                if (item is JSArray)
                {
                    allFiles.AddRange((JSArray)item);
                }
                else
                {
                    allFiles.Add(item);
                }
            }
            foreach (var file in allFiles)
            {
                if (!FileUnity.SafeCreateDirectory(file.savePath.Substring(0, file.savePath.LastIndexOf("\\"))))
                {
                    // todo: 写入日志，并退出
                }
            }            
            var result = base.getFile(allFiles.ToArray());
            if (result)
            {
                invokerContext.TaskContext.SetStat(files.Length, TaskStatTypes.File);
            }
            return result;
        }

        #region 作废

        //public bool getFiles(JSArray urls)
        //{
        //    var invokerContext = ContextManager.GetTaskInvokerContext();
        //    if (invokerContext == null || invokerContext.TaskContext == null)
        //    {
        //        // todo: 写入日志
        //        return false;
        //    }
        //    var cfg = invokerContext.TaskContext.Settings;
        //    if (cfg == null || cfg.Resource == null || string.IsNullOrEmpty(cfg.Resource.Directory))
        //    {
        //        // todo: 写入日志
        //        return false;
        //    }
        //    var downFiles = from item in urls
        //                    select this.GetDownFiles(item, cfg.Resource.Directory);

        //    return this.getFile((from file in downFiles where file != null select file).ToArray());
        //}

        //public bool getFiles(params string[] urls)
        //{
        //    var invokerContext = ContextManager.GetTaskInvokerContext();
        //    if (invokerContext == null || invokerContext.TaskContext == null)
        //    {
        //        // todo: 写入日志
        //        return false;
        //    }
        //    var cfg = invokerContext.TaskContext.Settings;
        //    if (cfg == null || cfg.Resource == null || string.IsNullOrEmpty(cfg.Resource.Directory))
        //    {
        //        // todo: 写入日志
        //        return false;
        //    }
        //    var downFiles = from item in urls
        //                    select this.GetDownFiles(item, cfg.Resource.Directory);

        //    return this.getFile((from file in downFiles where file != null select file).ToArray());
        //}   

        /// <summary>
        /// 根据url获取下载文件对象
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private dynamic GetDownFiles(string url, string rootPath)
        {
            List<string> dics = new List<string>();
            dics.Add(rootPath);
            var parts = FileUnity.GetPartDirectories(url);
            if (parts != null && parts.Length > 1)
            {
                for (var i = 0; i < parts.Length - 1; i++)
                {
                    dics.Add(parts[i]);
                }
            }
            var fileName = parts?.Length > 0 ? parts[parts.Length - 1] : FileUnity.GetFileName(url);
            if (FileUnity.CreateDirectory(out string fullPath, dics.ToArray()))
            {
                dynamic file = new System.Dynamic.ExpandoObject();
                file.uri = url;
                file.savePath = Path.Combine(fullPath, fileName);
                return file;
            }
            // 记录日志
            return null;
        }

        #endregion

    }
}
