using MessageManager.Domain.Import;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MessageManager.Services
{
    public class FileService : IFileService
    {
        public List<Message> GetFilesFromDirectory(string directoryLocation)
        {
            var messagesToImport = new List<Message>();

            foreach (var file in Directory.GetFiles(directoryLocation))
            {
                var fileMetadata = GetFileMetadata(file);

                messagesToImport.Add(new Message()
                {
                    MessagePath = fileMetadata.Mp3FileName,
                    MessageRecordingDate = fileMetadata.DateOfRecording
                });
            }

            return messagesToImport;
        }

        private FileMetadata GetFileMetadata(string file)
        {
            try
            {
                var fileMetadata = new FileMetadata
                {
                    Mp3FileName = file
                };

                var fileParts = file.Split(Convert.ToChar("_"));

                fileMetadata.DateOfRecording = GetDateOfRecording(fileParts);

                return fileMetadata;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private DateTime GetDateOfRecording(string[] fileParts)
        {
            var date = fileParts[1].Substring(0, 8);

            return DateTime.ParseExact(date, "MMddyyyy", CultureInfo.InvariantCulture);
        }
    }
}
