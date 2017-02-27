namespace BullsAndCows.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Game
    {
        public Game()
        {
            this.Guesses = new List<Guess>();
        }

        [Key]
        public Guid Id { get; set; }

        [StringLength(4)]
        [MinLength(4)]
        public string Number { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public bool Solved { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<Guess> Guesses { get; set; }
    }
}
