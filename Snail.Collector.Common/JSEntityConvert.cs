using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snail.Collector.JSAdapter
{
    /// <summary>
    /// js实体转换
    /// </summary>
    public class JSEntityConvert
    {
        /// <summary>
        /// 将js对象转换成JObject
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<object> ConvertEntities(params object[] entities)
        {
            var jObjArray = new List<object>();
            if (entities != null)
            {
                var fDataArray = ConvertToJsonModel(entities);
                foreach (var fData in (IEnumerable<object>)fDataArray)
                {
                    if (fData is JArray)
                    {
                        var currData = ((JArray)fData).First;
                        while (currData != null)
                        {
                            jObjArray.Add(currData);
                            currData = currData.Next;
                        }
                    }
                    else
                    {
                        jObjArray.Add(fData);
                    }
                }
            }
            return jObjArray;
        }

        /// <summary>
        /// 将脚本引擎传递的对象转化成Dapper识别的对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static object ConvertToJsonModel(dynamic entity)
        {
            return JsonConvert.DeserializeObject<object>(JsonConvert.SerializeObject(entity));
        }
    }
}
