/*using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MusicApp.Models
{
    public class AudioHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string connectionString =
              ConfigurationManager.ConnectionStrings[
              "uploadConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Video, Video_Name" +
                             " FROM Videos WHERE ID = @id", connection);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value =
                               context.Request.QueryString["id"];
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        context.Response.ContentType = reader["Video_Name"].ToString();
                        context.Response.BinaryWrite((byte[])reader["Video"]);
                    }
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        private DataTable GetSpecificVideo(object i)
        //pass the id of the video
        {
            string connectionString =
              ConfigurationManager.ConnectionStrings[
              "uploadConnectionString"].ConnectionString;
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Video, ID " +
                                     "FROM Videos WHERE ID = @id", connectionString);
            adapter.SelectCommand.Parameters.Add("@id", SqlDbType.Int).Value = (int)i;
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        protected void ButtonShowVideo_Click(object sender, EventArgs e)
        {
            Repeater1.DataSource = GetSpecificVideo(2);
            //the video id (2 is example)

            Repeater1.DataBind();
        }
    }
   
}*/