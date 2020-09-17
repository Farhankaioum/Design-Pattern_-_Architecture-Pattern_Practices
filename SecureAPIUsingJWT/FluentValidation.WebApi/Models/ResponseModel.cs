using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidation.WebApi.Models
{
    public class ResponseModel
    {
        public ResponseModel()
        {

        }
        public bool IsValid { get; set; }
        public List<string> ValidationMessage { get; set; }
    }
}
