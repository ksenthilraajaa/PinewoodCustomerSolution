# PinewoodCustomerSolution

Hello! This repository includes an API and an MVC UI built with C# and ASP.NET Core 8.0.4. for managing Customer information

This Repository contains 2 solutions
 -	PinewoodCustomerApiSolution
 -	PinewoodCustomerClientSolution

**Prerequisities:**

  - Visual Studio Code
  - OS: Windows 10 / Linux
  - SDK: .Net Core 8.0
  - Nuget Packages:
    - Microsoft.EntityFrameworkCore.InMemory 8.0.6
    - Microsoft.EntityFrameworkCore 8.0.6
    - Microsoft.AspNetCore.OpenApi 8.0.4
    - Microsoft.NET.Test.Sdk 17.8.0
    - coverlet.collector 6.0.2
    - coverlet.msbuild 6.0.2
    - Moq 4.20.70
    - xunit 2.5.3
  - Visual studio extensions
    - .NET Core Test Explorer (To run/Debug test projects)
    - Coverage Gutters (For viualizing the test coverage)

**PinewoodCustomerApiSolution:**

This backend API is designed using Clean Architecture principles, adhering to the Domain-Driven Design approach and utilizing the Repository pattern for CRUD operations.

**API Endpoints**
1. Swagger UI - http://localhost:5166/swagger/index.html
2. GET - http://localhost:5166/api/customer/{id} - To retrieve customer with the id param
3. GET - http://localhost:5166/api/customer/all - To retieve the list of all customers from the in memory database
4. POST - http://localhost:5166/api/customer/add - To add a new customer with customer as JSON object
5. PUT - http://localhost:5166/api/customer/update - To update an existing customer with updated customer as JSON object
6. DELETE - http://localhost:5166/api/customer/delete/{id} - To delete the existing customer with customer id as param

 ![image](https://github.com/ksenthilraajaa/PinewoodCustomerSolution/assets/54350680/2afe63aa-1422-4769-b547-eb846295ab8a)

*Entity Framework Core in-memory database* used for storing data without having to worry about setting up a database. This can be used for building PoC applications and can be switched to real database easily when the prototype is ready.
DbContext added to the Configure section of the Startup class

 ![image](https://github.com/ksenthilraajaa/PinewoodCustomerSolution/assets/54350680/ab3335d4-e624-4bc6-9b1e-19945fa95d60)

*Initial Test Data*

 ![image](https://github.com/ksenthilraajaa/PinewoodCustomerSolution/assets/54350680/3caa7a72-1776-4fd3-a7a9-fe92b33d1950)

Connection Settings in appsettings.Development.json file

```
       "ConnectionStrings": {
         "DefaultConnection": "Server=(localdb)\\mssqllocaldb; Database=CleanDb; Trusted_Connection=True;"
       }
```

WebHostBuilder is utilized to build and run the API. It can also be included in a Dockerfile to facilitate running the application within Docker containers if required. Unit and Integration tests can be added to make sure the API is working as expected.

API URL(localhost): http://localhost:5166/swagger/index.html
 ![image](https://github.com/ksenthilraajaa/PinewoodCustomerSolution/assets/54350680/49a8f7c2-2b3a-494f-8919-bafe90b05536)


**PinewoodCustomerClientSolution:**

This front-end web UI application is built using the MVC architecture to interact with the backend web API for accessing customer data. Validations are added to the view model, Exceptions are handled properly and the CustomerApi service is used to invoke the API via HTTP requests.
Here, the WebApplication is utilized to build and run the web application.


MVC Client URL: http://localhost:5081/customer/list

 ![image](https://github.com/ksenthilraajaa/PinewoodCustomerSolution/assets/54350680/ae450ed1-4fd6-4e50-8a9f-f12841422fc5)


*Default ErrorPage* view with original exceptions from the API for troubleshooting purpose.

 ![image](https://github.com/ksenthilraajaa/PinewoodCustomerSolution/assets/54350680/3bb02526-ec19-46af-8d49-0a35c7ecdc09)


*Add New Customer*

 ![image](https://github.com/ksenthilraajaa/PinewoodCustomerSolution/assets/54350680/7a3d27c6-5caf-470f-8c36-10cef9b039a9)


*Edit Customer*

 ![image](https://github.com/ksenthilraajaa/PinewoodCustomerSolution/assets/54350680/a375f9da-0279-4e4e-9357-648930f7aca1)


*Delete*

![image](https://github.com/ksenthilraajaa/PinewoodCustomerSolution/assets/54350680/60301816-9ca4-41e6-8c23-bca826134ae0)


**Executing the Unit & Integration Tests**

All the below 3 unit test methods are written to test the CustomerController.GetAllCustomersAsync() and the test coverage for this method is 100%
  - CustomerControllerTests.Get_AllCustomers_Test()
  - CustomerControllerTests..Get_AllCustomers_NotFound_Test()
  - CustomerControllerTests.Get_AllCustomers_Exception_Test()

Run the test projects using .Net Core Test Explorer extensions

![image](https://github.com/ksenthilraajaa/PinewoodCustomerSolution/assets/54350680/a405f924-67c6-4eab-9474-df1b57e2aceb)


CLI to run the test and view the coverage info
```
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./coverage/
```
*Unit Test execution with coverage*
![image](https://github.com/ksenthilraajaa/PinewoodCustomerSolution/assets/54350680/1ca90b87-27b1-48c0-9f11-e815459b60b6)

*Integration test execution with coverage*
![image](https://github.com/ksenthilraajaa/PinewoodCustomerSolution/assets/54350680/a52cd86f-0880-45c4-8b1c-dffe2221b050)

*CLI to prepare the coverage report*

```
reportgenerator -reports:./**/coverage.info -targetdir:./coverage-report -reporttypes:Html
```
![image](https://github.com/ksenthilraajaa/PinewoodCustomerSolution/assets/54350680/5afdeea6-c723-4234-915d-0829631bbdb1)

*HTML coverage report* generated under coverage-report folder in the solution 

![image](https://github.com/ksenthilraajaa/PinewoodCustomerSolution/assets/54350680/51388095-6424-4c04-9be7-040fbf38ff67)

*Coverage gutter view* of the Controller method showing the test coverage

Run this command in the VS code command pallette to display the code coverage in the vs editor

![image](https://github.com/ksenthilraajaa/PinewoodCustomerSolution/assets/54350680/504617cf-721d-4d2c-8e53-0b9b0e8ab95a)

Test coverage are showin in Green and the ones in Red are with no test coverage
![image](https://github.com/ksenthilraajaa/PinewoodCustomerSolution/assets/54350680/b319f57f-c0c2-4919-9230-ec9facc223f0)

