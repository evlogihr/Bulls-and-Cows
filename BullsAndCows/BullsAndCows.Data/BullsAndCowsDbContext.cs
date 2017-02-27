namespace BullsAndCows.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;

    using BullsAndCows.Data.Models;
    using BullsAndCows.Data.Contracts;
    using System;
    using System.Data.Entity;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IBullsAndCowsDbContext
    {
        public ApplicationDbContext()
            : base("BullsAndCowsConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Game> Games { get; set; }

        public IDbSet<Guess> Guesses { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
