using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Data
{
    public class GardaVettingSystemDbContext(DbContextOptions<GardaVettingSystemDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<ApplicantAddress> ApplicantAddresses { get; set; }
        public DbSet<AccessCode> AccessCodes { get; set; }
    }
}
