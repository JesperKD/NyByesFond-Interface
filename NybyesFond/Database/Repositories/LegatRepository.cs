using DataAccess.Database;
using DataAccess.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class LegatRepository : ILegatRepository
    {
        private readonly IDatabase _database;

        public LegatRepository(IDatabase database)
        {
            _database = database;
        }

        public async Task<bool> CheckForNewRecords(long existingDataCount)
        {
            try
            {
                using MySqlCommand cmd = new()
                {
                    CommandText = @"SELECT COUNT(`ansoeg_id`) FROM `nybyesfo_nybyesfond`.`wordpress_ansoeg`;",

                    CommandType = CommandType.Text,
                    Connection = ((MySqlDatabase)_database).SqlConnection
                };

                await _database.OpenConnectionAsync();

                using DbDataReader reader = await cmd.ExecuteReaderAsync(behavior: CommandBehavior.CloseConnection);

                if (reader.HasRows == false) return false;

                long rowCount = 0;

                while(await reader.ReadAsync())
                {
                    rowCount = reader.GetInt64(0);
                }

                if (existingDataCount < rowCount || existingDataCount > rowCount) return true;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await _database.CloseConnectionAsync();
            }
        }

        public async Task Create(Legat createEntity)
        {
            try
            {
                using MySqlCommand cmd = new()
                {
                    CommandText = @"INSERT INTO `nybyesfo_nybyesfond`.`wordpress_ansoeg`
                                    (`first_name`, `last_name`,`e_mail`, `road_name`, `house_number`, `zip_number`, `city`, `youth_education`, `ongoing_education`, `place_of_education`, `reason_for_search`, `wished_amount`, `budget`, `date_from`, `date_to`, `is_searched_already`, `knowledge_of_other_search`, `additional_info`,`todays_date`)
                                     VALUES (@first_name, @last_name, @e_mail, @road_name, @house_number, @zip_number, @city, @youth_education, @ongoing_education, @place_of_education, @reason_for_search, @wished_amount, @budget, @date_from, @date_to, @is_searched_already, @knowledge_of_other_search, @additional_info, @todays_date)",

                    CommandType = CommandType.Text,
                    Connection = ((MySqlDatabase)_database).SqlConnection
                };

                cmd.Parameters.AddWithValue("@first_name", createEntity.Person.FirstName);
                cmd.Parameters.AddWithValue("@last_name", createEntity.Person.LastName);
                cmd.Parameters.AddWithValue("@e_mail", createEntity.Person.EMail);
                cmd.Parameters.AddWithValue("@road_name", createEntity.Person.Address.Roadname);
                cmd.Parameters.AddWithValue("@house_number", createEntity.Person.Address.HouseNumber.ToString());
                cmd.Parameters.AddWithValue("@zip_number", createEntity.Person.Address.ZipNumber);
                cmd.Parameters.AddWithValue("@city", createEntity.Person.Address.City);
                cmd.Parameters.AddWithValue("@youth_education", createEntity.Person.Education.YouthEducation);
                cmd.Parameters.AddWithValue("@ongoing_education", createEntity.Person.Education.OngoingEducation);
                cmd.Parameters.AddWithValue("@place_of_education", createEntity.Person.Education.PlaceOfEducation);
                cmd.Parameters.AddWithValue("@reason_for_search", createEntity.ReasonForSearch);
                cmd.Parameters.AddWithValue("@wished_amount", createEntity.WishedAmount.ToString());
                cmd.Parameters.AddWithValue("@budget", createEntity.Budget);
                cmd.Parameters.AddWithValue("@date_from", createEntity.DateFrom);
                cmd.Parameters.AddWithValue("@date_to", createEntity.DateTo);
                cmd.Parameters.AddWithValue("@is_searched_already", createEntity.IsSearchedAlready);
                cmd.Parameters.AddWithValue("@knowledge_of_other_search", createEntity.KnowledgeOfOtherSearch);
                cmd.Parameters.AddWithValue("@additional_info", createEntity.AdditionalInfo);
                cmd.Parameters.AddWithValue("@todays_date", createEntity.TodaysDate);

                await _database.OpenConnectionAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await _database.CloseConnectionAsync();
            }
        }

        public async Task Delete(Legat deleteEntity)
        {
            try
            {
                using MySqlCommand cmd = new()
                {
                    CommandText = @"DELETE FROM `nybyesfo_nybyesfond`.`wordpress_ansoeg` WHERE `ansoeg_id` = @id;",

                    CommandType = CommandType.Text,
                    Connection = ((MySqlDatabase)_database).SqlConnection
                };

                cmd.Parameters.AddWithValue("@id", deleteEntity.Id);
                await _database.OpenConnectionAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await _database.CloseConnectionAsync();
            }
        }

        public async Task<IEnumerable<Legat>> GetAll()
        {
            try
            {
                using MySqlCommand cmd = new()
                {
                    CommandText = @"SELECT * FROM `nybyesfo_nybyesfond`.`wordpress_ansoeg`;",

                    CommandType = CommandType.Text,
                    Connection = ((MySqlDatabase)_database).SqlConnection
                };

                await _database.OpenConnectionAsync();

                using DbDataReader reader = await cmd.ExecuteReaderAsync(behavior: CommandBehavior.CloseConnection);

                if (reader.HasRows == false) return Enumerable.Empty<Legat>();

                List<Legat> legats = new();

                while (await reader.ReadAsync())
                {
                    Legat temporaryLegat = new(
                        id: reader.GetInt64(0),
                        person: new Person(
                            firstName: reader.GetString(1),
                            lastName: reader.GetString(2),
                            eMail: reader.GetString(3),
                            address: new Address(
                                roadName: reader.GetString(4),
                                houseNumber: reader.GetString(5),
                                zipNumber: reader.GetString(6),
                                city: reader.GetString(7)),
                            education: new Education(
                                youthEducation: reader.GetString(8),
                                ongoinEducation: reader.GetString(9),
                                placeOfEducation: reader.GetString(10))),
                        reasonForSearch: reader.GetString(11),
                        wishedAmount: reader.GetString(12),
                        budget: reader.GetString(13),
                        dateFrom: reader.GetDateTime(14),
                        dateTo: reader.GetDateTime(15),
                        isSearchedAlready: reader.GetString(16),
                        knowledgeOfOtherSearch: reader.GetString(17),
                        additionalInfo: reader.GetString(18),
                        todaysDate: reader.GetDateTime(19)
                    );

                    legats.Add(temporaryLegat);
                }
                return legats;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            finally
            {
                await _database.CloseConnectionAsync();
            }
        }

        public async Task<Legat> GetById(long id)
        {
            try
            {
                using MySqlCommand cmd = new()
                {
                    CommandText = @"SELECT * FROM `nybyesfo_nybyesfond`.`wordpress_ansoeg` WHERE `ansoeg_id` = @id;",

                    CommandType = CommandType.Text,
                    Connection = ((MySqlDatabase)_database).SqlConnection
                };

                cmd.Parameters.AddWithValue("@id", id);

                await _database.OpenConnectionAsync();

                using DbDataReader reader = await cmd.ExecuteReaderAsync(behavior: CommandBehavior.CloseConnection);

                if (reader.HasRows == false) return null;

                Legat legat = null;

                while (await reader.ReadAsync())
                {
                    legat = new(
                        id: reader.GetInt64(0),
                        person: new Person(
                            firstName: reader.GetString(1),
                            lastName: reader.GetString(2),
                            eMail: reader.GetString(3),
                            address: new Address(
                                roadName: reader.GetString(4),
                                houseNumber: reader.GetString(5),
                                zipNumber: reader.GetString(6),
                                city: reader.GetString(7)),
                            education: new Education(
                                youthEducation: reader.GetString(8),
                                ongoinEducation: reader.GetString(9),
                                placeOfEducation: reader.GetString(10))),
                        reasonForSearch: reader.GetString(11),
                        wishedAmount: reader.GetString(12),
                        budget: reader.GetString(13),
                        dateFrom: reader.GetDateTime(14),
                        dateTo: reader.GetDateTime(15),
                        isSearchedAlready: reader.GetString(16),
                        knowledgeOfOtherSearch: reader.GetString(17),
                        additionalInfo: reader.GetString(18),
                        todaysDate: reader.GetDateTime(19)
                        );
                }
                return legat;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                await _database.CloseConnectionAsync();
            }



        }

        public async Task Update(Legat updateEntity)
        {
            try
            {
                using MySqlCommand cmd = new()
                {
                    CommandText = @"UPDATE `nybyesfo_nybyesfond`.`wordpress_ansoeg` 
                                SET
                                `first_name` = @firstName,
                                `last_name` = @lastName,
                                `e_mail` = @eMail,
                                `road_name` = @roadName,
                                `house_number` = @houseNumber,
                                `zip_number` = @zipNumber,
                                `city` = @city,
                                `youth_education` = @youthEducation,
                                `ongoing_education` = @ongoingEducation,
                                `place_of_education` = @placeOfEducation>,
                                `reason_for_search` = @reasonForSearch,
                                `wished_amount` = @wishedAmount,
                                `budget` = @budget,
                                `date_from` = @dateFrom,
                                `date_to` = @dateTo,
                                `is_searched_already` = @isSearchedAlready,
                                `knowledge_of_other_search` = @knowledgeOfOtherSearch,
                                `additional_info` = @additionalInfo,
                                `todays_date` = @todaysDate
                                WHERE `ansoeg_id` = @id;",

                    CommandType = CommandType.Text,
                    Connection = ((MySqlDatabase)_database).SqlConnection
                };

                cmd.Parameters.AddWithValue("@firstName", updateEntity.Person.FirstName);
                cmd.Parameters.AddWithValue("@lastName", updateEntity.Person.LastName);
                cmd.Parameters.AddWithValue("@eMail", updateEntity.Person.EMail);
                cmd.Parameters.AddWithValue("@roadName", updateEntity.Person.Address.Roadname);
                cmd.Parameters.AddWithValue("@houseNumber", updateEntity.Person.Address.HouseNumber.ToString());
                cmd.Parameters.AddWithValue("@zipNumber", updateEntity.Person.Address.ZipNumber);
                cmd.Parameters.AddWithValue("@city", updateEntity.Person.Address.City);
                cmd.Parameters.AddWithValue("@youthEducation", updateEntity.Person.Education.YouthEducation);
                cmd.Parameters.AddWithValue("@ongoingEducation", updateEntity.Person.Education.OngoingEducation);
                cmd.Parameters.AddWithValue("@placeOfEducation", updateEntity.Person.Education.PlaceOfEducation);
                cmd.Parameters.AddWithValue("@reasonForSearch", updateEntity.ReasonForSearch);
                cmd.Parameters.AddWithValue("@wishedAmount", updateEntity.WishedAmount.ToString());
                cmd.Parameters.AddWithValue("@budget", updateEntity.Budget);
                cmd.Parameters.AddWithValue("@dateFrom", updateEntity.DateFrom);
                cmd.Parameters.AddWithValue("@dateTo", updateEntity.DateTo);
                cmd.Parameters.AddWithValue("@isSearchedAlready", updateEntity.IsSearchedAlready);
                cmd.Parameters.AddWithValue("@knowledgeOfOtherSearch", updateEntity.KnowledgeOfOtherSearch);
                cmd.Parameters.AddWithValue("@additionalInfo", updateEntity.AdditionalInfo);
                cmd.Parameters.AddWithValue("@todaysDate", updateEntity.TodaysDate);

                await _database.OpenConnectionAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await _database.CloseConnectionAsync();
            }
        }
    }
}
