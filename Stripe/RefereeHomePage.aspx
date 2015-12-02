<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RefereeHomePage.aspx.cs" Inherits="Stripe.RefereeHomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Referee Home Page</title>
    <link href="./bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="./bootstrap/customBootstrapJS/jquery-latest.js"></script>
    <script src="./bootstrap/js/bootstrap.js"></script>

    <link href="./bootstrap/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="./bootstrap/css/bootstrap.css" rel="stylesheet" />

    <link href="./bootstrap/customBootstrapCSS/prettify.css" rel="stylesheet" />
    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment-with-locales.js"></script>
    <link href="Content/Custom_StyleSheet/RefereeHomePage.css" rel="stylesheet" />


</head>
<body>
    <form id="form1" runat="server">
        <div class="container container-fluid">
            <div class="row" id="headerImage">
                <%--image div starts--%>
                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3"></div>
                <div class="col-xs-4 col-sm-4 col-md-6 col-lg-6">
                    <img src="Images/MainLogo.png" />
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



            <div class="row">

                <div class="col-xs-1  col-sm-0 col-md-1 col-lg-1"></div>

                <div class="col-xs-5  col-sm-5 col-md-5 col-lg-5">
                    <%--personal information panel starts --%>
                    <div class="panel" id="personalInformationMainPanelID">
                        <div class="panel panel-default" id="personalInformationSecPanelID">
                            <div class="panel-heading" id="personalInformationHeadingID">
                                <center><b>Personal Information</b></center>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <img class="userProfilePicture" src="Images/stormTrooper.jpg" />
                                    </div>
                                    <div class="col-md-6">
                                        <h2 class="googleFont">
                                            <asp:Label ID="UserProfileNameID" runat="server" Text="User"></asp:Label>'s Profile</h2>
                                    </div>
                                </div>
                                <table class="table table-hover">
                                    <%--insert image code here --%>

                                    <%--insert image code here --%>


                                    <tbody>
                                        <tr>
                                            <td class="text-center">First Name</td>
                                            <td class="text-center">
                                                <asp:Label ID="firstNameLabel" runat="server" Text="First Name"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="text-center">Last Name</td>
                                            <td class="text-center">
                                                <asp:Label ID="lastNameLabel" runat="server" Text="Last Name"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="text-center">Email</td>
                                            <td class="text-center">
                                                <asp:Label ID="emailLabel" runat="server" Text="Email"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="text-center">Phone Number</td>
                                            <td class="text-center">
                                                <asp:Label ID="phoneNumberLabel" runat="server" Text="Phone Number"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="text-center">Street Address</td>
                                            <td class="text-center">
                                                <asp:Label ID="streetAddressLabel" runat="server" Text="Street Address"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="text-center">City</td>
                                            <td class="text-center">
                                                <asp:Label ID="cityLabel" runat="server" Text="City"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="text-center">State</td>
                                            <td class="text-center">
                                                <asp:Label ID="stateLabel" runat="server" Text="State"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="text-center">Zip</td>
                                            <td class="text-center">
                                                <asp:Label ID="zipLabel" runat="server" Text="Zip"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <center>
                                
                                <asp:LinkButton ID="updateUserProfileeID" runat="server"
                                                                class="btn btn-success btn-lg" OnClick="updateInformationButton_Click">
                                                        <i aria-hidden="true" class="glyphicon glyphicon-floppy-disk"></i>Update Profile

                                </asp:LinkButton>

                                </center>
                            </div>
                        </div>
                    </div>
                </div>
                <%--personal information panel ends --%>


                <div class="col-xs-5  col-sm-5 col-md-5 col-lg-5">
                    <%--game information panel starts --%>
                    <div class="panel" id="gameInformationMainPanelID">
                        <div class="panel panel-default" id="gameInformationSecPanelID">
                            <div class="panel-heading" id="gameInformationHeadingID">
                                <center><b>Game Information</b></center>
                            </div>
                            <div class="panel-body">

                                <div class="panel" style="text-align: center; margin-top: 2em; background-color: white">
                                    <span class="rating-star">
                                        <span class="rating-item">5</span>
                                        <span class="rating-item">4</span>
                                        <span class="rating-item">3</span>
                                        <span class="rating-item">2</span>
                                        <span class="rating-item">1</span>

                                        <span class="rating-item result" id="refereeRatingStarsID" style="width: 20%" runat="server"></span>
                                    </span>
                                    <h2 class="googleFont" style="color: black">Current Rating:
                                        <asp:Label ID="refereeRatingValueID" runat="server" Text="1"></asp:Label>/5</h2>
                                </div>

                                <table class="table table-hover">
                                    <%--insert image code here --%>

                                    <%--insert image code here --%>

                                    <tbody>
                                        <tr>
                                            <td class="text-center">Total Number of Ratings</td>
                                            <td class="text-center">
                                                <asp:Label ID="totalNumberOfRatingsID" runat="server" Text="Total Ratings"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="text-center">Total Games Officiated</td>
                                            <td class="text-center">
                                                <asp:Label ID="totalNumberOfGamesID" runat="server" Text="Games Officiated"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="text-center">Game Specalization Type</td>
                                            <td class="text-center">
                                                <asp:Label ID="gameSpecializationTypeID" runat="server" Text="Game Specialization"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="text-center">Background Description</td>
                                            <td class="text-center"></td>
                                        </tr>

                                    </tbody>
                                </table>
                                <div class="well">
                                    <div class="tab-content">
                                        <div class="tab-pane active">

                                            <asp:Label ID="backgroundDescriptionID" runat="server" Text="Background Description"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <%--game information panel ends --%>

                <div class="col-xs-1  col-sm-0 col-md-1 col-lg-1"></div>
            </div>
            <%--personal information panel ends --%>
        </div>



    </form>
</body>
</html>
