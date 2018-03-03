﻿using System;
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
                var fileMetadata = GetFileMetadata(file);

                messagesToImport.Add(new MessageToImport()
                {
                    Mp3FileName = fileMetadata.Mp3FileName,
                    DateOfRecording = fileMetadata.DateOfRecording
                });
            }

            return messagesToImport;
        }

        private FileMetadata GetFileMetadata(string file)
        {
            try
            {
                var fileMetadata = new FileMetadata();

                fileMetadata.Mp3FileName = file;

                var fileParts = file.Split("_");

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
