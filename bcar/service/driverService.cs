using bcar.model;
using bcar.Socket;
using Hup.MessageBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.service
{
    public class driverService : Hup.MessageBus.Service.ServiceBase
    {
        public static Dictionary<string, driverlocation> driverinfo = new Dictionary<string, driverlocation>();
        static bool isl = false;
        public static bool isLock
        {
            get { return isl; }
            set
            {
                if (action != null) { action(); action = null; }
                isl = value;
            }
        }
        public static Action action = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Register"></param>
        public override void RegisterServieceRequest(Hup.MessageBus.Service.ServiceRegister Register)
        {
            Register.Register("uploadLocation", (Request parm, ref Request results) => {
                driverlocation result = new driverlocation();
                var parameter = (Hup.MessageBus.Request)parm;
                result.title = "快车";
                result.content = parameter.Head["KeyID"];
                result.pointy = double.Parse(parameter.Head["latitude"]);
                result.pointx = double.Parse(parameter.Head["longitude"]);

                Action t1 = () =>
                 {
                     if (driverinfo.ContainsKey(parameter.Head["wxcount"]))
                         driverinfo[parameter.Head["wxcount"]] = result;
                     else driverinfo.Add(parameter.Head["wxcount"], result);
                     var state = parameter.AddService();
                     result.status = int.Parse(state) > 0 ? 0 : 1;
                     
                 };
                if (isLock)
                    action += t1;
                else t1();
                results = parameter;
            });

            Register.Register("orderMessage", (Request parameter, ref Request results) => {
                var parm = (Hup.MessageBus.Request)parameter;
                UseSocket.CreateSocket().Send(((Request)parameter),"");
            });
        }


    }
}
