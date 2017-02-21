namespace BullsAndCows.Core.Results
{
    public class GuessResult
    {
        public GuessResult(string number)
        {
            this.Number = number;
        }

        public string Number { get; set; }

        public int Cows { get; set; }

        public int Bulls { get; set; }
    }
}
