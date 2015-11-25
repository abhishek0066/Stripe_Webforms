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
    public partial class SchoolApprovalAndRatingPage : System.Web.UI.Page
    {
        int schoolDirectorUserProfileId;
        string cookieUsername = "";
        int sportEventID;
        int refereeProfileID;
        int refereeSportType_TypeID;
        string sportEventDate;
        string sportEventTime;
        string sportEventFieldName;
        int sportEventHomeSchoolID;
        int sportEventAwaySchoolID;
        int eventSportNameID;
        string refereeFirstName;
        string refereeLastName;
        string refereeEmail;
        string refereePhone;
        string refereeStreet;
        string refereeState;
        string refereeCity;
        string refereeZip;
        string refereePhoto;
        int refereeSportNameID;
        int refereeTotalRatings;
        int refereeTotalGamesOfficiated;
        string sportEventHomeTeamName;
        string sportEventAwayTeamName;
        string sportEventHomeTeamLogo;
        string sportEventAwayTeamLogo;
        string refereeSportType_TypeName;
        string refereeSportName;
        int pendingApprovalCount;
        int refereeCurrentRatingPercentage;
        int refereeCurrentRatingFraction;

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

            if (!Page.IsPostBack) {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringLocalDB"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    using (SqlCommand command = connection.CreateCommand()) { 
                        command.CommandText= 
                            "SELECT TOP 1 "
                            +"Sport_Event_event_ID, "
                            +"User_Profile_Referee_Profile_ID, "
                            +"Sport_Type_Referees_sptTypeRef_ID "
                            +"FROM "
                            +"Referee_Event_History "
                            +"WHERE "
                            +"User_Profile_School_Director_Profile_ID =@User_Profile_School_Director_Profile_ID "
                            +"AND Referee_Status_refStatus_ID='P'";

                        command.Parameters.AddWithValue("@User_Profile_School_Director_Profile_ID", schoolDirectorUserProfileId);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows) {
                                while (reader.Read())
                                {
                                    sportEventID = reader.GetInt32(0);
                                    refereeProfileID = reader.GetInt32(1);
                                    refereeSportType_TypeID = reader.GetInt32(2);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                            
                    }
                }

                if (sportEventID>0)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText= 
                                "SELECT "
                                +" event_Date, "
                                +" event_Time, "
                                +" event_School_Field_Name, "
                                +" School_Home_sch_ID, "
                                +" School_Away_sch_ID, "
                                +" Sport_Name_spt_Sport_Name_ID "
                                +" FROM Sport_Event WHERE event_ID=@event_ID";
                            command.Parameters.AddWithValue("@event_ID", sportEventID);

                            try
                            {
                                connection.Open();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows) {

                                    while (reader.Read())
                                    {
                                        sportEventDate = (reader.GetDateTime(0)).ToString();
                                        sportEventTime = (reader.GetTimeSpan(1)).ToString();
                                        sportEventFieldName = reader.GetString(2);
                                        sportEventHomeSchoolID = reader.GetInt32(3);
                                        sportEventAwaySchoolID = reader.GetInt32(4);
                                        eventSportNameID = reader.GetInt32(5);
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
                                 + "U.userProfile_First_Name, "
                                 + "U.userProfile_Last_Name, "
                                 + "U.userProfile_Email, "
                                 + "U.userProfile_Phone, "
                                 + "U.userProfile_Street, "
                                 + "U.userProfile_City, "
                                 + "U.userProfile_State, "
                                 + "U.userProfile_Zip, "
                                 + "U.userProfile_Photo, "
                                 + "R.Sport_Name_spt_Sport_Name_ID, "
                                 + "R.User_Profile_Referee_Total_Ratings, "
                                 + "R.User_Profile_Referee_Games_Officiated  "
                                 + "FROM USER_PROFILE U JOIN USER_PROFILE_REFEREE R "
                                 + "ON U.userProfile_ID= R.User_Profile_userProfile_ID "
                                 + "WHERE U.userProfile_ID=@userProfile_ID";

                             command.Parameters.AddWithValue("@userProfile_ID", refereeProfileID);

                             try
                             {
                                 connection.Open();
                                 SqlDataReader reader = command.ExecuteReader();
                                 if (reader.HasRows) {

                                     while (reader.Read()) {
                                         refereeFirstName = reader.GetString(0);
                                         refereeLastName = reader.GetString(1);
                                         refereeEmail = reader.GetString(2);
                                         refereePhone = reader.GetString(3);
                                         refereeStreet = reader.GetString(4);
                                         refereeCity = reader.GetString(5);
                                         refereeState= reader.GetString(6);
                                         refereeZip = reader.GetString(7);
                                         refereePhoto = reader.GetString(8);
                                         refereeSportNameID = reader.GetInt32(9);
                                         refereeTotalRatings = reader.GetInt32(10);
                                         refereeTotalGamesOfficiated = reader.GetInt32(11);
                                        
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
                                " sch_Name, sch_Logo FROM School WHERE sch_ID=@sch_ID";
                            command.Parameters.AddWithValue("@sch_ID", sportEventAwaySchoolID);
                            try
                            { 
                                connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows) {
                                while (reader.Read())
                                {
                                    sportEventAwayTeamName = reader.GetString(0);
                                    sportEventAwayTeamLogo = reader.GetString(1);
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
                                " sch_Name, sch_Logo FROM School WHERE sch_ID=@sch_ID";
                            command.Parameters.AddWithValue("@sch_ID", sportEventHomeSchoolID);
                            try
                            {
                                connection.Open();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        sportEventHomeTeamName = reader.GetString(0);
                                        sportEventHomeTeamLogo = reader.GetString(1);
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
                                " spt_Name from Sport_Name where spt_Sport_Name_ID=@spt_Sport_Name_ID";


                            command.Parameters.AddWithValue("@spt_Sport_Name_ID", refereeSportNameID);
                            try
                            {
                                connection.Open();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                         refereeSportName = reader.GetString(0);
                                        
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
                                " sptTypeRef_Referee_Title FROM SPORT_TYPE_REFEREES where sptTypeRef_ID=@sptTypeRef_ID";


                            command.Parameters.AddWithValue("@sptTypeRef_ID", refereeSportType_TypeID);
                            try
                            {
                                connection.Open();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        refereeSportType_TypeName = reader.GetString(0);
                                       
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
                                "SELECT TOP 1 count(Sport_Event_event_ID) " +
                                "FROM Referee_Event_History "
                                +"WHERE User_Profile_School_Director_Profile_ID=@User_Profile_School_Director_Profile_ID "
                                +"AND Referee_Status_refStatus_ID='P'; ";


                            command.Parameters.AddWithValue("@User_Profile_School_Director_Profile_ID", schoolDirectorUserProfileId);
                            try
                            {
                                connection.Open();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        pendingApprovalCount = reader.GetInt32(0);

                                    }
                                }
                            }

                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }



                    pendingApprovalRefereeCountID.Text = pendingApprovalCount.ToString();
                    homeSchoolNameID.Text = sportEventHomeTeamName;
                    awaySchoolNameID.Text = sportEventAwayTeamName;
                    eventDateLabelID.Text = sportEventDate;
                    eventTimeLabelID.Text = sportEventTime;
                    sportEventTypeID.Text = refereeSportName;
                    gameLocationFieldNameID.Text = sportEventFieldName;
                    refereeType2ID.Text = refereeSportType_TypeName;
                    referee2NameID.Text = refereeFirstName + " " + refereeLastName;
                    referee1gameSpecializationID.Text = refereeSportName;
                    referee1TotalGamesOfficiatedID.Text = refereeTotalGamesOfficiated.ToString();
                    referee1ContactEmailID.Text = refereeEmail;
                    referee1PhoneNumberID.Text = refereePhone;
                    refereeStreetAddressLabelID.Text = refereeStreet;
                    refereeCityLabelID.Text = refereeCity;
                    refereeStateLabelID.Text = refereeZip;
                    referee1GameTypeID.Text = "Sport Name "+ refereeSportName;
                    referee1FullNameID.Text = refereeFirstName + " " + refereeLastName;
                    if (refereeTotalGamesOfficiated != 0 && refereeTotalRatings > 0)
                    { refereeCurrentRatingFraction = refereeTotalRatings / refereeTotalGamesOfficiated; }

                    else
                    {
                        refereeCurrentRatingFraction = 1;
                    }

                    refereeCurrentRatingPercentage = (refereeCurrentRatingFraction * 100) / 5;
                   
                    referee1StarRatingID.Style.Add("width", refereeCurrentRatingPercentage + "%");
                   
                }
                else
                {
                    //code to right if no values are present
                }



            }
        }

        protected void approveReferee_Click(object sender, EventArgs e) {
            char refereeApprovalStatus = 'A';
            
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringLocalDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        "SELECT TOP 1 "
                        + "Sport_Event_event_ID, "
                        + "User_Profile_Referee_Profile_ID, "
                        + "Sport_Type_Referees_sptTypeRef_ID "
                        + "FROM "
                        + "Referee_Event_History "
                        + "WHERE "
                        + "User_Profile_School_Director_Profile_ID =@User_Profile_School_Director_Profile_ID "
                        + "AND Referee_Status_refStatus_ID='P'";

                    command.Parameters.AddWithValue("@User_Profile_School_Director_Profile_ID", schoolDirectorUserProfileId);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                sportEventID = reader.GetInt32(0);
                                refereeProfileID = reader.GetInt32(1);
                                refereeSportType_TypeID = reader.GetInt32(2);
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
                        + "U.userProfile_First_Name, "
                        + "U.userProfile_Last_Name, "
                        + "U.userProfile_Email, "
                        + "U.userProfile_Phone, "
                        + "U.userProfile_Street, "
                        + "U.userProfile_City, "
                        + "U.userProfile_State, "
                        + "U.userProfile_Zip, "
                        + "U.userProfile_Photo, "
                        + "R.Sport_Name_spt_Sport_Name_ID, "
                        + "R.User_Profile_Referee_Total_Ratings, "
                        + "R.User_Profile_Referee_Games_Officiated  "
                        + "FROM USER_PROFILE U JOIN USER_PROFILE_REFEREE R "
                        + "ON U.userProfile_ID= R.User_Profile_userProfile_ID "
                        + "WHERE U.userProfile_ID=@userProfile_ID";

                    command.Parameters.AddWithValue("@userProfile_ID", refereeProfileID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                refereeFirstName = reader.GetString(0);
                                refereeLastName = reader.GetString(1);
                                refereeEmail = reader.GetString(2);
                                refereePhone = reader.GetString(3);
                                refereeStreet = reader.GetString(4);
                                refereeCity = reader.GetString(5);
                                refereeState = reader.GetString(6);
                                refereeZip = reader.GetString(7);
                                refereePhoto = reader.GetString(8);
                                refereeSportNameID = reader.GetInt32(9);
                                refereeTotalRatings = reader.GetInt32(10);
                                refereeTotalGamesOfficiated = reader.GetInt32(11);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            int newTotalNumberOfGamesOfficiated = refereeTotalGamesOfficiated + 1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE USER_PROFILE_REFEREE " +
                        " SET User_Profile_Referee_Games_Officiated= @User_Profile_Referee_Games_Officiated " +
                        " WHERE User_Profile_userProfile_ID=@User_Profile_userProfile_ID";
                    command.Parameters.AddWithValue("@User_Profile_Referee_Games_Officiated", newTotalNumberOfGamesOfficiated);
                    command.Parameters.AddWithValue("@User_Profile_userProfile_ID", refereeProfileID);
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
                    command.CommandText ="UPDATE Referee_Event_History " +
                        " SET Referee_Status_refStatus_ID= @Referee_Status_refStatus_ID " +
                        " WHERE Sport_Event_event_ID=@Sport_Event_event_ID AND User_Profile_Referee_Profile_ID=@User_Profile_Referee_Profile_ID";
                    command.Parameters.AddWithValue("@Referee_Status_refStatus_ID", refereeApprovalStatus);
                    command.Parameters.AddWithValue("@User_Profile_Referee_Profile_ID", refereeProfileID);
                    command.Parameters.AddWithValue("@Sport_Event_event_ID", sportEventID);
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
                    command.CommandText =
                        "SELECT TOP 1 "
                        + "Sport_Event_event_ID, "
                        + "User_Profile_Referee_Profile_ID, "
                        + "Sport_Type_Referees_sptTypeRef_ID "
                        + "FROM "
                        + "Referee_Event_History "
                        + "WHERE "
                        + "User_Profile_School_Director_Profile_ID =@User_Profile_School_Director_Profile_ID "
                        + "AND Referee_Status_refStatus_ID='P'";

                    command.Parameters.AddWithValue("@User_Profile_School_Director_Profile_ID", schoolDirectorUserProfileId);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                sportEventID = reader.GetInt32(0);
                                refereeProfileID = reader.GetInt32(1);
                                refereeSportType_TypeID = reader.GetInt32(2);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }

            if (sportEventID > 0)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText =
                            "SELECT "
                            + " event_Date, "
                            + " event_Time, "
                            + " event_School_Field_Name, "
                            + " School_Home_sch_ID, "
                            + " School_Away_sch_ID, "
                            + " Sport_Name_spt_Sport_Name_ID "
                            + " FROM Sport_Event WHERE event_ID=@event_ID";
                        command.Parameters.AddWithValue("@event_ID", sportEventID);

                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    sportEventDate = (reader.GetDateTime(0)).ToString();
                                    sportEventTime = (reader.GetTimeSpan(1)).ToString();
                                    sportEventFieldName = reader.GetString(2);
                                    sportEventHomeSchoolID = reader.GetInt32(3);
                                    sportEventAwaySchoolID = reader.GetInt32(4);
                                    eventSportNameID = reader.GetInt32(5);
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
                            + "U.userProfile_First_Name, "
                            + "U.userProfile_Last_Name, "
                            + "U.userProfile_Email, "
                            + "U.userProfile_Phone, "
                            + "U.userProfile_Street, "
                            + "U.userProfile_City, "
                            + "U.userProfile_State, "
                            + "U.userProfile_Zip, "
                            + "U.userProfile_Photo, "
                            + "R.Sport_Name_spt_Sport_Name_ID, "
                            + "R.User_Profile_Referee_Total_Ratings, "
                            + "R.User_Profile_Referee_Games_Officiated  "
                            + "FROM USER_PROFILE U JOIN USER_PROFILE_REFEREE R "
                            + "ON U.userProfile_ID= R.User_Profile_userProfile_ID "
                            + "WHERE U.userProfile_ID=@userProfile_ID";

                        command.Parameters.AddWithValue("@userProfile_ID", refereeProfileID);

                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    refereeFirstName = reader.GetString(0);
                                    refereeLastName = reader.GetString(1);
                                    refereeEmail = reader.GetString(2);
                                    refereePhone = reader.GetString(3);
                                    refereeStreet = reader.GetString(4);
                                    refereeCity = reader.GetString(5);
                                    refereeState = reader.GetString(6);
                                    refereeZip = reader.GetString(7);
                                    refereePhoto = reader.GetString(8);
                                    refereeSportNameID = reader.GetInt32(9);
                                    refereeTotalRatings = reader.GetInt32(10);
                                    refereeTotalGamesOfficiated = reader.GetInt32(11);

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
                            " sch_Name, sch_Logo FROM School WHERE sch_ID=@sch_ID";
                        command.Parameters.AddWithValue("@sch_ID", sportEventAwaySchoolID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    sportEventAwayTeamName = reader.GetString(0);
                                    sportEventAwayTeamLogo = reader.GetString(1);
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
                            " sch_Name, sch_Logo FROM School WHERE sch_ID=@sch_ID";
                        command.Parameters.AddWithValue("@sch_ID", sportEventHomeSchoolID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    sportEventHomeTeamName = reader.GetString(0);
                                    sportEventHomeTeamLogo = reader.GetString(1);
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
                            " spt_Name from Sport_Name where spt_Sport_Name_ID=@spt_Sport_Name_ID";


                        command.Parameters.AddWithValue("@spt_Sport_Name_ID", refereeSportNameID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    refereeSportName = reader.GetString(0);

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
                            " sptTypeRef_Referee_Title FROM SPORT_TYPE_REFEREES where sptTypeRef_ID=@sptTypeRef_ID";


                        command.Parameters.AddWithValue("@sptTypeRef_ID", refereeSportType_TypeID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    refereeSportType_TypeName = reader.GetString(0);

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
                            "SELECT TOP 1 count(Sport_Event_event_ID) " +
                            "FROM Referee_Event_History "
                            + "WHERE User_Profile_School_Director_Profile_ID=@User_Profile_School_Director_Profile_ID "
                            + "AND Referee_Status_refStatus_ID='P'; ";


                        command.Parameters.AddWithValue("@User_Profile_School_Director_Profile_ID", schoolDirectorUserProfileId);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    pendingApprovalCount = reader.GetInt32(0);

                                }
                            }
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }



                pendingApprovalRefereeCountID.Text = pendingApprovalCount.ToString();
                homeSchoolNameID.Text = sportEventHomeTeamName;
                awaySchoolNameID.Text = sportEventAwayTeamName;
                eventDateLabelID.Text = sportEventDate;
                eventTimeLabelID.Text = sportEventTime;
                sportEventTypeID.Text = refereeSportName;
                gameLocationFieldNameID.Text = sportEventFieldName;
                refereeType2ID.Text = refereeSportType_TypeName;
                referee2NameID.Text = refereeFirstName + " " + refereeLastName;
                referee1gameSpecializationID.Text = refereeSportName;
                referee1TotalGamesOfficiatedID.Text = refereeTotalGamesOfficiated.ToString();
                referee1ContactEmailID.Text = refereeEmail;
                referee1PhoneNumberID.Text = refereePhone;
                refereeStreetAddressLabelID.Text = refereeStreet;
                refereeCityLabelID.Text = refereeCity;
                refereeStateLabelID.Text = refereeZip;
                referee1GameTypeID.Text = "Sport Name " + refereeSportName;
                referee1FullNameID.Text = refereeFirstName + " " + refereeLastName;
                if (refereeTotalGamesOfficiated != 0 && refereeTotalRatings > 0)
                { refereeCurrentRatingFraction = refereeTotalRatings / refereeTotalGamesOfficiated; }

                else
                {
                    refereeCurrentRatingFraction = 1;
                }

                refereeCurrentRatingPercentage = (refereeCurrentRatingFraction * 100) / 5;

                referee1StarRatingID.Style.Add("width", refereeCurrentRatingPercentage + "%");

            }
            else
            {
                //code to right if no values are present
            }


        }

        protected void rejectReferee_Click(object sender, EventArgs e)
        {
            char refereeRejectionStatus = 'D';
            
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringLocalDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        "SELECT TOP 1 "
                        + "Sport_Event_event_ID, "
                        + "User_Profile_Referee_Profile_ID, "
                        + "Sport_Type_Referees_sptTypeRef_ID "
                        + "FROM "
                        + "Referee_Event_History "
                        + "WHERE "
                        + "User_Profile_School_Director_Profile_ID =@User_Profile_School_Director_Profile_ID "
                        + "AND Referee_Status_refStatus_ID='P'";

                    command.Parameters.AddWithValue("@User_Profile_School_Director_Profile_ID", schoolDirectorUserProfileId);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                sportEventID = reader.GetInt32(0);
                                refereeProfileID = reader.GetInt32(1);
                                refereeSportType_TypeID = reader.GetInt32(2);
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
                        + "U.userProfile_First_Name, "
                        + "U.userProfile_Last_Name, "
                        + "U.userProfile_Email, "
                        + "U.userProfile_Phone, "
                        + "U.userProfile_Street, "
                        + "U.userProfile_City, "
                        + "U.userProfile_State, "
                        + "U.userProfile_Zip, "
                        + "U.userProfile_Photo, "
                        + "R.Sport_Name_spt_Sport_Name_ID, "
                        + "R.User_Profile_Referee_Total_Ratings, "
                        + "R.User_Profile_Referee_Games_Officiated  "
                        + "FROM USER_PROFILE U JOIN USER_PROFILE_REFEREE R "
                        + "ON U.userProfile_ID= R.User_Profile_userProfile_ID "
                        + "WHERE U.userProfile_ID=@userProfile_ID";

                    command.Parameters.AddWithValue("@userProfile_ID", refereeProfileID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                refereeFirstName = reader.GetString(0);
                                refereeLastName = reader.GetString(1);
                                refereeEmail = reader.GetString(2);
                                refereePhone = reader.GetString(3);
                                refereeStreet = reader.GetString(4);
                                refereeCity = reader.GetString(5);
                                refereeState = reader.GetString(6);
                                refereeZip = reader.GetString(7);
                                refereePhoto = reader.GetString(8);
                                refereeSportNameID = reader.GetInt32(9);
                                refereeTotalRatings = reader.GetInt32(10);
                                refereeTotalGamesOfficiated = reader.GetInt32(11);

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
                    command.CommandText = "UPDATE Referee_Event_History " +
                        " SET Referee_Status_refStatus_ID= @Referee_Status_refStatus_ID " +
                        " WHERE Sport_Event_event_ID=@Sport_Event_event_ID AND User_Profile_Referee_Profile_ID=@User_Profile_Referee_Profile_ID";
                    command.Parameters.AddWithValue("@Referee_Status_refStatus_ID", refereeRejectionStatus);
                    command.Parameters.AddWithValue("@User_Profile_Referee_Profile_ID", refereeProfileID);
                    command.Parameters.AddWithValue("@Sport_Event_event_ID", sportEventID);
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
                    command.CommandText =
                        "SELECT TOP 1 "
                        + "Sport_Event_event_ID, "
                        + "User_Profile_Referee_Profile_ID, "
                        + "Sport_Type_Referees_sptTypeRef_ID "
                        + "FROM "
                        + "Referee_Event_History "
                        + "WHERE "
                        + "User_Profile_School_Director_Profile_ID =@User_Profile_School_Director_Profile_ID "
                        + "AND Referee_Status_refStatus_ID='P'";

                    command.Parameters.AddWithValue("@User_Profile_School_Director_Profile_ID", schoolDirectorUserProfileId);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                sportEventID = reader.GetInt32(0);
                                refereeProfileID = reader.GetInt32(1);
                                refereeSportType_TypeID = reader.GetInt32(2);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }

            if (sportEventID > 0)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText =
                            "SELECT "
                            + " event_Date, "
                            + " event_Time, "
                            + " event_School_Field_Name, "
                            + " School_Home_sch_ID, "
                            + " School_Away_sch_ID, "
                            + " Sport_Name_spt_Sport_Name_ID "
                            + " FROM Sport_Event WHERE event_ID=@event_ID";
                        command.Parameters.AddWithValue("@event_ID", sportEventID);

                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    sportEventDate = (reader.GetDateTime(0)).ToString();
                                    sportEventTime = (reader.GetTimeSpan(1)).ToString();
                                    sportEventFieldName = reader.GetString(2);
                                    sportEventHomeSchoolID = reader.GetInt32(3);
                                    sportEventAwaySchoolID = reader.GetInt32(4);
                                    eventSportNameID = reader.GetInt32(5);
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
                            + "U.userProfile_First_Name, "
                            + "U.userProfile_Last_Name, "
                            + "U.userProfile_Email, "
                            + "U.userProfile_Phone, "
                            + "U.userProfile_Street, "
                            + "U.userProfile_City, "
                            + "U.userProfile_State, "
                            + "U.userProfile_Zip, "
                            + "U.userProfile_Photo, "
                            + "R.Sport_Name_spt_Sport_Name_ID, "
                            + "R.User_Profile_Referee_Total_Ratings, "
                            + "R.User_Profile_Referee_Games_Officiated  "
                            + "FROM USER_PROFILE U JOIN USER_PROFILE_REFEREE R "
                            + "ON U.userProfile_ID= R.User_Profile_userProfile_ID "
                            + "WHERE U.userProfile_ID=@userProfile_ID";

                        command.Parameters.AddWithValue("@userProfile_ID", refereeProfileID);

                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    refereeFirstName = reader.GetString(0);
                                    refereeLastName = reader.GetString(1);
                                    refereeEmail = reader.GetString(2);
                                    refereePhone = reader.GetString(3);
                                    refereeStreet = reader.GetString(4);
                                    refereeCity = reader.GetString(5);
                                    refereeState = reader.GetString(6);
                                    refereeZip = reader.GetString(7);
                                    refereePhoto = reader.GetString(8);
                                    refereeSportNameID = reader.GetInt32(9);
                                    refereeTotalRatings = reader.GetInt32(10);
                                    refereeTotalGamesOfficiated = reader.GetInt32(11);

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
                            " sch_Name, sch_Logo FROM School WHERE sch_ID=@sch_ID";
                        command.Parameters.AddWithValue("@sch_ID", sportEventAwaySchoolID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    sportEventAwayTeamName = reader.GetString(0);
                                    sportEventAwayTeamLogo = reader.GetString(1);
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
                            " sch_Name, sch_Logo FROM School WHERE sch_ID=@sch_ID";
                        command.Parameters.AddWithValue("@sch_ID", sportEventHomeSchoolID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    sportEventHomeTeamName = reader.GetString(0);
                                    sportEventHomeTeamLogo = reader.GetString(1);
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
                            " spt_Name from Sport_Name where spt_Sport_Name_ID=@spt_Sport_Name_ID";


                        command.Parameters.AddWithValue("@spt_Sport_Name_ID", refereeSportNameID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    refereeSportName = reader.GetString(0);

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
                            " sptTypeRef_Referee_Title FROM SPORT_TYPE_REFEREES where sptTypeRef_ID=@sptTypeRef_ID";


                        command.Parameters.AddWithValue("@sptTypeRef_ID", refereeSportType_TypeID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    refereeSportType_TypeName = reader.GetString(0);

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
                            "SELECT TOP 1 count(Sport_Event_event_ID) " +
                            "FROM Referee_Event_History "
                            + "WHERE User_Profile_School_Director_Profile_ID=@User_Profile_School_Director_Profile_ID "
                            + "AND Referee_Status_refStatus_ID='P'; ";


                        command.Parameters.AddWithValue("@User_Profile_School_Director_Profile_ID", schoolDirectorUserProfileId);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    pendingApprovalCount = reader.GetInt32(0);

                                }
                            }
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }



                pendingApprovalRefereeCountID.Text = pendingApprovalCount.ToString();
                homeSchoolNameID.Text = sportEventHomeTeamName;
                awaySchoolNameID.Text = sportEventAwayTeamName;
                eventDateLabelID.Text = sportEventDate;
                eventTimeLabelID.Text = sportEventTime;
                sportEventTypeID.Text = refereeSportName;
                gameLocationFieldNameID.Text = sportEventFieldName;
                refereeType2ID.Text = refereeSportType_TypeName;
                referee2NameID.Text = refereeFirstName + " " + refereeLastName;
                referee1gameSpecializationID.Text = refereeSportName;
                referee1TotalGamesOfficiatedID.Text = refereeTotalGamesOfficiated.ToString();
                referee1ContactEmailID.Text = refereeEmail;
                referee1PhoneNumberID.Text = refereePhone;
                refereeStreetAddressLabelID.Text = refereeStreet;
                refereeCityLabelID.Text = refereeCity;
                refereeStateLabelID.Text = refereeZip;
                referee1GameTypeID.Text = "Sport Name " + refereeSportName;
                referee1FullNameID.Text = refereeFirstName + " " + refereeLastName;
                if (refereeTotalGamesOfficiated != 0 && refereeTotalRatings > 0)
                { refereeCurrentRatingFraction = refereeTotalRatings / refereeTotalGamesOfficiated; }

                else
                {
                    refereeCurrentRatingFraction = 1;
                }

                refereeCurrentRatingPercentage = (refereeCurrentRatingFraction * 100) / 5;

                referee1StarRatingID.Style.Add("width", refereeCurrentRatingPercentage + "%");

            }
            else
            {
                //code to right if no values are present
            }


        }

    }
}