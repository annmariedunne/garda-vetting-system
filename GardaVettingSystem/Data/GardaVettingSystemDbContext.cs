using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Data
{
    /// <summary>
    /// The Entity Framework Core database context for the Garda Vetting Data Reuse System.
    /// Extends <see cref="IdentityDbContext"/> to include ASP.NET Identity tables alongside
    /// the application's domain models.
    /// </summary>
    public class GardaVettingSystemDbContext(DbContextOptions<GardaVettingSystemDbContext> options) : IdentityDbContext(options)
    {
        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of applicant profiles.
        /// </summary>
        public DbSet<Applicant> Applicants { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of applicant addresses.
        /// </summary>
        public DbSet<ApplicantAddress> ApplicantAddresses { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of access codes.
        /// </summary>
        public DbSet<AccessCode> AccessCodes { get; set; }
    }
}
