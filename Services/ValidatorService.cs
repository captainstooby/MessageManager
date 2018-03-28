using System.IO;

namespace MessageManager.Services
{
    public class ValidatorService : IValidatorService
    {
        public bool IsValidDirectory(string directoryLocation)
        {
            return Directory.Exists(directoryLocation);
        }
    }
}
