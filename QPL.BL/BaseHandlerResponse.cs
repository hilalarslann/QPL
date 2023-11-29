namespace QPL.BL
{
    public enum HandlerResponseCode
    {
        Success = 0,
        Fail = 1,
        BadRequest = 2,
        TechnicalError = 3,
        NotProcessed = 4
    }

    public class BaseHandlerResponse
    {
        public HandlerResponseCode ResponseCode { get; set; }
        public string Message { get; set; }
        public string ResponseCodeStr => ResponseCode.ToString();
    }
}