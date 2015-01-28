using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class Runner
    {
        public int RunnerID { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        [Range(1900,2015)]
        public int YOB { get; set; }
        public Double Weight { get; set; }
        public Double Mileage { get; set; }
        [Display(Name="Number of Training")]
        public int NumberOfTraining { get; set; }
        public List<Workout> Workout { get; set; }
        public List<Shoes> Shoes { get; set; }

    }
}