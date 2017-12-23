using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Snail.Collector.Storage.DB.StorageModel
{
    /// <summary>
    /// 用于与Dapper交互的新增实体
    /// </summary>
    public class InsertModel : BaseModel
    {
        public InsertModel(object entity) : base(entity)
        {

        }         
    }
}
