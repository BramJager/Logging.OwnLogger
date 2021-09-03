using EntityFrameworkCore.WeekOpdracht.Business.Entities;
using EntityFrameworkCore.WeekOpdracht.Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCore.WeekOpdracht.Business
{
    public class MessageService : IMessageService
    {
        private readonly DataContext context;
        private readonly ILogger logger;

        public MessageService()
        {
            this.context = new DataContext();
            this.logger = new DatabaseLogger(context);
        }

        public Message Add(Message message)
        {
            if (message == null)
            {
                Exception ex = new ArgumentNullException(nameof(message));
                logger.LogException(ex);
                throw ex;
            }

            context.Set<Message>().Add(message);
            context.SaveChanges();

            logger.LogInformation($"Message: {message.Title} with ID: {message.Id} has been added to the database");
            return message;
        }

        public void Delete(int id)
        {
            var entity = context.Set<Message>().Single(x=>x.Id == id);
            context.Set<Message>().Remove(entity);
            logger.LogInformation($"Message: {entity.Title} with ID: {entity.Id} removed from database");
            context.SaveChanges();
        }

        public IEnumerable<Message> Get(int userId)
        {
            if (userId <= 0)
            {
                var ex = new ArgumentNullException(nameof(userId));
                logger.LogException(ex);
                throw ex;
            }

            return context.Set<Message>()
                .Include(x => x.Sender)
                .Where(x => x.SenderId == userId);
        }

        public IEnumerable<Message> GetAll()
        {
            return context.Set<Message>().ToList();
        }

        public Message GetById(int id)
        {
            if (id <= 0)
            {
                var ex = new ArgumentNullException(nameof(id));
                logger.LogException(ex);
                throw ex;
            }

            return context.Set<Message>()
                .Include(x => x.Sender)
                .Single(x => x.Id == id);
        }
    }
}
