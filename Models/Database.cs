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


        // ts is the code related to register 
        public int InsertUser(UsersClass user)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Users 
                        (Username, PasswordHash, FullName, Email, Phone)
                        VALUES 
                        (@Username, @PasswordHash, @FullName, @Email, @Phone)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
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



        public void UpdateVehicleStatus(int vehicleId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                string query = "UPDATE Vehicle SET Status = @Status WHERE VehicleId = @VehicleId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "Unavailable");
                    cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        //Booking related query */
        public List<BookingClass> GetBookingClasses()
        {
            List<BookingClass> bookings = new List<BookingClass>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT 
                BookingId,
                UserId,
                VehicleId,
                StartDate,
                EndDate,
                TotalPrice,
                BookingStatus
              FROM Booking", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bookings.Add(new BookingClass
                            {
                                BookingId = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                VehicleId = reader.GetInt32(2),
                                StartDate = reader.GetDateTime(3),
                                EndDate = reader.GetDateTime(4),
                                TotalPrice = reader.GetDecimal(5),
                                BookingStatus = reader.GetString(6)
                            });
                        }
                    }
                }
            }

            return bookings;
        }


        public int AddBooking(BookingClass booking)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO Booking
              (UserId, VehicleId, StartDate, EndDate, TotalPrice, BookingStatus)
              OUTPUT INSERTED.BookingId
              VALUES
              (@UserId, @VehicleId, @StartDate, @EndDate, @TotalPrice, @BookingStatus)", conn))
                {
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = booking.UserId;
                    cmd.Parameters.Add("@VehicleId", SqlDbType.Int).Value = booking.VehicleId;
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = booking.StartDate;
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = booking.EndDate;
                    cmd.Parameters.Add("@TotalPrice", SqlDbType.Decimal).Value = booking.TotalPrice;
                    cmd.Parameters.Add("@BookingStatus", SqlDbType.VarChar, 50).Value = booking.BookingStatus;

                    return (int)cmd.ExecuteScalar(); // returns new BookingId
                }
            }
        }

        public bool UpdateBookingStatus(int bookingId, string status)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(
                    @"UPDATE Booking 
              SET BookingStatus = @BookingStatus
              WHERE BookingId = @BookingId", conn))
                {
                    cmd.Parameters.Add("@BookingStatus", SqlDbType.VarChar, 50).Value = status;
                    cmd.Parameters.Add("@BookingId", SqlDbType.Int).Value = bookingId;

                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }

        public List<BookingHistoryDto> GetBookingsByUserId(int userId)
        {
            List<BookingHistoryDto> list = new();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(
                @"SELECT 
            b.BookingId,
            b.UserId,
            u.Username,
            b.VehicleId,
            b.StartDate,
            b.EndDate,
            b.TotalPrice,
            b.BookingStatus
          FROM Booking b
          INNER JOIN Users u ON b.UserId = u.UserId
          WHERE b.UserId = @UserId
          ORDER BY b.BookingId DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new BookingHistoryDto
                            {
                                BookingId = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                Username = reader.GetString(2),
                                VehicleId = reader.GetInt32(3),
                                StartDate = reader.GetDateTime(4),
                                EndDate = reader.GetDateTime(5),
                                TotalPrice = reader.GetDecimal(6),
                                BookingStatus = reader.GetString(7)
                            });
                        }
                    }
                }
            }

            return list;
        }



        /* 
         //Payment related qMicrosoft.Data.SqlClient.SqlException: 'The INSERT uery 

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
       
       

        //this is use in the get partiuclar vechile in detail 
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
    v.StartDate, v.EndDate, v.Location, v.Seats,
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

                                    // NEW FIELDS
                                    StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : (DateTime?)null,
                                    EndDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]) : (DateTime?)null,
                                    Location = reader["Location"].ToString(),
                                    Seats = reader["Seats"] != DBNull.Value ? (int?)Convert.ToInt32(reader["Seats"]) : null,

                                    Images = new List<VehicleImagesClass>()
                                };
                            }

                            // Add vehicle images
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
       
        // this is the filter in the search section it is use 
       
        public List<VehicleClass> GetVehiclesWithImagess(
    string vehicle = null,
    DateTime? checkin = null,
    DateTime? checkout = null,
    string location = null,
    int? seats = null)
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
            v.StartDate, v.EndDate, v.Location, v.Seats,
            vi.ImageId, vi.ImageUrl AS VehicleImage, vi.VehicleName
        FROM Vehicle v
        LEFT JOIN VehicleImages vi
            ON v.VehicleId = vi.VehicleId
        WHERE 
            (@vehicle IS NULL OR v.Type = @vehicle)
            AND (@location IS NULL OR v.Location = @location)
            AND (@seats IS NULL OR v.Seats = @seats OR v.Seats IS NULL)
            AND (
                @checkin IS NULL OR 
                @checkout IS NULL OR
                (v.StartDate <= @checkin AND v.EndDate >= @checkout)
            )
        ORDER BY v.VehicleId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@vehicle", SqlDbType.VarChar).Value =
                        (object)vehicle ?? DBNull.Value;

                    cmd.Parameters.Add("@location", SqlDbType.VarChar).Value =
                        (object)location ?? DBNull.Value;

                    cmd.Parameters.Add("@seats", SqlDbType.Int).Value =
                        (object)seats ?? DBNull.Value;

                    cmd.Parameters.Add("@checkin", SqlDbType.DateTime).Value =
                        (object)checkin ?? DBNull.Value;

                    cmd.Parameters.Add("@checkout", SqlDbType.DateTime).Value =
                        (object)checkout ?? DBNull.Value;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int vehId = Convert.ToInt32(reader["VehicleId"]);

                            var vehicleObj = vehicles.FirstOrDefault(v => v.VehicleId == vehId);

                            if (vehicleObj == null)
                            {
                                vehicleObj = new VehicleClass
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

                                    StartDate = reader["StartDate"] == DBNull.Value
                                        ? null
                                        : Convert.ToDateTime(reader["StartDate"]),

                                    EndDate = reader["EndDate"] == DBNull.Value
                                        ? null
                                        : Convert.ToDateTime(reader["EndDate"]),

                                    Location = reader["Location"].ToString(),
                                    Seats = reader["Seats"] == DBNull.Value
                                        ? (int?)null
                                        : Convert.ToInt32(reader["Seats"]),

                                    Images = new List<VehicleImagesClass>()
                                };

                                vehicles.Add(vehicleObj);
                            }

                            if (reader["VehicleImage"] != DBNull.Value)
                            {
                                vehicleObj.Images.Add(new VehicleImagesClass
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
            }

            return vehicles;
        }


    }
}
