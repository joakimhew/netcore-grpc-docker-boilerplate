namespace ExampleGateway
{
    using System;
    using System.Net;
    using ExampleGateway.RequestModels;
    using Grpc.Core;
    using Microsoft.AspNetCore.Mvc;
    using Security.API;
    using static Security.API.UserService;

    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly UserServiceClient userServiceClient;

        public AccountController(UserServiceClient userServiceClient)
        {
            this.userServiceClient = userServiceClient ?? throw new ArgumentNullException(nameof(userServiceClient));
        }

        [HttpGet]
        public IActionResult GetUsers([FromQuery]GetUsersRequest request)
        {
            var userResponse = this.userServiceClient.GetUsers(request);

            return Ok(userResponse.Users);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody]AddUserRequestModel requestModel) 
        {
            try
            {
                var userResponse = this.userServiceClient.AddUser(new AddUserRequest
                {
                    Username = requestModel.Username,
                    Firstname = requestModel.Firstname,
                    Lastname = requestModel.Lastname,
                    CustomerId = new UUID { Value = requestModel.CustomerId.ToString() },
                    StatusId = requestModel.StatusId
                });
                
                return Ok(userResponse.Id);
            }
            catch 
            {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError, 
                    "Could not create user");
            }
        }
    }
}