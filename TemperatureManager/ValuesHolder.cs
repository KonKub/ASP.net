using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemperatureManager
{
    public class ValuesHolder
    {
        public List<TemperatureValue> Values = new List<TemperatureValue>();
        private int _idCnt = 0;

        public List<TemperatureValue> Get()
        {
            return Values;
        }

        public List<TemperatureValue> Get(string fromDate, string toDate)
        {
            try
            {
                return Values.Where(w => (w.Date >= DateTime.Parse(fromDate)) &&
                                         (w.Date <= DateTime.Parse(toDate)))
                                         .ToList();
            }
            catch
            {
                return null;
            }
        }

        public void Add(DateTime ForDateTime, int Temperature)
        {
            TemperatureValue Value = new TemperatureValue();

            _idCnt++;
            Value.Id = _idCnt;
            Value.Date = ForDateTime;
            Value.Temperature = Temperature;
            Values.Add(Value);
        }
    }
}
