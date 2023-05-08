using LocalTest.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace LocalTest.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class LocalTestController : AbpControllerBase
{
    protected LocalTestController()
    {
        LocalizationResource = typeof(LocalTestResource);
    }
}
