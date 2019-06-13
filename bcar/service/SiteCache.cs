using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.service
{
    public class SiteCache
    {
        public IDbConnection db { get; set; }
        public SiteCache(IDbConnection con)
        {
            this.db = con;
        }
    }
}
