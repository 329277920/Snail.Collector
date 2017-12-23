using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.IDE
{
    /// <summary>
    /// 任务运行统计
    /// </summary>
    public class TaskStatistics
    {
        public TaskStatistics()
        {
            this.clear();

            new TaskFactory().StartNew(() => 
            {
                while (true)
                {
                    if (this.TotalReq <= 0)
                    {
                        System.Threading.Thread.Sleep(1000);
                        continue;
                    }
                    this.Concurrent = this.TotalReq - this._preTotalReq;
                    this._preTotalReq = this.TotalReq;
                    System.Threading.Thread.Sleep(1000);
                }
            });
        }

        private int _preTotalReq = 0;

        /// <summary>
        /// 统计项
        /// </summary>
        private List<TaskStatisticsItem> items;

        /// <summary>
        /// 获取总请求数
        /// </summary>
        public int TotalReq { get; private set; }

        /// <summary>
        /// 获取成功的请求数
        /// </summary>
        public int TotalReqSuccess { get; private set; }

        /// <summary>
        /// 获取失败的请求数
        /// </summary>
        public int TotalReqError { get; private set; }

        /// <summary>
        /// 当前用户数
        /// </summary>
        public int TotalUser { get; private set; }

        /// <summary>
        /// 每秒并发数
        /// </summary>
        public int Concurrent { get; private set; }

        /// <summary>
        /// 注册统计接口地址
        /// </summary>
        /// <param name="idx">序号</param>
        /// <param name="uri">接口地址</param>
        public void reg(int idx, string uri)
        {
            lock (this)
            {
                if (this.items.FirstOrDefault((item) => item.Index == idx) != null)
                {
                    return;
                }
                this.items.Add(new TaskStatisticsItem(idx, uri));
            }
        }

        /// <summary>
        /// 加入异常统计值
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="num"></param>
        public void error(int idx, int num = 1)
        {
            lock (this)
            {
                var item = this.items.FirstOrDefault((obj) => obj.Index == idx);
                if (item == null)
                {
                    return;
                }
                item.TotalReqError += num;
                item.TotalReq += num;
                this.TotalReq += num;
                this.TotalReqError += num;
            }
        }

        /// <summary>
        /// 加入成功统计值
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="num"></param>
        public void success(int idx, int num = 1)
        {
            lock (this)
            {
                var item = this.items.FirstOrDefault((obj) => obj.Index == idx);
                if (item == null)
                {
                    return;
                }
                item.TotalReqSuccess+= num;
                item.TotalReq += num;
                this.TotalReq += num;
                this.TotalReqSuccess += num;
            }
        }

        /// <summary>
        /// 添加用户数
        /// </summary>
        /// <param name="num"></param>
        public void addUser(int num = 1)
        {
            lock (this)
            {
                this.TotalUser += num;
            }
        }

        public void each(Action<TaskStatisticsItem> callBack)
        {
            lock (this)
            {
                foreach (var item in items)
                {
                    callBack(item);
                }
            }
        }   

        public void clear()
        {
            lock (this)
            {
                this.items = new List<TaskStatisticsItem>();
                this.TotalReqError = 0;
                this.TotalReq = 0;
                this.TotalReqSuccess = 0;
                this.TotalUser = 0;
                this.Concurrent = 0;
            }
        }
    }
}
