namespace LibHipChat.Domain
{
    public enum ResultCode
    {
        OK,
        BadRequest = 400,
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