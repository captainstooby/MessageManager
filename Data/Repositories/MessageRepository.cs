using Dapper;
using MessageManager.Domain.Import;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Data.Repositories
{
    public interface IMessageRepository
    {
        bool Add(Message message);
        List<Message> GetAllMessages();
    }

    public class MessageRepository : IMessageRepository
    {
        private readonly IConfiguration _configuration;

        public MessageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Add(Message message)
        {
            try
            {
                using (var db = new SqlConnection(_configuration["Data:ConnectionString"]))
                {
                    db.Execute("dbo.InsertMessage",
                        new
                        {
                            @MessagePath = message.MessagePath,
                            @MessageRecordingDate = message.MessageRecordingDate
                        },
                        null,
                        null,
                        CommandType.StoredProcedure);
                }

                return true;
            }
            catch (System.Exception)
            {
                //TODO:  Probably want to eventually log what happened if it failed.
                return false;
            }
        }

        public List<Message> GetAllMessages()
        {
            List<Message> allMessages;
            try
            {
                using (var db = new SqlConnection(_configuration["Data:ConnectionString"]))
                {
                    allMessages = db
                        .Query<Message>("dbo.GetAllMessages", null, null, false, null, CommandType.StoredProcedure)
                        .ToList();
                }
            }
            catch (System.Exception)
            {
                //TODO:  Probably want to eventually log what happened if it failed.
                throw;
            }

            return allMessages;
        }
    }
}
