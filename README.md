# LeAnhTuan-Nals-DotNet-Test

##Setup
- Run below code in **Authentication\ClientApp** folder to install UI Package 
```
NPM RUN INSTALL
```
- RUN ASP.NET Application in visual studio
- - Wait a momment and refresh page if it show error page.
## Git Repository 
**https://github.com/realx94/LeAnhTuan-Nals-DotNet-Test**
Repository has 4 branches
- Master
- Release (Code to release)
- Develop (Code in develop)
- Feature/Authentication (Code of Authentication fuction. It will be belete after merge to Develop branch).

## Authentication method
- API using JwtBearer for authentication.
- IAuthenticationManager to generate and verify token.
- JwtAuthAttribute to authorize APIs by user type.
- UI use Vuex to store user information and store token in LocalStorage. Each time user access page the system will check token exist or not then redirect user to login page.
## Project Structure
### Authentication
- This project includes APIs Controller, VueJs Code, Configuration
### Core
- This project includes Authrize, Entities, Extension methods, Repository Interfaces, Service Interfaces, Setting model, ViewModels (Profile of AutoMapper, Request models, Response models)
### Domain
- This project includes Service implementings, Validator config use FluentValidate
### Infrastructure
- This project includes DbContext, Entity Config, Repository implementings.
### Unit Test
- This project use for unit testing.

## UI 
**UI using Vuejs framework**
### Pages
- https://localhost:44362 **Home page**
- https://localhost:44362/login **Login page**
- https://localhost:44362/register **Regster page**
- https://localhost:44362/misc **misc page**
