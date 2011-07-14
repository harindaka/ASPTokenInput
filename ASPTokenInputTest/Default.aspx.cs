using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPTokenInputLib;

namespace ASPTokenInputTest
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                lblRequestHandlerPath.Text = tiTest2.RequestHandlerPath;
                chkPostbackOnItemAdded.Checked = true;
                chkPostbackOnItemRemoved.Checked = true;
                
                ddlTheme.Items.Add(new ListItem("default", "list"));
                ddlTheme.Items.Add(new ListItem("facebook", "facebook"));
                ddlTheme.Items.Add(new ListItem("mac", "mac"));
                ddlTheme.SelectedValue = "facebook";

                chkAnimateDropdown.Checked = true;

                txtHintText.Text = tiTest2.HintText;
            }
        }
        
        protected void btnFullPostback_Click(object sender, EventArgs e)
        {
            lbList1.Items.Add("Full postback triggered");
        }

        protected void btnPartialPostback_Click(object sender, EventArgs e)
        {
            lbList2.Items.Add("Partial postback triggered");
        }

        protected void tiTest1_ListChanged(object sender, ASPTokenInput.ListChangedEventArgs e)
        {
            if (e.Action == ASPTokenInput.ListChangeActions.Add)
                lbList1.Items.Add("Added Item id:" + Convert.ToString(e.Item.id) + " and name: " + e.Item.name);
            else
                lbList1.Items.Add("Removed Item id:" + Convert.ToString(e.Item.id) + " and name: " + e.Item.name);
        }

        protected void tiTest2_ListChanged(object sender, ASPTokenInput.ListChangedEventArgs e)
        {
            if (e.Action == ASPTokenInput.ListChangeActions.Add)
                lbList2.Items.Add("Added Item id:" + Convert.ToString(e.Item.id) + " and name: " + e.Item.name);
            else
                lbList2.Items.Add("Removed Item id:" + Convert.ToString(e.Item.id) + " and name: " + e.Item.name);
        }

        protected void btnSetProperties_Click(object sender, EventArgs e)
        {
            tiTest2.PostbackOnItemAdded = chkPostbackOnItemAdded.Checked;
            tiTest2.PostbackOnItemRemoved = chkPostbackOnItemRemoved.Checked;
            tiTest2.HintText = txtHintText.Text;
            tiTest2.NoResultsText = txtNoResultsText.Text;
            tiTest2.SearchingText = txtSearchingText.Text;
            tiTest2.DeleteText = txtDeleteText.Text;
            tiTest2.Theme = ddlTheme.SelectedValue;
            tiTest2.AnimateDropdown = chkAnimateDropdown.Checked;

            int intVal;
            if (Int32.TryParse(txtSearchDelay.Text.Trim(), out intVal))
                tiTest2.SearchDelay = intVal;
            else
                txtSearchDelay.Text = "You entered an invalid value";
                        
            if (Int32.TryParse(txtMinChars.Text.Trim(), out intVal))
                tiTest2.MinChars = intVal;
            else
                txtMinChars.Text = "You entered an invalid value";

            if (Int32.TryParse(txtTokenLimit.Text.Trim(), out intVal))
                tiTest2.TokenLimit = intVal;
            else
                txtTokenLimit.Text = "You entered an invalid value";

            tiTest2.PreventDuplicates = chkPreventDuplicates.Checked;
            
            lbList2.Items.Add("Properties Set.");
        }
    }
}