
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.service
{
    class DynamicTask : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            JobAction action= (JobAction)context.JobDetail.JobDataMap["jobaction"];
            return action(context);
        }
        
    }
}
