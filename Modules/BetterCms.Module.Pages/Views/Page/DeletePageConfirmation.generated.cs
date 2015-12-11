﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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
    
    #line 28 "..\..\Views\Page\DeletePageConfirmation.cshtml"
    using BetterCms.Module.Pages.Content.Resources;
    
    #line default
    #line hidden
    
    #line 29 "..\..\Views\Page\DeletePageConfirmation.cshtml"
    using BetterCms.Module.Pages.Controllers;
    
    #line default
    #line hidden
    
    #line 30 "..\..\Views\Page\DeletePageConfirmation.cshtml"
    using BetterCms.Module.Pages.ViewModels.Page;
    
    #line default
    #line hidden
    
    #line 31 "..\..\Views\Page\DeletePageConfirmation.cshtml"
    using BetterCms.Module.Root;
    
    #line default
    #line hidden
    
    #line 32 "..\..\Views\Page\DeletePageConfirmation.cshtml"
    using BetterCms.Module.Root.Mvc;
    
    #line default
    #line hidden
    
    #line 33 "..\..\Views\Page\DeletePageConfirmation.cshtml"
    using BetterCms.Module.Root.Mvc.Helpers;
    
    #line default
    #line hidden
    
    #line 34 "..\..\Views\Page\DeletePageConfirmation.cshtml"
    using Microsoft.Web.Mvc;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Page/DeletePageConfirmation.cshtml")]
    public partial class _Views_Page_DeletePageConfirmation_cshtml : System.Web.Mvc.WebViewPage<DeletePageViewModel>
    {
        public _Views_Page_DeletePageConfirmation_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

WriteLiteral("\r\n");

            
            #line 38 "..\..\Views\Page\DeletePageConfirmation.cshtml"
 if (Model.ValidationMessage != null)
{

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" data-inuse=\"true\"");

WriteLiteral(">This template cannot be deleted, because it is used by some pages or templates.<" +
"/div>\r\n");

            
            #line 41 "..\..\Views\Page\DeletePageConfirmation.cshtml"
}
else
{
    using (Html.BeginForm<PageController>(f => f.DeletePage(null), FormMethod.Post,
            new
            {
                @class = "bcms-ajax-form",
                data_readonly = (Model.ValidationMessage != null) ? "true" : "false"
            }))
    {

            
            #line default
            #line hidden
WriteLiteral("        <div");

WriteLiteral(" class=\"bcms-input-list-holder\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"bcms-content-titles\"");

WriteLiteral(">");

            
            #line 52 "..\..\Views\Page\DeletePageConfirmation.cshtml"
                                        Write(PagesGlobalization.DeletePage_Dialog_ConfirmationMessage_Header);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n            <div>");

            
            #line 53 "..\..\Views\Page\DeletePageConfirmation.cshtml"
            Write(PagesGlobalization.DeletePage_Dialog_ConfirmationMessage_Text);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n        </div>\r\n");

            
            #line 55 "..\..\Views\Page\DeletePageConfirmation.cshtml"


            
            #line default
            #line hidden
WriteLiteral("        <div");

WriteLiteral(" class=\"bcms-input-list-holder\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"bcms-content-titles\"");

WriteLiteral(">");

            
            #line 57 "..\..\Views\Page\DeletePageConfirmation.cshtml"
                                        Write(PagesGlobalization.DeletePage_Dialog_RedirectUrl);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

WriteLiteral("            ");

            
            #line 58 "..\..\Views\Page\DeletePageConfirmation.cshtml"
       Write(Html.Tooltip(PagesGlobalization.DeletePage_Dialog_RedurectUrl_Tooltip_Description));

            
            #line default
            #line hidden
WriteLiteral("\r\n            <div");

WriteLiteral(" class=\"bcms-field-wrapper\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 60 "..\..\Views\Page\DeletePageConfirmation.cshtml"
           Write(Html.TextBoxFor(m => m.RedirectUrl, new { @class = "bcms-field-text", @id = "PageTitle" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 61 "..\..\Views\Page\DeletePageConfirmation.cshtml"
           Write(Html.BcmsValidationMessageFor(f => f.RedirectUrl));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n        </div>\r\n");

            
            #line 64 "..\..\Views\Page\DeletePageConfirmation.cshtml"


            
            #line default
            #line hidden
WriteLiteral("        <div");

WriteLiteral(" class=\"bcms-input-list-holder\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"bcms-content-titles\"");

WriteLiteral(">");

            
            #line 66 "..\..\Views\Page\DeletePageConfirmation.cshtml"
                                        Write(PagesGlobalization.DeletePage_Dialog_SecurityWord);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

WriteLiteral("            ");

            
            #line 67 "..\..\Views\Page\DeletePageConfirmation.cshtml"
       Write(Html.Tooltip(PagesGlobalization.DeletePage_Dialog_SecurityWord_Tooltip_Description));

            
            #line default
            #line hidden
WriteLiteral("\r\n            <div");

WriteLiteral(" class=\"bcms-field-wrapper\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 69 "..\..\Views\Page\DeletePageConfirmation.cshtml"
           Write(Html.TextBoxFor(m => m.SecurityWord, new { @class = "bcms-field-text", @id = "SecurityWord" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 70 "..\..\Views\Page\DeletePageConfirmation.cshtml"
           Write(Html.BcmsValidationMessageFor(f => f.SecurityWord));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n        </div>\r\n");

            
            #line 73 "..\..\Views\Page\DeletePageConfirmation.cshtml"

        if (Model.IsInSitemap && (ViewContext.Controller as CmsControllerBase).SecurityService.IsAuthorized(RootModuleConstants.UserRoles.EditContent))
        {

            
            #line default
            #line hidden
WriteLiteral("            <div");

WriteLiteral(" class=\"bcms-input-list-holder\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"bcms-content-titles\"");

WriteLiteral(">");

            
            #line 77 "..\..\Views\Page\DeletePageConfirmation.cshtml"
                                            Write(PagesGlobalization.DeletePage_Dialog_UpdateSitemap);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

WriteLiteral("                ");

            
            #line 78 "..\..\Views\Page\DeletePageConfirmation.cshtml"
           Write(Html.Tooltip(PagesGlobalization.DeletePage_Dialog_UpdateSitemap_Description));

            
            #line default
            #line hidden
WriteLiteral("\r\n                <div");

WriteLiteral(" class=\"bcms-checkbox-holder\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 80 "..\..\Views\Page\DeletePageConfirmation.cshtml"
               Write(Html.CheckBoxFor(m => m.UpdateSitemap, new { @id = "UpdateSitemap" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    <div");

WriteLiteral(" class=\"bcms-checkbox-label bcms-js-edit-label\"");

WriteLiteral(">");

            
            #line 81 "..\..\Views\Page\DeletePageConfirmation.cshtml"
                                                                   Write(PagesGlobalization.DeletePage_Dialog_UpdateSitemap_RemovePageFromSitemap);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                </div>\r\n            </div>\r\n");

            
            #line 84 "..\..\Views\Page\DeletePageConfirmation.cshtml"
        }

        
            
            #line default
            #line hidden
            
            #line 86 "..\..\Views\Page\DeletePageConfirmation.cshtml"
   Write(Html.HiddenFor(m => m.PageId));

            
            #line default
            #line hidden
            
            #line 86 "..\..\Views\Page\DeletePageConfirmation.cshtml"
                                      
        
            
            #line default
            #line hidden
            
            #line 87 "..\..\Views\Page\DeletePageConfirmation.cshtml"
   Write(Html.HiddenFor(m => m.Version));

            
            #line default
            #line hidden
            
            #line 87 "..\..\Views\Page\DeletePageConfirmation.cshtml"
                                       
        
            
            #line default
            #line hidden
            
            #line 88 "..\..\Views\Page\DeletePageConfirmation.cshtml"
   Write(Html.HiddenSubmit());

            
            #line default
            #line hidden
            
            #line 88 "..\..\Views\Page\DeletePageConfirmation.cshtml"
                            
    }
}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
