namespace Services.API
{
    public class ApiBase
    {
        protected string RequestUrl(string resource)
            => Constants.ServerUrl + resource;
    }
}