using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyCode_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("开始");
            var IsSeted = MemcachedHelper.Set("123", "456", DateTime.Now.AddMinutes(60));
            Console.WriteLine("写入成功");
            var mcValue = MemcachedHelper.Get("123");
            Console.WriteLine("读取到的值为：" + mcValue);

            var mc = MemcachedHelper.GetInstance();
            var s = mc.ExecuteGet("123");
            Console.ReadKey();
        }
    }
}
