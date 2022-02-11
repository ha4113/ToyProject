namespace Common.Protocol.Enums
{
    public enum ResponseResult
    {
        NONE = 0,
        Success = 1001,
        
        // Critical 2001 ~ 3000
        CRITICAL = 2000, 
        BadRequest,
        InvalidAccessToken,        
        SessionWorking,
        
        InvalidResponseStream,
        
        // Error 3001 ~ 4000
        ERROR = 3000,
        InternalSystemError,
       
        // Warning 4001 ~ 5000
        WARNING = 4000,
    }
}