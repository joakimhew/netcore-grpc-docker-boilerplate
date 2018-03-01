namespace UserService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Google.Protobuf.WellKnownTypes;
    using Grpc.Core;
    using Microsoft.EntityFrameworkCore;
    using Security.API;
    using Security.Domain;
    using static Security.API.UserService;

    using User = Security.Domain.Models.User;
    
    public class UserServiceImpl : UserServiceBase
    {
        private readonly DbContextOptions<SecurityDbContext> securityDbContextOptions;

        public UserServiceImpl(DbContextOptions<SecurityDbContext> securityDbContextOptions)
        {
            this.securityDbContextOptions = securityDbContextOptions;
        }

        public override async Task<AddUserResponse> AddUser(AddUserRequest request, ServerCallContext context) 
        {
            if(!Guid.TryParse(request.CustomerId.Value, out Guid customerId))
                throw new RpcException(
                    Status.DefaultCancelled, 
                    $"Could not parse {nameof(request.CustomerId)}"
                );

            var user = new User 
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                Username = request.Username,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                StatusId = request.StatusId
            };

            using(var dbContext = new SecurityDbContext(this.securityDbContextOptions)) 
            {
                await dbContext.AddAsync(user);
                await dbContext.SaveChangesAsync();
            }

            return await Task.FromResult(new AddUserResponse 
            {
                Id = new UUID { Value = user.Id.ToString() }
            });
        }

        public override async Task<GetUsersResponse> GetUsers(GetUsersRequest request, ServerCallContext context)
        {
            List<User> foundUsers;
            Guid id;
            Guid customerId;    

            if(request.Id != null) 
            {
                if(!Guid.TryParse(request.Id.Value, out id))
                    throw new RpcException(
                        Status.DefaultCancelled, 
                        $"Could not parse {nameof(request.Id)}"
                    );
            }

            if(request.CustomerId != null)
            {
                if(!Guid.TryParse(request.CustomerId.Value, out customerId))
                    throw new RpcException(
                        Status.DefaultCancelled,
                        $"Could not parse {nameof(request.CustomerId)}"
                    );
            }

            using(var dbContext = new SecurityDbContext(this.securityDbContextOptions)) 
            {
                foundUsers = dbContext.Users.ToList();
            }

            var response = new GetUsersResponse
            {
                Users = 
                    { 
                        foundUsers.Select(x => new Security.API.User 
                        {
                            Id = new Security.API.UUID { Value = x.Id.ToString() },
                            Username = x.Username,
                            Firstname = x.Firstname,
                            Lastname = x.Lastname,
                            CustomerId = new Security.API.UUID { Value = x.CustomerId.ToString() },
                            StatusId = x.StatusId,
                        })
                    }
            };

            return await Task.FromResult(response);
        }
    }
}