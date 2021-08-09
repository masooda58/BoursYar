using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebServiceManager;

namespace UiWebBoursYar.Helpers 
{
    public class TableHelper
    {
        public static  IHtmlContent  BuildTable<T>(List<T> data, string classname) where T:class
        {
            //Tags
            var table = new TagBuilder("table class="+"'"+classname+"'");
            var tr = new TagBuilder("tr");
            var headers = GetProperty.GetFeildName<T>(data);

            //Add headers
            foreach (var s in headers)
            {
                var th = new TagBuilder("th  ");
                th.InnerHtml.Append(s);
                tr.InnerHtml.AppendHtml(th);
            }
            table.InnerHtml.AppendHtml(tr);

            //Add data
            foreach (var d in data)
            {
                tr = new TagBuilder("tr");
                foreach (var h in headers)
                {
                    var td = new TagBuilder("td");
                    if (h == "name" || h == "Name")
                    {
                        var a= new TagBuilder("a href="+"/Home/detail/"+d.GetType().GetProperty("Code").GetValue(d, null)?.ToString());
                        a.InnerHtml.Append(d.GetType().GetProperty(h).GetValue(d, null)?.ToString()) ;
                        td.InnerHtml.AppendHtml(a);
                    }
                    else
                    {
                        td.InnerHtml.Append( d.GetType().GetProperty(h).GetValue(d, null)?.ToString());
                    }
                   
                    tr.InnerHtml.AppendHtml(td);
                }
            
                table.InnerHtml.AppendHtml(tr);
            }

            return table;
        }
    }

}
