using System.Net;/// HttpStatusCode
using System.Net.Http.Json; // GetFromJsonAsync,PostAsJsonAsync
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Testing; // await using
using Microsoft.EntityFrameworkCore;// DBContext
using Microsoft.EntityFrameworkCore.Storage; // InmemoryDatabaseRoot 
using Microsoft.Extensions.DependencyInjection; //Services.AddDbContext<TodoDb>
using Microsoft.Extensions.DependencyInjection.Extensions; //Service.Removeall
using Microsoft.Extensions.Hosting; //IHost
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Xunit;


namespace TodoAPITests.Tests
{
    public class APITodoTests
    {
        public HttpClient client = new TodoApplication().CreateClient();
        public async Task API_GetMethodValidation(string endpoint, string responseContent, int StatusCode = 200)
        {
            var response = await client.GetAsync(endpoint);
            Assert.Equal(responseContent, await response.Content.ReadAsStringAsync());
            Assert.Equal((HttpStatusCode)StatusCode, response.StatusCode);
        }
        public async Task API_PostMethodValidation(string endpoint, Todo todoList, int StatusCode = 201)
        {

            var response = await client.PostAsJsonAsync(endpoint, todoList);
            Assert.Equal((HttpStatusCode)StatusCode, response.StatusCode);
        }

        public async Task API_PutMethodValidation(string endpoint, Todo todoList, int ID, int StatusCode = 204)
        {

            var response = await client.PutAsJsonAsync($"{endpoint}/{ID}", todoList);
            Assert.Equal((HttpStatusCode)StatusCode, response.StatusCode);
        }

        public async Task API_DeleteMethodValidation(string endpoint, string ID, int StatusCode = 200)
        {
            var delResponse = await client.DeleteAsync($"{endpoint}/{ID}");
            Assert.Equal((HttpStatusCode)StatusCode, delResponse.StatusCode);

            if (delResponse.StatusCode == HttpStatusCode.OK)
            {
                var getResponse = await client.GetAsync($"{endpoint}/{ID}");
                Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
            }
        }
       
        public async Task API_GETRequestToVerifyPayload(string endpoint, Todo todoList)
        {
            var newPayloadItems = await client.GetFromJsonAsync<List<Todo>>(endpoint);
            foreach (var newList in newPayloadItems)
            {
                Assert.Equal(todoList.Id, newList.Id);
                Assert.Equal(todoList.Name, newList.Name);
                Assert.Equal(todoList.IsComplete, newList.IsComplete);
            }
        }
        
        public async Task API_SchemaValidation(string endpoint, Todo todoList, string fileName)
        {
            var response = await client.PostAsJsonAsync(endpoint, todoList);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            //SchemaValidaton

            if(new StreamReader(fileName).ReadLine() ==null)
            {
                Console.WriteLine("unavailable schema");
            }
            else 
            { 
                var strFileData = new StreamReader(fileName);
                JSchema inputSchema = JSchema.Parse(strFileData.ReadToEnd());

                var responseData = await response.Content.ReadAsStringAsync();
                var dynamicObj = JObject.Parse(responseData);
 
                dynamicObj.IsValid(inputSchema, out IList<string> messages);

                foreach (var item in messages)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }

    class TodoApplication : WebApplicationFactory<Program>
    {
       protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<TodoDb>));

                services.AddDbContext<TodoDb>(options =>
                    options.UseInMemoryDatabase("Testing", root));
            });

            return base.CreateHost(builder);
        }
    }


}