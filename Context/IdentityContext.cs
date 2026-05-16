
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Models;

namespace SchoolApi.Context;

public class IdentityContext : IdentityDbContext<User>
{
    public IdentityContext(DbContextOptions options) :base(options)
    {
        
    }
}

