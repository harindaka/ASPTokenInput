using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ASPTokenInputLib
{
    public class ASPTokenInput : CompositeControl, IPostBackEventHandler
    {
        [Serializable]
        public class Item
        {
            public object id { get; set; }
            public string name { get; set; }
        }

        public class PostbackEventArgs : Item
        {
            public string eventName { get; set; }
        }

        public enum ListChangeActions { Add, Remove }

        public class ListChangedEventArgs : EventArgs
        {
            public ListChangedEventArgs(ListChangeActions action, Item item)
                : base()
            {
                this.Item = item;
                this.Action = action;
            }

            public ListChangeActions Action
            {
                get;
                private set;
            }

            public Item Item
            {
                get;
                private set;
            }
        }

        public delegate void ListChangedEventHandler(object sender, ListChangedEventArgs e);

        [Browsable(true)]
        public event ListChangedEventHandler ListChanged;

        TextBox _txtText;
        HiddenField _hfPersist;
        
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.Controls.Clear();

            _txtText = new TextBox();
            this.Controls.Add(_txtText);

            _hfPersist = new HiddenField();
            this.Controls.Add(_hfPersist);            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.Page.IsPostBack)
                this.Items = new List<Item>();
            else if (String.IsNullOrEmpty(_hfPersist.Value))
                this.Items = new List<Item>();
            else
                this.Items = (List<Item>)new JavaScriptSerializer().Deserialize<IList<Item>>(_hfPersist.Value);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
                                    
            if (!String.IsNullOrEmpty(this.RequestHandlerPath))
            {
                string handlerPath = this.Page.ResolveUrl(this.RequestHandlerPath);

                JSONObject opts = new JSONObject();

                opts.AddValueMember("hintText", this.HintText, null);
                opts.AddValueMember("noResultsText", this.NoResultsText, null);
                opts.AddValueMember("searchingText", this.SearchingText, null);
                opts.AddValueMember("deleteText", this.DeleteText, null);
                opts.AddValueMember("theme", this.Theme, null);
                opts.AddValueMember("animateDropdown", this.AnimateDropdown, null);
                opts.AddValueMember("searchDelay", this.SearchDelay, null);
                opts.AddValueMember("minChars", this.MinChars, null);
                opts.AddValueMember("tokenLimit", this.TokenLimit, null);
                opts.AddValueMember("preventDuplicates", this.PreventDuplicates, null);
                opts.AddValueMember("jsonContainer", this.JSONContainer, null);
                opts.AddValueMember("method", this.Method, null);
                opts.AddValueMember("queryParam", this.QueryParam, null);
                opts.AddValueMember("crossDomain", this.CrossDomain, null);

                if (this.Items.Count > 0)
                {
                    string prePopulate = new JavaScriptSerializer().Serialize(this.Items.ToArray());
                    opts.AddReferenceMember("prePopulate", prePopulate);
                }               

                string postBackCall = "";
                if (this.PostbackOnItemAdded)
                    postBackCall = "__doPostBack('" + this.UniqueID + "', JSON.stringify({ eventName:'Add', id:item.id, name:item.name }));";

                opts.AddReferenceMember("onAdd", "function(item){ saveTokenInputSelections('" + this.ClientID + "', '" + _txtText.ClientID + "', '" + _hfPersist.ClientID + "'); \n" + postBackCall + " }");

                postBackCall = "";
                if (this.PostbackOnItemRemoved)
                    postBackCall = "__doPostBack('" + this.UniqueID + "', JSON.stringify({ eventName:'Remove', id:item.id, name:item.name }));";

                opts.AddReferenceMember("onDelete", "function(item){ saveTokenInputSelections('" + this.ClientID + "', '" + _txtText.ClientID + "', '" + _hfPersist.ClientID + "'); \n" + postBackCall + " }");

                string initializeTokenInputScript = "$('#" + _txtText.ClientID + "').tokenInput('" + handlerPath + "', " + opts.ToString() + ");"; // loadTokenInputSelections('" + this.ClientID + "', '" + _txtText.ClientID + "', '" + _hfPersist.ClientID + "');";

                string initializeTokenInputScriptKey = "ASPTokenInputInitializeScript_" + this.ClientID;
                ScriptManager.RegisterStartupScript(this, this.GetType(), initializeTokenInputScriptKey, initializeTokenInputScript, true);

                const string persistanceScript = "function saveTokenInputSelections(tokenContainerID, tokenTextBoxID, persistanceFieldID) { \n"
                                           + "var ids = []; \n"
                                           + "ids = $('#' + tokenTextBoxID).val().split(','); \n"
                                           + "if (ids.length > 0) { \n"
                                           + "var valElements = $('#' + tokenContainerID + ' p'); \n"

                                           + "if (valElements.length == ids.length) { \n"
                                           + "var index = 0; \n"
                                           + "var names = []; \n"
                                           + "for (index = 0; index < valElements.length; index++) \n"
                                           + "names[index] = $(valElements[index]).text(); \n"

                                           + "var jsonSelections = []; \n"
                                           + "for (index = 0; index < ids.length; index++) \n"
                                           + "jsonSelections[index] = { id: ids[index], name: names[index] }; \n"

                                           + "var jsonString = JSON.stringify(jsonSelections); \n"
                                           + "$('#' + persistanceFieldID).val(jsonString); \n"
                                           + "} \n"
                                           + "else \n"
                                           + "$('#' + persistanceFieldID).val(''); \n"
                                           + "} \n"
                                           + "else \n"
                                           + "$('#' + persistanceFieldID).val(''); \n"
                                           + "} \n";

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ASPTokenInputPersistanceManagementScript", persistanceScript, true);
            }
        }

        protected override void RecreateChildControls()
        {
            base.RecreateChildControls();
            this.EnsureChildControls();
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            PostbackEventArgs args = new JavaScriptSerializer().Deserialize<PostbackEventArgs>(eventArgument);
            ListChangedEventHandler listChanged = null;
            switch (args.eventName)
            {

                case "Add":
                    listChanged = this.ListChanged;
                    if (listChanged != null)
                        listChanged(this, new ListChangedEventArgs(ListChangeActions.Add, new Item { id = args.id, name = args.name }));
                    break;

                case "Remove":
                    listChanged = this.ListChanged;
                    if (listChanged != null)
                        listChanged(this, new ListChangedEventArgs(ListChangeActions.Remove, new Item { id = args.id, name = args.name }));
                    break;
            }
        }

        public List<Item> Items
        {
            get;
            private set;
        }

        [Browsable(true)]
        public string RequestHandlerPath
        {
            get { return Convert.ToString(this.ViewState["RequestHandlerPath"]); }
            set { this.ViewState["RequestHandlerPath"] = value; }
        }

        [Browsable(true)]
        public bool PostbackOnItemAdded
        {
            get
            {
                object val = this.ViewState["PostbackOnItemAdded"];
                if (val == null)
                    return false;
                else
                    return Convert.ToBoolean(this.ViewState["PostbackOnItemAdded"]);
            }
            set { this.ViewState["PostbackOnItemAdded"] = value; }
        }

        [Browsable(true)]
        public bool PostbackOnItemRemoved
        {
            get
            {
                object val = this.ViewState["PostbackOnItemRemoved"];
                if (val == null)
                    return false;
                else
                    return Convert.ToBoolean(this.ViewState["PostbackOnItemRemoved"]);
            }
            set { this.ViewState["PostbackOnItemRemoved"] = value; }
        }

        [Browsable(true)]
        public string HintText
        {
            get { return Convert.ToString(this.ViewState["HintText"]); }
            set { this.ViewState["HintText"] = value; }
        }
        [Browsable(true)]
        public string NoResultsText
        {
            get { return Convert.ToString(this.ViewState["NoResultsText"]); }
            set { this.ViewState["NoResultsText"] = value; }
        }
        [Browsable(true)]
        public string SearchingText
        {
            get { return Convert.ToString(this.ViewState["SearchingText"]); }
            set { this.ViewState["SearchingText"] = value; }
        }
        [Browsable(true)]
        public string DeleteText
        {
            get { return Convert.ToString(this.ViewState["DeleteText"]); }
            set { this.ViewState["DeleteText"] = value; }
        }
        [Browsable(true)]
        public string Theme
        {
            get { return Convert.ToString(this.ViewState["Theme"]); }
            set { this.ViewState["Theme"] = value; }
        }
        [Browsable(true)]
        public bool? AnimateDropdown
        {
            get { return (bool?)this.ViewState["AnimateDropdown"]; }
            set { this.ViewState["AnimateDropdown"] = value; }
        }
        [Browsable(true)]
        public int? SearchDelay
        {
            get { return (int?)this.ViewState["SearchDelay"]; }
            set { this.ViewState["SearchDelay"] = value; }
        }
        [Browsable(true)]
        public int? MinChars
        {
            get { return (int?)this.ViewState["MinChars"]; }
            set { this.ViewState["MinChars"] = value; }
        }
        [Browsable(true)]
        public int? TokenLimit
        {
            get { return (int?)this.ViewState["TokenLimit"]; }
            set { this.ViewState["TokenLimit"] = value; }
        }        
        [Browsable(true)]
        public bool? PreventDuplicates
        {
            get { return (bool?)this.ViewState["PreventDuplicates"]; }
            set { this.ViewState["PreventDuplicates"] = value; }
        }
        [Browsable(true)]
        public string JSONContainer
        {
            get { return Convert.ToString(this.ViewState["JSONContainer"]); }
            set { this.ViewState["JSONContainer"] = value; }
        }
        [Browsable(true)]
        public string Method
        {
            get { return Convert.ToString(this.ViewState["Method"]); }
            set { this.ViewState["Method"] = value; }
        }
        [Browsable(true)]
        public string QueryParam
        {
            get { return Convert.ToString(this.ViewState["QueryParam"]); }
            set { this.ViewState["QueryParam"] = value; }
        }
        [Browsable(true)]
        public bool? CrossDomain
        {
            get { return (bool?)this.ViewState["CrossDomain"]; }
            set { this.ViewState["CrossDomain"] = value; }
        }
    }
}