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

namespace BetterCms.Module.Root.Views.Sidebar
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
    
    #line 1 "..\..\Views\Sidebar\Body.cshtml"
    using BetterCms.Core.Modules.Projections;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Views\Sidebar\Body.cshtml"
    using BetterCms.Module.Root.Mvc;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Sidebar/Body.cshtml")]
    public partial class Body : System.Web.Mvc.WebViewPage<BetterCms.Module.Root.ViewModels.PageProjectionsViewModel>
    {
        public Body()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"bcms-sidemenu-body\"");

WriteLiteral(">\r\n");

            
            #line 5 "..\..\Views\Sidebar\Body.cshtml"
    
            
            #line default
            #line hidden
            
            #line 5 "..\..\Views\Sidebar\Body.cshtml"
      
        var lastItemWasSeparator = true;
        foreach (var projection in Model.Projections)
        {
            var isSeparator = projection is SeparatorProjection;
            if (!(lastItemWasSeparator && isSeparator))
            {
                if (projection.Render(Model.Page, (ViewContext.Controller as CmsControllerBase).SecurityService, Html))
                {
                    lastItemWasSeparator = isSeparator;
                }
            }
        }
    
            
            #line default
            #line hidden
WriteLiteral("\r\n</div>");

        }
    }
}
#pragma warning restore 1591
