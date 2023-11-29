namespace QPL.BL
{
    public class BaseHandlerRequest
    {
        public BaseHandlerRequest()
        {
            Retry = 1;
            RetryOnSqlException = false;
        }

        public int Retry { get; set; }
        public bool RetryOnSqlException { get; set; }
    }
}