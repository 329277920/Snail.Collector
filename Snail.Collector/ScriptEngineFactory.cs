using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snail.Collector
{
    /// <summary>
    /// 脚本引擎工厂
    /// </summary>
    public class ScriptEngineFactory
    {
        private Queue<V8ScriptEngine> _freeEngines;
        private Semaphore _sh;        
        public ScriptEngineFactory(int poolSize)
        {
            this._sh = new Semaphore(poolSize, poolSize);
            this._freeEngines = new Queue<V8ScriptEngine>();                       
        }

        
        public V8ScriptEngine NewScriptEngine()
        {
            this._sh.WaitOne(1);
            lock (this)
            {
                if (this._freeEngines.Count <= 0)
                {
                    return this.CreateScriptEngine();
                }
                return this._freeEngines.Dequeue();
            }             
        }

        public void Release(V8ScriptEngine scriptEngine)
        {
            this._sh.Release();
            lock (this)
            {
                this._freeEngines.Enqueue(scriptEngine);
            }
        }

        private V8ScriptEngine CreateScriptEngine()
        {
            return new V8ScriptEngine();
        }


    }
}
