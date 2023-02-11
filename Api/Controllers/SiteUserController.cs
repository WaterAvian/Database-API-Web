using Api.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteUserController : ApiBase
    {
        [HttpGet, Route("{id}")] //ask about attributes [] //ask about params
        public SiteUser GetSiteUser(int id)
        {
            using (var sqlCommand = GetCommand()) //ask about using
            {
                sqlCommand.CommandText = "GetSiteUser";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@Id", id));
                var FirstnameParam = new SqlParameter("@Firstname", System.Data.SqlDbType.VarChar, 50);
                FirstnameParam.Direction = ParameterDirection.Output;
                var LastnameParam = new SqlParameter("@Lastname", System.Data.SqlDbType.VarChar, 50);
                LastnameParam.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(FirstnameParam);
                sqlCommand.Parameters.Add(LastnameParam);
                sqlCommand.ExecuteNonQuery();
                return new SiteUser { Id = id, Firstname = (string)FirstnameParam.Value, Lastname = (string)LastnameParam.Value };
            }
        }

        [HttpGet, Route("all")]

        //        [EnableCors(origins: "http://www.example.com", headers: "*", methods: "get,post")]
        public SiteUser[] SiteUsers()
        {
            using (var sqlCommand = GetCommand())
            {
                sqlCommand.CommandText = "GetAllSiteUsers";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                dataTable.Load(sqlCommand.ExecuteReader());
                List<SiteUser> siteUsers = new List<SiteUser>();

                foreach (DataRow row in dataTable.Rows)
                {
                    siteUsers.Add(new SiteUser
                    {
                        Id = (int)row["Id"],
                        Firstname = (string)row["Firstname"],
                        Lastname = (string)row["Lastname"]
                    });
                }
                return siteUsers.ToArray();
            }
        }

        [HttpPost, Route("ups")]
        public SiteUser[] AddSiteUser(SiteUser user)
        {
            using (var sqlCommand = GetCommand())
            {
                try
                {
                    sqlCommand.CommandText = "dbo.UpsertSiteUser";
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    var paramId = new SqlParameter("@Id", SqlDbType.Int);
                    paramId.Value = 0;
                    sqlCommand.Parameters.Add(paramId);
                    sqlCommand.Parameters.Add(new SqlParameter("@Firstname", user.Firstname));
                    sqlCommand.Parameters.Add(new SqlParameter("@Lastname", user.Lastname));
                    sqlCommand.Parameters.Add(new SqlParameter("@Password", user.Password));
                    sqlCommand.ExecuteNonQuery();
                    return SiteUsers();
                }
                catch (Exception ex)
                {
                    Debugger.Break();
                    // ignore it
                    return new SiteUser[] { };
                }
            }
        }

        [HttpPut, Route("update")]
        public SiteUser[] UpdateSiteUser(SiteUser user)
        {
            using (var sqlCommand = GetCommand())
            {
                sqlCommand.CommandText = "UpsertSiteUser";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@Id", user.Id));
                sqlCommand.Parameters.Add(new SqlParameter("@Firstname", user.Firstname));
                sqlCommand.Parameters.Add(new SqlParameter("@Lastname", user.Lastname));
                sqlCommand.ExecuteNonQuery();
            }
            return SiteUsers();
        }

    }
}