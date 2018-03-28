using Data.Repositories;
using MessageManager.Domain.Import;
using System.Collections.Generic;

namespace MessageManager.Services
{
    public interface IMessageService
    {
        IEnumerable<Message> GetAllMessages();
        Message GetMessageById(int id);
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

        public Message GetMessageById(int id)
        {
            return _messageRepository.GetMessageById(id);
        }
    }
}
