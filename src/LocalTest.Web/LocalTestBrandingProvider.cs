using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace LocalTest.Web;

[Dependency(ReplaceServices = true)]
public class LocalTestBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "LocalTest";
}
