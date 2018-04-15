using DomainModel.Entities;
using DomainModel.Interfaces.Repositories;
using DomainModel.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DomainServices.Services
{
    public class ConversationService : IConversationService
    {
        private IMessageRepository _messageRepository;
        private IConversationRepository _conversationRepository;

        public ConversationService(IConversationRepository conversationRepository,
            IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
            _conversationRepository = conversationRepository;
           
        }

        public void DeleteConversation(Conversation conversation)
        {
            _conversationRepository.Delete(conversation);
        }

        public void DeleteMessage(Message message)
        {
            _messageRepository.Delete(message);
        }

        public IEnumerable<Conversation> GetAllConversations(User user)
        {
            return _conversationRepository.GetAll()
                .Where(c => c.FirstToLike.Id == user.Id 
                    || c.SecondToLike.Id == user.Id)
                .OrderBy(c => c.LastUpdate)
                .Reverse();
        }

        public IEnumerable<Message> GetAllMessages(Conversation conversation)
        {
            return _conversationRepository.Get(conversation)
                .Messages
                .OrderBy(m => m.DateTimeSent)
                .Reverse();
            
        }

        public void SendMessage(Conversation conversation, Message message)
        {
            conversation.LastUpdate = DateTime.Now;
            message.DateTimeSent = conversation.LastUpdate;
            conversation.Messages.Add(message);
        }
    }
}