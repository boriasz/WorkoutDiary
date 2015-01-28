using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WebApplication9.Models
{
    public class Shoes
    {
        public int ShoesID { get; set; }
        public String Brand { get; set; }
        public String Model { get; set; }
        [DataType(DataType.Currency)]
        public Double Price { get; set; }
        [Display(Name="Runner's weight")]
        public Double RunnersWeight { get; set; }
        public int RunnerID { get; set; }
        public Runner Runner { get; set; }

    }
}
