using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using bcar.model;
using bcar.uilt;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Cors;
using bcar.Socket;

namespace bcar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("any")]
    [Authentication]
    public class irouteController : ControllerBase
    {
        public IDbConnection db { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public irouteController(IDbConnection db)
        {
            this.db = db;
        }

        // GET: api/iroute
        [HttpGet]
        public IEnumerable<iroute> Get()
        {
            return this.db.Query<iroute>("select * from iroute");
        }

        // GET: api/iroute/5
        [HttpGet("{id}", Name = "Get")]
        public iroute Get(int id)
        {
            return this.db.QueryFirst<iroute>("select * from iroute id="+id);
        }

        // POST: api/iroute
        [HttpPost]
        public void Post([FromBody] iroute value)
        {
            this.db.Execute(value.Insert());
        }

        // PUT: api/iroute/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] JObject value)
        {
            this.db.Execute(uiltT.Update(value, "iroute", " where id=" + id));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.db.Execute(uiltT.Delete( "iroute", "id=" + id));
        }
    }
}
