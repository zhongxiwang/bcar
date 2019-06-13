using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper;
using log4net;
using Microsoft.AspNetCore.Cors;
using bcar.Socket;

namespace bcar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authentication]
    [EnableCors("any")]
    public class FileupController : ControllerBase
    {
        public IDbConnection connection { get; set; }
        public IConfiguration Config { get; set; }
        public ILog log { get; set; }
        public FileupController(IDbConnection con,IConfiguration conf,ILog log)
        {
            this.connection = con;
            this.Config = conf;
            this.log = log;
        }
        [HttpPost]
        [Route("upload")]
        public object upload()
        {
            
            var path = System.IO.Directory.GetCurrentDirectory();
            string[] result = new string[this.Request.Form.Files.Count];
            int i = 0;
            if (this.connection.State != ConnectionState.Open) this.connection.Open();
            var trans = this.connection.BeginTransaction();
            try
            {
                foreach (var key in this.Request.Form.Files)
                {
                    model.upload tmp = new model.upload();
                    tmp.filename = key.FileName;
                    byte[] buffer = new byte[key.Length];
                    key.OpenReadStream().Read(buffer, 0, buffer.Length);
                    System.IO.File.WriteAllBytes(path + "/data/image/" + tmp.id, buffer);
                    result[i++] = tmp.id;
                    this.connection.ExecuteScalar(tmp.Insert());
                }
                trans.Commit();
            }
            catch(Exception e)
            {
                this.log.Error(e.Message);
                trans.Rollback();
                return new { msg="", code = 2 };
            }
            return new { msg = string.Join(',', result), code = 0 } ;
        }

        [HttpGet()]
        [Route("image")]
        public FileResult Image(string id)
        {
            var path = System.IO.Directory.GetCurrentDirectory();
            return File(System.IO.File.ReadAllBytes( path + "/data/image/" + id), "image/png");// System.IO.File.ReadAllBytes(path + "/data/image/" + id);
        }
    }
}