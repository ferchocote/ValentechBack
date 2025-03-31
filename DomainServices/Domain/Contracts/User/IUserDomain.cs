using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Domain.Contracts.User
{
    public interface IUserDomain
    {
        Task<PruebaAppApi.DataAccess.Entities.User> GetUser(string documentNumber);
    }
}
