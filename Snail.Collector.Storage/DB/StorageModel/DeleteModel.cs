using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Snail.Collector.Core.Storage.DB;

namespace Snail.Collector.Storage.DB.StorageModel
{
    /// <summary>
    /// 用于与Dapper交互的更新实体
    /// </summary>
    public class DeleteModel : BaseModel
    {
        protected object Filter { get; private set; }

        public DeleteModel(object entity, object filter) : base(entity)
        {
            this.Filter = filter;
        }

        public override void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            var filters = JsonParser.GetProperties(this.Filter);

            foreach (var property in JsonParser.GetProperties(this.Entity))
            {
                if (filters.FirstOrDefault(item => item.Name.Equals(property.Name)) == null ||
                    command.Parameters.Contains(property.Name))
                {
                    continue;
                }
                var parameter = command.CreateParameter();
                parameter.ParameterName = property.Name;
                parameter.Value = GetValue(property);
                command.Parameters.Add(parameter);
            }
        }
    }
}