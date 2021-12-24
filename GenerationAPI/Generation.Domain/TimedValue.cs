using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Generation.Domain
{
    public class TimedValue : Entity
    {
        public DateTime Timestamp { get; set; }
        public bool? Good { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public string Value { get; set; }

        public Guid PowerPlantId { get; set; }
        public PowerPlant PowerPlant { get; set; }

        public TimedValue(Guid powerPlantId, DateTime timestamp, bool? good, string value)
        {
            if (powerPlantId == null
                || powerPlantId == Guid.Empty
                || string.IsNullOrEmpty(value))
            {
                throw new Exception("Fields are not valid to create a new timed value.");
            }

            PowerPlantId = powerPlantId;
            Timestamp = timestamp;
            Good = good;
            Value = value;
        }

        protected TimedValue()
        {

        }

        public void SetFields(Guid powerPlantId, DateTime timestamp, bool? good, string value)
        {
            if (powerPlantId == null
                || powerPlantId == Guid.Empty
                || string.IsNullOrEmpty(value))
            {
                throw new Exception("Fields are not valid to update.");
            }

            PowerPlantId = powerPlantId;
            Timestamp = timestamp;
            Good = good;
            Value = value;
        }
    }
}
