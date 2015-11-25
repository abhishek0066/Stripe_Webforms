<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RefereeEventsPage.aspx.cs" Inherits="Stripe.RefereeEventsPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Referee Event Page</title>
    <link href="./bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="./bootstrap/customBootstrapJS/jquery-latest.js"></script>
    <script src="./bootstrap/js/bootstrap.js"></script>

    <link href="./bootstrap/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="./bootstrap/css/bootstrap.css" rel="stylesheet" />

    <link href="./bootstrap/customBootstrapCSS/prettify.css" rel="stylesheet" />
    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment-with-locales.js"></script>

    <link href="Content/Custom_StyleSheet/RefereeEventPage.css" rel="stylesheet" />
    <script src="Content/Custom_Javascript/jquery-2.1.4.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container container-fluid">
            <div class="row" id="headerImage">
                <%--image div starts--%>
                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3"></div>
                <div class="col-xs-4 col-sm-4 col-md-6 col-lg-6">
                    <img src="Images/logo.png" />
                </div>
                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3"></div>
            </div>
            <%--image div ends--%>
            <div class="row">
                <%--header div starts--%>
                <div class="col-xs-1  col-sm-0 col-md-1 col-lg-1"></div>

                <div id="header" class="col-xs-7 col-sm-12 col-md-12 col-lg-12">
                    <%--"nav nav-tabs nav-justified"--%>
                    <nav class="navbar navbar-default">
                        <div class="container-fluid">
                            <%--container1 begins--%>
                            <div class="navbar-header">
                                <%--header begins--%>
                            </div>
                            <%--header ends--%>
                            <div class="collapse navbar-collapse">
                                <%--collapse header content begins--%>
                                <ul class="nav nav-tabs nav-justified">
                                    <%--<ul class="nav navbar-nav">--%>
                                    <li class="active">
                                        <asp:HyperLink NavigateUrl="RefereeHomePage.aspx" ID="refereeProfileHomeID" runat="server"><span class="glyphicon glyphicon-home" aria-hidden="true"></span><b> Home</b><span class="sr-only"></span></asp:HyperLink>

                                    </li>
                                    <li>
                                        <asp:HyperLink NavigateUrl=" " ID="eventsID" runat="server"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span><b> Events</b><span class="sr-only"></span></asp:HyperLink>


                                    </li>
                                    <li>
                                        <asp:HyperLink NavigateUrl=" " ID="searchID" runat="server"><span class="glyphicon glyphicon-search" aria-hidden="true"></span><b> Search</b><span class="sr-only"></span></asp:HyperLink>


                                    </li>
                                    <li>
                                        <asp:LinkButton ID="logoutout" runat="server"><span class="glyphicon glyphicon-off" aria-hidden="true"></span><b> Logout</b><span class="sr-only"></span></asp:LinkButton>

                                    </li>
                                </ul>

                            </div>
                            <%--collapse header ends--%>
                        </div>
                        <%--container1 ends --%>
                    </nav>
                </div>
                <div class="col-xs-0 col-sm-0 col-md-1 col-lg-1"></div>
            </div>
            <%--header div ends--%>

            <%--body section starts--%>
            <div class="row">
           
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            
            <div id="mainSection">
                
                <input type="radio" id="settings" value="1" name="tractor" checked='checked'/>
                <input type="radio" id="posts" value="2" name="tractor" />
                

                <div class="navSection">
                    <label for="settings" class='fontawesome-cogs'>Upcoming Events</label>
                    <label for="posts" class='fontawesome-folder-open'>Previous Events</label>
                    
                </div>

                <div class='uno fontawesome-umbrella article'>
               
                    <div class="col-xs-0 col-sm-0 col-md-4 col-lg-4">
                        <div class="panel">
                            <table class="table table-hover">
                                <tbody>
                                    <tr class=" addcolor1">
                                        <td class="text-center">
                                            <asp:Label ID="Label2" runat="server" Text="HOME"></asp:Label>
                                        </td>

                                        <td></td>
                                        <td class="text-center">
                                            <asp:Label ID="Label1" runat="server" Text="AWAY"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class=" addcolor3">
                                        <td class="text-center">
                                            <img src="Images/stormTrooper.jpg" id="homeSchoolImageID" />
                                        </td>

                                        <td>
                                            <h2 class="googleFont" style="color: black">VS</h2>
                                        </td>
                                        <td class="text-center">
                                            <img src="Images/stormTrooper.jpg" id="awaySchoolImageID" />
                                        </td>
                                    </tr>
                                    <tr class=" addcolor2">
                                        <td class="text-center">
                                            <asp:Label ID="homeSchoolNameID" runat="server" Text="Home School Name"></asp:Label>
                                        </td>

                                        <td></td>
                                        <td class="text-center">
                                            <asp:Label ID="awaySchoolNameID" runat="server" Text="Away School Name"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="col-xs-0 col-sm-0 col-md-2 col-lg-2">
                        <div class="panel">
                            <table class="table table-hover">
                                <tbody>
                                    <tr class=" addcolor1">
                                        <td class="text-center">
                                            <asp:Label ID="eventDateLabelID" runat="server" Text="Date"></asp:Label>
                                        </td>

                                        
                                        <td class="text-center">
                                            <asp:Label ID="eventTimeLabelID" runat="server" Text="Time"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class=" addcolor3">
                                        <td class="text-center">
                                            <asp:Label ID="sportEventTypeID" runat="server" Text="Sport Type"></asp:Label>
                                        </td>

                                        <td class="text-center">
                                            <img src="Images/stormTrooper.jpg" id="sportTypeImageID" />
                                        </td>
                                        
                                    </tr>
                                    <tr class=" addcolor2">
                                        <td class="text-center">
                                            <asp:Label ID="gameLocationID" runat="server" Text="Location"></asp:Label>
                                        </td>

                                        
                                        <td class="text-center">
                                            <asp:Label ID="gameLocationFieldNameID" runat="server" Text="Field Name"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="col-xs-0 col-sm-0 col-md-4 col-lg-4">
                        <div class="panel">
                        <table class="table table-hover">
                                <tbody>
                                    <tr class=" addcolor1">
                                        <td class="text-center">
                                            <asp:Label ID="refereeType1ID" runat="server" Text="Referee Type 1"></asp:Label>
                                        </td>

                                        <td class="text-center">
                                            <asp:Label ID="refereeType2ID" runat="server" Text="Referee Type 2"></asp:Label>
                                        </td>
                                        <td class="text-center">
                                            <asp:Label ID="refereeType3ID" runat="server" Text="Referee Type 3"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class=" addcolor3">
                                        <td class="text-center">
                                            <img src="Images/stormTrooper.jpg" id="refereeType1ImageID" />
                                        </td>

                                        <td class="text-center">
                                            <img src="Images/stormTrooper.jpg" id="refereeType2ImageID" />
                                        </td>
                                        <td class="text-center">
                                            <img src="Images/stormTrooper.jpg" id="refereeType3ImageID" />
                                        </td>
                                    </tr>
                                    <tr class=" addcolor2">
                                        <td class="text-center">
                                            <asp:Label ID="refereeName1ID" runat="server" Text="Referee Name 1"></asp:Label>
                                        </td>

                                        <td class="text-center">
                                            <asp:Label ID="refereeName2ID" runat="server" Text="Referee Name 2"></asp:Label>
                                        </td>
                                        <td class="text-center">
                                            <asp:Label ID="refereeName3ID" runat="server" Text="Referee Name 3"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            </div>
                    </div>
                  
                </div>
                    </div>

                </div>
            </div>
            
            
            
            </div>
            <%--body sections ends--%>
        
    </form>

</body>
</html>
