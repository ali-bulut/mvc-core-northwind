using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result:IResult
    {
        //burada this'in çalışma mantığı eğer çift parametreli ctor'u çağırırsak tek parametreli
        //ctoru da çağırmış oluruz. Yani success kısmını ordan alırız message'ı da çift parametreli ctor
        //sayesinde doldururuz.
        public Result(bool success, string message):this(success)
        {
            Message = message;
            //bu alttaki kod yerine direkt yukarıda this kısmını da kullanabiliriz aynı şey
            //Success = success;
        }
        public Result(bool success)
        {
            Success = success;
        }
        public bool Success { get; }
        public string Message { get; }
    }
}
