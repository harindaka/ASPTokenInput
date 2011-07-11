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
    }
}