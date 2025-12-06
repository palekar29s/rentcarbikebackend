using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

        public List<UserClass> GetAllUserPass()
        {
            List<UserClass> Userpass = new List<UserClass>();

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
                            Userpass.Add(new UserClass
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
                    cmd.Parameters.AddWithValue("@PasswordHash", UsersClass.PasswordHash);
                    cmd.Parameters.AddWithValue("@FullName", UsersClass.FullName);
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

                string query = @"
            SELECT VehicleId, Name, Type, Brand, Model,
                   PricePerHour, PricePerDay, FuelType,
                   Transmission, ImageUrl, Status
            FROM Vehicle";

                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vehicles.Add(new VehicleClass
                        {
                            VehicleId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            type = reader.GetString(2),
                            brand = reader.GetString(3),
                            model = reader.GetString(4),
                            priceperhour = reader.GetDecimal(5), // Correct order
                            priceperday = reader.GetDecimal(6),  // Correct order
                            fueltype = reader.GetString(7),
                            transmission = reader.GetString(8),
                          //  imageUrl = reader.GetString(9),
                            status = reader.GetString(10)
                        });
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

                string query = @"
            INSERT INTO Vehicle
            (Name, Type, Brand, Model, PricePerHour, PricePerDay,
             FuelType, Transmission, ImageUrl, Status)
            VALUES
            (@Name, @Type, @Brand, @Model, @PricePerHour, @PricePerDay,
             @FuelType, @Transmission, @ImageUrl, @Status)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", vehicles.Name);
                    cmd.Parameters.AddWithValue("@Type", vehicles.type);
                    cmd.Parameters.AddWithValue("@Brand", vehicles.brand);
                    cmd.Parameters.AddWithValue("@Model", vehicles.model);
                    cmd.Parameters.AddWithValue("@PricePerHour", vehicles.priceperhour);
                    cmd.Parameters.AddWithValue("@PricePerDay", vehicles.priceperday);
                    cmd.Parameters.AddWithValue("@FuelType", vehicles.fueltype);
                    cmd.Parameters.AddWithValue("@Transmission", vehicles.transmission);
                    cmd.Parameters.AddWithValue("@ImageUrl", vehicles.imageUrl);
                    cmd.Parameters.AddWithValue("@Status", vehicles.status);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        //Booking related query */
        public List<BookingClass> GetBookingClasses()
          {
              List<BookingClass> book = new List<BookingClass>();

              using (SqlConnection conn = new SqlConnection(_connectionString))
              {
                  conn.Open();
                  using(SqlCommand cmd = new SqlCommand(
                      "Select BookingId,userid,vechileisd,startdate,enddate,tototprice,bookingstatus from Booking", conn))
                  {
                      using(SqlDataReader reader = cmd.ExecuteReader())
                      {
                          while (reader.Read())
                          {
                              book.Add(new BookingClass
                              {
                                  BookingId = reader.GetInt32(0),
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
        /* 
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

         

         */

        //the code related to reviews
        public List<ReviewsClass> GetReviews()
        {
            List<ReviewsClass> reviews = new List<ReviewsClass>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                string query = @"SELECT ReviewId, UserId, VehicleId, Rating, Comment, CreatedAt 
                         FROM Reviews";

                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reviews.Add(new ReviewsClass
                        {
                            ReviewId = reader.GetInt32(0),
                            UserId = reader.GetInt32(1),
                            VehicleId = reader.GetInt32(2),
                            Rating = reader.GetInt32(3),
                            Comment = reader.GetString(4),
                            CreatedAt = reader.GetDateTime(5)
                        });
                    }
                }
            }

            return reviews;
        }
        public void InsertReview(ReviewsClass review)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                string query = @"INSERT INTO Reviews 
                        (UserId, VehicleId, Rating, Comment, CreatedAt)
                         VALUES 
                        (@UserId, @VehicleId, @Rating, @Comment, @CreatedAt)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", review.UserId);
                    cmd.Parameters.AddWithValue("@VehicleId", review.VehicleId);
                    cmd.Parameters.AddWithValue("@Rating", review.Rating);
                    cmd.Parameters.AddWithValue("@Comment", review.Comment);
                    cmd.Parameters.AddWithValue("@CreatedAt", review.CreatedAt);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        //the query related to images vehicle 
        //this query related to images 
        public List<VehicleImagesClass> GetImages()
        {
            var images = new List<VehicleImagesClass>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM VehicleImages";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var image = new VehicleImagesClass
                        {
                            ImageId = Convert.ToInt32(reader["ImageId"]),
                            VehicleId = Convert.ToInt32(reader["VehicleId"]),
                            ImageUrl = reader["ImageUrl"] as string,
                            VehicleName = reader["VehicleName"].ToString()
                        };

                        images.Add(image); // <-- you forgot this
                    }
                }
            }

            return images;
        }

        public void InsertVehicleImage(VehicleImagesClass image)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO VehicleImages (VehicleId, ImageUrl, VehicleName)
                         VALUES (@VehicleId, @ImageUrl, @VehicleName)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VehicleId", image.VehicleId);
                    cmd.Parameters.AddWithValue("@ImageUrl", image.ImageUrl); // byte[]
                    cmd.Parameters.AddWithValue("@VehicleName", image.VehicleName);

                    cmd.ExecuteNonQuery();
                }
            }
        }
       
        public List<VehicleImagesClass> GetVehicleImagesById(int vehicleId)
        {
            var images = new List<VehicleImagesClass>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("GetVehicleImagesByIdd", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            images.Add(new VehicleImagesClass
                            {
                                ImageId = Convert.ToInt32(reader["ImageId"]),
                                VehicleId = Convert.ToInt32(reader["VehicleId"]),
                                ImageUrl = reader["ImageUrl"] as string,
                                VehicleName = reader["VehicleName"].ToString()
                            });
                        }
                    }
                }
            }

            return images;
        }

        public List<VehicleClass> GetVehiclesWithImages()
        {
            var vehicles = new List<VehicleClass>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                string query = @"
            SELECT 
                v.VehicleId, v.Name, v.Type, v.Brand, v.Model,
                v.PricePerHour, v.PricePerDay, v.FuelType, v.Transmission,
                v.ImageUrl, v.Status,
                vi.ImageId, vi.ImageUrl AS VehicleImage, vi.VehicleName
            FROM Vehicle v
            LEFT JOIN VehicleImages vi
                ON v.VehicleId = vi.VehicleId
            ORDER BY v.VehicleId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int vehId = Convert.ToInt32(reader["VehicleId"]);

                        // Does this vehicle already exist in the list?
                        var vehicle = vehicles.FirstOrDefault(v => v.VehicleId == vehId);

                        if (vehicle == null)
                        {
                            vehicle = new VehicleClass
                            {
                                VehicleId = vehId,
                                Name = reader["Name"].ToString(),
                                type = reader["Type"].ToString(),
                                brand = reader["Brand"].ToString(),
                                model = reader["Model"].ToString(),
                                priceperhour = Convert.ToDecimal(reader["PricePerHour"]),
                                priceperday = Convert.ToDecimal(reader["PricePerDay"]),
                                fueltype = reader["FuelType"].ToString(),
                                transmission = reader["Transmission"].ToString(),
                                imageUrl = reader["ImageUrl"].ToString(),
                                status = reader["Status"].ToString(),
                                Images = new List<VehicleImagesClass>()   // Initialize list
                            };

                            vehicles.Add(vehicle);
                        }

                        // Add image if exists
                        if (reader["VehicleImage"] != DBNull.Value)
                        {
                            vehicle.Images.Add(new VehicleImagesClass
                            {
                                ImageId = Convert.ToInt32(reader["ImageId"]),
                                VehicleId = vehId,
                                ImageUrl = reader["VehicleImage"].ToString(),
                                VehicleName = reader["VehicleName"].ToString()
                            });
                        }
                    }
                }
            }

            return vehicles;
        }


        public VehicleClass GetVehicleById(int vehicleId)
        {
            VehicleClass vehicle = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                string query = @"
        SELECT 
            v.VehicleId, v.Name, v.Type, v.Brand, v.Model,
            v.PricePerHour, v.PricePerDay, v.FuelType, v.Transmission,
            v.ImageUrl AS DefaultImage, v.Status,
            vi.ImageId,
            vi.ImageUrl AS VehicleImage,
            vi.VehicleName
        FROM Vehicle v
        LEFT JOIN VehicleImages vi ON v.VehicleId = vi.VehicleId
        WHERE v.VehicleId = @VehicleId
        ORDER BY vi.ImageId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (vehicle == null)
                            {
                                vehicle = new VehicleClass
                                {
                                    VehicleId = Convert.ToInt32(reader["VehicleId"]),
                                    Name = reader["Name"].ToString(),
                                    type = reader["Type"].ToString(),
                                    brand = reader["Brand"].ToString(),
                                    model = reader["Model"].ToString(),
                                    priceperhour = Convert.ToDecimal(reader["PricePerHour"]),
                                    priceperday = Convert.ToDecimal(reader["PricePerDay"]),
                                    fueltype = reader["FuelType"].ToString(),
                                    transmission = reader["Transmission"].ToString(),
                                    imageUrl = reader["DefaultImage"].ToString(),
                                    status = reader["Status"].ToString(),
                                    Images = new List<VehicleImagesClass>()
                                };
                            }

                            // Add normal URL (varchar)
                            if (reader["VehicleImage"] != DBNull.Value)
                            {
                                vehicle.Images.Add(new VehicleImagesClass
                                {
                                    ImageId = reader["ImageId"] != DBNull.Value ? Convert.ToInt32(reader["ImageId"]) : 0,
                                    VehicleId = vehicleId,
                                    ImageUrl = reader["VehicleImage"].ToString(),
                                    VehicleName = reader["VehicleName"].ToString()
                                });
                            }
                        }
                    }
                }
            }

            return vehicle;
        }


    }
}
