using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class ErrorResponse
    {
        public int? Code { get; set; }
        public object  Message { get; set; }
    }
}
