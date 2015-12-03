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
    public partial class SchoolDirectorEventsPage : System.Web.UI.Page
    {
        int schoolDirectorUserProfileId;
        string cookieUsername;
        string selectedSchoolName;
        string selectedSportName;
        int selectedSchoolID;
        int selectedSportID;
        string eventDateSelected;
        DateTime eventDateSelected_Date;
        string eventTimeSelected;
        DateTime eventTimeSelected_Time;
        string eventLocationSelected;
        int homeSchoolID;

        readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringLocalDB"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] != null)
            {
                try
                {
                    schoolDirectorUserProfileId = Convert.ToInt32(Session["userid"]);
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




            SqlDataSource1.SelectCommand = "SELECT SE.event_Date AS EVENT_DATE, SE.event_Time AS EVENT_TIME, SE.event_Home_Team_Score AS HOME_TEAM_SCORE, SE.event_Away_Team_Score AS AWAY_TEAM_SCORE, SE.event_School_Field_Name AS FIELD_NAME FROM Sport_Event SE JOIN SCHOOL SCH ON SE.School_Home_sch_ID= SCH.sch_ID WHERE SCH.User_Profile_Director_Profile_ID= " + schoolDirectorUserProfileId + " AND SE.event_Date<=SYSDATETIME()";

            SqlDataSource2.SelectCommand = "SELECT SE.event_Date AS EVENT_DATE, SE.event_Time AS EVENT_TIME, SE.event_Home_Team_Score AS HOME_TEAM_SCORE, SE.event_Away_Team_Score AS AWAY_TEAM_SCORE, SE.event_School_Field_Name AS FIELD_NAME FROM Sport_Event SE JOIN SCHOOL SCH ON SE.School_Home_sch_ID= SCH.sch_ID WHERE SCH.User_Profile_Director_Profile_ID= " + schoolDirectorUserProfileId + " AND SE.event_Date>=SYSDATETIME()";

            if (!Page.IsPostBack)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT spt_Name FROM SPORT_NAME";
                        try
                        {
                            connection.Open();
                            GameTypeSelectionValue.Items.Clear();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    GameTypeSelectionValue.Items.Add(new ListItem(reader[0].ToString(), reader[0].ToString()));
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
                        command.CommandText = "SELECT sch_Name FROM SCHOOL";
                        try
                        {
                            connection.Open();
                            AwayTeamSelection.Items.Clear();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    AwayTeamSelection.Items.Add(new ListItem(reader[0].ToString(), reader[0].ToString()));
                                }

                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                    }
                }


            }

        }


        protected void createEventButton_Click(object sender, EventArgs e)
        {
            selectedSportName = GameTypeSelectionValue.SelectedItem.Text;
            selectedSchoolName = AwayTeamSelection.SelectedItem.Text;
            eventDateSelected = EventDateTextBoxID.Text;
            eventTimeSelected = EventTimeFieldID.Text;
            eventDateSelected_Date = Convert.ToDateTime(eventDateSelected);
            eventTimeSelected_Time = Convert.ToDateTime(eventTimeSelected);
            eventLocationSelected = eventLocationFieldID.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT spt_Sport_Name_ID FROM SPORT_NAME WHERE spt_Name=@spt_Name";
                    command.Parameters.AddWithValue("@spt_Name", selectedSportName);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                selectedSportID = reader.GetInt32(0);
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
                    command.CommandText = "SELECT sch_ID FROM SCHOOL WHERE sch_Name=@sch_Name";
                    command.Parameters.AddWithValue("@sch_Name", selectedSchoolName);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                selectedSchoolID = reader.GetInt32(0);
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
                    command.CommandText = "SELECT sch_ID FROM SCHOOL WHERE User_Profile_Director_Profile_ID=@User_Profile_Director_Profile_ID";
                    command.Parameters.AddWithValue("@User_Profile_Director_Profile_ID", schoolDirectorUserProfileId);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                homeSchoolID = reader.GetInt32(0);
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
                    command.CommandText = " insert into SPORT_EVENT "
                                + " (event_Completion, event_Date, "
                                + " event_Time, event_Home_Team_Score, "
                                + " event_Away_Team_Score, event_School_Field_Name, "
                                + " School_Home_sch_ID, School_Away_sch_ID, "
                                + " Sport_Name_spt_Sport_Name_ID) values "
                                + " ('n', @event_Date, @event_Time, 0, 0, "
                                + " @event_School_Field_Name, @School_Home_sch_ID, "
                                + " @School_Away_sch_ID, @Sport_Name_spt_Sport_Name_ID)";

                    command.Parameters.AddWithValue("@event_Date", eventDateSelected_Date);
                    command.Parameters.AddWithValue("@event_Time", eventTimeSelected_Time);
                    command.Parameters.AddWithValue("@event_School_Field_Name", eventLocationSelected);
                    command.Parameters.AddWithValue("@School_Home_sch_ID", homeSchoolID);
                    command.Parameters.AddWithValue("@School_Away_sch_ID", selectedSchoolID);
                    command.Parameters.AddWithValue("@Sport_Name_spt_Sport_Name_ID", selectedSportID);

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
            SqlDataSource1.SelectCommand = "SELECT SE.event_Date AS EVENT_DATE, SE.event_Time AS EVENT_TIME, SE.event_Home_Team_Score AS HOME_TEAM_SCORE, SE.event_Away_Team_Score AS AWAY_TEAM_SCORE, SE.event_School_Field_Name AS FIELD_NAME FROM Sport_Event SE JOIN SCHOOL SCH ON SE.School_Home_sch_ID= SCH.sch_ID WHERE SCH.User_Profile_Director_Profile_ID= " + schoolDirectorUserProfileId + " AND SE.event_Date<=SYSDATETIME()";

            SqlDataSource2.SelectCommand = "SELECT SE.event_Date AS EVENT_DATE, SE.event_Time AS EVENT_TIME, SE.event_Home_Team_Score AS HOME_TEAM_SCORE, SE.event_Away_Team_Score AS AWAY_TEAM_SCORE, SE.event_School_Field_Name AS FIELD_NAME FROM Sport_Event SE JOIN SCHOOL SCH ON SE.School_Home_sch_ID= SCH.sch_ID WHERE SCH.User_Profile_Director_Profile_ID= " + schoolDirectorUserProfileId + " AND SE.event_Date>=SYSDATETIME()";

        }

        protected void logoutout_Click(object sender, EventArgs e)
        {
            Session["loginid"] = null;
            Session["userid"] = null;
            Response.Redirect("LoginForm.aspx", false);
        }
    }
}