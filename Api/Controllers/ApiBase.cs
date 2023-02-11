using System.Collections.Generic;
using System.Data;
using System.Web.Http;
//using System.Web.Http;
using Api.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
namespace Api.Controllers //ask about namespace specifics vs using
{
    //    [EnableCors(origins: "http://localhost:5062", headers: "*", methods: "*")]
    public class ApiBase : ApiController //ask about inheritance
    {
        // public SiteUser GetSiteUser { get; set; }
        //const string ConnectionString = @"Server=.\SqlExpress;Database=LakeDemo;User Id=LakeDemo;Password=test;";
        //const string ConnectionString = @"data source=DESKTOP-B0VID88\SQLEXPRESS;Initial Catalog=LakeDemo;Integrated security = true; TrustServerCertificate=Yes;";
        const string ConnectionString = @"data source=.\sql2017;Initial Catalog=LakeDemo;Integrated security = true; TrustServerCertificate=Yes;";

        public SqlCommand GetCommand()
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            return sqlConnection.CreateCommand();
        }

    }
}
