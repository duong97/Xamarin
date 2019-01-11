using System;
using System.Collections.Generic;
using System.Text;

namespace ProductChecker.Models
{
    class Product
    {
        public String product_name { get; set; }

        public String quantity { get; set; }

        public String generic_name { get; set; }

        public String created_datetime { get; set; }

        public String creator { get; set; }

        public String image_url { get; set; }
    }
}
