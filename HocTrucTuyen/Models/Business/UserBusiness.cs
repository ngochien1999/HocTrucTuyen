using HocTrucTuyen.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HocTrucTuyen.Models.Business
{
    public class UserBusiness
    {
        private HocTrucTuyenEntities db = new HocTrucTuyenEntities();

        public IEnumerable<User> getAll(int page, int pagesize)
        {
            return db.Users.Where(x => x.FullName != "Administrator").OrderByDescending(x => x.ID).ToPagedList(page, pagesize);
        }

        public IEnumerable<Test> getInfoUser(long user_id, int page, int pagesize)
        {
            return db.Tests.Where(x => x.User_ID == user_id).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pagesize);
        }

        public User findUser(long ID)
        {
            return db.Users.Find(ID);
        }


        public bool checkLogin(User user)
        {
            if(db.Users.Count(x => x.Email == user.Email) > 0 && db.Users.Count(x => x.Password == user.Password) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool addUser(User entity)
        {
            try
            {
                var user = new User();
                user.Email = entity.Email;
                user.Password = entity.Password;
                user.FullName = entity.FullName;
                user.BirthDay = entity.BirthDay;
                user.Phone = entity.Phone;
                user.Address = entity.Address;
                db.Users.Add(user);
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public User FindUserByEmail(string email)
        {
            return db.Users.Single(x => x.Email == email);
        }

        public bool checkExitsEmail(string email)
        {
            if(db.Users.Count(x => x.Email == email) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}