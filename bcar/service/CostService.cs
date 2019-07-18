using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using bcar.model;
using bcar.uilt;
using Dapper;
using log4net;
using Newtonsoft.Json.Linq;

namespace bcar.service
{

    delegate void CostPeilline(Dictionary<string, string> list);
    public class CostService
    {
        IDbConnection db { get; set; }
        userCache uca { get; set; }
        ILog log { get; set; }
        public CostService(IDbConnection db,userCache uc,ILog log)
        {
            this.db = db;
            this.uca = uc;
            this.log = log;
            if (Config.Count == 0) reloadConfig();
        }

        public static Dictionary<string, string> Config = new Dictionary<string, string>();

        public void reloadConfig()
        {
            var list= this.db.Query< Config>("select * from configuration");
            foreach(var key in list)
                Config.Add(key.keyv, key.valuev);
        }
        /// <summary>
        /// 计算成为代理商收益
        /// </summary>
        /// <param name="list"></param>
        public bool proxy(string wxcount)
        {
            var p= int.Parse(Config["proxycost"]);
            var user= this.uca.read(wxcount);
            if (this.db.State != ConnectionState.Open) this.db.Open();
            var transaction = this.db.BeginTransaction();
            try
            {
                if (!string.IsNullOrWhiteSpace(user.recommender))
                {
                    var recommender= this.uca.read(user.recommender);
                    JObject obj = new JObject();
                    var prc = double.Parse(Config["proxyRecommender"]);
                    var reresult= Math.Round(p * prc, 2);
                    var nwobill = Math.Round(recommender.bill + reresult,2);
                    obj.Add("bill", nwobill);
                    int result = this.db.Execute(uiltT.Update(obj, "userinfo", " where wxCount='" + recommender.recommender+"'"));
                    if (result != 1)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
                if (user.bill >= p)
                {
                    JObject obj = new JObject();
                    var nwobill = Math.Round(user.bill - p, 2);
                    obj.Add("bill", nwobill);
                    obj.Add("proxy", 1);
                    int result = (int)this.db.Execute(uiltT.Update(obj, "userinfo", " where recommender='" + user.recommender + "'"));
                    transaction.Commit();
                    this.uca.update(wxcount);
                    this.uca.update(user.recommender);
                    return true;
                }else transaction.Rollback();
            }
            catch(Exception e)
            {
                this.log.Error(e.Message);
                transaction.Rollback();
            }
            return false;
        }

        /// <summary>
        /// 计算司机收益，我方收益 Profit  driverProfit
        /// </summary>
        /// <param name="list"></param>
        public void Profit(Dictionary<string,string> list)
        {
            var p = double.Parse(list["price"]);
            double profit = double.Parse(Config["Profit"]);
            var result = Math.Round( p * profit,2);
            list.Add("Profit", result.ToString());
            list.Add("driverProfit",Math.Round(p - result,2).ToString());
        }
    }
}
