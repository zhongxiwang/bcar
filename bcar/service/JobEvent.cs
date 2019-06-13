using Quartz;
using Quartz.Listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace bcar.service
{
    public class JobEvent: JobListenerSupport
    {
        public override string Name { get; }

        public JobEvent() { this.Name = "jobevent"; }

        public override Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default(CancellationToken))
        {
            context.Scheduler.DeleteJob(context.JobDetail.Key);
            return base.JobWasExecuted(context, jobException, cancellationToken);
        }
    }
}
