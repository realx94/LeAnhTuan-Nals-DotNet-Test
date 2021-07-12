using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Authorize;
using Core.Entities.Enums;
using Core.Services;
using Core.Settings;
using Core.ViewModels.Requests.Users;
using Core.ViewModels.Responses.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersControllers : ApiControllerBase
    {
        IUserService service;
        IAuthenticationManager auth;
        IMapper mapper;
        public UsersControllers(IUserService service, IAuthenticationManager auth, IMapper mapper)
        {
            this.service = service;
            this.auth = auth;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Login(UserLoginRequest request)
        {
            var user = service.Authenticate(request);

            if (user == null)
                return CreateFailResult("Username or password is incorrect");
            var token = auth.GenerateToken(user);

            return CreateSuccessResult(new
            {
                token = token,
                user = mapper.Map<UserResponse>(user)
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] UserRegisterRequest request)
        {
            try
            {
                service.Create(request);

                return CreateSuccess();
            }
            catch (Exception ex)
            {
                return CreateFailResult(ex.Message);
            }

        }

        [HttpGet]
        [Route("test")]
        [JwtAuth()]
        public IActionResult Test()
        {
            return CreateSuccessResult("This API Access by Auth User");
        }

        [HttpGet]
        [Route("AdminOnly")]
        [JwtAuth(UserTypes.Admin)]
        public IActionResult AdminOnly()
        {        
            return CreateSuccessResult("This API Access by Admin Only");
        }

        [HttpGet]
        [Route("UserOnly")]
        [JwtAuth(UserTypes.User)]
        public IActionResult UserOnly()
        {
            return CreateSuccessResult("This API Access by User Only");
        }

        [HttpGet]
        [Route("PartnerOnly")]
        [JwtAuth(UserTypes.Partner)]
        public IActionResult PartnerOnly()
        {
            return CreateSuccessResult("This API Access by Partner Only");
        }
    }
}
