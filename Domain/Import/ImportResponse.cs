namespace MessageManager.Domain.Import
{
    public class ImportResponse
    {
        public ImportResponse()
        {
            IsError = false;
        }

        public bool IsError { get; internal set; }
        public string ErrorMessage { get; internal set; }
        public string SuccessMessage { get; internal set; }
    }
}