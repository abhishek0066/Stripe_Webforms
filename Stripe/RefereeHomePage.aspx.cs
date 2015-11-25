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
    public partial class RefereeHomePage : System.Web.UI.Page
    {
        int mainRefereeUsername;
        string refereeFirstName = "";
        string refereeLastName = "";
        string refereeEmail = "";
        string refereePhoneNumber = "";
        string refereeStreetAddress = "";
        string refereeCity = "";
        string refereeState = "";
        string refereeZip = "";
        int refereeTotalRatings;
        int refereeTotalGamesOfficiated;
        string refereeSpecializationType = "";
        string refereeBackgroundDescription = "";


        protected void Page_Load(object sender, EventArgs e)
        {

            string connectionString =
            ConfigurationManager.ConnectionStrings["ConnectionStringLocalDB"].ConnectionString;

            int refereeUsername=11;
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
            int refereeSpecializationTypeID=0;
            int refereeTotalRatings=0;
            int refereeTotalGamesOfficiated=0;
            int refereeCurrentRatingFraction;
            int refereeCurrentRatingPercentage;
            


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand()) {
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

                    command.Parameters.AddWithValue("@usernameDB", refereeUsername);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows) {
                            while (reader.Read()) {
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
                                refereeLatitude= reader.GetDecimal(11).ToString();
                                refereeLongitude= reader.GetDecimal(12).ToString();
                                refereeSpecializationTypeID = reader.GetInt32(13);
                                refereeTotalRatings= reader.GetInt32(14);
                                refereeTotalGamesOfficiated= reader.GetInt32(15);
                                
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                   
                }
            
            }

            using (SqlConnection connection = new SqlConnection(connectionString)) {

                using (SqlCommand command = connection.CreateCommand()) { 
                    command.CommandText =
                        "SELECT "+
                        "S.spt_Name "+
                        "FROM Sport_Name S "+
                        "WHERE spt_Sport_Name_ID=@sportNameDB";
                    command.Parameters.AddWithValue("@sportNameDB", refereeSpecializationTypeID);
                
                 try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows) {
                        while (reader.Read()) {
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

            if (refereeTotalGamesOfficiated != 0 && refereeTotalRatings > 0)
            { refereeCurrentRatingFraction = refereeTotalRatings / refereeTotalGamesOfficiated; }

            else
            {
                refereeCurrentRatingFraction = 1;
            }
            
            refereeCurrentRatingPercentage = (refereeCurrentRatingFraction * 100) / 5;

            mainRefereeUsername = refereeUsername;
            firstNameLabel.Text = refereeFirstName;
            lastNameLabel.Text = refereeLastName;
            emailLabel.Text = refereeEmail;
            phoneNumberLabel.Text = refereePhoneNumber;
            streetAddressLabel.Text = refereeStreetAddress;
            cityLabel.Text = refereeCity;
            stateLabel.Text = refereeState;
            zipLabel.Text = refereeZip;
            gameSpecializationTypeID.Text = refereeSpecializationType;
            backgroundDescriptionID.Text = refereeBackgroundDescription;
            totalNumberOfRatingsID.Text = refereeTotalRatings.ToString();
            totalNumberOfGamesID.Text = refereeTotalGamesOfficiated.ToString();
            refereeRatingValueID.Text = refereeCurrentRatingFraction.ToString();
            refereeRatingStarsID.Style.Add("width", refereeCurrentRatingPercentage+"%");

            //Save user information to cookie
            HttpCookie createUserCookie = new HttpCookie("user");
            createUserCookie.Values["refereeUsername"] = mainRefereeUsername.ToString();
            createUserCookie.Expires = DateTime.Now.AddDays(14);
            Response.Cookies.Add(createUserCookie);

           }

        protected void updateInformationButton_Click(object sender, EventArgs e) {

           // Response.Redirect("RefereeUpdateInformationPage.aspx?userProfile_ID= " + mainRefereeUsername);
           Response.Redirect("RefereeUpdateInformationPage.aspx");
        }

        
    }
}