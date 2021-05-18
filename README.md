# Serverless-Functions
Functions Serverless with basic operations CRUD



## Using

- .Net 3.1
- Azure Functions
- Trigger HTTP
- Storage MongoDB


## Get Started prepare Environment

On root folder run docker-compose with: *docker-compose up*


### Run Application
Run local azure function 
- dotnet restore
- dotnet build
- dotnet run

*Functions will be executed locally as standard on the port:* **7071**

### Database
**To Access Database Management:** *http://localhost:8081/*  


### Use Function

You can use Postman to trigger HTTP events to functions  

- *HTTP-GET: Get Car:* http://localhost:7071/api/GetCar?id=[id]
- *HTTP-GET: Get All Cars (limited 10):* http://localhost:7071/api//GetCars
- *HTTP-POST: Create Car (post json):* http://localhost:7071/api/CreateCar
- *HTTP-PUT: Update Car (put json):* http://localhost:7071/api/UpdateCar?id=[id]
- *HTTP-DELETE: Delete Car:* http://localhost:7071/api/DeleteCar?id=[id]


### Models

*Car*  
```json
{
    "plate": "string",
    "model": "string",
    "price": decimal
}
```