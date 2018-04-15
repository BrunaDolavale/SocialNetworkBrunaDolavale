using DomainModel.Entities;
using DomainModel.Interfaces.Services;
using Data.Repositories;
using DomainServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Interfaces.Repositories;
using Data.Context;
using StorageService;

namespace Application
{
    //Disponibilizar todos os serviços do nosso Domínio.
    public class ApplicationServices
    {
        //Serviço de Domínio
        private IUserService _userServices;
        private IMessageService _messageService;

        //Única instância de "ApplicationServices"
        private static ApplicationServices _instance;
        private static SocialNetworkContext context;

        private ApplicationServices(IUserService userServices, IMessageService messageService)
        {
            _userServices = userServices;
            _messageService = messageService;

        }

        //Para poder acessar esse método sem ele ter sido instanciado
        //é necessário que ele seja estático.
        public static ApplicationServices GetInstance()
        {
            if (_instance == null)
            {
                //Pseudo Injeção de Dependência
                context = new SocialNetworkContext();
                IUserRepository userRepository = new UserRepository(context);
                IConversationRepository conversationRepository = new ConversationRepository(context);
                IMessageRepository messageRepository = new MessageRepository(context);
                IUserService userServices = new UserServices(userRepository);
                IMessageService messageService = new MessageService (messageRepository);
                IConversationService conversationService = new ConversationService(conversationRepository, messageRepository);
                _instance = new ApplicationServices(userServices, messageService);

            }
            return _instance;
        }

        //########### Serviços de Perfil #############
        public void AddNewUser(User user)
        {
            _userServices.CreateUser(user);
            context.SaveChanges();
        }
        public void UpdateUser(User user)
        {
            //Get Original User
            User originalUser = _userServices.GetUser(user);
            //Copy Altered User properties to OriginalUser
            Common.CopyPropertiesTo<User>(user, originalUser);
            _userServices.UpdateUser(originalUser);
            context.SaveChanges();
        }
        public User GetUser(User user)
        {
            return _userServices.GetUser(user);
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _userServices.GetAllUsers();
        }
        //############################################

        //######### Utils #########
        public string UploadPhoto(System.IO.Stream photo, string contentType)
        {
            string photoUrl = BlobService.GetInstance().UploadFile("bruna", Guid.NewGuid().ToString(), photo, contentType);
            return photoUrl;
        }
        //#########################

        //########### Serviços de Conversation e Mensagem #############

        //private static List<Conversation> conversations = new List<Conversation>();

        public void SendMessage(Message message)
        {
            _messageService.SendMessage(message);
            context.SaveChanges();
        }
        //############################################################

    }
}
