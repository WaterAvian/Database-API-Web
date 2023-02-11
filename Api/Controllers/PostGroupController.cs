using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostGroupController : ApiBase
    {
        [HttpGet, Route("{id}")] //PostGroup about attributes [] //PostGroup about params
        public PostGroup PostGroup(int id)
        {
            using (var sqlCommand = GetCommand()) //PostGroup about using
            {
                sqlCommand.CommandText = "GetPostGroup";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@Id", id));
                var SiteUserIdParam = new SqlParameter("@SiteUserId", System.Data.SqlDbType.Int);
                SiteUserIdParam.Direction = ParameterDirection.Output;
                var GroupNameParam = new SqlParameter("@GroupName", System.Data.SqlDbType.VarChar, 50);
                GroupNameParam.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(SiteUserIdParam);
                sqlCommand.Parameters.Add(GroupNameParam);
                sqlCommand.ExecuteNonQuery();
                return new PostGroup { Id = id, SiteUserId = (int)SiteUserIdParam.Value, GroupName = (string)GroupNameParam.Value };
            }
        }
      
        [HttpGet, Route("all")]
        public PostGroup[] PostGroups()
        {
            using (var sqlCommand = GetCommand())
            {
                sqlCommand.CommandText = "GetAllPostGroups";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                dataTable.Load(sqlCommand.ExecuteReader());
                List<PostGroup> PostGroups = new List<PostGroup>();

                foreach (DataRow row in dataTable.Rows)
                {
                    PostGroups.Add(new PostGroup
                    {
                        Id = (int)row["Id"],
                        SiteUserId = (int)row["SiteUserId"],
                        GroupName = (string)row["GroupName"]
                    });
                }
                return PostGroups.ToArray();
            }
        }
    }
}
