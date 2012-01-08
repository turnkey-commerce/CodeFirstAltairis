using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CodeFirstAltairis.Models
{ 
    public class UserRepository : IUserRepository
    {
        ApplicationDB context = new ApplicationDB();

        public IQueryable<User> All
        {
			get { return context.Users; }
        }

        public IQueryable<User> AllIncluding(params Expression<Func<User, object>>[] includeProperties)
        {
            IQueryable<User> query = context.Users;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public User Find(string id)
        {
            return context.Users.Find(id);
        }

        public void InsertOrUpdate(User user)
        {
            if (user.UserName == default(string)) {
                // New entity
                context.Users.Add(user);
            } else {
                // Existing entity
                context.Users.Attach(user);
                context.Entry(user).State = EntityState.Modified;
            }
        }

        public void Delete(string id)
        {
            var user = context.Users.Find(id);
            if (user != null) {
                context.Users.Remove(user);
            }
        }

        public void AddRole(string id, string roleName) {
            var user = context.Users.Find(id);
            var role = context.Roles.Find(roleName);
            user.Roles.Add(role);
            context.Users.Attach(user);
            context.Entry(user).State = EntityState.Modified;
        }

        public void RemoveRole(string id, string roleName) {
            var user = context.Users.Find(id);
            var role = context.Roles.Find(roleName);
            user.Roles.Remove(role);
            context.Users.Attach(user);
            context.Entry(user).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IUserRepository
    {
		IQueryable<User> All { get; }
		IQueryable<User> AllIncluding(params Expression<Func<User, object>>[] includeProperties);
		User Find(string id);
		void InsertOrUpdate(User user);
        void Delete(string id);
        void AddRole(string id, string roleName);
        void RemoveRole(string id, string roleName);
        void Save();
    }
}