using Data.Repositories;
using DomainModel.Entities;
using DomainModel.Interfaces.Repositories;
using DomainModel.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Services
{
    public class MessageService : IMessageService
    {
        private IMessageRepository _messageRepository;
        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        private static List<Message> messages = new List<Message>();

        public void SendMessage(Message message)
        {
            messages.Add(message);
        }

        public static List<Message> GetAllMessages()
        {
            return messages;

        }
    }
}
