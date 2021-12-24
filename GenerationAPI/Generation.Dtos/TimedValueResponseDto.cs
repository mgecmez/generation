using System;
using System.Collections.Generic;
using System.Text;

namespace Generation.Dto
{
    public class TimedValuesResponseDto
    {
        public List<TimedValueResponseDto> Items { get; set; } = new List<TimedValueResponseDto>();
    }

    public class TimedValueResponseDto
    {
        public DateTime Timestamp { get; set; }
        public bool? Good { get; set; }
        public object Value { get; set; }
    }
}
