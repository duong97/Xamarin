using System;
using System.Collections.Generic;
using System.Text;

namespace ProductChecker.Models
{
    class JsonData
    {
        public String code { get; set; }

        public String status { get; set; }

        public String status_verbose { get; set; }

        public Product product { get; set; }
    }
}
