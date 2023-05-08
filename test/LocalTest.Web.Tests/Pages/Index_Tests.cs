using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace LocalTest.Pages;

public class Index_Tests : LocalTestWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
