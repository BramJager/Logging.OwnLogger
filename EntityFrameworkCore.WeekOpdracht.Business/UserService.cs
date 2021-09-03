using EntityFrameworkCore.WeekOpdracht.Business.Entities;
using EntityFrameworkCore.WeekOpdracht.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCore.WeekOpdracht.Business
{
    public class UserService : IUserService
    {
        private readonly DataContext context;
        private readonly ILogger logger;

        public UserService()
        {
            context = new DataContext();
            logger = new DatabaseLogger(context);
        }

        public User Add(User user)
        {
            if (user == null)
            {
                Exception ex = new ArgumentNullException(nameof(user));
                logger.LogException(ex);
                throw ex;
            }
            var userContext = context.Set<User>();
            var exists = userContext.Any(x=>x.Email == user.Email);

            if (exists)
            {
                Exception ex = new Exception("User already exists with given e-mail");
                logger.LogException(ex);
                throw ex;
            }
            userContext.Add(user);
            context.SaveChanges();

            logger.LogInformation($"User: {user.Lastname} has been created with ID: {user.Id}");

            return user;
        }

        public void Delete(int id)
        {
            var entity = context.Set<User>().Single(x => x.Id == id);
            context.Set<User>().Remove(entity);
            context.SaveChanges();

            logger.LogInformation($"User: {entity.Name} with ID: {entity.Id} has been removed from the database");
        }

        public User Get(int id)
        {
            if (id <= 0)
            {
                Exception ex = new ArgumentNullException(nameof(id));
                logger.LogException(ex);
                throw ex;
            }

            return context.Set<User>().Single(x => x.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return context.Set<User>().ToList();
        }
    }
}
