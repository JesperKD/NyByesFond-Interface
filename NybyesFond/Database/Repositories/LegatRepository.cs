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
            var cmdText = @"SELECT COUNT(`ansoeg_id`) FROM `nybyesfo_nybyesfond`.`wordpress_ansoeg`;";

            using var dataReader = await _database.GetDataReaderAsync(cmdText);

            if (dataReader.HasRows == false) return false;

            while (await dataReader.ReadAsync())
            {
                long currentDataCount = dataReader.GetInt64(0);

                if (ThereIsNewRecords(existingDataCount, currentDataCount)) return true;
            }

            // If No new Records are found.
            return false;
        }

        public async Task CreateAsync(Legat createEntity)
        {
            string cmdText = @"INSERT INTO `nybyesfo_nybyesfond`.`wordpress_ansoeg`
                                    (`first_name`, `last_name`,`e_mail`, `road_name`, `house_number`, `zip_number`, `city`, `youth_education`, `ongoing_education`, `place_of_education`, `reason_for_search`, `wished_amount`, `budget`, `date_from`, `date_to`, `is_searched_already`, `knowledge_of_other_search`, `additional_info`,`todays_date`)
                                     VALUES (@first_name, @last_name, @e_mail, @road_name, @house_number, @zip_number, @city, @youth_education, @ongoing_education, @place_of_education, @reason_for_search, @wished_amount, @budget, @date_from, @date_to, @is_searched_already, @knowledge_of_other_search, @additional_info, @todays_date)";

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@first_name", createEntity.Person.FirstName },
                { "@last_name", createEntity.Person.LastName },
                { "@e_mail", createEntity.Person.EMail },
                { "@road_name", createEntity.Person.Address.Roadname },
                { "@house_number", createEntity.Person.Address.HouseNumber.ToString() },
                { "@zip_number", createEntity.Person.Address.ZipNumber },
                { "@city", createEntity.Person.Address.City },
                { "@youth_education", createEntity.Person.Education.YouthEducation },
                { "@ongoing_education", createEntity.Person.Education.OngoingEducation },
                { "@place_of_education", createEntity.Person.Education.PlaceOfEducation },
                { "@reason_for_search", createEntity.ReasonForSearch },
                { "@wished_amount", createEntity.WishedAmount.ToString() },
                { "@budget", createEntity.Budget },
                { "@date_from", createEntity.DateFrom },
                { "@date_to", createEntity.DateTo },
                { "@is_searched_already", createEntity.IsSearchedAlready },
                { "@knowledge_of_other_search", createEntity.KnowledgeOfOtherSearch },
                { "@additional_info", createEntity.AdditionalInfo },
                { "@todays_date", createEntity.TodaysDate }
            };

            await _database.ExecuteNonQueryAsync(cmdText, sqlParams);
        }

        public async Task DeleteAsync(Legat deleteEntity)
        {
            string cmdText = @"DELETE FROM `nybyesfo_nybyesfond`.`wordpress_ansoeg` WHERE `ansoeg_id` = @id;";

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@id", deleteEntity.Id }
            };

            await _database.ExecuteNonQueryAsync(cmdText, sqlParams);
        }

        public async Task<IEnumerable<Legat>> GetAllAsync()
        {
            string cmdText = @"SELECT * FROM `nybyesfo_nybyesfond`.`wordpress_ansoeg`;";

            using var dataReader = await _database.GetDataReaderAsync(cmdText);

            if (dataReader.HasRows == false) return Enumerable.Empty<Legat>();

            List<Legat> legats = new();

            while (await dataReader.ReadAsync())
            {
                Legat temporaryLegat = new(
                    id: dataReader.GetInt64(0),
                    person: new Person(
                        firstName: dataReader.GetString(1),
                        lastName: dataReader.GetString(2),
                        eMail: dataReader.GetString(3),
                        address: new Address(
                            roadName: dataReader.GetString(4),
                            houseNumber: dataReader.GetString(5),
                            zipNumber: dataReader.GetString(6),
                            city: dataReader.GetString(7)),
                        education: new Education(
                            youthEducation: dataReader.GetString(8),
                            ongoinEducation: dataReader.GetString(9),
                            placeOfEducation: dataReader.GetString(10))),
                    reasonForSearch: dataReader.GetString(11),
                    wishedAmount: dataReader.GetString(12),
                    budget: dataReader.GetString(13),
                    dateFrom: dataReader.GetDateTime(14),
                    dateTo: dataReader.GetDateTime(15),
                    isSearchedAlready: dataReader.GetString(16),
                    knowledgeOfOtherSearch: dataReader.GetString(17),
                    additionalInfo: dataReader.GetString(18),
                    todaysDate: dataReader.GetDateTime(19)
                );

                legats.Add(temporaryLegat);
            }
            return legats;
        }

        public async Task<Legat> GetByIdAsync(long id)
        {
            string cmdText = @"SELECT * FROM `nybyesfo_nybyesfond`.`wordpress_ansoeg` WHERE `ansoeg_id` = @id;";

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@id", id }
            };

            var dataReader = await _database.GetDataReaderAsync(cmdText, sqlParams);

            if (dataReader.HasRows == false) return null;

            Legat legat = null;

            while (await dataReader.ReadAsync())
            {
                legat = new(
                    id: dataReader.GetInt64(0),
                    person: new Person(
                        firstName: dataReader.GetString(1),
                        lastName: dataReader.GetString(2),
                        eMail: dataReader.GetString(3),
                        address: new Address(
                            roadName: dataReader.GetString(4),
                            houseNumber: dataReader.GetString(5),
                            zipNumber: dataReader.GetString(6),
                            city: dataReader.GetString(7)),
                        education: new Education(
                            youthEducation: dataReader.GetString(8),
                            ongoinEducation: dataReader.GetString(9),
                            placeOfEducation: dataReader.GetString(10))),
                    reasonForSearch: dataReader.GetString(11),
                    wishedAmount: dataReader.GetString(12),
                    budget: dataReader.GetString(13),
                    dateFrom: dataReader.GetDateTime(14),
                    dateTo: dataReader.GetDateTime(15),
                    isSearchedAlready: dataReader.GetString(16),
                    knowledgeOfOtherSearch: dataReader.GetString(17),
                    additionalInfo: dataReader.GetString(18),
                    todaysDate: dataReader.GetDateTime(19)
                    );
            }
            return legat;
        }

        public async Task TruncateData()
        {
            string cmdText = @"TRUNCATE TABLE `wordpress_ansoeg`";

            await _database.ExecuteNonQueryAsync(cmdText);
        }

        public async Task UpdateAsync(Legat updateEntity)
        {
            string cmdText = @"UPDATE `nybyesfo_nybyesfond`.`wordpress_ansoeg` 
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
                                WHERE `ansoeg_id` = @id;";

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@id", updateEntity.Id },
                { "@first_name", updateEntity.Person.FirstName },
                { "@last_name", updateEntity.Person.LastName },
                { "@eMail", updateEntity.Person.EMail },
                { "@roadName", updateEntity.Person.Address.Roadname },
                { "@houseNumber", updateEntity.Person.Address.HouseNumber.ToString() },
                { "@zipNumber", updateEntity.Person.Address.ZipNumber },
                { "@city", updateEntity.Person.Address.City },
                { "@youthEducation", updateEntity.Person.Education.YouthEducation },
                { "@ongoingEducation", updateEntity.Person.Education.OngoingEducation },
                { "@placeOfEducation", updateEntity.Person.Education.PlaceOfEducation },
                { "@reasonForSearch", updateEntity.ReasonForSearch },
                { "@wishedAmount", updateEntity.WishedAmount.ToString() },
                { "@budget", updateEntity.Budget },
                { "@dateFrom", updateEntity.DateFrom },
                { "@dateTo", updateEntity.DateTo },
                { "@isSearchedAlready", updateEntity.IsSearchedAlready },
                { "@knowledgeOfOtherSearch", updateEntity.KnowledgeOfOtherSearch },
                { "@additionalInfo", updateEntity.AdditionalInfo },
                { "@todaysDate", updateEntity.TodaysDate }
            };

            await _database.ExecuteNonQueryAsync(cmdText, sqlParams);
        }

        private static bool ThereIsNewRecords(long existingDataCount, long rowCount)
        {
            return existingDataCount < rowCount || existingDataCount > rowCount;
        }
    }
}
