using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CHBase.Omron.Omron.Pages
{
    public partial class Data : System.Web.UI.Page
    {
        DateTime dtSince = DateTime.Now.AddDays(-8);

        protected void Page_Init(object sender, EventArgs e)
        {
            _PopulateDropDown();
            litHeader.Text = "Click the button to get data since " + dtSince.ToShortDateString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var response = DBHelper.Get();
            if (response == null)
                throw new Exception("No access token saved in file");

            // DataAPIHelper uses session saved values.
            OAuthV2.OAuthV2SessionManager.AccessToken = response.AccessToken;
            OAuthV2.OAuthV2SessionManager.RefreshToken = response.RefreshToken;
            OAuthV2.OAuthV2SessionManager.UserId = response.UserId;

        }
        private void _PopulateDropDown()
        {
            ddlMethods.DataSource = Enum.GetNames(typeof(DataAPI.APIMethods));
            ddlMethods.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DataAPI.APIMethods selectedMethod = (DataAPI.APIMethods)Enum.Parse(typeof(DataAPI.APIMethods), ddlMethods.SelectedValue);
            litOutput.Text = HttpUtility.HtmlEncode(DataAPI.DataAPIHelper.GetDataFor(selectedMethod, dtSince));
        }
    }
}