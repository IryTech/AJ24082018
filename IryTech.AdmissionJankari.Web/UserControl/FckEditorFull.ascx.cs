﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IryTech.AdmissionJankari.Web.UserControl
{
    public partial class FckEditorFull : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string FckValue
        {
            get { return txtFckEditorFull.Text; }
            set { txtFckEditorFull.Text = value; }
        }
    }
}