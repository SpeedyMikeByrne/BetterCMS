﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BetterCms.Module.Pages.Views.Option.Partial
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 1 "..\..\Views\Option\Partial\UseDefaultValueCell.cshtml"
    using BetterCms.Module.Root;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Views\Option\Partial\UseDefaultValueCell.cshtml"
    using BetterCms.Module.Root.Mvc.Grids;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Option/Partial/UseDefaultValueCell.cshtml")]
    public partial class UseDefaultValueCell : System.Web.Mvc.WebViewPage<EditableGridColumn>
    {
        public UseDefaultValueCell()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

WriteLiteral("<!-- ko if: canEditOption() -->\r\n    <td>&nbsp;</td>\r\n<!-- /ko -->\r\n\r\n<!-- ko ifn" +
"ot: canEditOption() -->\r\n");

            
            #line 11 "..\..\Views\Option\Partial\UseDefaultValueCell.cshtml"
Write(Html.Partial(RootModuleConstants.EditableGridBooleanCellTemplate, new EditableGridBooleanColumn(Model.HeaderTitle, Model.SortColumn, Model.ValueBind)
                                                                        {
                                                                            HiddenFieldName = Model.HiddenFieldName,
                                                                            HeaderAttributes = Model.HeaderAttributes,
                                                                            AutoFocus = Model.AutoFocus,
                                                                            ShowValueAsText = true
                                                                        }));

            
            #line default
            #line hidden
WriteLiteral("\r\n<!-- /ko -->\r\n");

        }
    }
}
#pragma warning restore 1591
