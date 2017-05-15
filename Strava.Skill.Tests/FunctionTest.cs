using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using Strava.Skill;
using Alexa.NET.Request;
using Alexa.NET.Response;

namespace Strava.Skill.Tests
{
    public class FunctionTest
    {
        private Function _function;
        private TestLambdaContext _context;

        public FunctionTest()
        {
            _function = new Function();
            _context = new TestLambdaContext();
        }


        [Fact]
        public void TestResponseType()
        {
            // Invoke the lambda function and confirm the return was a skill response type                                   
            var skillResponse = _function.FunctionHandler(CreateDummyRequest(), _context);
            Assert.IsType<SkillResponse>(skillResponse);
        }

        private SkillRequest CreateDummyRequest()
        {
            return new SkillRequest
            {
                Session = new Session { User = CreateDummyUser() }
            };
        }

        private User CreateDummyUser()
        {
            return new User
            {
                UserId = "Luke123"                
            };
        }
    }
}
