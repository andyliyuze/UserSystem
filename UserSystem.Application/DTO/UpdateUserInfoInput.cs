using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSystem.Application.DTO
{
    public class UpdateUserInfoInput
    {
        public string UserName { get; set; }

        public DateTime? BirthDay { get; set; }

        public string Email { get; set; }
    }
}
