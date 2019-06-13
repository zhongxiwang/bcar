using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.model
{
    public class Result
    {
        public string msg { get; set; }
        public int code { get; set; }
        public dynamic data { get; set; }
        public long count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Result Run(Action<Result> action)
        {
            var result = new Result();
            result.code = 1;
            try
            {
                action(result);
            }
            catch(Exception e)
            {
                result.msg = e.Message;
                result.code = -1;
            }
            return result;
        }
    }
}
