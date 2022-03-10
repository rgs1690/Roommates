using Microsoft.Data.SqlClient;
using Roommates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Repositories
{
 public class RoommateRepository : BaseRepository
    {
        public RoommateRepository(string connectionString) : base(connectionString) { }
      
        public Roommate GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
                                        FirstName, 
                                        RentPortion,
                                        MoveInDate,
                                        Room.Name 
                                     FROM Roommate 
                                     LEFT JOIN Room ON 
                                        Roommate.RoomId = Room.Id 
                                     WHERE Roommate.Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Roommate roommate = null;

                    if (reader.Read())
                    {
                        roommate = new Roommate()
                        {
                            Id = id,
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                            MovedInDate = reader.GetDateTime(reader.GetOrdinal("MoveInDate")),
                            Room = new Room() { Name = reader.GetString(reader.GetOrdinal("Name"))}

                        };
                    }
                    reader.Close();
                    return roommate;

                }
            }
        }
                public List<Roommate> GetAll()
                {
                  
                    using (SqlConnection conn = Connection)
                    {

                conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                          
                            cmd.CommandText = @"SELECT rm.Id,
                                               FirstName,
                                               LastName,
                                               RentPortion,
                                               MoveInDate,
                                               r.Name
                                        FROM Roommate rm
                                        LEFT JOIN Room r ON
                                            rm.RoomId = r.Id";

                            SqlDataReader reader = cmd.ExecuteReader();

                         
                            List<Roommate> roommates = new List<Roommate>();

                            while (reader.Read())
                            {
                           
                                int idColumnPosition = reader.GetOrdinal("Id");

                         
                                int idValue = reader.GetInt32(idColumnPosition);

                                int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                                string firstNameValue = reader.GetString(firstNameColumnPosition);

                                int lastNameColumPosition = reader.GetOrdinal("LastName");
                                string lastNameValue = reader.GetString(lastNameColumPosition);

                                Roommate roommate = new Roommate
                                {
                                    Id = idValue,
                                    FirstName = firstNameValue,
                                    LastName = lastNameValue,
                                };

                                roommates.Add(roommate);
                            }

                            
                            reader.Close();

                            return roommates;
                        }
                    }
                }

            }
        }

