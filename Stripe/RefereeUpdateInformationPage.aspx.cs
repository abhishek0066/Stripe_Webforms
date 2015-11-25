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
    public partial class RefereeUpdateInformationPage : System.Web.UI.Page
    {
        int refereeUserProfileID;
        int refereeLoginID;

        protected void Page_Load(object sender, EventArgs e)
        {

            string cookieUsername = "";

            int refereeUsername;
            string refereeFirstName = "";
            string refereeLastName = "";
            string refereeEmail = "";
            string refereePhoneNumber = "";
            string refereeStreetAddress = "";
            string refereeCity = "";
            string refereeState = "";
            string refereeZip = "";
            string refereeProfilePicture = "";
            string refereeBackgroundDescription = "";
            string refereeLatitude = "";
            string refereeLongitude = "";
            string refereeSpecializationType = "";
            int refereeSpecializationTypeID = 0;
            int refereeTotalRatings = 0;
            int refereeTotalGamesOfficiated = 0;
            int refereeCurrentRatingFraction;
            int refereeCurrentRatingPercentage;
            string refereePassword = "";

            if (Request.Cookies["user"] != null)
            {
                // Get username from cookie
                HttpCookie getUserCookie = Request.Cookies["user"];
                cookieUsername = getUserCookie.Values["refereeUsername"];
                try
                {
                    refereeUserProfileID = Int32.Parse(cookieUsername);
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
                            + "U.userProfile_ID, "
                            + "U.userProfile_First_Name, "
                            + "U.userProfile_Last_Name, "
                            + "U.userProfile_Email, "
                            + "U.userProfile_Phone, "
                            + "U.userProfile_Street, "
                            + "U.userProfile_City, "
                            + "U.userProfile_State, "
                            + "U.userProfile_Zip, "
                            + "U.userProfile_Photo, "
                            + "U.userProfile_Background_Description, "
                            + "U.userProfile_Lat, "
                            + "U.userProfile_Long, "
                            + "R.Sport_Name_spt_Sport_Name_ID, "
                            + "R.User_Profile_Referee_Total_Ratings, "
                            + "R.User_Profile_Referee_Games_Officiated "
                            + "FROM "
                            + "USER_PROFILE U JOIN USER_PROFILE_REFEREE R "
                            + "ON U.userProfile_ID= R.User_Profile_userProfile_ID "
                            + "WHERE U.userProfile_ID=@usernameDB";

                        command.Parameters.AddWithValue("@usernameDB", refereeUserProfileID);
                        try
                        {

                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    refereeUsername = reader.GetInt32(0);
                                    refereeFirstName = reader.GetString(1);
                                    refereeLastName = reader.GetString(2);
                                    refereeEmail = reader.GetString(3);
                                    refereePhoneNumber = reader.GetString(4);
                                    refereeStreetAddress = reader.GetString(5);
                                    refereeCity = reader.GetString(6);
                                    refereeState = reader.GetString(7);
                                    refereeZip = reader.GetString(8);
                                    refereeProfilePicture = reader.GetString(9);
                                    refereeBackgroundDescription = reader.GetString(10);
                                    refereeLatitude = reader.GetDecimal(11).ToString();
                                    refereeLongitude = reader.GetDecimal(12).ToString();
                                    refereeSpecializationTypeID = reader.GetInt32(13);
                                    refereeTotalRatings = reader.GetInt32(14);
                                    refereeTotalGamesOfficiated = reader.GetInt32(15);

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
                            "SELECT " +
                            "S.spt_Name " +
                            "FROM Sport_Name S " +
                            "WHERE spt_Sport_Name_ID=@sportNameDB";
                        command.Parameters.AddWithValue("@sportNameDB", refereeSpecializationTypeID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    refereeSpecializationType = reader.GetString(0);
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
                            + "Login_login_ID "
                            + "FROM "
                            + "User_Profile "
                            + "WHERE userProfile_ID=@refereeUserProfileID";

                        command.Parameters.AddWithValue("@refereeUserProfileID", refereeUserProfileID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    refereeLoginID = reader.GetInt32(0);
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

                        command.Parameters.AddWithValue("@refereeLogin_ID", refereeLoginID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    refereePassword = reader.GetString(0);
                                }

                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                }



                if (refereeTotalGamesOfficiated != 0 && refereeTotalRatings > 0)
                { refereeCurrentRatingFraction = refereeTotalRatings / refereeTotalGamesOfficiated; }

                else
                {
                    refereeCurrentRatingFraction = 1;
                }

                refereeCurrentRatingPercentage = (refereeCurrentRatingFraction * 100) / 5;

                firstNameFieldID.Text = refereeFirstName;
                lastNameFieldID.Text = refereeLastName;
                emailFieldID.Text = refereeEmail;
                phoneNumberFieldID.Text = refereePhoneNumber;
                streetAddressFieldID.Text = refereeStreetAddress;
                cityFieldID.Text = refereeCity;
                stateFieldID.Text = refereeState;
                zipFieldID.Text = refereeZip;
                passwordFieldID.Attributes.Add("value", refereePassword);
                confirmPasswordFieldID.Attributes.Add("value", refereePassword);
                backgroundDescriptionID.Text = refereeBackgroundDescription;
                totalNumberOfRatingsID.Text = refereeTotalRatings.ToString();
                totalNumberOfGamesID.Text = refereeTotalGamesOfficiated.ToString();
                refereeRatingValueID.Text = refereeCurrentRatingFraction.ToString();
                refereeRatingStarsID.Style.Add("width", refereeCurrentRatingPercentage + "%");


            }

        }

        protected void updateInformationButton_Click(object sender, EventArgs e)
        {

            string refereeFirstName = firstNameFieldID.Text;
            string refereeLastName = lastNameFieldID.Text;
            string refereeEmail = emailFieldID.Text;
            string refereePassword = passwordFieldID.Text;
            string refereePhoneNumber = phoneNumberFieldID.Text;
            string refereeStreetAddress = streetAddressFieldID.Text;
            string refereeCity = cityFieldID.Text;
            string refereeState = stateFieldID.Text;
            string refereeZip = zipFieldID.Text;
            string refereeBackgroundInformation = backgroundDescriptionID.Text; 
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringLocalDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString)) { 
                using (SqlCommand command = connection.CreateCommand()){
                    command.CommandText = "UPDATE LOGIN "+
                        "SET login_password=@refereeLoginPassword WHERE login_ID = @login_ID";
                    command.Parameters.AddWithValue("@refereeLoginPassword", refereePassword);
                    command.Parameters.AddWithValue("@login_ID", refereeLoginID);
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


            using (SqlConnection connection = new SqlConnection(connectionString)) {
                using (SqlCommand command = connection.CreateCommand()) { 
                    command.CommandText = "UPDATE User_Profile "+
                        "SET userProfile_First_Name=@userProfile_First_Name, userProfile_Last_Name= @userProfile_Last_Name, userProfile_Email= @userProfile_Email, userProfile_Phone= @userProfile_Phone, userProfile_Street=@userProfile_Street, userProfile_City=@userProfile_City, userProfile_State=@userProfile_State, userProfile_Zip=@userProfile_Zip, "
                        + "userProfile_Background_Description=@userProfile_Background_Description WHERE userProfile_ID=@userProfile_ID";
                    command.Parameters.AddWithValue("@userProfile_First_Name", refereeFirstName);
                    command.Parameters.AddWithValue("@userProfile_Last_Name", refereeLastName);
                    command.Parameters.AddWithValue("@userProfile_Email", refereeEmail);
                    command.Parameters.AddWithValue("@userProfile_Phone", refereePhoneNumber);
                    command.Parameters.AddWithValue("@userProfile_Street", refereeStreetAddress);
                    command.Parameters.AddWithValue("@userProfile_City", refereeCity);
                    command.Parameters.AddWithValue("@userProfile_State", refereeState);
                    command.Parameters.AddWithValue("@userProfile_Zip", refereeZip);
                    command.Parameters.AddWithValue("@userProfile_Background_Description", refereeBackgroundInformation);
                    command.Parameters.AddWithValue("@userProfile_ID", refereeUserProfileID);
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
            }


        }




    }
