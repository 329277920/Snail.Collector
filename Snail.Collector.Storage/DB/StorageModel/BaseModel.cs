using Newtonsoft.Json.Linq;
using Snail.Collector.Core.Storage.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Snail.Collector.Storage.DB.StorageModel
{
    public abstract class BaseModel : IDynamicParameters
    {
        /// <summary>
        /// 获取当前实体
        /// </summary>
        protected object Entity { get; private set; }

        public BaseModel(object entity)
        {
            this.Entity = entity;
        }

        /// <summary>
        /// 添加Sql执行参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="identity"></param>
        public virtual void AddParameters(IDbCommand command, Identity identity)
        {
            foreach (var property in JsonParser.GetProperties(this.Entity))
            {
                if (command.Parameters.Contains(property.Name))
                {
                    continue;
                }
                var parameter = command.CreateParameter();
                parameter.ParameterName = property.Name;                
                parameter.Value = GetValue(property);                
                command.Parameters.Add(parameter);
            }
        }

        protected virtual object GetValue(JProperty property)
        {
            if (property.Value is JValue)
            {
                if (((JValue)property.Value).Type == JTokenType.Date)
                {
                    return DateTime.Parse(property.Value.ToString()).ToString("s");
                }
            }            
            return property.Value;
        }
    }
}

