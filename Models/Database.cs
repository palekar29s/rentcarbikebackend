using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace rentcarbike.Models
{
    public class Database: DbContext
    {

        private readonly string _connectionString;


        public Database(string connectionString)
        {
            _connectionString = connectionString;
        }

        //this is the code related to the login 
        public List<UsersClass> GetAllSignups()
        {
            List<UsersClass> signups = new List<UsersClass>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "SELECT UserId, username, passwordHash, FullName, email, phone FROM Users", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            signups.Add(new UsersClass
                            {
                                UserId = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                PasswordHash = reader.GetString(2),
                                FullName = reader.GetString(3),
                                Email = reader.GetString(4),
                                Phone = reader.GetString(5)
                            });
                        }
                    }
                }
            }

            return signups;
        }

        public List<UsersClass> GetAllUserPass()
        {
            List<UsersClass> Userpass = new List<UsersClass>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "SELECT UserId, username, PasswordHash FROM Users", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Userpass.Add(new UsersClass
                            {
                                UserId = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                PasswordHash = reader.GetString(2),

                            });
                        }
                    }
                }
            }

            return Userpass;
        }

        public void InsertSignup(UsersClass UsersClass)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string query = " INSERT INTO Users(Username,PasswordHash,FullName,Email,Phone) VALUES (@Username,@PasswordHash,@FullName,@Email,@Phone)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", UsersClass.Username);
                    cmd.Parameters.AddWithValue("@Password", UsersClass.PasswordHash);
                    cmd.Parameters.AddWithValue("@Name", UsersClass.FullName);
                    cmd.Parameters.AddWithValue("@Email", UsersClass.Email);
                    cmd.Parameters.AddWithValue("@Phone", UsersClass.Phone);
                    cmd.ExecuteNonQuery();

                }
            }
        }



    }
}
