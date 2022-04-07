using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;


//Написать свой контроллер и методы в нем, которые бы предоставляли следующую функциональность:
//Возможность сохранить температуру в указанное время
//Возможность отредактировать показатель температуры в указанное время
//Возможность удалить показатель температуры в указанный промежуток времени
//Возможность прочитать список показателей температуры за указанный промежуток времени


namespace TemperatureManager.Controllers
{
    [Route("api")]
    [ApiController]
    public class CRUDTemperatures : ControllerBase
    {
        private readonly ValuesHolder _holder;

        public CRUDTemperatures(ValuesHolder holder)
        {
            _holder = holder;
        }

        //Сохранение температуры в указанное время
        [HttpPost("create")]
        public IActionResult Create([FromQuery] string dateTime, string tmpr)
        {
            try
            {
                _holder.Add(DateTime.Parse(dateTime), Convert.ToInt32(tmpr));
                return Ok();
            }
            catch
            {
                return NoContent();
            }
        }

        //Прочитать весь список показателей температуры
        [HttpGet("read")]
        public IActionResult Read()
        {
            return Ok(_holder.Get());
        }

        //Прочитать список показателей температуры за указанный промежуток времени
        [HttpGet("readper")]
        public IActionResult ReadPeriod([FromQuery] string fromDate, [FromQuery] string toDate)
        {
            return Ok(_holder.Get(fromDate, toDate));
        }

        //Редактирование показателя температуры в указанное время
        [HttpPut("update")]
        public IActionResult Update([FromQuery] string Id, [FromQuery] string Value)
        {
            int id;

            try
            {
                id = Int32.Parse(Id);
                for (int i = 0; i < _holder.Values.Count; i++)
                {
                    if (_holder.Values[i].Id == id)
                    {
                        int T = Convert.ToInt32(Value);
                        _holder.Values[i].Temperature = T;
                        return Ok();
                    }
                }
            }
            catch
            {
            }
            return NoContent();
        }

        //Удаление показателя температуры по его ID
        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string Id)
        {
            int id;

            try
            {
                id = Int32.Parse(Id);
                _holder.Values = _holder.Values.Where(w => w.Id != id).ToList();
                return Ok();
            }
            catch
            {
                return NoContent();
            }
        }

        //Удаление показателей температуры в указанный промежуток времени
        [HttpDelete("deleteper")]
        public IActionResult DeletePer([FromQuery] string fromDate, [FromQuery] string toDate)
        {
            try
            {
                _holder.Values = _holder.Values.Where(w => (w.Date < DateTime.Parse(fromDate)) ||
                                                           (w.Date > DateTime.Parse(toDate)))
                                                           .ToList();
                return Ok();
            }
            catch
            {
                return NoContent();
            }
        }
    }

}
