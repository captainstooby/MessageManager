namespace MessageManager.Domain.Import
{
    public class ImportResponse
    {
        public ImportResponse()
        {
            IsError = false;
        }

        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
    }
}