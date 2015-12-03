using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace Stripe
{
    public partial class SchoolUpdateInformationPage : System.Web.UI.Page
    {
        string cookieUsername = "";
        string schoolName;
        string schoolStreetAddress;
        string schoolCityName;
        string schoolStateName;
        string schoolZipCode;
        string schoolLogo;
        string schoolDirectorFirstName;
        string schoolDirectorLastName;
        string schoolDirectorEmail;
        string schoolDirectorPhoneNumber;
        string schoolDirectorStreetAddress;
        string schoolDirectorCity;
        string schoolDirectorState;
        string schoolDirectorZipCode;
        string schoolDirectorBackgroundDescription;
        string schoolDirectorPersonalPicture;
        decimal schoolDirectorUserProfileLat;
        decimal schoolDirectorUserProfileLong;
        int schoolDirectorLoginID;
        int schoolId;
        //Change Here
        int schoolDirectorUserProfileId;
        int schoolDirectorUserProfileIdDummy;
        int schoolTableUserProfileDummy;
        string schoolDirectorProfilePassword;
        string schoolDirectorPassword;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["user"] != null)
            {

                HttpCookie getUserCookie = Request.Cookies["user"];
                cookieUsername = getUserCookie.Values["schoolDirectorUsername"];
                try
                {
                    schoolDirectorUserProfileId = Int32.Parse(cookieUsername);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            else
            {

                Response.Redirect("About.aspx");
            }

            if (!Page.IsPostBack)
            {

                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringLocalDB"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {

                        command.CommandText =
                            "SELECT "
                            + " * FROM "
                            + " User_Profile "
                            + " WHERE userProfile_ID=@userProfile_ID";
                        command.Parameters.AddWithValue("@userProfile_ID", schoolDirectorUserProfileId);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    schoolDirectorUserProfileIdDummy = reader.GetInt32(0);
                                    schoolDirectorFirstName = reader.GetString(1);
                                    schoolDirectorLastName = reader.GetString(2);
                                    schoolDirectorEmail = reader.GetString(3);
                                    schoolDirectorPhoneNumber = reader.GetString(4);
                                    schoolDirectorStreetAddress = reader.GetString(5);
                                    schoolDirectorCity = reader.GetString(6);
                                    schoolDirectorState = reader.GetString(7);
                                    schoolDirectorZipCode = reader.GetString(8);
                                    schoolDirectorPersonalPicture = reader.GetString(9);
                                    schoolDirectorBackgroundDescription = reader.GetString(10);
                                    schoolDirectorUserProfileLat = reader.GetDecimal(11);
                                    schoolDirectorUserProfileLong = reader.GetDecimal(12);
                                    schoolDirectorLoginID = reader.GetInt32(13);

                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                    }
                }


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText =
                            "SELECT "
                            + " * FROM "
                            + "School WHERE "
                            + "User_Profile_Director_Profile_ID=@User_Profile_Director_Profile_ID";
                        command.Parameters.AddWithValue("@User_Profile_Director_Profile_ID", schoolDirectorUserProfileId);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    schoolId = reader.GetInt32(0);
                                    schoolName = reader.GetString(1);
                                    schoolStreetAddress = reader.GetString(2);
                                    schoolCityName = reader.GetString(3);
                                    schoolStateName = reader.GetString(4);
                                    schoolZipCode = reader.GetString(5);
                                    schoolLogo = reader.GetString(6);
                                    schoolTableUserProfileDummy = reader.GetInt32(7);

                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText =
                            "SELECT "
                            + "login_password "
                            + "FROM "
                            + "LOGIN "
                            + "WHERE login_ID=@refereeLogin_ID";

                        command.Parameters.AddWithValue("@refereeLogin_ID", schoolDirectorLoginID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    schoolDirectorPassword = reader.GetString(0);
                                }

                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                }


                schoolNameLabelID.Text = schoolName;
                schoolNameId.Text = schoolName;
                schoolStreetAddressFieldID.Text = schoolStreetAddress;
                schoolCityFieldID.Text = schoolCityName;
                schoolStateFieldID.Text = schoolStateName;
                schoolZipFieldID.Text = schoolZipCode;
                directorFirstNameFieldID.Text = schoolDirectorFirstName;
                directorLastNameFieldID.Text = schoolDirectorLastName;
                schoolNameFieldID.Text = schoolName;
                directorPasswordFieldID.Attributes.Add("value", schoolDirectorPassword);
                directorConfirmPasswordFieldID.Attributes.Add("value", schoolDirectorPassword);
                directorEmailFieldID.Text = schoolDirectorEmail;
                directorPhoneNumberFieldID.Text = schoolDirectorPhoneNumber;
                directorStreetAddressFieldID.Text = schoolDirectorStreetAddress;
                directorCityFieldID.Text = schoolDirectorCity;
                directorStateFieldID.Text = schoolDirectorState;
                directorZipFieldID.Text = schoolDirectorZipCode;
                directorBackgroundDescriptionFieldID.Text = schoolDirectorBackgroundDescription;
                directorFullNameLabelID.Text = schoolDirectorFirstName + " " + schoolDirectorLastName;
            }

        }


        protected void updateInformationButton_Click(object sender, EventArgs e)
        {

            schoolStreetAddress = schoolStreetAddressFieldID.Text;
            schoolCityName = schoolCityFieldID.Text;
            schoolStateName = schoolStateFieldID.Text;
            schoolZipCode = schoolZipFieldID.Text;
            schoolDirectorFirstName = directorFirstNameFieldID.Text;
            schoolDirectorLastName = directorLastNameFieldID.Text;
            schoolName = schoolNameFieldID.Text;
            schoolDirectorPassword = directorPasswordFieldID.Text;
            schoolDirectorEmail = directorEmailFieldID.Text;
            schoolDirectorPhoneNumber = directorPhoneNumberFieldID.Text;
            schoolDirectorStreetAddress = directorStreetAddressFieldID.Text;
            schoolDirectorCity = directorCityFieldID.Text;
            schoolDirectorState = directorStateFieldID.Text;
            schoolDirectorZipCode = directorZipFieldID.Text;
            schoolDirectorBackgroundDescription = directorBackgroundDescriptionFieldID.Text;
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringLocalDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE LOGIN " +
                        "SET login_password=@loginPassword WHERE login_ID = @login_ID";
                    command.Parameters.AddWithValue("@loginPassword", schoolDirectorPassword);
                    command.Parameters.AddWithValue("@login_ID", schoolDirectorLoginID);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException exception)
                    {
                        Response.Write("<p>Error code " + exception.Number
                                       + ": " + exception.Message + "</p>");
                    }

                }
            }


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE User_Profile " +
                        "SET userProfile_First_Name=@userProfile_First_Name, userProfile_Last_Name= @userProfile_Last_Name, userProfile_Email= @userProfile_Email, userProfile_Phone= @userProfile_Phone, userProfile_Street=@userProfile_Street, userProfile_City=@userProfile_City, userProfile_State=@userProfile_State, userProfile_Zip=@userProfile_Zip, "
                        + "userProfile_Background_Description=@userProfile_Background_Description WHERE userProfile_ID=@userProfile_ID";
                    command.Parameters.AddWithValue("@userProfile_First_Name", schoolDirectorFirstName);
                    command.Parameters.AddWithValue("@userProfile_Last_Name", schoolDirectorLastName);
                    command.Parameters.AddWithValue("@userProfile_Email", schoolDirectorEmail);
                    command.Parameters.AddWithValue("@userProfile_Phone", schoolDirectorPhoneNumber);
                    command.Parameters.AddWithValue("@userProfile_Street", schoolDirectorStreetAddress);
                    command.Parameters.AddWithValue("@userProfile_City", schoolDirectorCity);
                    command.Parameters.AddWithValue("@userProfile_State", schoolDirectorState);
                    command.Parameters.AddWithValue("@userProfile_Zip", schoolDirectorZipCode);
                    command.Parameters.AddWithValue("@userProfile_Background_Description", schoolDirectorBackgroundDescription);
                    command.Parameters.AddWithValue("@userProfile_ID", schoolDirectorUserProfileId);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException exception)
                    {
                        Response.Write("<p>Error code " + exception.Number
                                       + ": " + exception.Message + "</p>");
                    }
                }
            }


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE SCHOOL " +
                         "SET sch_Name=@sch_Name, sch_Street= @sch_Street, sch_City= @sch_City, sch_State= @sch_State, sch_Zip=@sch_Zip "
                         + "WHERE sch_ID=@sch_ID";
                    command.Parameters.AddWithValue("@sch_Name", schoolName);
                    command.Parameters.AddWithValue("@sch_Street", schoolStreetAddress);
                    command.Parameters.AddWithValue("@sch_City", schoolCityName);
                    command.Parameters.AddWithValue("@sch_State", schoolStateName);
                    command.Parameters.AddWithValue("@sch_Zip", schoolZipCode);
                    command.Parameters.AddWithValue("@sch_ID", schoolId);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException exception)
                    {
                        Response.Write("<p>Error code " + exception.Number
                                       + ": " + exception.Message + "</p>");
                    }
                }
            }


            Page_Load(null, EventArgs.Empty);
            directorFullNameLabelID.Text = schoolDirectorFirstName + " " + schoolDirectorLastName;
            schoolNameLabelID.Text = schoolName;
            schoolNameId.Text = schoolName;

        }


    }
}