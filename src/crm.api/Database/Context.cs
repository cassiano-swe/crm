using Microsoft.EntityFrameworkCore;
using crm.api.Entities;

namespace crm.api.Database;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Contact> Contacts{get; init;}
}