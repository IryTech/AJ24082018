﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IryTech.AdmissionJankari.Web.UserControl
{
    public partial class CourseStreamFuture : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string Future
        {
            set
            {
                future.InnerHtml = value;
            }
        }
    }
}