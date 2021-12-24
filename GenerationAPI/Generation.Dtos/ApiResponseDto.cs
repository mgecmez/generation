using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Dto
{
    public class ApiResponseDto<T>
    {
        public T Data { get; set; }

        public static ApiResponseDto<T> Set(T value)
        {
            return new ApiResponseDto<T>() { Data = value };
        }
    }
}
