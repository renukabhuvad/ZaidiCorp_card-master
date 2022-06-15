using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZaidiCorp.Models
{
    public class pincode
    {
        public int pincodeno { get; set; }
        public int city_id { get; set; }
        public int state_id { get; set; }
        public string city_name { get; set; }
        public string state_name { get; set; }
    }
}