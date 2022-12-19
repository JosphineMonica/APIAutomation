using RestSharp;
using TodoAPITests.DTO;
using Newtonsoft.Json;

namespace TodoAPITests
{
    public class APITestMethods
    {
        private const string BaseURL = "http://localhost/";
        //public string GetItems()
        //{
        //    var restClient = new RestClient(BaseURL);
        //    var restRequest = new RestRequest("/", Method.Get);
        //    restRequest.AddHeader("Accept", "application/json");
        //    restRequest.RequestFormat = DataFormat.Json;

        //    RestResponse response = restClient.Execute(restRequest);
        //    var content = response.Content;

        //    //var users = JsonConvert.DeserializeObject<TodoDTO>(content);
        //    return content.ToString();
        //}
    }
  
}
