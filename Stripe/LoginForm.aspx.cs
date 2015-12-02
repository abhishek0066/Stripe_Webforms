using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Stripe
{
    public partial class LoginForm : System.Web.UI.Page
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringLocalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            var username = Username.Text;
            var password = Password.Text;
            bool loginSuccessful = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    //fetching sport Id of the referee
                    command.CommandText = "SELECT * FROM [dbo].[Login] " +
                                             "WHERE login_username = @username AND login_password = @password";

                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                loginSuccessful = true;
                                Session["loginid"] = reader.GetInt32(0);

                                string usertype = reader.GetString(5);
                                switch (usertype)
                                {
                                    case "A":
                                        Response.Redirect("AdminHome.aspx", false);
                                        break;
                                    case "R":
                                        Response.Redirect("RefereeHomePage.aspx", false);
                                        break;
                                    case "S":
                                        Response.Redirect("SchooHomePage.aspx", false);
                                        break;
                                    default:
                                        Response.Redirect("Default.aspx", false);
                                        break;
                                }
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}