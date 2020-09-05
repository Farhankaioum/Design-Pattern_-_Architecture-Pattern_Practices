﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Versioning.WebApi.Controllers.v2
{
    [ApiVersion("2.0")]
   // [Route("api/v{version:apiVersion}/[controller]")] // Url based api versioning
   [Route("api/[controller]")] // Query based routing
    [ApiController]
    public class DataController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "data from api v2";
        }
    }
}

