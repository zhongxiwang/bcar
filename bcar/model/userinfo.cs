using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.model
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class userinfo
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 微信账号
        /// </summary>
        public string wxCount { get; set; }
        /// <summary>
        /// 用户账号金额
        /// </summary>
        public double bill { get; set; }

        /// <summary>
        /// 介绍人
        /// </summary>
        public string recommender { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public int userid { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string headimgurl { get; set; }

        /// <summary>
        /// 二维码
        /// </summary>
        public string qrCode { get; set; }

        DateTime ct = new DateTime();

        public DateTime createtime { get; set; }

        /// <summary>
        /// 代理商
        /// </summary>
        public int proxy { get; set; }

        public string formatTime { get { return createtime.ToString("yyyy年MM月dd日"); } }
        /// <summary>
        /// 插入用户数据
        /// </summary>
        /// <returns></returns>
        public string Insert()
        {
            var t= "insert into userinfo(username,wxCount,recommender,userid,headimgurl,qrCode)values('" + username + "','" + wxCount + "','"+recommender+ "','"+userid+"','"+headimgurl+"','"+ qrCode+"')";
            return t;
        }
    }
}
