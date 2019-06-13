using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bcar.model;
using Hup.MessageBus;
using Hup.MessageBus.Service;

namespace bcar.service
{
    public class orderService : Hup.MessageBus.Service.ServiceBase
    {
        public static Dictionary<string, orderinfo> riderTypeOne = new Dictionary<string, orderinfo>();
        public static Dictionary<string, orderinfo> riderTypeTwo = new Dictionary<string, orderinfo>();
        public static Dictionary<string, orderinfo> riderTypeThere = new Dictionary<string, orderinfo>();
        public override void RegisterServieceRequest(ServiceRegister Register)
        {
            Register.Register("riderType1", (Request parameter, ref Request result) =>
            {
                var request= (Request)parameter;
                orderinfo param =(orderinfo) request.Content;
                param.driverprice = param.price - Math.Round( param.price * double.Parse( CostService.Config["Profit"]),2);
                param.id = request.Head["KeyID"];
                riderTypeOne.Add(param.id, param);
                result.Content = createorder(param);
            });
            Register.Register("riderType2", (Request parameter, ref Request result) =>
            {
                var request = (Request)parameter;
                orderinfo param = (orderinfo)request.Content;
                param.driverprice = param.price - Math.Round(param.price * double.Parse(CostService.Config["Profit"]), 2);
                param.id = request.Head["KeyID"];
                riderTypeTwo.Add(param.id, param);
                result.Content = createorder(param);
            });
            Register.Register("riderType3", (Request parameter, ref Request result) =>
            {
                var request = (Request)parameter;
                orderinfo param = (orderinfo)request.Content;
                param.driverprice = param.price - Math.Round(param.price * double.Parse(CostService.Config["Profit"]), 2);
                param.id = request.Head["KeyID"];
                riderTypeThere.Add(param.id, param);
                result.Content = createorder(param);
            });
            Register.Register("riderType4", (Request parameter, ref Request result) =>
            {
                var request = (Request)parameter;
                orderinfo param = (orderinfo)request.Content;
                param.driverprice = param.price - Math.Round(param.price * double.Parse(CostService.Config["Profit"]), 2);
                param.id = request.Head["KeyID"];
                riderTypeThere.Add(param.id, param);
                result.Content = createorder(param);
            });
        }

        orders createorder(orderinfo param)
        {
            orders order = new orders();
            order.ordersInfo = Newtonsoft.Json.JsonConvert.SerializeObject(param);
            order.startlocation = param.startingPoint;
            order.endlocation = param.endingPoint;
            order.state = 0;
            order.userprice = (float)param.price;
            order.driverprice = (float)param.driverprice;
            order.orderType = param.riderType;
            order.routeid =int.Parse( param.routeid);
            order.StartTime = param.startDate;
            return order;
        }
    }
}
