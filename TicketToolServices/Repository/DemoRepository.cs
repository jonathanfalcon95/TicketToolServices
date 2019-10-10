using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using TicketToolServices.Models;

namespace TicketToolServices.Repository
{
    class DemoRepository
    {
        //static SFAgentGroupDetail SFA = new SFAgentGroupDetail();
       // public static readonly List<SFAgentGroupDetail> Items = new List<SFAgentGroupDetail>();
        private static object MapToAgent(SqlDataReader reader)
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
        }

        // guia de github de tickets
        /*
        public static async Task<List<object>> SelectAsync(ExecutionContext context)
        {
            // log.LogInformation(str.ToString());
            var response = new List<object>();
            var str = Conexion.GetConnectionString(context);
            //var str2 = Conexion.GetDataApi("/agents");
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
                            response.Add(MapToAgent(reader));
                        }
                    }
                    //log.LogInformation();

                }
                return response;
            }

        }
        */

        

        // obtener los datos de los Grupos

            /*
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
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToGroups(reader));
                        }
                    }
                   
                }
                return response;
            }

        }

        */
      


        // obtener todos los datos de SFAgentGroupDetail por ID
        /*
        public static async Task GetGroupDetailId(SFAgentGroupDetail groupdetail, ExecutionContext context)
        {

            var str = Conexion.GetConnectionString(context);

            using (SqlConnection sql = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetGroupDetailById", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@groupID", groupdetail.groupID));
                    cmd.Parameters.Add(new SqlParameter("@groupName", groupdetail.groupName));
                    cmd.Parameters.Add(new SqlParameter("@agentID", groupdetail.agentID));
                    cmd.Parameters.Add(new SqlParameter("@agentName", groupdetail.agentName));
                    cmd.Parameters.Add(new SqlParameter("@date", groupdetail.date));
                    cmd.Parameters.Add(new SqlParameter("@hours", groupdetail.hours));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
        */


        // otro intento grupo por id

            /*
        public static async Task<List<object>> GetAllGroups2intento(ExecutionContext context)
        {

            // log.LogInformation(str.ToString());
            var response = new List<object>();
            var str = Conexion.GetConnectionString(context);

            using (SqlConnection conn = new SqlConnection(str))
            {
                await conn.OpenAsync();
                var text = "Select * from SFAgentGroupDetail";
                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToGroupsDetail(reader));
                        }
                    }

                }
                return response;
            }

        }*/


    }
}
