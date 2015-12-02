using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Stripe
{
    public partial class RefereeEventsPage : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringLocalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource SqlDataSource1 = new SqlDataSource();
            SqlDataSource1.ID = "SqlDataSource1";
            this.Page.Controls.Add(SqlDataSource1);
            SqlDataSource1.ConnectionString = connectionString;
            SqlDataSource1.SelectCommand = "select sportevent.event_ID, homeschool.sch_Name as homeschool, awayschool.sch_Name as awayschool, " +
                                        "homeschool.sch_Street + ' ' +  homeschool.sch_City + ' ' +  homeschool.sch_State + ' ' +   " +
                                        " homeschool.sch_Zip as homeschooladdress, sportevent.event_Date, sportevent.event_Time, " +
                                        "sportevent.event_School_Field_Name, sportname.spt_Name " +
                                        "from [Sport_Event] sportevent " +
                                        "join [School] homeschool on sportevent.[School_Home_sch_ID] = homeschool.[sch_ID] " +
                                        "join [School] awayschool on sportevent.[School_Away_sch_ID] = awayschool.[sch_ID] " +
                                        "join [Sport_Name] sportname on sportevent.[Sport_Name_spt_Sport_Name_ID] = sportname.[spt_Sport_Name_ID] " +
                                        "join [dbo].[Referee_Event_History] refereeHistory on sportevent.event_ID = refereeHistory.[Sport_Event_event_ID] " +
                                        "and refereeHistory.[User_Profile_Referee_Profile_ID] = @refId and refereeHistory.[Referee_Status_refStatus_ID] like @refereeStatus " +
                                        "where [Sport_Name_spt_Sport_Name_ID] = @sportId " +
                                        " AND [event_Date] > @systemDate; ";

            SqlDataSource1.SelectParameters.Add("refereeStatus", "A");
            SqlDataSource1.SelectParameters.Add("refId", "15");
            SqlDataSource1.SelectParameters.Add("sportId", "2");
            SqlDataSource1.SelectParameters.Add("systemDate", DateTime.Now.ToString());
            //GridView1.DataSource = SqlDataSource1;
            //ridView1.DataBind();
            //GetEventsList();
        }




        protected void GetEventsList()
        {


            // HttpCookie getUserCookie = Request.Cookies["user"];
            //int refereeId = Convert.ToInt32(getUserCookie.Values["schoolDirectorUsername"]);
            int refereeId = 14;
            int refereeSportId = 0;
            string eventDetails = string.Empty;
            string allEvents = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    //fetching sport Id of the referee
                    command.CommandText = "SELECT [Sport_Name_spt_Sport_Name_ID] FROM [dbo].[User_Profile_Referee] " +
                        "WHERE [ref_id] = @refereeId;";

                    command.Parameters.AddWithValue("@refereeId", refereeId);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                refereeSportId = reader.GetInt32(0);
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    refereeSportId = 2;
                }
                using (SqlCommand command = connection.CreateCommand())
                {
                    //fetching upcoming events
                    command.CommandText = "select sportevent.event_ID, homeschool.sch_Name, awayschool.sch_Name, " +
                                        "homeschool.sch_Street, homeschool.sch_City, homeschool.sch_State, " +
                                        "homeschool.sch_Zip, sportevent.event_Date, sportevent.event_Time, " +
                                        "sportevent.event_School_Field_Name, sportname.spt_Name " +
                                        "from [Sport_Event] sportevent " +
                                        "join [School] homeschool on sportevent.[School_Home_sch_ID] = homeschool.[sch_ID] " +
                                        "join [School] awayschool on sportevent.[School_Away_sch_ID] = awayschool.[sch_ID] " +
                                        "join [Sport_Name] sportname on sportevent.[Sport_Name_spt_Sport_Name_ID] = sportname.[spt_Sport_Name_ID] " +
                                        "join [dbo].[Referee_Event_History] refereeHistory on sportevent.event_ID = refereeHistory.[Sport_Event_event_ID] " +
                                        "and refereeHistory.[User_Profile_Referee_Profile_ID] = @refId and refereeHistory.[Referee_Status_refStatus_ID] like @refereeStatus " +
                                        "where [Sport_Name_spt_Sport_Name_ID] = @sportId " +
                                        " AND [event_Date] > @systemDate; ";

                    command.Parameters.AddWithValue("@sportId", refereeSportId);
                    command.Parameters.AddWithValue("@systemDate", DateTime.Now);
                    command.Parameters.AddWithValue("@refId", refereeId);
                    command.Parameters.AddWithValue("@refereeStatus", "A");
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            eventDetails = "<table class='table table-bordered'>";
                            eventDetails += "<thead><tr>" +
                                TableColumnHeader("Sport Name") + TableColumnHeader("Home School Name") +
                                TableColumnHeader("Away School Name") + TableColumnHeader("Field Name") +
                                TableColumnHeader("Home School address") + TableColumnHeader("Event Date") +
                                TableColumnHeader("Event Time") + TableColumnHeader(string.Empty)
                                + "</tr></thead>";

                            while (reader.Read())
                            {

                                eventDetails += "<tbody><tr>" +
                                    TableColumnBody(reader.GetString(10)) +
                                    TableColumnBody(reader.GetString(1)) +
                                    TableColumnBody(reader.GetString(2)) +
                                    TableColumnBody(reader.GetString(9)) +
                                    TableColumnBody(reader.GetString(3) + ", " + reader.GetString(4) + ", " + reader.GetString(5) + ", " + reader.GetString(6)) +
                                    TableColumnBody(reader.GetDateTime(7).ToString()) +
                                    TableColumnBody(reader.GetTimeSpan(8).ToString()) +
                                    "<td><asp:Button onclick='Register_Click' text='Click' runat='server' /></td>";
                                refereeSportId = reader.GetInt32(0);
                            }
                            eventDetails += "</table>";
                            //Label2.Text = eventDetails;
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                //fetching all previous events matching referee's sport type
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "select sportevent.event_ID, homeschool.sch_Name, awayschool.sch_Name, " +
                                        "homeschool.sch_Street, homeschool.sch_City, homeschool.sch_State, " +
                                        "homeschool.sch_Zip, sportevent.event_Date, sportevent.event_Time, " +
                                        "sportevent.event_School_Field_Name, sportname.spt_Name " +
                                        "sportevent.[event_Home_Team_Score], sportevent.[event_Away_Team_Score] " +
                                        "from [Sport_Event] sportevent " +
                                        "join [School] homeschool on sportevent.[School_Home_sch_ID] = homeschool.[sch_ID] " +
                                        "join [School] awayschool on sportevent.[School_Away_sch_ID] = awayschool.[sch_ID] " +
                                        "join [Sport_Name] sportname on sportevent.[Sport_Name_spt_Sport_Name_ID] = sportname.[spt_Sport_Name_ID] " +
                                        "join [dbo].[Referee_Event_History] refereeHistory on sportevent.event_ID = refereeHistory.[Sport_Event_event_ID] " +
                                        "where [Sport_Name_spt_Sport_Name_ID] = @sportId " +
                                        "and refereeHistory.[User_Profile_Referee_Profile_ID] = @refId  AND [event_Date] < @systemDate; ";

                    command.Parameters.AddWithValue("@sportId", refereeSportId);
                    command.Parameters.AddWithValue("@systemDate", DateTime.Now);
                    command.Parameters.AddWithValue("@refId", refereeId);
                    command.Parameters.AddWithValue("@refereeStatus", "A");

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            eventDetails = "<table>";
                            eventDetails += "<thead><tr>" +
                                TableColumnHeader("Sport Name") + TableColumnHeader("Home School Name") +
                                TableColumnHeader("Away School Name") + TableColumnHeader("Field Name") +
                                TableColumnHeader("Home School address") + TableColumnHeader("Event Date") +
                                TableColumnHeader("Event Time") + TableColumnHeader("Home Team Score") + TableColumnHeader("Away Team Score")
                                + "</tr></thead>";

                            while (reader.Read())
                            {
                                eventDetails += "<tbody><tr>" +
                                    TableColumnBody(reader.GetString(10)) +
                                    TableColumnBody(reader.GetString(1)) +
                                    TableColumnBody(reader.GetString(2)) +
                                    TableColumnBody(reader.GetString(9)) +
                                    TableColumnBody(reader.GetString(3) + ", " + reader.GetString(4) + ", " + reader.GetString(5) + ", " + reader.GetString(6)) +
                                    TableColumnBody(reader.GetDateTime(7).ToString()) +
                                    TableColumnBody(reader.GetTimeSpan(8).ToString()) +
                                    TableColumnBody(reader.GetString(10)) +
                                    TableColumnBody(reader.GetInt32(11).ToString()) +
                                    TableColumnBody(reader.GetInt32(12).ToString()) +
                                    "</tr></tbody>";
                                refereeSportId = reader.GetInt32(0);
                            }
                            eventDetails += "</table>";
                            Label1.Text = eventDetails;
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "select sportevent.event_ID, homeschool.sch_Name, awayschool.sch_Name, " +
                                            "homeschool.sch_Street, homeschool.sch_City, homeschool.sch_State, " +
                                            "homeschool.sch_Zip, sportevent.event_Date, sportevent.event_Time,  " +
                                            "sportevent.event_School_Field_Name, sportname.[spt_Name] " +
                                            "from [dbo].[Sport_Event] sportevent " +
                                            "join [dbo].[School] homeschool on sportevent.[School_Home_sch_ID] = homeschool.[sch_ID] " +
                                            "join [dbo].[School] awayschool on sportevent.[School_Away_sch_ID] = awayschool.[sch_ID] " +
                                            "join [dbo].[Sport_Name] sportname on sportevent.[Sport_Name_spt_Sport_Name_ID] = sportname.[spt_Sport_Name_ID] " +
                                            "join [dbo].[Referee_Event_History] refereeHistory on sportevent.event_ID = refereeHistory.[Sport_Event_event_ID] " +
                                            "and refereeHistory.[User_Profile_Referee_Profile_ID] <> @refId " +
                                            "where [Sport_Name_spt_Sport_Name_ID] = @sportId and [event_Date] > @systemDate " +
                                            "group by sportevent.event_ID, homeschool.sch_Name, awayschool.sch_Name, " +
                                            "homeschool.sch_Street, homeschool.sch_City, homeschool.sch_State, " +
                                            "homeschool.sch_Zip, sportevent.event_Date, sportevent.event_Time,  " +
                                            "sportevent.event_School_Field_Name, sportname.[spt_Name]; ";

                    command.Parameters.AddWithValue("@sportId", "2");
                    command.Parameters.AddWithValue("@systemDate", DateTime.Now);
                    command.Parameters.AddWithValue("@refId", "11");

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            eventDetails = "<table>";
                            eventDetails += "<thead><tr>" +
                                TableColumnHeader("Sport Name") + TableColumnHeader("Home School Name") +
                                TableColumnHeader("Away School Name") + TableColumnHeader("Field Name") +
                                TableColumnHeader("Home School address") + TableColumnHeader("Event Date") +
                                TableColumnHeader("Event Time")
                                + "<th></th></tr></thead>";

                            while (reader.Read())
                            {
                                eventDetails += "<tbody><tr>" +
                                    TableColumnBody(reader.GetString(10)) +
                                    TableColumnBody(reader.GetString(1)) +
                                    TableColumnBody(reader.GetString(2)) +
                                    TableColumnBody(reader.GetString(9)) +
                                    TableColumnBody(reader.GetString(3) + ", " + reader.GetString(4) + ", " + reader.GetString(5) + ", " + reader.GetString(6)) +
                                    TableColumnBody(reader.GetDateTime(7).ToString()) +
                                    TableColumnBody(reader.GetTimeSpan(8).ToString()) +
                                    TableColumnBody(reader.GetString(10)) +
                                    "<td>Register Button</td>" +
                                    "</tr></tbody>";
                            }
                            eventDetails += "</table>";
                            //Label3.Text = eventDetails;
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


        public string TableColumnHeader(string columnName)
        {
            columnName = "<th>" + columnName + "</th>";
            return columnName;
        }

        public string TableColumnBody(string cellContent)
        {
            cellContent = "<th>" + cellContent + "</th>";
            return cellContent;
        }

        protected void Register_Click(object sender, EventArgs e)
        {
            int refereeId = 8;

            //fetch this values from form submission
            //int eventId = Convert.ToInt32(Request.QueryString["eventId"]);
            //int refereeSportType = Convert.ToInt32(Request.QueryString["sportTypeId"]);

            //demo values
            int eventId = 3;
            int refereeSportType = 5;
            int schoolDirectorId = 0;

            string eventDetails = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    //fetching home team director id from event id
                    command.CommandText = "SELECT SCHOOL.User_Profile_Director_Profile_ID FROM [dbo].[Sport_Event] SPORTEVENT " +
                                            "JOIN [dbo].[School] SCHOOL ON SPORTEVENT.School_Home_sch_ID = SCHOOL.sch_ID " +
                                            "WHERE event_ID = @eventId";

                    command.Parameters.AddWithValue("@eventId", eventId);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                schoolDirectorId = reader.GetInt32(0);
                                reader.Close();
                            }

                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                using (SqlCommand command = connection.CreateCommand())
                {
                    //fetching sport Id of the referee
                    command.CommandText = "insert into REFEREE_EVENT_HISTORY " +
                        " (Sport_Event_event_ID, User_Profile_Referee_Profile_ID, [User_Profile_School_Director_Profile_ID], Referee_Status_refStatus_ID, Sport_Type_Referees_sptTypeRef_ID) " +
                        "values (@eventId, @refereeId, @directorId, @refereeStatus, @sportRefereeType);";

                    command.Parameters.AddWithValue("@eventId", eventId);
                    command.Parameters.AddWithValue("@refereeId", refereeId);
                    command.Parameters.AddWithValue("@directorId", schoolDirectorId);
                    command.Parameters.AddWithValue("@refereeStatus", "P");
                    command.Parameters.AddWithValue("@sportRefereeType", refereeSportType);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }
    }
}