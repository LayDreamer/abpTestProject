using Volo.Abp.Settings;

namespace LocalTest.Settings;

public class LocalTestSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(LocalTestSettings.MySetting1));
    }
}
