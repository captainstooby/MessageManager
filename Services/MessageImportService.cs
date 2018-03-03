using MessageManager.Domain.Import;
using MessageManager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageManager.Services
{
    public class MessageImportService : IMessageImportService
    {
        private readonly IFileService _fileService;
        private readonly ILanguageHelper _languageHelper;

        public MessageImportService(IFileService fileService,
            ILanguageHelper languageHelper)
        {
            _fileService = fileService;
            _languageHelper = languageHelper;
        }

        public ImportResponse ImportMessages(string messageSourceDirectory)
        {
            var importedMessagesCount = 0;

            try
            {
                List<Message> messagesToImport = 
                    _fileService.GetFilesFromDirectory(messageSourceDirectory);

                if (messagesToImport.Count > 0)
                {
                    foreach (var messageToImport in messagesToImport)
                    {
                        ImportMessage(messageToImport);
                    }

                    importedMessagesCount = messagesToImport.Count(m => m.Imported);
                }

                return new ImportResponse()
                {
                    SuccessMessage =
                    string.Format("Import process complete!  {0} {1} {2} succesfully imported.", 
                    importedMessagesCount,
                    _languageHelper.NumberizeText("message", importedMessagesCount),
                    _languageHelper.NumberizeText("were", importedMessagesCount))
                };
            }
            catch (Exception ex)
            {
                return new ImportResponse()
                {
                    IsError = true,
                    ErrorMessage =
                    string.Format("There was a problem importing the {1}:  {0}",
                    ex.Message,
                    _languageHelper.NumberizeText("message", importedMessagesCount))
                };
            }
        }

        private void ImportMessage(Message messageToImport)
        {
            try
            {
                //Do import into db
                /*
                    1. First check for uniqueness.  If the message is already in the db, just update the record
                    2. If the message does not already exist in the db, insert it
                 */

                messageToImport.Imported = true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}