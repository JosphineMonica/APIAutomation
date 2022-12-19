using TechTalk.SpecFlow;
using TodoAPITests.Tests;

namespace TodoAPITests.StepDefinitions
{
    [Binding]
  
    public class TodoAPITestsStepDefinitions
    {
        public static string _strTestcase = String.Empty;
        public string _endpoint = string.Empty;
        public APITodoTests apiTodoTests = new APITodoTests();

        [Given(@"I set api '(.*)' for '(.*)'")]
        public void GivenISetApiFor(string endpoint, string testcase)
        {
            _endpoint = endpoint;
            _strTestcase = testcase;
        }

        [Then(@"I make GET Request and verify the valid HTTPS Status code and '(.*)'")]
        public async Task ThenIMakeGETRequestAndVerifyTheValidHTTPSStatusCodeAnd(string responseContent)
        {
            await apiTodoTests.API_GetMethodValidation(_endpoint, responseContent);
        }


        [Then(@"I make GET Request '(.*)' and verify '(.*)'")]
        public async Task ThenIMakeGETRequestAndVerify(string _endpoint, string responseContent)
        {
            await apiTodoTests.API_GetMethodValidation(_endpoint, responseContent);
        }

        [Then(@"I make GET Request '(.*)' to verify '(.*)' and '(.*)'")]
        public async Task ThenIMakeGETRequestToVerifyAnd(string _endpoint, string responseContent, int Statuscode)
        {
            await apiTodoTests.API_GetMethodValidation(_endpoint, responseContent, Statuscode);
        }


        /// <summary>
        /// /
        /// </summary>
        /// <param name="payloadData"></param>
        /// <returns></returns>

        [When(@"I make POST Request using '(.*)' and verify POST Creation HTTPS Status Code")]
        [Given(@"I make POST Request using '(.*)' and verify POST Creation HTTPS Status Code")]
        public async Task GivenIMakePOSTRequestUsingAndVerifyPOSTCreationHTTPSStatusCode(string payloadData)
        {
            await apiTodoTests.API_PostMethodValidation(_endpoint, ConvertPayloadToList(payloadData));
        }

        [When(@"I send GET request to verify new todo list'(.*)'")]
        [Then(@"I send GET request to verify new todo list'(.*)'")]
        public async Task ThenISendGETRequestToVerifyNewTodoList(string payloadData)
        {
            await apiTodoTests.API_GETRequestToVerifyPayload(_endpoint, ConvertPayloadToList(payloadData));
        }

        [Then(@"I send GET request to verify '(.*)' using '(.*)'")]
        public async Task ThenISendGETRequestToVerifyUsing(string payloadData, string _testEndpoint)
        {
            await apiTodoTests.API_GETRequestToVerifyPayload($"{_endpoint}/{_testEndpoint}", ConvertPayloadToList(payloadData));
        }


        [Then(@"I make another POST Request using '(.*)' and verify error HTTPS status code '(.*)'")]
        public async Task ThenIMakeAnotherPOSTRequestUsingAndVerifyErrorHTTPSStatusCode(string payloadData, int errorCode)
        {
           await apiTodoTests.API_PostMethodValidation(_endpoint, ConvertPayloadToList(payloadData), errorCode);
        }


        [Given(@"I make POST Request using '(.*)' and send GET request to verify '(.*)'")]
        public async Task GivenIMakePOSTRequestUsingAndSendGETRequestToVerify(string payloadData, string testEndPoint)
        {
            await apiTodoTests.API_GETRequestToVerifyPayload(testEndPoint, ConvertPayloadToList(payloadData));
        }


        [When]
        [When(@"I send PUT Request to update '(.*)' using '(.*)'")]
        [Then(@"I send PUT Request to update '(.*)' using '(.*)'")]
        public async Task ThenISendPUTRequestToUpdateUsing(string testData, int ID)
        {
            await apiTodoTests.API_PutMethodValidation(_endpoint, ConvertPayloadToList(testData), ID);
        }

        [Then(@"I send PUT Request '(.*)' using '(.*)' and verify HTTPS Status Code '(.*)'")]
        public async Task ThenISendPUTRequestUsingAndVerifyHTTPSStatusCode(string testData, int ID, int StatusCode)
        {
            await apiTodoTests.API_PutMethodValidation(_endpoint, ConvertPayloadToList(testData), ID, StatusCode);
        }


        [Then(@"send DELETE Request to delete the record using '(.*)' and verify '(.*)'")]
        public async Task ThenSendDELETERequestToDeleteTheRecordUsingAndVerify(string ID, int StatusCode)
        {
            await apiTodoTests.API_DeleteMethodValidation(_endpoint, ID, StatusCode);
        }

        [Given(@"I make POST Request using '(.*)' and validate Schema '(.*)' against responsecontent")]
        public async Task GivenIMakePOSTRequestUsingAndValidateSchemaAgainstResponsecontent(string payload, string fileName)
        {
            await apiTodoTests.API_SchemaValidation(_endpoint, ConvertPayloadToList(payload), fileName);
        }

        public Todo ConvertPayloadToList(string payload)
        {
 
            var _strPayloadList = new List<string>();
            var keyValueList = new Dictionary<string, dynamic>();
            var todoList = new Todo();

            if (payload != String.Empty)
            {
                /////
                if (payload.Contains(","))
                { _strPayloadList = payload.Split(',').ToList(); }
                else
                { _strPayloadList.Add(payload); }

                ///
                foreach (var item in _strPayloadList)
                {
                    string[] keyValue = item.Split(':');
                    keyValueList.Add(keyValue[0], keyValue[1]);
                }

                foreach (var list in keyValueList)
                {
                    todoList.Id = list.Key.Contains("Id") ? int.Parse(list.Value) : 1;
                    if (list.Key.Contains("Name")) todoList.Name = list.Value;
                    if (list.Key.Contains("Complete")) todoList.IsComplete = bool.Parse(list.Value);
                }
            }
            else
            {
                todoList.Id = 1;
            }


            return todoList;
        }





    }
}
