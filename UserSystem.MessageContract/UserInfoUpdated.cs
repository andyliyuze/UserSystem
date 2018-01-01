using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSystem.MessageContract
{
    /// <summary>
    /// 用户信息修改契约
    /// </summary>
    public interface UserInfoUpdated
    {
        string Id { get; set; }

        string UserName { get; set; }

        DateTime? BirthDay { get; set; }

        string Email { get; set; }
    }
}
