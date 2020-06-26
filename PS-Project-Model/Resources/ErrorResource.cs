namespace PS_Project_Model.Resources
{
    public class ErrorResource
    {
        public bool Success => false;
        public string Message { get; private set; }

        public ErrorResource(string message)
        {
            Message = message;
        }
    }
}