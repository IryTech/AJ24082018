<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ColleegRankChart.ascx.cs" Inherits="IryTech.AdmissionJankari.Web.UserControl.ColleegRankChart" %>


<div id="CollegeRank" runat="server" class="box1">
    <h3 class="streamCompareH3">College Rank</h3>


    <label>Year:</label>
    <asp:DropDownList ID="ddlYear" Width="20%" runat="server" AutoPostBack="true"
        Font-Bold="true"
        OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
    </asp:DropDownList>

    <div class="boxPlane" runat="server">
        <div style="margin-left: auto; margin-right: auto; text-align: center">
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
        </div>
    </div>


</div>

