using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CodeFirstAltairis.Models
{ 
    public class RoleRepository : IRoleRepository
    {
        ApplicationDB context = new ApplicationDB();

        public IQueryable<Role> All
        {
			get { return context.Roles; }
        }

        public IQueryable<Role> AllIncluding(params Expression<Func<Role, object>>[] includeProperties)
        {
            IQueryable<Role> query = context.Roles;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Role Find(string id)
        {
            return context.Roles.Find(id);
        }

        public void InsertOrUpdate(Role role)
        {
            // Roles are different as there is only one column (primary key). Therefore there is really only insert
            // without messing up all of the relationships.
            context.Roles.Add(role);
        }

        public void Delete(string id)
        {
            var role = context.Roles.Find(id);
            context.Roles.Remove(role);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IRoleRepository
    {
		IQueryable<Role> All { get; }
		IQueryable<Role> AllIncluding(params Expression<Func<Role, object>>[] includeProperties);
		Role Find(string id);
		void InsertOrUpdate(Role role);
        void Delete(string id);
        void Save();
    }
}