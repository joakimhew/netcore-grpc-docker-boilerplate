using System;

namespace Security.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Guid CustomerId { get; set; }
        public int StatusId { get; set; }
    }
}