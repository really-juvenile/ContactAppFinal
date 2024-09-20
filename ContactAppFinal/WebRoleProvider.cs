using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ContactAppFinal.Data;
using ContactAppFinal.Models;

namespace ContactAppFinal
{
    public class WebRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            return new[] { "Admin", "Staff" };
        }

        public override string[] GetRolesForUser(string username)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var user = session.Query<User>().FirstOrDefault(u => u.FName == username);
                if (user != null)
                {
                    if (user.IsAdmin)
                        return new[] { "Admin" };
                    else
                        return new[] { "Staff" };
                }
                return new string[] { };
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var roles = GetRolesForUser(username);
            return roles.Contains(roleName);
        }

       //public override string ApplicationName { get; set; }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            return (roleName == "Admin" || roleName == "Staff");
        }
    }
}