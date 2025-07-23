using System;
using System.Collections.Generic;
using System.Text;

namespace DClean.Application.Interfaces.Identity
{
    public interface ICurrentUser<TUserPK> where TUserPK : allows ref struct
    {
        public TUserPK? UserId { get; }
    }
    public interface ICurrentUser : ICurrentUser<Guid?>
    {
        public string UserIdString { get; }

    }
}