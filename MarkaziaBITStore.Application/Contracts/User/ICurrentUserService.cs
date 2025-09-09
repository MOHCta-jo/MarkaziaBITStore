using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.Contracts.User
{
    public interface ICurrentUserService
    {
        int GetUserId();
    }
}
