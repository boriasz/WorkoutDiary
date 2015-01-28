using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WebApplication9.Models
{
    public class Workout
    {
        public int WorkoutID { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public Double Distance { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public String Place { get; set; }
        public String Note { get; set; }
        public int RunnerID { get; set; }
        public Runner Runner { get; set; }

    }
}
