using Dapper;
using MessageManager.Domain.Import;
using System.Data;
using System.Data.SqlClient;

namespace Data.Repositories
{
    public interface IMessageRepository
    {
        bool Add(Message message);
    }

    public class MessageRepository : IMessageRepository
    {
        public bool Add(Message message)
        {
            try
            {
                using (var db = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=MediaManagement;Integrated Security=True;MultipleActiveResultSets=True"))
                {
                    db.Execute("dbo.InsertMessage",
                        new
                        {
                            @MessagePath = message.Mp3FileName,
                            @MessageRecordingDate = message.DateOfRecording
                        },
                        null,
                        null,
                        CommandType.StoredProcedure);
                }

                return true;
            }
            catch (System.Exception)
            {
                //Probably want to eventually log what happened if it failed.
                return false;
            }
        }
    }
}
