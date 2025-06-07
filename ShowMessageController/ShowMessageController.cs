namespace ShowMessageBoxController
{
    public class ShowMessageController
    {
        public void ShowMessage(string message)
        {
            NotifyShowMessage?.Invoke(message);
        }

        public delegate void ShowMessageDelegate(string message);
        public event ShowMessageDelegate NotifyShowMessage;
    }
}
