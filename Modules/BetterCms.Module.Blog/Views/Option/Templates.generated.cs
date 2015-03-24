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

namespace BetterCms.Module.Blog.Views.Option
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
    
    #line 1 "..\..\Views\Option\Templates.cshtml"
    using BetterCms.Module.Blog.Content.Resources;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Views\Option\Templates.cshtml"
    using BetterCms.Module.Root.Content.Resources;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Views\Option\Templates.cshtml"
    using BetterCms.Module.Root.Mvc.Helpers;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Option/Templates.cshtml")]
    public partial class Templates : System.Web.Mvc.WebViewPage<dynamic>
    {
        public Templates()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"bcms-blog-templates-container\"");

WriteLiteral(">\r\n    \r\n    <div");

WriteLiteral(" class=\"bcms-top-block-holder\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"bcms-large-titles\"");

WriteLiteral(">");

            
            #line 8 "..\..\Views\Option\Templates.cshtml"
                                  Write(BlogGlobalization.SiteSettings_Templates_Title);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n        <div");

WriteLiteral(" class=\"bcms-search-block\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"bcms-editor-field-box\"");

WriteAttribute("placeholder", Tuple.Create(" placeholder=\"", 427), Tuple.Create("\"", 476)
            
            #line 10 "..\..\Views\Option\Templates.cshtml"
, Tuple.Create(Tuple.Create("", 441), Tuple.Create<System.Object, System.Int32>(RootGlobalization.WaterMark_Search
            
            #line default
            #line hidden
, 441), false)
);

WriteLiteral(" data-bind=\"value: searchQuery, valueUpdate: \'afterkeydown\', enterPress: search\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"bcms-btn-search bcms-search-query\"");

WriteLiteral(" data-bind=\"click: search\"");

WriteLiteral(">");

            
            #line 11 "..\..\Views\Option\Templates.cshtml"
                                                                                Write(RootGlobalization.Button_Search);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n        </div>\r\n    </div>\r\n\r\n");

WriteLiteral("    ");

            
            #line 15 "..\..\Views\Option\Templates.cshtml"
Write(Html.TabbedContentMessagesBox());

            
            #line default
            #line hidden
WriteLiteral("\r\n                    \r\n    <div");

WriteLiteral(" class=\"bcms-blog-templates-container\"");

WriteLiteral(">\r\n        <!-- ko foreach: templateRows -->\r\n        <div");

WriteLiteral(" class=\"bcms-blog-templates-holder\"");

WriteLiteral(">\r\n            <!-- ko foreach: templates() -->\r\n            <div");

WriteLiteral(" class=\"bcms-blog-templates\"");

WriteLiteral(" data-bind=\"css: {\'bcms-blog-templates-active\': isCompatible && isActive(), \'bcms" +
"-blog-templates-notcompatible\': !isCompatible }\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"bcms-title-holder\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"bcms-content-titles\"");

WriteLiteral(" data-bind=\"text: title\"");

WriteLiteral("></div>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"bcms-blog-templates-image-holder\"");

WriteLiteral(">\r\n                    <!-- ko if: previewUrl -->\r\n                    <div");

WriteLiteral(" class=\"bcms-zoom-overlay\"");

WriteLiteral(" data-bind=\"click: previewImage\"");

WriteLiteral("></div>\r\n                    <div");

WriteLiteral(" class=\"bcms-blog-templates-image\"");

WriteLiteral("><img");

WriteLiteral(" data-bind=\"attr:{src: previewUrl, alt: title}\"");

WriteLiteral(" /></div>\r\n                    <!-- /ko -->\r\n                </div>\r\n            " +
"    <div");

WriteLiteral(" class=\"bcms-blog-templates-button-holder\"");

WriteLiteral(">\r\n                    <!-- ko if: isCompatible && isActive() -->\r\n              " +
"      <div");

WriteLiteral(" class=\"bcms-blog-templates-active-text\"");

WriteLiteral(">");

            
            #line 33 "..\..\Views\Option\Templates.cshtml"
                                                            Write(BlogGlobalization.SiteSettings_Templates_DefaultTemplate);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                    <!-- /ko -->\r\n                    <!-- ko if: isCompa" +
"tible && !isActive() -->\r\n                    <div");

WriteLiteral(" class=\"bcms-btn-grid\"");

WriteLiteral(" data-bind=\"click: select\"");

WriteLiteral(">");

            
            #line 36 "..\..\Views\Option\Templates.cshtml"
                                                                    Write(BlogGlobalization.SiteSettings_Templates_SelectButton);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                    <!-- /ko -->\r\n                    <!-- ko if: !isComp" +
"atible -->\r\n                    <div");

WriteLiteral(" class=\"bcms-blog-templates-notcompatible-text\"");

WriteLiteral(">");

            
            #line 39 "..\..\Views\Option\Templates.cshtml"
                                                                   Write(BlogGlobalization.SiteSettings_Templates_IncompatibleTemplate);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                    <!-- /ko -->\r\n                </div>\r\n            </d" +
"iv>\r\n            <!-- /ko -->\r\n        </div>\r\n        <!-- /ko -->\r\n    </div>\r" +
"\n</div>");

        }
    }
}
#pragma warning restore 1591
