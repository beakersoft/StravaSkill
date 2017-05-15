using Strava.Skill.DAL;
using Strava.Skill.DomainItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strava.Skill.Services
{    
    public class StravaActivity
    {
        private StravaApiCall _apiCaller;

        public StravaActivity()
        {
            _apiCaller = new StravaApiCall();
        }

        public IEnumerable<Activity> GeActivity(int numberOfActivitys, string accessToken)
        {
            var urlEndpoint = $"https://www.strava.com/api/v3/athlete/activities?access_token={accessToken}&per_page={numberOfActivitys}";
            return _apiCaller.GetStravaData<IEnumerable<Activity>>(urlEndpoint);
        }

    }
}
