using Data.Repositories;
using MessageManager.Domain.Import;
using System.Collections.Generic;

namespace MessageManager.Services
{
    public interface IMessageService
    {
        IEnumerable<Message> GetAllMessages();
    }

    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public IEnumerable<Message> GetAllMessages()
        {
            return _messageRepository.GetAllMessages();
        }
    }
}
