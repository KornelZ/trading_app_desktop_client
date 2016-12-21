using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LGSA_Server.Model.DTO
{
    public class UserDto
    {
        [Required]
        public int Id { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        public int? Rating { get; set; }
        public int? AddressId { get; set; }
        public AddressDto Address { get; set; }
    }
}