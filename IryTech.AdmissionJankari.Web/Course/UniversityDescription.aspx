<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UniversityDescription.aspx.cs" Inherits="IryTech.AdmissionJankari.Web.Course.UniversityDescription" %>
<%@ Register Src="~/UserControl/UniversityBasicDetails.ascx" TagPrefix="AJ" TagName="UniversityBasicDetails" %>
<%@ Register Src="~/UserControl/UniversityContactDetails.ascx" TagPrefix="AJ" TagName="UniversityContactDetails" %>
<%@ Register Src="~/UserControl/UniversityDescription.ascx" TagPrefix="AJ" TagName="UniversityDescription" %>
<%@ Register Src="~/UserControl/UcCollegeListRelatedUniversity.ascx" TagPrefix="AJ" TagName="UniversityRealtedCollege" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">

<div class="five_sixth fleft last">

 <div class="boxPlane bgYellow marginbottom">
            <ul class="vertical">
                <li class="Imgarrow marginRight">
                    <asp:Image runat="server" ID="imgExam" Width="100px" Height="100px"></asp:Image></li>
                <li>
                    <h1>
                        <asp:Label runat="server" ID="lblHeader"></asp:Label></h1>
                    <h2 style="font-size: 20px !important;">
                        Admission Helpline :<asp:Label ID="txtHelpLineNo" Style="color: Maroon;" runat="server"></asp:Label></h2>
                </li>
            </ul>
            <div class="clearBoth"></div>
        </div>

  <div class="pageTargetMenu">
    <ul class="vertical">
    <li>
    <a href="#Overview" title="Overview">Overview</a>
    </li>
    <li>
    <a href="#pnlDescrip" title="Description">Description</a>
    </li>
    <li>
    <a href="#pnlContactDetails" title="ContactDetails">Contact Details</a>
    </li>
    </ul>
    <div class="clearBoth"></div>
  </div>
  
  <div class="four_fifth last fleft">
  <div id="Overview">
  <AJ:UniversityBasicDetails runat="server" ID="usUniversityBasicDetails"/>
  </div>
  <div id="pnlDescrip">
  <AJ:UniversityDescription runat="server" ID="usUniversityDescription"/>
  </div>
  <div id="pnlContactDetails">
  <AJ:UniversityContactDetails runat="server" ID="usUniversityContactDetails"/>
  </div>
  </div>
  <div class="one_third fright last" >
   <AJ:UniversityRealtedCollege runat="server" ID="ucUniversityRealtedCollege"/>
  </div>
</div>



</asp:Content>