using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.IDE
{
    /// <summary>
    /// 任务统计项
    /// </summary>
    public class TaskStatisticsItem
    {
        /// <summary>
        /// 统计项索引号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 统计项接口地址
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// 总请求数
        /// </summary>
        public int TotalReq { get; set; }

        /// <summary>
        /// 成功请求数
        /// </summary>
        public int TotalReqSuccess { get; set; }

        /// <summary>
        /// 失败请求数
        /// </summary>
        public int TotalReqError { get; set; }

        /// <summary>
        /// 第一次请求时间
        /// </summary>
        public DateTime FirstReqTime { get; set; }

        /// <summary>
        /// 最后一次请求时间
        /// </summary>
        public DateTime EndReqTime { get; set; }

        /// <summary>
        /// 获取接口每次请求的时间
        /// </summary>
        public List<TimeSpan> ExecTimes { get; private set; }

        /// <summary>
        /// 记录所有统计的每秒处理请求数
        /// </summary>
        public List<int> AllConcurrent { get; private set; }

        /// <summary>
        /// 每秒并发处理请求数
        /// </summary>
        public int Concurrent
        {
            get; private set;
        }
       
        public TimeSpan MinTime { get; private set; }

        public TimeSpan MaxTime { get; private set; }

        /// <summary>
        /// 平均响应时间
        /// </summary>
        public TimeSpan AvgTime
        {
            get
            {
                if (this.ExecTimes.Count <= 0)
                {
                    return new TimeSpan(0);
                }
                lock (this)
                {
                    long ticks = 0L;
                    this.ExecTimes.ForEach(item =>
                    {
                        ticks += item.Ticks;
                    });
                    return new TimeSpan(ticks / this.ExecTimes.Count);
                }
            }
        }

        public TaskStatisticsItem(int idx, string uri)
        {
            this.Index = idx;
            this.Uri = uri;
            this.FirstReqTime = DateTime.MinValue;
            this.EndReqTime = DateTime.MinValue;
            this.ExecTimes = new List<TimeSpan>();
            this.AllConcurrent = new List<int>();
        }

        internal void start()
        {
            //lock (this)
            //{
            //    if (this.FirstReqTime == DateTime.MinValue)
            //    {
            //        this.FirstReqTime = DateTime.Now;
            //    }
            //}
            if (this.FirstReqTime == DateTime.MinValue)
            {
                this.FirstReqTime = DateTime.Now;
            }
            CallContext.SetData(Key_Exec_Time, DateTime.Now);
        }

        internal void end()
        {
            //lock (this)
            //{
            //    this.EndReqTime = DateTime.Now;
            //}
            this.EndReqTime = DateTime.Now;

            var objTime = CallContext.GetData(Key_Exec_Time);
            if (objTime != null)
            {
                var startTime = (DateTime)objTime;
                if (startTime == DateTime.MinValue)
                {
                    return;
                }
                var time = DateTime.Now - startTime;
                lock (this)
                {
                    this.ExecTimes.Add(time);
                }
                if (this.MinTime == TimeSpan.MinValue || this.MinTime > time)
                {
                    this.MinTime = time;
                }
                if (this.MaxTime == TimeSpan.MinValue || this.MaxTime < time)
                {
                    this.MaxTime = time;
                }
                CallContext.SetData(Key_Exec_Time, DateTime.MinValue);
            }
        }
       

        private int _preTotalReq = 0;
        internal void setConcurrent()
        {
            lock (this)
            {
                this.Concurrent = this.TotalReq - this._preTotalReq;
                this._preTotalReq = this.TotalReq;
                this.AllConcurrent.Add(this.Concurrent);
            }
        }

        private const string Key_Exec_Time = "TaskStatisticsItem_Time";
    }
}
