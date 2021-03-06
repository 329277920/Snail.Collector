﻿
using Snail.Collector.Common;
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
    public static class HttpModuleExtend 
    {
        public static bool getFile(this HttpModule http, params dynamic[] files)
        {
            try
            {
                List<dynamic> allFiles = new List<dynamic>();
                var invokerContext = ContextManager.GetTaskInvokerContext();
                if (invokerContext == null)
                {
                    throw new Exception("failed to get the taskInvokerContext.");
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
                        return false;
                    }
                }
                var result = http.getFiles(allFiles.ToArray());
                if (result)
                {
                    invokerContext.Task?.SetStat(files.Length, TaskStatTypes.File);
                }
                return result;
            }
            catch (Exception ex)
            {
                LoggerProxy.Error("HttpModuleExtend", "call getFile error.", ex);
            }
            return false;
        }         
    }
}
