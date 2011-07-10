using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Script.Serialization;
using ASPTokenInputLib;

namespace ASPTokenInputTest
{
    public partial class ItemList : System.Web.UI.Page
    {        
        private static Random random = new Random((int)DateTime.Now.Ticks);
        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public string GetList(string filter)
        {
            if (String.IsNullOrEmpty(filter))
                filter = "";

            List<ASPTokenInput.Item> slist = new List<ASPTokenInput.Item>();
            for (int i = 0; i < 50; i++)
                slist.Add(new ASPTokenInput.Item() { id = i, name = filter + " Element " + Convert.ToString(i) });

            JavaScriptSerializer s = new JavaScriptSerializer();
            return s.Serialize(slist);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if logged in
            string filter = this.Request.QueryString["q"];

            string list = this.GetList(filter);
            this.Response.Clear();
            this.Response.ContentType = "text/plain";
            this.Response.Write(list);
            this.Response.End();
        }
    }
}