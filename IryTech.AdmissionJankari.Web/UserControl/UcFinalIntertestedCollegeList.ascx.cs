using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari.Web.UserControl
{
    public partial class UcFinalIntertestedCollegeList : System.Web.UI.UserControl
    {
        public string SPonserValue;
        SecurePage objSecurePage = new SecurePage();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindUserIntertestedList(objSecurePage.LoggedInUserId);
        }


        // Method To Bind User Final Intertested List
        protected void BindUserIntertestedList(int userId)
        {
            Consulling ObjConsulling = new Consulling();
            DataSet ds = new DataSet();
            try
            {
               
                ds = ObjConsulling.GetIntertestedCollege(userId);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        hndCollegeList.Value = ds.Tables[0].Rows.Count.ToString();
                        rptCollegeDetails.DataSource = ds.Tables[0];
                        rptCollegeDetails.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindUserIntertestedList in UcFinalIntertestedCollegeList.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        public string CollegeCourseId
        {
            get; set; }
        protected void CollegeDetailsItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Consulling ObjConsulling = new Consulling();
            if (e.CommandName == "delete")
            {
                string errMsg = "";
                int i = ObjConsulling.DeleteIntertestedCollege(Convert.ToInt32(e.CommandArgument), out errMsg);
                if (i > 0)
                {
                    lblSucess.Visible = true;
                    lblSucess.Text = errMsg;
                    lblSucess.Focus();
                    BindUserIntertestedList(7);
                }
            }
        }
        public string Sponser
        {
            get
            {
                return SPonserValue;

            }
            set { SPonserValue = value; }
        }

        //protected void rptCollegeDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    var hndCollegeBranchCourseId = (HiddenField)(e.Item.FindControl("hndCollegeBranchCourseId"));
        //    var rptStream =(Repeater)( e.Item.FindControl("rptCourse"));
        //    if (rptStream != null)
        //    {
        //        var streamData = CollegeProvider.Instance.GetCollegeCourseStreamDetailsByBranchCourseId(Convert.ToInt32(hndCollegeBranchCourseId.Value)); 
        //        if (streamData.Count > 0)
        //        {
        //            rptStream.DataSource = streamData;
        //            rptStream.DataBind();
        //        }
        //    }
        //}
    }
}