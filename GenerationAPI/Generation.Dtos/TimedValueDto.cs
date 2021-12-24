using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Dto
{
    public class TimedValueDto
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public bool? Good { get; set; }
        public object Value { get; set; }
        public Guid PowerPlantId { get; set; }
    }
}
