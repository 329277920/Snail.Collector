using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using static Dapper.SqlMapper;

namespace Snail.Collector.Storage.DB.StorageModel
{
    /// <summary>
    /// 用于与Dapper交互的更新实体
    /// </summary>
    public class UpdateModel : BaseModel
    {
        protected object Filter { get; private set; }

        public UpdateModel(object entity, object filter) : base(entity)
        {
            this.Filter = filter;
        }        
    }
}