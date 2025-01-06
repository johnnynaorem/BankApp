namespace BankingApp.Exceptions
{
    public class EmptyCollectionException : Exception
    {
        string message;

        public EmptyCollectionException(string message) { 
            this.message = message;
        }

        public override string Message => message;
    }
}
