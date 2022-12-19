# SDET Technical Test - API

## Purpose

As part of our hiring process for technical roles we require all candidates to complete a small technical exercise. This is an important part of our process to allow us to assess an individuals technical ability when completing an exercise in their own time.

Given the importance of this exercise to our hiring process, we highly recommend you give this adequate consideration and address this task as you would do any other professional assignment in your current/previous workplace.

## Instructions

- A code based assessment, testing your ability to automate a test scenario
- Please work on a new branch within this repository and when you are happy to submit, create a pull request back into the main branch
- Please use the pull request to comment on any aspects of your solution that you didn't have time to complete, are not complete to your satisfaction or are not working
- Please add a markdown file with instructions on how to setup and run your solution 

**Please note that your submission must only contain your own work.  Under no circumstances should your submission contain any content owned (in whole or in part) by a third party except where you are expressly permitted to do so by the relevant third party, for example an open-source library creators.**

 
## Question

### Introduction

To do Items API's are simple CRUD operations where an user can create a to do item, get all the items which are already created. User can update status of an item or delete or get a specific item using ID of the item.

For this question, you can create a branch off of this repository and include any C# test framework of your choice. We would like you to write automated tests for each of the API endpoints.

- GET /todoitems
- POST /todoitems
- GET /todoitems/complete
- GET /todoitems{id}
- PUT /todoitems{id}
- DELETE /todoitems{id}

### How to run the application

You will need Visual Studio to run the appilication
- https://visualstudio.microsoft.com/downloads/

Once you have installed Visual Studio, clone this repo and follow the instructions below
 
This application needs NuGet packages
- https://docs.microsoft.com/en-gb/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio#add-nuget-packages

To run the appilcation
- https://docs.microsoft.com/en-gb/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio#run-the-app

To test using Postman
- https://docs.microsoft.com/en-gb/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio#install-postman-to-test-the-app

### Test Scenario

1. Write Test cases for each of the endpoints mentioned above 
2. Try to cover maximum number of test cases for each endpoints
3. Use BDD to describe your test in a feature file
4. Implement schema validation
5. Find as many bugs as you can and write test to cover the defects
6. Bonus points if you manage to enhance with end-to-end test coverage 
7. Can you think of any non-functional tests with which you could enhance the automation coverage? If so, describe them (and document it in new Notes.md file).


### Evaluation

Evaluation will be done on the following criteria:

- Clean and tidiness of the solution
- Design and architecture
- Readability
- Maintainability
- Stability of the tests
- Clarity of setup and execution documentation

Please provide instructions for setting up and how to run the tests.

## Copyright
© 2022 Willis Towers Watson. All rights reserved. Proprietary and Confidential. For Willis Towers Watson employees and candidates only.
