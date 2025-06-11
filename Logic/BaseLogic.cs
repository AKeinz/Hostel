namespace Logic
{
    public interface BaseLogic
    {
        public delegate void ErrorsHandler(string message);
        public event ErrorsHandler? NotifyError;
    }
}
