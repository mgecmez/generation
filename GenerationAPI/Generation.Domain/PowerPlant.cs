using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Domain
{
    public class PowerPlant : Entity
    {
        [MaxLength(50)]
        public string WebId { get; set; }

        public ICollection<TimedValue> TimedValues { get; set; } = new List<TimedValue>();

        public PowerPlant(string webId)
        {
            if (string.IsNullOrEmpty(webId)
                || webId.Length > 50)
            {
                throw new Exception("Fields are not valid to create a new powerplant.");
            }

            WebId = webId;
        }

        protected PowerPlant()
        {

        }

        public void SetFields(string webId)
        {
            if (string.IsNullOrEmpty(webId)
                || webId.Length > 50)
            {
                throw new Exception("Fields are not valid to update.");
            }

            WebId = webId;
        }
    }
}
