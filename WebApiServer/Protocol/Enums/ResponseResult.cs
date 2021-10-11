namespace Common.Protocol.Enums
{
    public enum ResponseResult
    {
        NONE = 0,
        Success = 1001,
        
        // Critical 2001 ~ 3000
        BadRequest = 2001,
        InvalidAccessToken,        
        SessionWorking,
        
        // Error 3001 ~ 4000
        InternalSystemError = 3001,
        
        // Warning 4001 ~ 5000
    }
}