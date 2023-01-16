namespace BiTrend.Services;

internal class AppServiceProvider
{
    private static AppServiceProvider _instance;
    private const string _serverRootUrl = "http://192.168.2.33:45455";
    HttpClient httpClient;

    private AppServiceProvider() 
    {
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_serverRootUrl);
    }

    public static AppServiceProvider Instance()
    {
        if (_instance == null)
            _instance = new AppServiceProvider();

        return _instance;
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        await Task.Delay(3000);
            try
            {
                var response = await httpClient.GetStringAsync(httpClient.BaseAddress + "api/Category/GetCategories");
                var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(response);

                return categories;
            }
            catch (Exception ex)
            {
                var result = new ResponseHttpMessage();
                result.StatusCode = 500;
                result.Message = ex.Message;

                return null;
            }
    }
}
