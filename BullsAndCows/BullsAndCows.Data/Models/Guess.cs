namespace BullsAndCows.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Guess
    {
        public Guid Id { get; set; }

        public Guid GameId { get; set; }

        public virtual Game Game { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [StringLength(4)]
        [MinLength(4)]
        public string Number { get; set; }

        public DateTime Date { get; set; }
    }
}
