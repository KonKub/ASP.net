using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemperatureManager
{
    public class TemperatureValue
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Temperature { get; set; }
    }
}
