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
    public partial class schooHomePage : System.Web.UI.Page
    {   //UserProfileID required as a cookie
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
        int schoolDirectorUserProfileIdDummy;
        int schoolTableUserProfileDummy;


        readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringLocalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int schoolDirectorUserProfileId = Convert.ToInt32(Session["loginid"].ToString());
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

                schoolNameId.Text = schoolName;
                schoolNameLabelID.Text = schoolName;
                streetAddressLabelID.Text = schoolStreetAddress;
                cityLabelID.Text = schoolCityName;
                stateLabelID.Text = schoolStateName;
                zipLabelID.Text = schoolZipCode;
                directorFullNamePicturePanelID.Text = schoolDirectorFirstName + " " + schoolDirectorLastName;
                emailLabelID.Text = schoolDirectorEmail;
                phoneNumberLabelID.Text = schoolDirectorPhoneNumber;
                directorStreetAddressLabelID.Text = schoolDirectorStreetAddress;
                directorCityLabel.Text = schoolDirectorCity;
                directoryStateLabel.Text = schoolDirectorState;
                directorZipLabel.Text = schoolDirectorZipCode;
                directorBackgroundDescriptionID.Text = schoolDirectorBackgroundDescription;

                //Save user information to cookie
                Session["user"] = schoolDirectorUserProfileId.ToString();

                //Session["loginid"] = null;

            }
            catch (Exception ex)
            {
                Response.Redirect("LoginForm.aspx", false);
            }
        }

        protected void updateInformationButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("SchoolUpdateInformationPage.aspx", false);
        }

        protected void logoutout_Click(object sender, EventArgs e)
        {
            Session["loginid"] = null;
            Session["user"] = null;
            Response.Redirect("LoginForm.aspx", false);
        }
    }
}