namespace Model
{
    public class HostelException : Exception
    {
        public string Message;
        public HostelException(string message) { Message = message; }
    }
}
