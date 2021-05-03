using DataAccess.Database;
using DataAccess.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

        public bool CheckForNewRecords(int existingDataCount)
        {
            throw new NotImplementedException();
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
                cmd.Parameters.AddWithValue("@e_mail", createEntity.Person.eMail);
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

        public Task Delete(Legat deleteEntity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Legat>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Legat> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Legat updateEntity)
        {
            throw new NotImplementedException();
        }
    }
}
