# Instructions

## Structure
All the project's items are located in **src\\UserRegistry** directory. The frontend isn't added to main solution and is placed in **user-registry-web-ui** directory and can be opened to review separately.

## Running
You can run the project by selecting **docker-compose** Startup Item and clicking run. Database initializes automatically. After starting you will be redirected to **http://localhost:4200/register** page which is the root page of frontend.

## Login
You can log in with follows credentials:
Username: user1@somemail.com
Password: password1

Also, the system has other users from user2@somemail.com (password2) to user23@somemail.com (password23). You can see details in **src\UserRegistry\Common\DataAccess\UserManagement.DataAccess\Extensions\InitialData.cs**.

## Testing and documentation
- Postman collections and Postman environment can be imported for testing located in the **src\UserRegistry\Postman** directory. 
- All services have a connected swagger and it can be reached via addresses **http://localhost:5000/swagger**, **http://localhost:5001/swagger** and **http://localhost:5002/swagger**.
- There are unit tests for several projects you can find it in **src\UserRegistry\Tests**.

# Solutions

## The entire solution uses Microservice-like architecture + Using MediatR.
- UserManagementService was done in DDD manier.
- For LocationService, Vertical Slice Architecture was implemented in order to save time and because it is sufficient with the current functionality.
- At the IdentityService level, the architecture is even simpler, since it consists of one endpoint, there are fewer potential scenarios and they are much simpler.

## Simplifications to maintain a balance of simplicity and demonstration of technical skills
- Far from 100% test coverage.
- No on-demand cache cleaning (I think it is necessary with synchronous state synchronization).
- Synchronous communication between UserManagementService and LocationService with caching is implemented. An alternative would be to implement a local cache and synchronize it via message brokers.
- There are no domain events either.
- The simplest implementation of JWT.
- No division into modules in the angular application.
- No tests in the angular application.
