using MessageManager.Domain.Import;
using System.Collections.Generic;

namespace MessageManager.Services
{
    public interface IFileService
    {
        List<MessageToImport> GetFilesFromDirectory(string directoryLocation);
    }
}