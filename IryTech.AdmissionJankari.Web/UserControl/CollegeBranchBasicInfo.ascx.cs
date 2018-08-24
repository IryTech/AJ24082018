﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari.Web.UserControl
{
    public partial class CollegeBranchBasicInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindManagement();
        }
        private void  BindManagement()
        {
            var dv = ClsSingelton.GetManagement();
            rbtManagement.DataSource = dv;
            rbtManagement.DataTextField = "AjMasterValues";
            rbtManagement.DataValueField = "AjMasterValueId";
            rbtManagement.DataBind();
         
        }
    }
}