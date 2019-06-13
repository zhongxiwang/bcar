using bcar.service;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WxUilt;

namespace bcar.uilt
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class uiltT
    {
        /// <summary>
        /// 创建更新语句
        /// </summary>
        /// <param name="List"></param>
        /// <param name="tableName"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static string Update(JObject  List, string tableName,string where="" )
        {
            string result = "update "+tableName+" set ";
            foreach(var key in List)
                result += " " + key.Key + "='" + key.Value.ToString() + "',";
            result=result.TrimEnd(',');
            return result + " " + where;
        }

        /// <summary>
        /// 创建删除sql
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static string Delete(string tableName,string where)
        {
            return "delete from " + tableName + " where " + where;
        }
        /// <summary>
        /// 翻页sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static string limit(string sql, int pageIndex,int pageSize)
        {
            var res = pageIndex * pageSize;
            return sql + " limit " + res + "," + (res+pageSize);
        }
        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string Count(string sql)
        {
            return "select count(1) from (" + sql + ") thisisacount";
        }
        /// <summary>
        /// 百度地图距离计算公式
        /// </summary>
        /// <param name="lat_a">29.000</param>
        /// <param name="lng_a">118.223</param>
        /// <param name="lat_b"></param>
        /// <param name="lng_b"></param>
        /// <returns></returns>
        public static double getDistance(double lat_a, double lng_a, double lat_b, double lng_b)
        {
            double pk = 180 / 3.14169;
            double a1 = lat_a / pk;
            double a2 = lng_a / pk;
            double b1 = lat_b / pk;
            double b2 = lng_b / pk;
            double t1 = Math.Cos(a1) * Math.Cos(a2) * Math.Cos(b1) * Math.Cos(b2);
            double t2 = Math.Cos(a1) * Math.Sin(a2) * Math.Cos(b1) * Math.Sin(b2);
            double t3 = Math.Sin(a1) * Math.Sin(b1);
            double tt = Math.Acos(t1 + t2 + t3);
            return 6371000 * tt;
        }
        /// <summary>
        /// 发送微信消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="message"></param>
        /// <param name="openid"></param>
        public static void SendWxMessage(TokenService token,string message,string openid)
        {
            string mstr= "{\"touser\":\""+openid+"\",\"msgtype\":\"text\",\"text\":{\"content\":\""+message+"\"}}";
            var task= Request.Post("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token="+token, mstr);
        }
    }
}
