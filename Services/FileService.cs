using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using MessageManager.Domain.Import;

namespace MessageManager.Services
{
    public class FileService : IFileService
    {
        public List<MessageToImport> GetFilesFromDirectory(string directoryLocation)
        {
            var messagesToImport = new List<MessageToImport>();

            foreach (var file in Directory.GetFiles(directoryLocation))
            {
                var fileMetadata = GetFileMetaData(file);

                messagesToImport.Add(new MessageToImport()
                {
                    Mp3FileName = fileMetadata.Mp3FileName,
                    DateOfRecording = fileMetadata.DateOfRecording
                });
            }

            return messagesToImport;
        }

        private FileMetaData GetFileMetaData(string file)
        {
            try
            {
                var fileMetaData = new FileMetaData();

                fileMetaData.Mp3FileName = file;

                var fileParts = file.Split("_");

                fileMetaData.DateOfRecording = GetDateOfRecording(fileParts);

                return fileMetaData;
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
