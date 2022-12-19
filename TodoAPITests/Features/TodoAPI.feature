Feature: TodoAPITests

@EndPointChecks
Scenario Outline: Validate_GETCheckpointsWithNoLists
	Given I set api '<Endpoint>' for '<Testcase>'
	Then I make GET Request and verify the valid HTTPS Status code and '<ResponseContent>' 
	Examples: 
	| Testcases                          | Endpoint   | ResponseContent |
	| TC1_VerifyGetEndpointRoot          | /          | Hello World!    |
	| TC2_VerifyGetEndpointWithEmptyList | /todoitems | []              |

	Scenario Outline: Validate_POSTCheckpoints
	Given I set api '<Endpoint>' for '<Testcase>'
	And I make POST Request using '<PayloadData>' and verify POST Creation HTTPS Status Code
	Then I send GET request to verify new todo list'<PayloadData>'
	Examples: 
	| Testcases                               | Endpoint   | PayloadData                      |
	| TC3_PostEndpointUsingName               | /todoitems | Name:User1                       |
	| TC4_PostEndpointUsingID                 | /todoitems | Id:2                             |
	| TC5_PostEndpointUsingComplete           | /todoitems | isComplete:true                  |
	| TC6_PostEndpointUsingAllItems           | /todoitems | Id:1,Name:User1,isComplete:false |
	| TC7_PostEndpointUsingNameID             | /todoitems | Id:1,Name:User1                  |
	| TC8_PostEndpointUsingIDComplete         | /todoitems | Id:1,isComplete:true             |
	| TC9_PostEndpointUsingNameComplete       | /todoitems | Name:User1,isComplete:true       |
	| TC2_UnhappyPath_PostEndpointEmptyList   | /todoitems |                                  |
	| TC3_UnhappyPath_PostEndpointinvalidChar | /todoitems | Id:1,Name:User#2,isComplete:true |

	Scenario Outline: Validate_GETCheckpointswithTodoLists
	Given I set api '<Endpoint>' for '<Testcase>'
	And I make POST Request using '<PayloadData>' and verify POST Creation HTTPS Status Code
	Then I send GET request to verify '<PayloadData>' using '<TestEndPoint>' 
	Examples: 
	| Testcases                                     | Endpoint   | PayloadData                      | TestEndPoint |
	| TC10_VerifyGetEndpointUsingComplete           | /todoitems | Name:User1,Id:1,isComplete:true  | Complete     |

	Scenario Outline: Validate_GETCheckpointResponseErrorValidation
	Given I set api '<Endpoint>' for '<Testcase>'
	And I make POST Request using '<PayloadData>' and verify POST Creation HTTPS Status Code
	Then I make GET Request '<TestEndPoint>' to verify '<ResponseContent>' and '<HTTPSCode>'
	Examples: 
	| Testcases                                | Endpoint   | PayloadData                      | TestEndPoint | ResponseContent | HTTPSCode |
	| TC4_UnhappyPath_GetEndpointUsingComplete | /todoitems | Name:User1,Id:1,isComplete:false | Complete     |                 | 404       |

	Scenario Outline: Validate_PUTCheckpoint
	Given I set api '<Endpoint>' for '<Testcase>'
	When I make POST Request using '<PayloadData>' and verify POST Creation HTTPS Status Code
	And I send GET request to verify new todo list'<PayloadData>'
	Then I send PUT Request to update '<TestData>' using '<ID>'
	And I send GET request to verify new todo list'<TestData>'
	Examples: 
	| Testcases                                       | Endpoint   | PayloadData          | TestData                   | ID |
	| TC12_VerifyPUTEndpointUsingIDwithName           | /todoitems | Name:User1,Id:1      | Name:User2                 | 1  |
	| TC13_VerifyPUTEndpointUsingIDwithNullNameChange | /todoitems | isComplete:true,Id:1 | Name:null,isComplete:false | 1  |
	
	Scenario Outline: Validate_PUTCheckpointErrorValidation
	Given I set api '<Endpoint>' for '<Testcase>'
	When I make POST Request using '<PayloadData>' and verify POST Creation HTTPS Status Code
	And I send GET request to verify new todo list'<PayloadData>'
	Then I send PUT Request '<TestData>' using '<ID>' and verify HTTPS Status Code '<ErrorCode>'
	Examples: 
	| Testcases                                     | Endpoint   | PayloadData          | TestData                   | ID | ErrorCode |
	| TC6_UnhappyPath_PUTEndpointUsingNonExistingID | /todoitems | isComplete:true,Id:1 | Name:User4,isComplete:true | 2  | 404       |


	Scenario Outline: Validate_DeleteCheckpoint
	Given I set api '<Endpoint>' for '<Testcase>'
	When I make POST Request using '<PayloadData>' and verify POST Creation HTTPS Status Code
	And I send GET request to verify new todo list'<PayloadData>'
	Then send DELETE Request to delete the record using '<ID>' and verify '<HTTPSCode>'
	Examples: 
	| Testcases                                        | Endpoint   | PayloadData                     | ID | HTTPSCode |
	| TC14_VerifyDeleteEndpointUsingID                 | /todoitems | Name:User1,Id:1                 | 1  | 200       |
	| TC7_UnhappyPath_DeleteEndpointUsingNonExistingID | /todoitems | Name:User1,Id:1,isComplete:true | 2  | 404       |


	Scenario Outline: EndToEndTesting_TodoAPI
	Given I set api '<Endpoint>' for '<Testcase>'
	And I make POST Request using '<PayloadData>' and verify POST Creation HTTPS Status Code
	Then I send GET request to verify '<PayloadData>' using '<TestEndPoint>'
	When I send PUT Request to update '<TestData>' using '<ID>'
	Then send DELETE Request to delete the record using '<ID>' and verify '<HTTPSCode>'
	Examples: 
	| Testcases                        | Endpoint   | TestEndPoint | PayloadData                     | TestData                    | ID | HTTPSCode |
	| TC15_VerifyEndToEndCRUDOperation | /todoitems | Complete     | Name:User1,Id:1,isComplete:true | Name:User5,isComplete:false | 1  | 200       |

	Scenario:SchemaValidation
	Given I set api '<Endpoint>' for '<Testcase>'
	And I make POST Request using '<PayloadData>' and validate Schema '<FileName>' against responsecontent
	Examples: 
	| Testcases                        | Endpoint   | PayloadData                     | FileName         |
	| TC16_ValidateSchemaWithValidJson | /todoitems | Name:User1,Id:1,isComplete:true | Schema.Json      |
	| TC17_ValidateSchemaWithEmptyJson | /todoitems | Name:User1,Id:1,isComplete:true | EmptySchema.Json |
	
	Scenario Outline: ToVerify_POSTEndpointErrors
	Given I set api '<Endpoint>' for '<Testcase>'
	When I make POST Request using '<PayloadData1>' and verify POST Creation HTTPS Status Code
	And I send GET request to verify new todo list'<PayloadData1>'
	Then I make another POST Request using '<PayloadData2>' and verify error HTTPS status code '<ErrorCode>'
	Examples: 
	| Testcases                    | Endpoint   | PayloadData1                    | PayloadData2                    | ErrorCode |
	| TC1_UnhappyPath_PostEndpoint | /todoitems | Id:1,Name:User2,isComplete:true | Id:1,Name:User2,isComplete:true | 500       |

