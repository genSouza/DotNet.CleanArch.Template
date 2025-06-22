using DotNet.CleanArch.Template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.CleanArch.Template.Domain.Common.Repositories
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
