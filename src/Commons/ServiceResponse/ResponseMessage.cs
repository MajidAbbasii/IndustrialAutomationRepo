using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.ServiceResponse;

public class ResponseMessage
{
    public ResponseMessage()
    {
        this.Result = new Result();
    }
    public Result Result { get; set; }
}

public class Result
{
    public Result()
    {
        this.Message = "";
        this.Description = "";
        this.Code = 0;
    }
    public string Message { get; set; }
    public string Description { get; set; }
    public int Code { get; set; }
}