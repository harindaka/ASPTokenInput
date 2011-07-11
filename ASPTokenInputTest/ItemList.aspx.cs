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
        private const string COUNTRIES = "United States, Canada, Afghanistan, Albania, Algeria, Andorra, Angola, Antigua & Deps, Argentina, Armenia, Australia, Austria, Azerbaijan, Bahamas, Bahrain, Bangladesh, Barbados, Belarus, Belgium, Belize, Benin, Bhutan, Bolivia, Bosnia Herzegovina, Botswana, Brazil, Brunei, Bulgaria, Burkina, Burundi, Cambodia, Cameroon, Canada, Cape Verdi, Central African Rep, Chad, Chile, China, Columbia, Comoros, Congo, Congo {Democratic Rep}, Costa Rica, Croatia, Cuba, Cyprus, Czech Republic, Denmark, Djibouti, Dominica, Dominican Republic, East Timor, Ecuador, Egypt, El Salvador, Equatorial Guinea, Eritrea, Estonia, Ethiopia, Fiji, Finland, France, Gabon, Gambia, Georgia, Germany, Ghana, Greece, Greneda, Guatemala, Guinea, Guinea-Bissau, Guyana, Haiti, Honduras, Hungary, Iceland, India, Indonesia, Iran, Iraq, Ireland {Republic}, Israel, Italy, Ivory Coast, Jamaica, Japan, Jordan, Kazakhstan, Kenya, Kiribati, Korea North, Korea South, Kuwait, Kyrgyzstan, Laos, Latvia, Lebanon, Lesotho, Liberia, Libya, Liechtenstein, Lithuania, Luxembourg, Macedonia, Madagascar, Malawi, Malaysia, Maldives, Mali, Malta, Marshall Islands, Mauritania, Mauritius, Mexico, Micronesia, Moldova, Monaco, Mongolia, Morocco, Mozambique, Myanmar, {Burma}, Namibia, Narau, Nepal, Netherlands, New Zealand, Nicaragua, Niger, Nigeria, Norway, Oman, Pakistan, Palau, Panama, Papua New Guinea, Paraguay, Peru, Philippines, Poland, Portugal, Qatar, Romania, Russian Federation, Rwanda, St Kitts & Nevis, St Lucia, St Vincent & Grenadines, San Marino, Sao Tome & Principe, Saudi Arabia, Senegal, Seychelles, Sierra Leone, Singapore, Slovakia, Slovenia, Solomon Islands, Somalia, South Africa, Spain, Sri Lanka, Sudan, Suriname, Swaziland, Sweden, Switzerland, Syria, Taiwan, Tajikstan, Tanzania, Thailand, Togo, Tonga, Trinidad & Tobago, Tunisia, Turkey, Turkmenistan, Tuvalu, Uganda, Ukraine, United Arab Emirates, United Kingdom, United States, Uruguay, Uzbekistan, Vanuatu, Vatican City, Venezuela, Vietnam, Western Samoa, Yemen, Yugoslavia, Zambia, Zimbabwe";
        
        public string GetCountries(string filter)
        {
            string[] countriesArray = COUNTRIES.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            var query =
                from c in
                    countriesArray.Select((item, index) => new { id = index, name = item })
                    .Where(p => p.name.IndexOf(filter, StringComparison.InvariantCultureIgnoreCase) >= 0)
                select new { id = c.id, name = c.name };

            return new JavaScriptSerializer().Serialize(query);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if logged in
            string filter = this.Request.QueryString["q"];

            string list = this.GetCountries(filter);
            this.Response.Clear();
            this.Response.ContentType = "text/plain";
            this.Response.Write(list);
            this.Response.End();
        }
    }
}