using System;

namespace ExampleGateway.RequestModels
{
    public class AddUserRequestModel
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Guid CustomerId { get; set; }
        public int StatusId { get; set; }
    }
}