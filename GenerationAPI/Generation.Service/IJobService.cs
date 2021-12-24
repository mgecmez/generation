using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Service
{
    public interface IJobService
    {
        Task ReccuringJob();
    }
}
