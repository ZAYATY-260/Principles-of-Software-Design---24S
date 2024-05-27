using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;

namespace bank_mangement_system.Repo
{
    class UserRepository
    {


        public void AddPerson(User person)
        {
                DBconfig Db = new DBconfig();
                Db.Open_connection();
                AESEncryption aesEncryption = new AESEncryption();

                string query = $"INSERT INTO AdminTbl (AdName, AdPass , AdType) VALUES (@Username, @Password,@Type)";
                SqlCommand command = new SqlCommand(query, Db.Get_Conn());
                command.Parameters.AddWithValue("@Username", aesEncryption.Encrypt(person.GetUsername()));
                command.Parameters.AddWithValue("@Password", aesEncryption.Encrypt(person.GetPassword()));
                command.Parameters.AddWithValue("@Type", aesEncryption.Encrypt(person.Gettype()));
                command.ExecuteNonQuery();

                Db.Close_connection();
        }

        public bool Login(User person)
        {
            bool userFound = false;
            DBconfig Db = new DBconfig();
            Db.Open_connection();
            AESEncryption aesEncryption = new AESEncryption();

            string query = "SELECT  AdId,AdName, AdPass, AdType FROM AdminTbl WHERE AdName = @username And AdPass = @password AND Adtype = @Adtype";
            SqlCommand command = new SqlCommand(query, Db.Get_Conn());
            command.Parameters.AddWithValue("@username" , aesEncryption.Encrypt(person.GetUsername()));
            command.Parameters.AddWithValue("@password" , aesEncryption.Encrypt(person.GetPassword()));
            command.Parameters.AddWithValue("@Adtype", aesEncryption.Encrypt("EMP"));
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    userFound = true;

                    // Set the values to the user object if needed
                    person.SetId(reader.GetInt32(reader.GetOrdinal("AdId")));
                    person.SetUsername(aesEncryption.Decrypt(reader.GetString(reader.GetOrdinal("AdName"))));
                    person.SetPassword(aesEncryption.Decrypt(reader.GetString(reader.GetOrdinal("AdPass"))));
                    person.Settype(aesEncryption.Decrypt(reader.GetString(reader.GetOrdinal("AdType"))));

                    Debug.WriteLine("User found in the database.");
                }
            }

            // Check if user was found
            if (!userFound)
            {
                Debug.WriteLine("User not found in the database.");
            }
            Db.Close_connection();

            return userFound;
        }

        public User GetPerson(User person)
        {
            bool userFound = false;
            DBconfig Db = new DBconfig();
            Db.Open_connection();
            AESEncryption aesEncryption = new AESEncryption();

            string query = "SELECT  AdId,AdName, AdPass, AdType FROM AdminTbl WHERE AdName LIKE @SearchTerm";
            SqlCommand command = new SqlCommand(query, Db.Get_Conn());
            command.Parameters.AddWithValue("@SearchTerm", "%" + aesEncryption.Encrypt(person.GetUsername()) + "%");

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    userFound = true;

                    // Set the values to the user object if needed
                    person.SetId(reader.GetInt32(reader.GetOrdinal("AdId")));
                    person.SetUsername(aesEncryption.Decrypt(reader.GetString(reader.GetOrdinal("AdName"))));
                    person.SetPassword(aesEncryption.Decrypt(reader.GetString(reader.GetOrdinal("AdPass"))));
                    person.Settype(aesEncryption.Decrypt(reader.GetString(reader.GetOrdinal("AdType"))));

                    Debug.WriteLine("User found in the database." + person.Gettype());
                }
            }

            // Check if user was found
            if (!userFound)
            {
                Debug.WriteLine("User not found in the database.");
            }
            Db.Close_connection();

            return person;
        }

        public String EditPerson(User person)
        {
            try
            {
                DBconfig Db = new DBconfig();
                Db.Open_connection();
                AESEncryption aesEncryption = new AESEncryption();
                User user = GetPerson(person);
                if (user.GetId() !=  0)
                {
                    
                    string query = "UPDATE AdminTbl SET AdName = @Username, AdPass = @Password, AdType = @Type WHERE Adid = @Id";
                    SqlCommand command = new SqlCommand(query, Db.Get_Conn());

                    command.Parameters.AddWithValue("@Id", person.GetId());
                    command.Parameters.AddWithValue("@Username", aesEncryption.Encrypt(person.GetUsername()));
                    command.Parameters.AddWithValue("@Password", aesEncryption.Encrypt(person.GetPassword()));
                    command.Parameters.AddWithValue("@Type", aesEncryption.Encrypt(person.Gettype()));
                    command.ExecuteNonQuery();

                    Db.Close_connection();

                    return " User data is updated  , ";
                }
                else
                {
                    return "User is not found";
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error adding account: " + ex.Message);
                throw;
            }
        }

        //    public void DeletePerson(int id)
        //    {
        //        using (var connection = new MySqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            string query = "DELETE FROM Persons WHERE Id = @Id";
        //            MySqlCommand command = new MySqlCommand(query, connection);
        //            command.Parameters.AddWithValue("@Id", id);
        //            command.ExecuteNonQuery();
        //        }
        //    }

        public List<User> GetAllPersons()
        {
            DBconfig Db = new DBconfig();
            Db.Open_connection();
            AESEncryption aesEncryption = new AESEncryption();
            List<User> users = new List<User>();
            string query = "SELECT ADId, AdName , AdType ,AdPass  FROM AdminTbl WHERE AdType = @AdType";
            SqlCommand command = new SqlCommand(query, Db.Get_Conn());
            command.Parameters.AddWithValue("@Adtype", aesEncryption.Encrypt("CUST"));
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    User user = new User();
                    user.SetId(reader.GetInt32(reader.GetOrdinal("AdId")));
                    user.SetUsername(aesEncryption.Decrypt(reader.GetString(reader.GetOrdinal("AdName"))));
                    user.Settype(aesEncryption.Decrypt(reader.GetString(reader.GetOrdinal("AdType"))));
                    user.SetPassword(aesEncryption.Decrypt(reader.GetString(reader.GetOrdinal("AdPass"))));
                    users.Add(user);

                    Debug.WriteLine("User found in the database.");
                }
            }
            Debug.WriteLine(users);
            return users;
        }

}
}
