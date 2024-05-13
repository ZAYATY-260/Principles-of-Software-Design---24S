using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_mangement_system
{
    class User
    {
        private static User instance;
        private int Id;
        private String Username;
        private String Password;
        private String Type;

        public User()
         {

         }

        public int GetId()
        {
            return Id;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public string GetUsername()
        {
            return Username;
        }

        public void SetUsername(string username)
        {
            Username = username;
        }

        public string GetPassword()
        {
            return Password;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public string Gettype()
        {
            return Type;
        }

        public void Settype(string type)
        {
            Type = type;
        }
        public User(int id, string username, string password, string type)
        {
            Id = id;
            Username = username;
            Password = password;
            Type = type;
        }
        public static User Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new User();
                }
                return instance;
            }
        }


    }
}
