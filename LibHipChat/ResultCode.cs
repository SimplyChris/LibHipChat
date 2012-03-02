using System;
using System.Collections.Generic;

namespace LibHipChat
{
    public enum ResultCode
    {
        OK,
        BadRequest,
        Unauthorized,
        Forbidden,
        NotFound,
        NotAcceptable,
        InternalServerError,
        ServiceUnavailable                  
    }

    
//TODO: Not quite sure what I was doing here 
//    public class ResponseCodes
//    {
//        Dictionary<ResultCode,String> codes = new Dictionary<ResultCode, string>(<ResultCode.OK, "sdf">d, )  
//    }
}