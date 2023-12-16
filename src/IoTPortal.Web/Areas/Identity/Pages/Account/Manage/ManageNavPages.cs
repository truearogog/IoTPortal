#nullable disable

using IoTPortal.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IoTPortal.Web.Areas.Identity.Pages.Account.Manage
{
    public static class ManageNavPages
    {
        public static string Index => "Index";
        public static string Email => "Email";
        public static string ChangePassword => "ChangePassword";
        public static string DownloadPersonalData => "DownloadPersonalData";
        public static string DeletePersonalData => "DeletePersonalData";
        public static string ExternalLogins => "ExternalLogins";
        public static string PersonalData => "PersonalData";
        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        public static string IndexNavClass(ViewContext viewContext) => NavPagesHelper.PageNavClass(viewContext, Index);
        public static string EmailNavClass(ViewContext viewContext) => NavPagesHelper.PageNavClass(viewContext, Email);
        public static string ChangePasswordNavClass(ViewContext viewContext) => NavPagesHelper.PageNavClass(viewContext, ChangePassword);
        public static string DownloadPersonalDataNavClass(ViewContext viewContext) => NavPagesHelper.PageNavClass(viewContext, DownloadPersonalData);
        public static string DeletePersonalDataNavClass(ViewContext viewContext) => NavPagesHelper.PageNavClass(viewContext, DeletePersonalData);
        public static string ExternalLoginsNavClass(ViewContext viewContext) => NavPagesHelper.PageNavClass(viewContext, ExternalLogins);
        public static string PersonalDataNavClass(ViewContext viewContext) => NavPagesHelper.PageNavClass(viewContext, PersonalData);
        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => NavPagesHelper.PageNavClass(viewContext, TwoFactorAuthentication);
    }
}
