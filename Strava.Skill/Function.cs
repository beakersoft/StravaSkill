using System.Linq;
using Alexa.NET.Response;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;

using Amazon.Lambda.Core;
using Strava.Skill.Services;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Strava.Skill
{
    public class Function
    {
        private ILambdaLogger _logger;
        private SkillResponse _response;
        private IOutputSpeech _innerResponse = null;
        private string _alexaUser;


        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            //standard stuff
            var requestType = input.GetRequestType();
            _logger = context.Logger;
            _alexaUser = input.Session.User.UserId;
            SetStandardResponse();

            if (requestType == typeof(LaunchRequest))
            {
                _logger.LogLine($"Default LaunchRequest made: 'Strava, open Jira");
                (_innerResponse as PlainTextOutputSpeech).Text = "Welcome to the Strava skill. Ask me some questions";
            }

            if (input.GetRequestType() == typeof(IntentRequest))
            {
                var intentRequest = (IntentRequest)input.Request;


                switch (intentRequest.Intent.Name)
                {
                    case "GetLastActivityIntent":
                        GetLastActivityIntent();
                        break;
                    default:
                        _logger.LogLine($"Unknown intent: {intentRequest.Intent.Name}");
                        (_innerResponse as PlainTextOutputSpeech).Text = "Sorry, Strava cant answer that question yet";
                        break;
                }
            }

            return _response;
        }


        private void GetLastActivityIntent()
        {
            var actService = new StravaActivity();
            var activityDetails = actService.GeActivity(1, GetStravaAccessToken(_alexaUser));

            if (activityDetails.Count() > 0)
            {

            }
        }



        private void SetStandardResponse()
        {
            _response = new SkillResponse();
            _innerResponse = new PlainTextOutputSpeech();
            _response.Response = new ResponseBody();
            _response.Response.ShouldEndSession = true;
            _response.Version = "1.0";            
        }

        private string GetStravaAccessToken(string alexaUserId)
        {
            return "dcc0aa89057d4043cb43bd039032140edb116a8a";
        }
    }
}
