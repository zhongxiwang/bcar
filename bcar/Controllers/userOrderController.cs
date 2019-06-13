using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bcar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("any")]
    public class userOrderController : ControllerBase
    {
        public userOrderController()
        {

        }
        
    }
}