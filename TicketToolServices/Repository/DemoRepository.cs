using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using System.Threading.Tasks;

namespace TicketToolServices.Repository
{
    class DemoRepository
    {

        private static object MapToActivities(SqlDataReader reader)
        {
            var Model = new
            {
                subjet = reader["agentID"].ToString(),
                description = reader["agentDescription"].ToString(),
            };
            return Model;
        }

        // probando con el grupo
        private static object MapToGroups(SqlDataReader reader2)
        {
            var Model = new
            {
                id = reader2["groupID"].ToString(),
                descriptiongro = reader2["groupDescription"].ToString(),

            };
            return Model;
        }

        // mapeo del grupo detalle
        /*
        private static object MapToGroupsDetail(SqlDataReader reader3)
        {
            var Model = new
            {
                id = reader3["groupID"].ToString(),
                groname = reader3["groupName"].ToString(),
                aid = reader3["agentID"].ToString(),
                aname = reader3["agentName"].ToString(),
                da = reader3["date"],
                hou = reader3["hours"],

            };
            return Model;
        }*/


        public static async Task<List<object>> SelectAsync(ExecutionContext context)
        {

            // log.LogInformation(str.ToString());
            var response = new List<object>();
            var str = Conexion.GetConnectionString(context);

            using (SqlConnection conn = new SqlConnection(str))
            {
                await conn.OpenAsync();
                var text = "Select * From Agent";


                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    // Execute the command and log the # rows affected.
                    //  var rows = await cmd.ExecuteNonQueryAsync();
                    //   log.LogInformation("rows were updated");

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToActivities(reader));
                        }
                    }
                    //log.LogInformation();

                }
                return response;
            }


         
        }

        // public 
        public static async Task<List<object>> GetAllGroups(ExecutionContext context)
        {

            // log.LogInformation(str.ToString());
            var response = new List<object>();
            var str = Conexion.GetConnectionString(context);

            using (SqlConnection conn = new SqlConnection(str))
            {
                await conn.OpenAsync();
                var text = "Select * From Groups";


                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    // Execute the command and log the # rows affected.
                    //  var rows = await cmd.ExecuteNonQueryAsync();
                    //   log.LogInformation("rows were updated");

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToGroups(reader));
                        }
                    }
                    //log.LogInformation();

                }
                return response;
            }



        }

        /*
        public static async Task<List<object>> GetGroupId(ExecutionContext context)
        {
            
            var response = new List<object>();
            var str = Conexion.GetConnectionString(context);

            using (SqlConnection conn = new SqlConnection(str))
            {
                await conn.OpenAsync();
                var text = string.Format("Select * from SFAgentGroupDetail where agentID = id");

                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    // Execute the command and log the # rows affected.
                    //  var rows = await cmd.ExecuteNonQueryAsync();
                    //   log.LogInformation("rows were updated");

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToGroups(reader));
                        }
                    }
                    //log.LogInformation();

                }
                return response;
            }

        }
        */
    }
}
