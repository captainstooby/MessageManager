using MessageManager.Domain.Import;

namespace MessageManager.Services
{
    public interface IMessageImportService
    {
        ImportResponse ImportMessages(string messageSourceDirectory);
    }
}