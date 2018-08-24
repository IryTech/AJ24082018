﻿using System;
using System.Linq;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegrationKit;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari.Web.UserControl
{
    public partial class ProductPayment : System.Web.UI.UserControl
    {
        Common _objCommon;
        Consulling _objConsulling;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            btnFinish.ValidationGroup = "VldgOnlinePayment";
            ValidateControl();
            BindUserDetailsForOnlinePayment();
        }
        public string RedirectUrl { get; set; }
      

        // Method to Validate The Control
        private void ValidateControl()
        {
            revAddress.ValidationExpression = ClsSingelton.aRegExpAlphaNumSpaceStrict;
            revPincode.ValidationExpression = ClsSingelton.aRegExpZip;
        }



        private void OnlinePayment(string orderNumber)
        {
            var objSecurePage = new SecurePage();
            _objCommon = new Common();

            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            var objMailTemplates = new MailTemplates();


            string transactionDetails = "You have selected the payment mode through Online Payment of" + " " + "Rs." + " " + hdnAmount.Value + "/- ";
            var userDetails = UserManagerProvider.Instance.GetUserListById(objSecurePage.LoggedInUserId);

            var objCarProduct = new Common().GetProductForCart(0, Request["id"] == null ? orderNumber : null,
                                                                  Request["id"] != null
                                                                      ? Convert.ToInt32(Request["id"])
                                                                      : 0);
            var mail = new MailMessage
            {
                From = new MailAddress(ApplicationSettings.Instance.Email),
                Subject = "Product Order Number:" + orderNumber
            };
            var body = objMailTemplates.SendProductConfirmationMail(objSecurePage.LoggedInUserName,
                                                                                orderNumber, transactionDetails, objCarProduct.Tables[0]);

            mail.Body = body;
            mail.To.Add(objSecurePage.LoggedInUserEmailId);
            mail.Bcc.Add(ClsSingelton.bccDirectAdmission);
            Utils.SendMailMessageAsync(mail);
            var sp = userDetails.First();
            var objUserRegistrationProperty = new
                  UserRegistrationProperty
            {
                UserFullName = objSecurePage.LoggedInUserName,
                UserGender = sp.UserGender,
                UserEmailid = objSecurePage.LoggedInUserEmailId,
                MobileNo = objSecurePage.LoggedInUserMobile,
                PhoneNo = sp.PhoneNo,
                UserId = objSecurePage.LoggedInUserId,
                CourseId = sp.CourseId,
                UserCategoryId = objSecurePage.LoggedInUserType,
                UserDOB = sp.UserDOB,
                UserStatus = true,
                UserPassword = sp.UserPassword,
                UserPincode = txtPincode.Text.Trim(),
                UserCorrespondenceAddress = txtAddress.Text.Trim()
            };
            var errMsg = "";
            var i = UserManagerProvider.Instance.UpdateUserInfo(objUserRegistrationProperty, 1, out errMsg);
            var amount =hdnAmount.Value;
            var myUtility = new libfuncs();
            Merchant_Id.Value = "M_shi18022_18022";
            Amount.Value = amount;
            Order_Id.Value = orderNumber + DateTime.Now.ToString("hh:mm:ss");
            Redirect_Url.Value = Request["id"] != null
                                     ? Utils.AbsoluteWebRoot + "account/paymentconfirmation.aspx?CID=" +
                                       objCrypto.Encrypt(objSecurePage.LoggedInUserEmailId) + "&orderNumber=" +
                                       objCrypto.Encrypt(orderNumber) + "&UID=" +
                                       objCrypto.Encrypt(objSecurePage.LoggedInUserId.ToString() + "&Amount=" + amount +
                                                         "&id=" + Request["id"])
                                     : Utils.AbsoluteWebRoot + "account/paymentconfirmation.aspx?CID=" +
                                       objCrypto.Encrypt(objSecurePage.LoggedInUserEmailId) + "&orderNumber=" +
                                       objCrypto.Encrypt(orderNumber) + "&UID=" +
                                       objCrypto.Encrypt(objSecurePage.LoggedInUserId.ToString());
            var workingKey = ClsSingelton.WorkingKey.Trim();
            Checksum.Value = myUtility.getchecksum(Merchant_Id.Value, Order_Id.Value, Amount.Value, Redirect_Url.Value, workingKey);
            billing_cust_name.Value = objSecurePage.LoggedInUserName;
            billing_cust_address.Value = txtAddress.Text.Trim();
            billing_cust_state.Value = txtState.Text;
            billing_cust_country.Value = "India";
            billing_cust_tel.Value = objSecurePage.LoggedInUserMobile;
            billing_cust_email.Value = objSecurePage.LoggedInUserEmailId;
            delivery_cust_name.Value = "";
            delivery_cust_address.Value = "";
            delivery_cust_state.Value = "";
            delivery_cust_country.Value = "";
            delivery_cust_tel.Value = "";
            billing_cust_city.Value = txtCity.Text;
            billing_zip_code.Value = txtPincode.Text.Trim();
            delivery_cust_city.Value = "";
            delivery_zip_code.Value = "";
            _objConsulling = new Consulling();
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "YourUniqueScriptKey",
                                              "PostDForm();", true);

        }

        public RadioButtonList GetPaymentMode
        {
            get { return rbtnPaymentType; }
        }

      
        // Method to Bind The User Details if User Want to make the payment
        protected void GetUserDetails(string courseId, string userId)
        {
            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            _objCommon = new Common();
            var objSecurePage = new SecurePage();
            try
            {
                courseId = objCrypto.Decrypt(courseId);
                userId = objCrypto.Decrypt(userId);
                _objCommon.CourseId = Convert.ToInt16(courseId);
                var userDetails = UserManagerProvider.Instance.GetUserListById(Convert.ToInt32(userId)).FirstOrDefault();
                if (userDetails != null)
                {
                    objSecurePage.LoggedInUserEmailId = userDetails.UserEmailid;
                    objSecurePage.LoggedInUserId = userDetails.UserId;
                    objSecurePage.LoggedInUserMobile = userDetails.MobileNo;
                    objSecurePage.LoggedInUserName = userDetails.UserFullName;
                    objSecurePage.LoggedInUserType = userDetails.UserCategoryId;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUserDetails in PaymentOptions.aspx for user :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void btnFinish_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
               
                var objSecurePage = new SecurePage();
                _objCommon = new Common();
                var objMailTemplates = new MailTemplates();
                string tranctionDetails;
                var orderNumber = "ADMJPROD" + DateTime.Now.Year + "UID" + objSecurePage.LoggedInUserId.ToString();

                var i = new Common().UpdateOrderIdForProduct(objSecurePage.LoggedInUserId, false,Request["id"]== null? orderNumber:null,
                                                             rbtnPaymentType.SelectedItem.ToString(),
                                                             Request["id"] != null ? Convert.ToInt32(Request["id"]) : 0);
               
                switch (rbtnPaymentType.SelectedValue)
                {
                    case "0":
                        tranctionDetails =
                            " You have selected the payment mode through cheque. Please make an account payee cheque of Rs." +
                            lblCash1.Text + " in favour of <b>" + " Admissionjankari.com " + " </b>";
                        tranctionDetails = tranctionDetails +
                                           " <br /><br /> Mention your Reference Id(Application form number), Name, Phone No, Email-id, at the back of the cheque.";
                        tranctionDetails = tranctionDetails +
                                           "<br /><br />To confirm the payment, please send your cheque at the following address (Via Speed/Registered Post) ";
                        tranctionDetails = tranctionDetails + " <br /><br />" + "Admissionjankari.com<br />";
                        tranctionDetails = tranctionDetails + "74 Amrit Chamber, 2nd floor, <br />" +
                                           "  202-204 Scindia House Connaught Place, <br />" + " New Delhi-110001. <br />" +
                                           "  Contact us : +91 - 9999 261 633, 9654 722 013 , 011-43391978<br/>";
                        break;
                    case "1":
                        tranctionDetails = "You have selected the payment mode through DD.<br/>  ";
                        tranctionDetails = tranctionDetails + "   <b>Make a single Demand Draft</b> (DD) of Rs." +
                                           lblCash1.Text + " in favour of <b>" + " Admissionjankari com" +
                                           "</b>Payable at <b>Delhi.</b>";
                        tranctionDetails = tranctionDetails +
                                           " <br /><br /> To confirm the payment, please send your  Demand Draft at the following address (Via Speed/Registered Post)";
                        tranctionDetails = tranctionDetails + "<br /><br />" + "Admissionjankari.com <br />";
                        tranctionDetails = tranctionDetails + "74 Amrit Chamber, 2nd floor, <br />" +
                                           "  202-204 Scindia House Connaught Place, <br />" +
                                           " New Delhi-110001. <br />" +
                                           "   Contact us : +91-11-43391978, +91-8800567711, +91-8800567733<br/>";
                        break;
                    case "2":
                        {
                            const string bankName = "Account Name: Admissionjankari.com";
                            const string ddNumber = "00032 0000 44418";
                            tranctionDetails =
                                "You have selected the payment mode through cash. You will need to deposit Rs." +
                                lblCash1.Text + " in the nearest HDFC Bank in the following account.   <br/><br/> ";
                            tranctionDetails = tranctionDetails + bankName + "<br/>";
                            tranctionDetails = tranctionDetails + "Account Number :" + ddNumber + " <br/>";
                            tranctionDetails = tranctionDetails + "RTGS/IFSC/NEFT Code: HDFC0000003 <br/>";
                            tranctionDetails = tranctionDetails + "Branch:Kasturba Gandhi Marg,New Delhi<br/>";
                            tranctionDetails = tranctionDetails +
                                               " <br /><br /> To confirm the payment, please send your  pay-in-slip at the following address (Via Speed/Registered Post)";
                            tranctionDetails = tranctionDetails + "<br /><br />" + "Admissionjankari.com";
                            tranctionDetails = tranctionDetails + "74 Amrit Chamber, 2nd floor, <br />" +
                                               "  202-204 Scindia House Connaught Place, <br />" +
                                               " New Delhi-110001. <br />" +
                                               "   Contact us : +91-11-43391978, +91-8800567711, +91-8800567733<br/>";
                        }
                        break;
                    default:
                        tranctionDetails = "You have selected the payment mode through Online Payment of" + " " +
                                           "Rs." + " " + hdnAmount.Value + "/- ";
                        OnlinePayment(orderNumber);
                        break;
                }
                var objCarProduct = new Common().GetProductForCart(0, Request["id"] == null ? orderNumber : null,
                                                                   Request["id"] != null
                                                                       ? Convert.ToInt32(Request["id"])
                                                                       : 0);
                     
                                    
                var mail = new MailMessage
                {
                    From = new MailAddress(ApplicationSettings.Instance.Email),
                    Subject = "Product Order Number:" + orderNumber
                };
                var body = objMailTemplates.SendProductConfirmationMail(objSecurePage.LoggedInUserName,
                                                                                    orderNumber, tranctionDetails, objCarProduct.Tables[0]);

                mail.Body = body;
                mail.To.Add(objSecurePage.LoggedInUserEmailId);
                mail.Bcc.Add(ClsSingelton.bccDirectAdmission);
                Utils.SendMailMessageAsync(mail);
               
                if (rbtnPaymentType.SelectedValue != "OnPayment")
                    Response.Redirect(Utils.AbsoluteWebRoot + "account/paymentconfirmation.aspx?mode=other", true);

            }
        }

        private void BindUserDetailsForOnlinePayment()
        {
            var objSecurePage = new SecurePage();
            try
            {
                if (objSecurePage.IsLoggedInUSer)
                {
                    var userDetails = UserManagerProvider.Instance.GetUserListById(objSecurePage.LoggedInUserId).FirstOrDefault();
                    if (userDetails != null)
                    {
                        txtAddress.Text = userDetails.UserCorrespondenceAddress;
                        txtCity.Text = userDetails.CityName;
                        txtState.Text = userDetails.StateName;
                        txtPincode.Text = userDetails.UserPincode;
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
                const string addInfo = "Error while executing BindUserDetailsForOnlinePayment in PaymentOptions.aspx for user :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void rbtnPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnFinish.ValidationGroup = rbtnPaymentType.SelectedValue.Equals("OnPayment") ? "VldgOnlinePayment" : "";
        }
    }
}