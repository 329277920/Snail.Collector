using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Snail.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core.Storage.DB
{
    /// <summary>
    /// 过滤sql解析器，应用于查询，修改，删除
    /// </summary>
    public class JsonParser
    {
        /// <summary>
        /// 获取某个json对象的所有JProperty
        /// </summary>
        /// <param name="jObj">Json对象</param>
        /// <returns></returns>
        public static JProperty[] GetProperties(object jObj)
        {
            if (jObj is JValue)
            {
                return new JProperty[0];
            }
            if (jObj is JProperty)
            {
                return GetProperties(((JProperty)jObj).Value);
            }
            var result = new List<JProperty>();
            if (jObj is JToken)
            {
                var child = ((JToken)jObj).First;
                while (child != null)
                {
                    if (child is JProperty)
                    {
                        result.Add((JProperty)child);
                    }
                    child = child.Next;
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// 根据Json对象解析出sql筛选语句
        /// </summary>       
        /// <param name="jObj">json对象</param>        
        /// <returns>返回执行的sql</returns>
        public static string ToSqlQuery(object jObj)
        {                              
            if (jObj is JArray)
            {
                var jArray = jObj as JArray;
                var sqlBuilder = new StringBuilder();
                var jToken = jArray.First;
                var j = 0;
                while (jToken != null)
                {
                    var sql = InnerToSqlQuery(jToken);                   
                    if (!string.IsNullOrEmpty(sql))
                    {
                        if (j++ > 0)
                        {
                            sqlBuilder.Append(" OR ");
                        }
                        sqlBuilder.AppendFormat("({0})", sql);
                    }
                    jToken = jToken.Next;
                }
                return sqlBuilder.ToString();
            }
            return InnerToSqlQuery(jObj);
        }

        /// <summary>
        /// 将对象转换成jObj
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

        /// <summary>
        /// 根据Json对象解析出sql筛选语句
        /// </summary>       
        /// <param name="jObj">json对象</param>      
        /// <returns>返回执行的sql</returns>
        private static string InnerToSqlQuery(object jObj)
        {         
            var jProperties = GetProperties(jObj);
            if (jProperties == null || jProperties.Length <= 0)
            {
                return null;
            }
            var sqlBuilder = new StringBuilder();
            int i = 0;           
            foreach (var property in jProperties)
            {
                var split = i++ > 0 ? " AND " : "";
                var condition = property.Name;
                // 普通条件
                if (property.Value is JValue)
                {
                    sqlBuilder.AppendFormat("{2}{0}='{1}'", condition, property.Value, split);
                    continue;
                }
                // 运算符
                var opPts = GetProperties(property);
                if (opPts.Length != 1)
                {
                    throw new Exception(string.Format("条件:{0},格式错误,它应该是一个json容器.", condition));
                }
                if (!opPts[0].Name.StartsWith("$"))
                {
                    throw new Exception(string.Format("条件:{0},格式错误,运算符必须包含'$'.", condition));
                }
                var sqlItem = ParserOperator(condition, opPts[0]);
               
                if (string.IsNullOrEmpty(sqlItem))
                {
                    throw new Exception(string.Format("条件:{0},格式错误,不支持的运算符.", condition));
                }
                sqlBuilder.AppendFormat("{1}{0}", sqlItem, split);
            }           
            return sqlBuilder.ToString();
        }

        /// <summary>
        /// 接续运算符（gt(greater than)大于；lt（less than）小于；gte(greater then equal)大于等于；lte(less than equal)小于等于；ne（not equal）不等于）
        /// </summary>
        /// <param name="name"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static string ParserOperator(string name, JProperty property)
        {           
            var opt = property.Name.Substring(1);
            switch (opt)
            {
                // 指定从模型属性中获取参数值
                case "p":
                    return ParseRefProperty(name, property);
                case "in":
                    return ParseIn(name, property);
                case "gt":
                    return ParseGT(name, property);
                case "lt":
                    return ParseLT(name, property);
                case "gte":
                    return ParseGTE(name, property);
                case "lte":
                    return ParseLTE(name, property);
                case "ne":
                    return ParseNE(name, property);
            }
            return null;
        }

        /// <summary>
        /// 解析引用某个属性
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static string ParseRefProperty(string condition, JProperty property)
        {
            return string.Format("{0}=@{1}", condition, property.Value);
        }

        /// <summary>
        /// 解析in操作符
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="property"></param>        
        /// <returns></returns>
        private static string ParseIn(string condition, JProperty property)
        {           
            var sqlBuilder = new StringBuilder();
            if (!(property.Value is JArray) && !(property.Value is JObject))
            {
                throw new Exception(string.Format("条件:{0},格式错误,值必须为数组.", condition));
            }
            int i = 0;
            var jValue = property.Value.First;
            while (jValue != null)
            {
                var value = jValue is JValue ? (JValue)jValue :
                            jValue is JProperty ? ((JProperty)jValue).Value :
                            null;
                if (value == null)
                {
                    throw new Exception(string.Format("条件:{0},格式错误,数组中必须为确定的值.", condition));
                }
                if (i++ > 0)
                {
                    sqlBuilder.Append(" OR ");
                }
                sqlBuilder.AppendFormat("{0} = '{1}'", condition, value);
                jValue = jValue.Next;
            }
            var sqlTemp = sqlBuilder.ToString();
            if (sqlTemp.Length > 0)
            {
                return string.Format("({0})", sqlTemp);
            }
            return null;
        }

        /// <summary>
        /// 解析大于符号
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static string ParseGT(string condition, JProperty property)
        {
            return string.Format("{0}>'{1}'", condition, property.Value);
        }

        /// <summary>
        /// 解析小于符号
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static string ParseLT(string condition, JProperty property)
        {
            return string.Format("{0}<'{1}'", condition, property.Value);
        }

        /// <summary>
        /// 解析大于等于符号
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static string ParseGTE(string condition, JProperty property)
        {
            return string.Format("{0}>='{1}'", condition, property.Value);
        }

        /// <summary>
        /// 解析小于等于符号
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static string ParseLTE(string condition, JProperty property)
        {
            return string.Format("{0}<='{1}'", condition, property.Value);
        }

        /// <summary>
        /// 解析不等于符号
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static string ParseNE(string condition, JProperty property)
        {
            return string.Format("{0}!='{1}'", condition, property.Value);
        }
    }
}

