using Snail.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Repositories
{
    public interface ICollectContentRepository
    {
        int Insert(CollectContentInfo contentInfo);
    }
}
