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
        string eventTime;
        string eventDate;
        string SchoolFieldName;
        int eventHomeSchoolID;
        int eventAwaySchoolID;
        int sportTypeID;
        int sportEventID;
        string homeSchoolName;
        string awaySchoolName;
        int schoolDirectorProfileID;
        string selectedRefereePosition;
        int selectedRefereePositionID;

        protected void Page_Load(object sender, EventArgs e)
        {
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

              SqlDataSource1.SelectCommand = "SELECT SE.event_Date, SE.event_Time, SE.event_Home_Team_Score, SE.event_Away_Team_Score, SE.event_School_Field_Name FROM SPORT_EVENT SE JOIN Referee_Event_History REH ON SE.event_ID=REH.Sport_Event_event_ID WHERE REH.User_Profile_Referee_Profile_ID= " + refereeUserProfileID + " AND SE.event_Date<=SYSDATETIME()";
              SqlDataSource2.SelectCommand = "SELECT SE.event_Date, SE.event_Time, SE.event_Home_Team_Score, SE.event_Away_Team_Score, SE.event_School_Field_Name FROM SPORT_EVENT SE JOIN Referee_Event_History REH ON SE.event_ID=REH.Sport_Event_event_ID WHERE REH.User_Profile_Referee_Profile_ID= " + refereeUserProfileID + " AND SE.event_Date>=SYSDATETIME()";

              if (!Page.IsPostBack) {

                  string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringLocalDB"].ConnectionString;
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
                              if (reader.HasRows) {
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
                          command.CommandText =
                              " SELECT TOP 1 SE.event_Date, SE.event_Time, SE.event_School_Field_Name, SE.School_Home_sch_ID, "
                                    + " SE.School_Away_sch_ID, SE.Sport_Name_spt_Sport_Name_ID FROM Sport_Event SE ,Referee_Event_History REH WHERE "
                                    + " REH.User_Profile_Referee_Profile_ID!=@User_Profile_Referee_Profile AND SE.Sport_Name_spt_Sport_Name_ID=1 "
                                    + " AND (Referee_Status_refStatus_ID!='A' OR Referee_Status_refStatus_ID!='D' OR "
                                    + " Referee_Status_refStatus_ID!='P') AND SE.event_Date>=SYSDATETIME() "
                                    + " AND SE.event_ID!=REH.Sport_Event_event_ID";
                          command.Parameters.AddWithValue("@User_Profile_Referee_Profile", refereeUserProfileID);
                          try
                          {
                              connection.Open();
                              SqlDataReader reader = command.ExecuteReader();
                              if (reader.HasRows)
                              {
                                  while (reader.Read())
                                  {
                                      eventDate = (reader.GetDateTime(0)).ToString();
                                      eventTime = (reader.GetTimeSpan(1)).ToString();
                                      SchoolFieldName=reader.GetString(2);
                                      eventHomeSchoolID=reader.GetInt32(3);
                                      eventAwaySchoolID= reader.GetInt32(4);
                                      sportTypeID = reader.GetInt32(5);
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
                              if (reader.HasRows) {
                                  while (reader.Read())
                                  {
                                      homeSchoolName = reader.GetString(0);
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
                                      awaySchoolName = reader.GetString(0);
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
                          command.Parameters.AddWithValue("@Sport_Name_spt_Sport_Name_ID", sportTypeID);
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



                  EventDateLabelID.Text = eventDate;
                  EventTimeLabelID.Text = eventTime;
                  EventLocationID.Text = SchoolFieldName;
                  homeSchoolNameLabelID.Text = homeSchoolName;
                  awaySchoolNameLabelID.Text = awaySchoolName;

              }

        }


        protected void registerEvent_Click(object sender, EventArgs e) {

            selectedRefereePosition = GamePositionTypeSelectionValue.SelectedItem.Text;
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringLocalDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                              " SELECT TOP 1 SE.event_ID, SE.School_Home_sch_ID "
                                    + " FROM Sport_Event SE ,Referee_Event_History REH WHERE "
                                    + " REH.User_Profile_Referee_Profile_ID!=@User_Profile_Referee_Profile AND SE.Sport_Name_spt_Sport_Name_ID=1 "
                                    + " AND (Referee_Status_refStatus_ID!='A' OR Referee_Status_refStatus_ID!='D' OR "
                                    + " Referee_Status_refStatus_ID!='P') AND SE.event_Date>=SYSDATETIME() "
                                    + " AND SE.event_ID!=REH.Sport_Event_event_ID";
                    command.Parameters.AddWithValue("@User_Profile_Referee_Profile", refereeUserProfileID);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                sportEventID = reader.GetInt32(0);
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
                                schoolDirectorProfileID = reader.GetInt32(0);
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
                                schoolDirectorProfileID = reader.GetInt32(0);
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

                    command.Parameters.AddWithValue("@Sport_Event_event_ID_Final", sportEventID);
                    command.Parameters.AddWithValue("@User_Profile_Referee_Profile_ID_Final", refereeUserProfileID);
                    command.Parameters.AddWithValue("@User_Profile_School_Director_Profile_ID_Final", schoolDirectorProfileID);
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
                    command.CommandText =
                        " SELECT TOP 1 SE.event_Date, SE.event_Time, SE.event_School_Field_Name, SE.School_Home_sch_ID, "
                              + " SE.School_Away_sch_ID, SE.Sport_Name_spt_Sport_Name_ID FROM Sport_Event SE ,Referee_Event_History REH WHERE "
                              + " REH.User_Profile_Referee_Profile_ID!=@User_Profile_Referee_Profile AND SE.Sport_Name_spt_Sport_Name_ID=1 "
                              + " AND (Referee_Status_refStatus_ID!='A' OR Referee_Status_refStatus_ID!='D' OR "
                              + " Referee_Status_refStatus_ID!='P') AND SE.event_Date>=SYSDATETIME() "
                              + " AND SE.event_ID!=REH.Sport_Event_event_ID";
                    command.Parameters.AddWithValue("@User_Profile_Referee_Profile", refereeUserProfileID);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                eventDate = (reader.GetDateTime(0)).ToString();
                                eventTime = (reader.GetTimeSpan(1)).ToString();
                                SchoolFieldName = reader.GetString(2);
                                eventHomeSchoolID = reader.GetInt32(3);
                                eventAwaySchoolID = reader.GetInt32(4);
                                sportTypeID = reader.GetInt32(5);
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
                                homeSchoolName = reader.GetString(0);
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
                                awaySchoolName = reader.GetString(0);
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
                    command.Parameters.AddWithValue("@Sport_Name_spt_Sport_Name_ID", sportTypeID);
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



            EventDateLabelID.Text = eventDate;
            EventTimeLabelID.Text = eventTime;
            EventLocationID.Text = SchoolFieldName;
            homeSchoolNameLabelID.Text = homeSchoolName;
            awaySchoolNameLabelID.Text = awaySchoolName;

            
        }
    }
}