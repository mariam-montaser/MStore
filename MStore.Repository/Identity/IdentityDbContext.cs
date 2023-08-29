using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MStore.Core.Entities.Identity;

namespace MStore.Repository.Identity
{
    public class IdentityContext:IdentityDbContext<AppUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options):base(options)
        {

        }

    }
}
