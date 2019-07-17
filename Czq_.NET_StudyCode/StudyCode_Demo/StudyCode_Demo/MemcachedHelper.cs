using Enyim.Caching;
using Enyim.Caching.Memcached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyCode_Demo
{
    public class MemcachedHelper
    {
        private static MemcachedClient MemClient;
        static readonly object padlock = new object();
        /// <summary>
        /// 线程安全的单例模式
        /// </summary>
        /// <returns></returns>
        public static MemcachedClient GetInstance()
        {
            if (MemClient == null)
            {
                lock (padlock)
                {
                    if (MemClient == null)
                    {
                        MemClientInit();
                    }
                }
            }
            return MemClient;
        }
        static void MemClientInit()
        {
            try
            {
                MemClient = new MemcachedClient();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 插入指定值
        /// </summary>
        /// <param name="key">缓存名称</param>
        /// <param name="value">缓存值</param>
        /// <param name="dateTime">过期时间</param>
        /// <returns>返回是否成功</returns>
        public static bool Set(string key,string value,DateTime dateTime)
        {
            MemcachedClient mc = GetInstance();
            var data = mc.Get<string>(key);
            if (data == null)
            {
                var result= mc.Store(StoreMode.Add, key, value, dateTime);
                return result;
            }
            else
            { 
                var result = mc.Store(StoreMode.Replace, key, value, dateTime);
                return result;
            }
        }
        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            MemcachedClient mc = GetInstance();
            return mc.Get(key);
        }
        /// <summary>
        /// 删除指定缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Remove(string key)
        {
            MemcachedClient mc = GetInstance();
            return mc.Remove(key);
        }
        /// <summary>
        /// 清空服务器上的缓存
        /// </summary>
        public static void FlushCache()
        {
            MemcachedClient mc = GetInstance();
            mc.FlushAll();
        }
    }
}
