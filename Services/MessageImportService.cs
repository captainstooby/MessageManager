using Data.Repositories;
using MessageManager.Domain.Import;
using MessageManager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessageManager.Services
{
    public class MessageImportService : IMessageImportService
    {
        private readonly IFileService _fileService;
        private readonly ILanguageHelper _languageHelper;
        private readonly IMessageRepository _messageRepository;

        public MessageImportService(IFileService fileService,
            ILanguageHelper languageHelper,
            IMessageRepository messageRepository)
        {
            _fileService = fileService;
            _languageHelper = languageHelper;
            _messageRepository = messageRepository;
        }

        public ImportResponse ImportMessages(string messageSourceDirectory)
        {
            var importedMessagesCount = 0;
            IEnumerable<Message> unimportedMessages = null;
            List<Message> messagesToImport = null;

            try
            {
                messagesToImport =
                    _fileService.GetFilesFromDirectory(messageSourceDirectory);

                if (messagesToImport.Count > 0)
                {
                    foreach (var messageToImport in messagesToImport.OrderBy(m => m.MessageRecordingDate))
                    {
                        ImportMessage(messageToImport);
                    }

                    importedMessagesCount = messagesToImport.Count(m => m.Imported);
                    unimportedMessages = messagesToImport.Where(m => m.Imported == false);
                }

                return new ImportResponse()
                {
                    SuccessMessage = GetSuccessMessage(importedMessagesCount),
                    ErrorMessage = GetErrorMessage(unimportedMessages),
                    IsError = unimportedMessages.Count() > 0
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
                    _languageHelper.NumberizeText("message", messagesToImport.Count))
                };
            }
        }

        private string GetErrorMessage(IEnumerable<Message> unimportedMessages)
        {
            if (unimportedMessages == null || unimportedMessages.Count() == 0)
                return "There were no failed message imports.";

            var failedMessagesStringBuilder = new StringBuilder();

            failedMessagesStringBuilder
                .Append("The following messages could not be imported: ")
                .Append("----------------------------------------------");

            foreach (Message message in unimportedMessages)
            {
                failedMessagesStringBuilder.Append(message.ToString());
            }

            return failedMessagesStringBuilder.ToString();
        }

        private string GetSuccessMessage(int importedMessagesCount)
        {
            return string.Format("Import process complete!  {0} {1} {2} succesfully imported.",
                                importedMessagesCount,
                                _languageHelper.NumberizeText("message", importedMessagesCount),
                                _languageHelper.NumberizeText("were", importedMessagesCount));
        }

        private void ImportMessage(Message messageToImport)
        {
            try
            {
                _messageRepository.Add(messageToImport);
                messageToImport.Imported = true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}