namespace Common.Protocol.Network
{
    public enum Result
    {
        NONE = 0,
        Success = 1,
        
        // Critical 1001 ~ 2000
        BadRequest = 1001,
        InvalidAccessToken,        
        SessionWorking,
        
        // Error 2001 ~ 3000
        InternalSystemError = 2001,
        
        // Warning 3001 ~ 4000
    }
}