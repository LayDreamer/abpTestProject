using LocalTest.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace LocalTest.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class LocalTestPageModel : AbpPageModel
{
    protected LocalTestPageModel()
    {
        LocalizationResourceType = typeof(LocalTestResource);
    }
}
