using Strava.Skill.Services;
using System.Linq;
using Xunit;

namespace Strava.Skill.Tests
{
    public class StravaActivityTest
    {
        private string _accessToken = "dcc0aa89057d4043cb43bd039032140edb116a8a";

        [Fact]
        public void TestActivityReturnsItem()
        {
            var actService = new StravaActivity();
            var result = actService.GeActivity(1, _accessToken);
            Assert.True(result.Count() > 0);
        }
    }
}
