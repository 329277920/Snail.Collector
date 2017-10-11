using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using static Dapper.SqlMapper;
using System.Dynamic;
using System.Linq.Expressions;


namespace Snail.Collector.Modules.Storage
{
    /// <summary>
    /// 存储实体
    /// </summary>
    public class StorageEntity : IDynamicParameters
    {
        private DynamicObject _innerEntity;

        public StorageEntity(object entity)
        {
            this._innerEntity = (DynamicObject)entity;
            if (this._innerEntity == null)
            {
                throw new Exception(string.Format("Type '{0}' does not implement interface 'IDynamicMetaObjectProvider'.",
                    entity.GetType().FullName));
            }
        }

        public void AddParameters(IDbCommand command, Identity identity)
        {
            foreach (var name in _innerEntity.GetDynamicMemberNames())
            {
                object value;
                this._innerEntity.TryGetMember(new StorageEntityGetMemberBinder(name, true), out value);
                if (command.Parameters.Contains(name))
                {
                    continue;
                }
                var p = command.CreateParameter();
                p.ParameterName = name;
                // p.DbType = null;
                // p.Size = 10;
                p.Value = value;
                command.Parameters.Add(p);
            }
        }
    }


    public class StorageEntityGetMemberBinder : GetMemberBinder
    {
        public StorageEntityGetMemberBinder(string name, bool ignoreCase) : base(name, ignoreCase) { }

        public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion)
        {
            throw new NotImplementedException();
        }
    }
}
