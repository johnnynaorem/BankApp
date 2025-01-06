namespace BankingApp.Exceptions
{
    public class CouldNotAddExecption:Exception
    {
        string message;
        public CouldNotAddExecption(string message)
        {
            this.message = message;
        }

        public override string Message => message;

    }
}
