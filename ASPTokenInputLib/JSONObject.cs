using System;
using System.Collections.Generic;

namespace ASPTokenInputLib
{   
 
    internal class JSONObject
    {
        readonly List<string> _jsonMembers;

        internal JSONObject()
        {
            _jsonMembers = new List<string>();
        }

        internal void AddValueMember(string memberName, object memberValue, object defaultValue)
        {
            string val = "";
            bool quoteValue = false;

            if (memberValue is String)
            {
                if (String.IsNullOrEmpty(Convert.ToString(memberValue)))
                    memberValue = null;
            }

            if (memberValue == null)
            {
                if (defaultValue != null)
                {
                    if (defaultValue is Boolean)
                    {
                        if ((bool)defaultValue)
                            val = "true";
                        else
                            val = "false";
                    }
                    else
                    {
                        val = Convert.ToString(defaultValue);
                        quoteValue = (defaultValue is String);
                    }
                }
            }
            else
            {
                if (memberValue is Boolean)
                {
                    if ((bool)memberValue)
                        val = "true";
                    else
                        val = "false";
                }
                else
                {
                    val = Convert.ToString(memberValue);
                    quoteValue = (memberValue is String);
                }
            }

            if (!String.IsNullOrEmpty(val))
            {
                if(quoteValue)
                    val = "'" + val + "'";

                val = memberName + ": " + val;

                _jsonMembers.Add(val);
            }
        }

        internal void AddReferenceMember(string memberName, string memberValue)
        {
            if (!String.IsNullOrEmpty(memberValue))
                _jsonMembers.Add(memberName + ": " + memberValue);            
        }

        public override string ToString()
        {
            return "{ \n" + String.Join(", \n", _jsonMembers) + " \n}";
        }
    }


}