using System;
using System.Collections.Generic;
using System.Text;
using LocalTest.Localization;
using Volo.Abp.Application.Services;

namespace LocalTest;

/* Inherit your application services from this class.
 */
public abstract class LocalTestAppService : ApplicationService
{
    protected LocalTestAppService()
    {
        LocalizationResource = typeof(LocalTestResource);
    }
}
