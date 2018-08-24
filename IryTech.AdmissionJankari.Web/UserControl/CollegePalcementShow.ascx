<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegePalcementShow.ascx.cs" Inherits="IryTech.AdmissionJankari.Web.UserControl.CollegePalcementShow" %>
<div class="box1" runat="server" id="divPlcement" visible="false">
    <h3 class="streamCompareH3">Placement</h3>
    <hr class="hrline" />
    <div class="box">
        <br />
        <label>Choose Year:</label>
        <asp:DropDownList ID="ddlPlacementYear" Width="20%" runat="server" AutoPostBack="true"
            Font-Bold="true"
            OnSelectedIndexChanged="ddlPlacementYear_SelectedIndexChanged">
        </asp:DropDownList>

        <aspChart:Chart ID="rankChart" ImageStorageMode="UseImageLocation" runat="server" SuppressExceptions="True"
            ImageLocation="~/Image/tempImage/ChartPic_#SEQ(1000,30)" Palette="None" Width="500px" Height="400px" BorderlineColor="White">
            <Series>
                <aspChart:Series Name="rankSeries" ChartType="Column" Font="Arial, 12pt"
                    CustomProperties="DrawingStyle=Cylinder"
                    IsValueShownAsLabel="True" BackSecondaryColor="Transparent" Color="teal" XAxisType="Primary">
                </aspChart:Series>
            </Series>

            <ChartAreas>
                <aspChart:ChartArea Name="ChartArea2" IsSameFontSizeForAllAxes="true" BackColor="#eff2f7">
                    <Area3DStyle Enable3D="true" LightStyle="Realistic"></Area3DStyle>
                    <AxisX Title="Source Name" TitleForeColor="#415983" TitleFont="Arial, 12pt, style=Bold">
                        <MajorGrid Enabled="false" />
                    </AxisX>
                    <AxisY Interval="30" Title="Overall Rank" TitleForeColor="Maroon" TitleFont="Arial, 12pt, style=Bold">
                        <MajorGrid Enabled="false" />
                    </AxisY>
                    <InnerPlotPosition Auto="true" />
                </aspChart:ChartArea>
            </ChartAreas>
            <Titles>
                <aspChart:Title BackColor="Transparent"
                    Font="Microsoft Sans Serif, 16pt, style=Bold" ForeColor="ForestGreen"
                    Name="Title1">
                </aspChart:Title>
            </Titles>
        </aspChart:Chart>
        <asp:Repeater ID="rptPalcement" runat="server">
            <ItemTemplate>
                <div class="accordion">

                    <h3 class="accord">Placement by-<%#Eval("CollegeBranchCoursePlacementCompanyName")%>  <span class="fright" style="display: none" id="spnPlacement">Show</span> </h3>
                    <p>
                        <span><strong>No. of student hired:</strong>&nbsp;<%#Eval("CollegeBranchCoursePlacementNoOfStudentHired")%></span><br />
                        <span><strong>Avgerage salary offered:</strong>&nbsp;<%#Eval("CollegeBranchCoursePlacementAvgSalaryOffered")%></span>
                    </p>

                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>

<script type="text/javascript" defer="defer">
    $(document).ready(function () {
        $(".accordion h3:first").addClass("active");
        $(".accordion p:not(:first)").hide();
        $(".accordion h3").click(function () {
            $(this).next("p").slideToggle("slow")
                .siblings("p:visible").slideUp("slow");
            $(this).toggleClass("active");
            $(this).siblings("h3").removeClass("active");
            $(this).find("#spnPlacement").html("");
            $(this).find("#spnPlacement").html("Hide");
        });

        $(".accordion h3").hover(function () {
            $(this).find("#spnPlacement").html("");
            $(this).find("#spnPlacement").html("Show");
            $(this).find("#spnPlacement").show();
        });
        $(".accordion h3").mouseout(function () {

            $(this).find("#spnPlacement").hide();
        });
    });

</script>
