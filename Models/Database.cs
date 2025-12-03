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
//vehicle related query 
        public List<VehicleClass> GetVehicles()
        {
            List<VehicleClass> vehicles = new List<VehicleClass>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "SELECT id, Name, type, brand, model, priceperhour ,priceperday ,fueltype, transmission , status FROM SIGNUP", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            vehicles.Add(new VehicleClass
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                type = reader.GetString(2),
                                brand = reader.GetString(3),
                                model = reader.GetString(4),
                                priceperday = reader.GetString(5),
                                priceperhour = reader.GetString(6),
                                fueltype = reader.GetString(7), 
                                transmission = reader.GetString(8),
                                status = reader.GetString(9)
                            });
                        }
                    }
                }
            }

            return vehicles;
        }
        public void InsertVehicle(VehicleClass vehicles)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string query = " INSERT INTO vehicle(id, Name, type, brand, model, priceperhour ,priceperday ,fueltype, transmission , status) VALUES (@id, @Name, @type, @brand,@model, @priceperhour ,@priceperday ,@fueltype, @transmission , @status)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", vehicles.Id);
                    cmd.Parameters.AddWithValue("@Name", vehicles.Name);
                    cmd.Parameters.AddWithValue("@type", vehicles.type);
                    cmd.Parameters.AddWithValue("@brand", vehicles.brand);
                    cmd.Parameters.AddWithValue("@model", vehicles.model);
                    cmd.Parameters.AddWithValue("@priceperhour", vehicles.priceperhour);
                    cmd.Parameters.AddWithValue("@priceperday", vehicles.priceperday);
                    cmd.Parameters.AddWithValue("@fueltype", vehicles.fueltype);
                    cmd.Parameters.AddWithValue("@transmission", vehicles.transmission);
                    cmd.Parameters.AddWithValue("@status", vehicles.status);
                    cmd.ExecuteNonQuery();

                }
            }
        }

//Booking related query 
        public List<BookingClass> GetBookingClasses()
        {
            List<BookingClass> book = new List<BookingClass>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(
                    "Select id,userid,vechileisd,startdate,enddate,tototprice,bookingstatus from booking",conn))
                {
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            book.Add(new BookingClass
                            {
                                Id=reader.GetInt32(0),
                                userid=reader.GetString(1),
                                vehicleid= reader.GetString(2),
                                startdate= reader.GetString(3),
                                enddate= reader.GetString(4),
                                totolprice= reader.GetString(5),
                                bookingstatus= reader.GetString(6)


                            });
                        }
                    }
                }
            }
            return book;
        }

        //Payment related query 

        public List<PaymentClass> getpayement()
        {
            List<PaymentClass> payment = new List<PaymentClass>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "SELECT id, Name, paymentMethod, bookingid, status FROM SIGNUP", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            payment.Add(new PaymentClass
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                paymentMethod = reader.GetString(2),
                                bookingid = reader.GetString(3),
                                status = reader.GetString(4),
                                
                            });
                        }
                    }
                }
            }

            return payment;
        }
        public void Insertpayment(PaymentClass vehicles)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string query = " INSERT INTO vehicle(id, Name, paymentMethod, bookingid, status) VALUES (@id, @Name, @paymentMethod, @bookingid, @status)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", vehicles.Id);
                    cmd.Parameters.AddWithValue("@Name", vehicles.Name);
                    cmd.Parameters.AddWithValue("@paymentMethod", vehicles.paymentMethod);
                    cmd.Parameters.AddWithValue("@bookingid", vehicles.bookingid);
                    cmd.Parameters.AddWithValue("@status", vehicles.status);
                  

                }
            }
        }

        //review related query 

        public List<ReviewsClass> getreview()
        {
            List<ReviewsClass> reviews = new List<ReviewsClass>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "SELECT id, Name, Description, userid, Vehicleid, rating FROM Review", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reviews.Add(new ReviewsClass
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                userid = reader.GetString(3),
                                Vehicleid = reader.GetString(4),
                                rating = reader.GetString(5),
                                
                            });
                        }
                    }
                }
            }

            return reviews;
        }
        public void Insertreviews(ReviewsClass review)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string query = " INSERT INTO vehicle(id, Name, Description, userid, Vehicleid, rating) VALUES (@id, @Name, @Description, @userid, @Vehicleid,@rating)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", review.Id);
                    cmd.Parameters.AddWithValue("@Name", review.Name);
                    cmd.Parameters.AddWithValue("@type", review.Description);
                    cmd.Parameters.AddWithValue("@brand", review.userid);
                    cmd.Parameters.AddWithValue("@model", review.Vehicleid);
                    cmd.Parameters.AddWithValue("@priceperhour", review.rating);
                    
                    cmd.ExecuteNonQuery();

                }
            }
        }


 

    }
}
