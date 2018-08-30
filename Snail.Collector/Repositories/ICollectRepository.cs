using Snail.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Repositories
{
    public interface ICollectRepository
    {
        int Insert(CollectInfo collectInfo);

        CollectInfo SelectSingle(int id);
    }
}
