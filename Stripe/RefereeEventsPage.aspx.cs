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
    public partial class RefereeEventsPage : System.Web.UI.Page
    {
        string cookieUsername = "";
        int refereeUserProfileID;
        int refereeSportNameID;
        int i = 0;
        int sportEventIDreceived;
        List<int> sportEventIDListToCheck = new List<int>();
        string eventDateToRegister;
        string eventTimeToRegister;
        string eventSchoolFieldName;
        int eventHomeSchoolID;
        int eventAwaySchoolID;
        string eventHomeSchoolName;
        string eventAwaySchoolName;
        string selectedRefereePosition;
        int eventIDSelected;
        int schoolDirectorProfileIDSelected;
        int selectedRefereePositionID;


        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringLocalDB"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] != null)
            {
                try
                {
                    refereeUserProfileID = Int32.Parse(Session["userid"].ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {

                Response.Redirect("LoginForm.aspx", false);
            }

            SqlDataSource1.SelectCommand = "SELECT SE.event_Date, SE.event_Time, SE.event_Home_Team_Score, SE.event_Away_Team_Score, SE.event_School_Field_Name FROM SPORT_EVENT SE JOIN Referee_Event_History REH ON SE.event_ID=REH.Sport_Event_event_ID WHERE REH.User_Profile_Referee_Profile_ID= " + refereeUserProfileID + " AND SE.event_Date<=SYSDATETIME()";
            SqlDataSource2.SelectCommand = "SELECT SE.event_Date, SE.event_Time, SE.event_Home_Team_Score, SE.event_Away_Team_Score, SE.event_School_Field_Name FROM SPORT_EVENT SE JOIN Referee_Event_History REH ON SE.event_ID=REH.Sport_Event_event_ID WHERE REH.User_Profile_Referee_Profile_ID= " + refereeUserProfileID + " AND SE.event_Date>=SYSDATETIME()";


            if (!Page.IsPostBack)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT Sport_Name_spt_Sport_Name_ID FROM USER_PROFILE_REFEREE WHERE User_Profile_userProfile_ID= @User_Profile_user";
                        command.Parameters.AddWithValue("@User_Profile_user", refereeUserProfileID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    refereeSportNameID = reader.GetInt32(0);
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
                        command.CommandText = "SELECT event_ID FROM SPORT_EVENT WHERE event_Date>=SYSDATETIME() AND Sport_Name_spt_Sport_Name_ID=@Sport_Name_spt_Sport_Name_ID_Referee";
                        command.Parameters.AddWithValue("@Sport_Name_spt_Sport_Name_ID_Referee", refereeSportNameID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    sportEventIDListToCheck.Add(reader.GetInt32(0));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }


                for (int i = 0; i < sportEventIDListToCheck.Count; )
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "SELECT refEventHistory_ID FROM Referee_Event_History "
                            + " WHERE Sport_Event_event_ID=@Sport_Event_event_ID_Second_Check AND "
                            + " User_Profile_Referee_Profile_ID=@User_Profile_Referee_Profile_ID_Second_Check";
                            command.Parameters.AddWithValue("@Sport_Event_event_ID_Second_Check", sportEventIDListToCheck[i]);
                            command.Parameters.AddWithValue("@User_Profile_Referee_Profile_ID_Second_Check", refereeUserProfileID);
                            try
                            {
                                connection.Open();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    i++;
                                }
                                else
                                {
                                    sportEventIDreceived = sportEventIDListToCheck[i];
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }


                        }
                    }
                }

                if (sportEventIDreceived > 1)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = " SELECT event_Date, event_Time, event_School_Field_Name, School_Home_sch_ID, "
                                + " School_Away_sch_ID FROM Sport_Event WHERE event_ID=@event_ID_ToRegister";
                            command.Parameters.AddWithValue("@event_ID_ToRegister", sportEventIDreceived);
                            try
                            {
                                connection.Open();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        eventDateToRegister = (reader.GetDateTime(0)).ToString();
                                        eventTimeToRegister = (reader.GetTimeSpan(1)).ToString(); ;
                                        eventSchoolFieldName = reader.GetString(2);
                                        eventHomeSchoolID = reader.GetInt32(3);
                                        eventAwaySchoolID = reader.GetInt32(4);
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
                            command.CommandText = "SELECT sch_Name FROM SCHOOL WHERE sch_ID=@sch_Home_ID_Obtained";
                            command.Parameters.AddWithValue("@sch_Home_ID_Obtained", eventHomeSchoolID);
                            try
                            {
                                connection.Open();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        eventHomeSchoolName = reader.GetString(0);

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
                            command.CommandText = "SELECT sch_Name FROM SCHOOL WHERE sch_ID=@sch_Home_ID_Obtained";
                            command.Parameters.AddWithValue("@sch_Home_ID_Obtained", eventAwaySchoolID);
                            try
                            {
                                connection.Open();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        eventAwaySchoolName = reader.GetString(0);

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
                            command.CommandText = "SELECT sptTypeRef_Referee_Title FROM SPORT_TYPE_REFEREES WHERE Sport_Name_spt_Sport_Name_ID=@Sport_Name_spt_Sport_Name_ID";
                            command.Parameters.AddWithValue("@Sport_Name_spt_Sport_Name_ID", refereeSportNameID);
                            try
                            {
                                connection.Open();
                                GamePositionTypeSelectionValue.Items.Clear();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        GamePositionTypeSelectionValue.Items.Add(new ListItem(reader[0].ToString(), reader[0].ToString()));
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }

                    EventDateLabelID.Text = eventDateToRegister;
                    EventTimeLabelID.Text = eventTimeToRegister;
                    EventLocationID.Text = eventSchoolFieldName;
                    homeSchoolNameLabelID.Text = eventHomeSchoolName;
                    awaySchoolNameLabelID.Text = eventAwaySchoolName;


                }
                else
                {
                    NoNewEventToRegister.Text = "No new event to register";
                    registerForEvent.Visible = false;
                    DeclineEvent.Visible = false;
                }



            }

        }

        protected void registerAsReferee(object sender, EventArgs e)
        {

            selectedRefereePosition = GamePositionTypeSelectionValue.SelectedItem.Text;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Sport_Name_spt_Sport_Name_ID FROM USER_PROFILE_REFEREE WHERE User_Profile_userProfile_ID= @User_Profile_user";
                    command.Parameters.AddWithValue("@User_Profile_user", refereeUserProfileID);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                refereeSportNameID = reader.GetInt32(0);
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
                    command.CommandText = "SELECT event_ID FROM SPORT_EVENT WHERE event_Date>=SYSDATETIME() AND Sport_Name_spt_Sport_Name_ID=@Sport_Name_spt_Sport_Name_ID_Referee";
                    command.Parameters.AddWithValue("@Sport_Name_spt_Sport_Name_ID_Referee", refereeSportNameID);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                sportEventIDListToCheck.Add(reader.GetInt32(0));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }


            for (int i = 0; i < sportEventIDListToCheck.Count; )
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT refEventHistory_ID FROM Referee_Event_History "
                        + " WHERE Sport_Event_event_ID=@Sport_Event_event_ID_Second_Check AND "
                        + " User_Profile_Referee_Profile_ID=@User_Profile_Referee_Profile_ID_Second_Check";
                        command.Parameters.AddWithValue("@Sport_Event_event_ID_Second_Check", sportEventIDListToCheck[i]);
                        command.Parameters.AddWithValue("@User_Profile_Referee_Profile_ID_Second_Check", refereeUserProfileID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                i++;
                            }
                            else
                            {
                                sportEventIDreceived = sportEventIDListToCheck[i];
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }


                    }
                }
            }

            if (sportEventIDreceived > 1)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = " SELECT event_ID, School_Home_sch_ID "
                            + " FROM Sport_Event WHERE event_ID=@event_ID_ToRegister";
                        command.Parameters.AddWithValue("@event_ID_ToRegister", sportEventIDreceived);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {


                                    eventIDSelected = reader.GetInt32(0);
                                    eventHomeSchoolID = reader.GetInt32(1);

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
                        command.CommandText = "SELECT User_Profile_Director_Profile_ID FROM SCHOOL WHERE sch_ID=@applySchoolID";
                        command.Parameters.AddWithValue("@applySchoolID", eventHomeSchoolID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    schoolDirectorProfileIDSelected = reader.GetInt32(0);
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
                        command.CommandText = "SELECT Sport_Name_spt_Sport_Name_ID FROM SPORT_TYPE_REFEREES WHERE sptTypeRef_Referee_Title=@sptTypeRef_Referee_Title";
                        command.Parameters.AddWithValue("@sptTypeRef_Referee_Title", selectedRefereePosition);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    selectedRefereePositionID = reader.GetInt32(0);
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

                        command.CommandText = " insert into REFEREE_EVENT_HISTORY (refEventHistory_Total_Stars_Rating, Sport_Event_event_ID, "
                    + " User_Profile_Referee_Profile_ID, User_Profile_School_Director_Profile_ID, Referee_Status_refStatus_ID, "
                    + " Sport_Type_Referees_sptTypeRef_ID) "
                    + " values (0, @Sport_Event_event_ID_Final, @User_Profile_Referee_Profile_ID_Final, @User_Profile_School_Director_Profile_ID_Final, "
                    + " 'P', @Sport_Type_Referees_sptTypeRef_ID_Final)";

                        command.Parameters.AddWithValue("@Sport_Event_event_ID_Final", sportEventIDreceived);
                        command.Parameters.AddWithValue("@User_Profile_Referee_Profile_ID_Final", refereeUserProfileID);
                        command.Parameters.AddWithValue("@User_Profile_School_Director_Profile_ID_Final", schoolDirectorProfileIDSelected);
                        command.Parameters.AddWithValue("@Sport_Type_Referees_sptTypeRef_ID_Final", selectedRefereePositionID);
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


                SqlDataSource1.SelectCommand = "SELECT SE.event_Date, SE.event_Time, SE.event_Home_Team_Score, SE.event_Away_Team_Score, SE.event_School_Field_Name FROM SPORT_EVENT SE JOIN Referee_Event_History REH ON SE.event_ID=REH.Sport_Event_event_ID WHERE REH.User_Profile_Referee_Profile_ID= " + refereeUserProfileID + " AND SE.event_Date<=SYSDATETIME()";
                SqlDataSource2.SelectCommand = "SELECT SE.event_Date, SE.event_Time, SE.event_Home_Team_Score, SE.event_Away_Team_Score, SE.event_School_Field_Name FROM SPORT_EVENT SE JOIN Referee_Event_History REH ON SE.event_ID=REH.Sport_Event_event_ID WHERE REH.User_Profile_Referee_Profile_ID= " + refereeUserProfileID + " AND SE.event_Date>=SYSDATETIME()";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT Sport_Name_spt_Sport_Name_ID FROM USER_PROFILE_REFEREE WHERE User_Profile_userProfile_ID= @User_Profile_user";
                        command.Parameters.AddWithValue("@User_Profile_user", refereeUserProfileID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    refereeSportNameID = reader.GetInt32(0);
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
                        command.CommandText = "SELECT event_ID FROM SPORT_EVENT WHERE event_Date>=SYSDATETIME() AND Sport_Name_spt_Sport_Name_ID=@Sport_Name_spt_Sport_Name_ID_Referee";
                        command.Parameters.AddWithValue("@Sport_Name_spt_Sport_Name_ID_Referee", refereeSportNameID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    sportEventIDListToCheck.Add(reader.GetInt32(0));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }


                for (int i = 0; i < sportEventIDListToCheck.Count; )
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "SELECT refEventHistory_ID FROM Referee_Event_History "
                            + " WHERE Sport_Event_event_ID=@Sport_Event_event_ID_Second_Check AND "
                            + " User_Profile_Referee_Profile_ID=@User_Profile_Referee_Profile_ID_Second_Check";
                            command.Parameters.AddWithValue("@Sport_Event_event_ID_Second_Check", sportEventIDListToCheck[i]);
                            command.Parameters.AddWithValue("@User_Profile_Referee_Profile_ID_Second_Check", refereeUserProfileID);
                            try
                            {
                                connection.Open();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    i++;
                                }
                                else
                                {
                                    sportEventIDreceived = sportEventIDListToCheck[i];
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }


                        }
                    }
                }

                if (sportEventIDreceived > 1)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = " SELECT event_Date, event_Time, event_School_Field_Name, School_Home_sch_ID, "
                                + " School_Away_sch_ID FROM Sport_Event WHERE event_ID=@event_ID_ToRegister";
                            command.Parameters.AddWithValue("@event_ID_ToRegister", sportEventIDreceived);
                            try
                            {
                                connection.Open();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        eventDateToRegister = (reader.GetDateTime(0)).ToString();
                                        eventTimeToRegister = (reader.GetTimeSpan(1)).ToString(); ;
                                        eventSchoolFieldName = reader.GetString(2);
                                        eventHomeSchoolID = reader.GetInt32(3);
                                        eventAwaySchoolID = reader.GetInt32(4);
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
                            command.CommandText = "SELECT sch_Name FROM SCHOOL WHERE sch_ID=@sch_Home_ID_Obtained";
                            command.Parameters.AddWithValue("@sch_Home_ID_Obtained", eventHomeSchoolID);
                            try
                            {
                                connection.Open();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        eventHomeSchoolName = reader.GetString(0);

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
                            command.CommandText = "SELECT sch_Name FROM SCHOOL WHERE sch_ID=@sch_Home_ID_Obtained";
                            command.Parameters.AddWithValue("@sch_Home_ID_Obtained", eventAwaySchoolID);
                            try
                            {
                                connection.Open();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        eventAwaySchoolName = reader.GetString(0);

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
                            command.CommandText = "SELECT sptTypeRef_Referee_Title FROM SPORT_TYPE_REFEREES WHERE Sport_Name_spt_Sport_Name_ID=@Sport_Name_spt_Sport_Name_ID";
                            command.Parameters.AddWithValue("@Sport_Name_spt_Sport_Name_ID", refereeSportNameID);
                            try
                            {
                                connection.Open();
                                GamePositionTypeSelectionValue.Items.Clear();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        GamePositionTypeSelectionValue.Items.Add(new ListItem(reader[0].ToString(), reader[0].ToString()));
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }

                    EventDateLabelID.Text = eventDateToRegister;
                    EventTimeLabelID.Text = eventTimeToRegister;
                    EventLocationID.Text = eventSchoolFieldName;
                    homeSchoolNameLabelID.Text = eventHomeSchoolName;
                    awaySchoolNameLabelID.Text = eventAwaySchoolName;


                }
                else
                {
                    NoNewEventToRegister.Text = "No new event to register";
                    registerForEvent.Visible = false;
                    DeclineEvent.Visible = false;
                }



            }



        }


        protected void denyAsReferee(object sender, EventArgs e)
        {
            selectedRefereePosition = GamePositionTypeSelectionValue.SelectedItem.Text;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Sport_Name_spt_Sport_Name_ID FROM USER_PROFILE_REFEREE WHERE User_Profile_userProfile_ID= @User_Profile_user";
                    command.Parameters.AddWithValue("@User_Profile_user", refereeUserProfileID);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                refereeSportNameID = reader.GetInt32(0);
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
                    command.CommandText = "SELECT event_ID FROM SPORT_EVENT WHERE event_Date>=SYSDATETIME() AND Sport_Name_spt_Sport_Name_ID=@Sport_Name_spt_Sport_Name_ID_Referee";
                    command.Parameters.AddWithValue("@Sport_Name_spt_Sport_Name_ID_Referee", refereeSportNameID);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                sportEventIDListToCheck.Add(reader.GetInt32(0));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }


            for (int i = 0; i < sportEventIDListToCheck.Count; )
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT refEventHistory_ID FROM Referee_Event_History "
                        + " WHERE Sport_Event_event_ID=@Sport_Event_event_ID_Second_Check AND "
                        + " User_Profile_Referee_Profile_ID=@User_Profile_Referee_Profile_ID_Second_Check";
                        command.Parameters.AddWithValue("@Sport_Event_event_ID_Second_Check", sportEventIDListToCheck[i]);
                        command.Parameters.AddWithValue("@User_Profile_Referee_Profile_ID_Second_Check", refereeUserProfileID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                i++;
                            }
                            else
                            {
                                sportEventIDreceived = sportEventIDListToCheck[i];
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }


                    }
                }
            }

            if (sportEventIDreceived > 1)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = " SELECT event_ID, School_Home_sch_ID "
                            + " FROM Sport_Event WHERE event_ID=@event_ID_ToRegister";
                        command.Parameters.AddWithValue("@event_ID_ToRegister", sportEventIDreceived);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {


                                    eventIDSelected = reader.GetInt32(0);
                                    eventHomeSchoolID = reader.GetInt32(1);

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
                        command.CommandText = "SELECT User_Profile_Director_Profile_ID FROM SCHOOL WHERE sch_ID=@applySchoolID";
                        command.Parameters.AddWithValue("@applySchoolID", eventHomeSchoolID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    schoolDirectorProfileIDSelected = reader.GetInt32(0);
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
                        command.CommandText = "SELECT Sport_Name_spt_Sport_Name_ID FROM SPORT_TYPE_REFEREES WHERE sptTypeRef_Referee_Title=@sptTypeRef_Referee_Title";
                        command.Parameters.AddWithValue("@sptTypeRef_Referee_Title", selectedRefereePosition);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    selectedRefereePositionID = reader.GetInt32(0);
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

                        command.CommandText = " insert into REFEREE_EVENT_HISTORY (refEventHistory_Total_Stars_Rating, Sport_Event_event_ID, "
                    + " User_Profile_Referee_Profile_ID, User_Profile_School_Director_Profile_ID, Referee_Status_refStatus_ID, "
                    + " Sport_Type_Referees_sptTypeRef_ID) "
                    + " values (0, @Sport_Event_event_ID_Final, @User_Profile_Referee_Profile_ID_Final, @User_Profile_School_Director_Profile_ID_Final, "
                    + " 'D', @Sport_Type_Referees_sptTypeRef_ID_Final)";

                        command.Parameters.AddWithValue("@Sport_Event_event_ID_Final", sportEventIDreceived);
                        command.Parameters.AddWithValue("@User_Profile_Referee_Profile_ID_Final", refereeUserProfileID);
                        command.Parameters.AddWithValue("@User_Profile_School_Director_Profile_ID_Final", schoolDirectorProfileIDSelected);
                        command.Parameters.AddWithValue("@Sport_Type_Referees_sptTypeRef_ID_Final", selectedRefereePositionID);
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


                SqlDataSource1.SelectCommand = "SELECT SE.event_Date, SE.event_Time, SE.event_Home_Team_Score, SE.event_Away_Team_Score, SE.event_School_Field_Name FROM SPORT_EVENT SE JOIN Referee_Event_History REH ON SE.event_ID=REH.Sport_Event_event_ID WHERE REH.User_Profile_Referee_Profile_ID= " + refereeUserProfileID + " AND SE.event_Date<=SYSDATETIME()";
                SqlDataSource2.SelectCommand = "SELECT SE.event_Date, SE.event_Time, SE.event_Home_Team_Score, SE.event_Away_Team_Score, SE.event_School_Field_Name FROM SPORT_EVENT SE JOIN Referee_Event_History REH ON SE.event_ID=REH.Sport_Event_event_ID WHERE REH.User_Profile_Referee_Profile_ID= " + refereeUserProfileID + " AND SE.event_Date>=SYSDATETIME()";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT Sport_Name_spt_Sport_Name_ID FROM USER_PROFILE_REFEREE WHERE User_Profile_userProfile_ID= @User_Profile_user";
                        command.Parameters.AddWithValue("@User_Profile_user", refereeUserProfileID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    refereeSportNameID = reader.GetInt32(0);
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
                        command.CommandText = "SELECT event_ID FROM SPORT_EVENT WHERE event_Date>=SYSDATETIME() AND Sport_Name_spt_Sport_Name_ID=@Sport_Name_spt_Sport_Name_ID_Referee";
                        command.Parameters.AddWithValue("@Sport_Name_spt_Sport_Name_ID_Referee", refereeSportNameID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    sportEventIDListToCheck.Add(reader.GetInt32(0));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }


                for (int i = 0; i < sportEventIDListToCheck.Count; )
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "SELECT refEventHistory_ID FROM Referee_Event_History "
                            + " WHERE Sport_Event_event_ID=@Sport_Event_event_ID_Second_Check AND "
                            + " User_Profile_Referee_Profile_ID=@User_Profile_Referee_Profile_ID_Second_Check";
                            command.Parameters.AddWithValue("@Sport_Event_event_ID_Second_Check", sportEventIDListToCheck[i]);
                            command.Parameters.AddWithValue("@User_Profile_Referee_Profile_ID_Second_Check", refereeUserProfileID);
                            try
                            {
                                connection.Open();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    i++;
                                }
                                else
                                {
                                    sportEventIDreceived = sportEventIDListToCheck[i];
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }


                        }
                    }
                }

                if (sportEventIDreceived > 1)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = " SELECT event_Date, event_Time, event_School_Field_Name, School_Home_sch_ID, "
                                + " School_Away_sch_ID FROM Sport_Event WHERE event_ID=@event_ID_ToRegister";
                            command.Parameters.AddWithValue("@event_ID_ToRegister", sportEventIDreceived);
                            try
                            {
                                connection.Open();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        eventDateToRegister = (reader.GetDateTime(0)).ToString();
                                        eventTimeToRegister = (reader.GetTimeSpan(1)).ToString(); ;
                                        eventSchoolFieldName = reader.GetString(2);
                                        eventHomeSchoolID = reader.GetInt32(3);
                                        eventAwaySchoolID = reader.GetInt32(4);
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
                            command.CommandText = "SELECT sch_Name FROM SCHOOL WHERE sch_ID=@sch_Home_ID_Obtained";
                            command.Parameters.AddWithValue("@sch_Home_ID_Obtained", eventHomeSchoolID);
                            try
                            {
                                connection.Open();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        eventHomeSchoolName = reader.GetString(0);

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
                            command.CommandText = "SELECT sch_Name FROM SCHOOL WHERE sch_ID=@sch_Home_ID_Obtained";
                            command.Parameters.AddWithValue("@sch_Home_ID_Obtained", eventAwaySchoolID);
                            try
                            {
                                connection.Open();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        eventAwaySchoolName = reader.GetString(0);

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
                            command.CommandText = "SELECT sptTypeRef_Referee_Title FROM SPORT_TYPE_REFEREES WHERE Sport_Name_spt_Sport_Name_ID=@Sport_Name_spt_Sport_Name_ID";
                            command.Parameters.AddWithValue("@Sport_Name_spt_Sport_Name_ID", refereeSportNameID);
                            try
                            {
                                connection.Open();
                                GamePositionTypeSelectionValue.Items.Clear();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        GamePositionTypeSelectionValue.Items.Add(new ListItem(reader[0].ToString(), reader[0].ToString()));
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }

                    EventDateLabelID.Text = eventDateToRegister;
                    EventTimeLabelID.Text = eventTimeToRegister;
                    EventLocationID.Text = eventSchoolFieldName;
                    homeSchoolNameLabelID.Text = eventHomeSchoolName;
                    awaySchoolNameLabelID.Text = eventAwaySchoolName;


                }
                else
                {
                    NoNewEventToRegister.Text = "No new event to register";
                    registerForEvent.Visible = false;
                    DeclineEvent.Visible = false;
                }



            }



        }

        protected void logoutout_Click(object sender, EventArgs e)
        {
            Session["loginid"] = null;
            Session["userid"] = null;
            Response.Redirect("LoginForm.aspx", false);
        }

    }
}