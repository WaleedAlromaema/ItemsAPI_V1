# Item Rest Service API Using .NET Core
-----------
Author : Waleed Alromaema
         wal.roma@outlook.it
-----------
###Content:

1- About The API.

2- Item Database Design

	   a- Database Design
	   b- Business Logic Design
	   c- Rest API Interface
   
3- Runing Application

----------------
## 1- About Movie Catalogue.

Item API is Microservice REST API Application developed by .NET Core API . It provide the following Services:

- Get :
     - All Items in Mongo DB
- Post :
     - Send Parameter of URL 
     - Post Function Parse URL and Get Type.
     - then It Quary Database for Item by Type.
     - Return the result. 


## 2- Item Database Design

### A- Database Design

the Database consists of only one Collection Items.
   
### B- Business Logic Design

The design pattern considered the separation between different layers,  
- Presentation layer [her is the REST API Service Provider] 
     - using Rest Controllers.
     
- Business Logic Layer
     - using Domain Objects as Item Class.
     - Service Interface and Implementation for all Item Services needed.
     
- Repository Data Access Layer 
     - I implemented MongoCRUD for all needed DB operation on Item
 
       
### C-Rest API Interface
 
 below are the set of REST Services and the associated URI.
 
 ![alt ItemEndpoints](ItemEndpoints.png)   

## 3- Setting and Running Application
### The tools required are: 
-  visual Studio 2019
-  use nuget to get Mongo db drivers
-  POSTMAN Chrome application for client test of rest service in server side download from https://go.pstmn.io/ 


#### - Git Link

```
 https://github.com/WaleedAlromaema/ItemsAPI_V1.git
```

#### - POSTMAN

using postman for testing the Rest api service
Enter in URL: LOCALHOST:44378/api/...
as in the rest api listed above:

###here is an example of GET Item result:

![alt getItem](ItemsGet.png)

### Example of Item Post :

![alt postItem](ItemsPost.png)


