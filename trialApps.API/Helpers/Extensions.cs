using System;
using Microsoft.AspNetCore.Http;

namespace trialApps.API.Helpers
{
    public static class Extensions
    {
        public static void AddAppError(this HttpResponse response, string msg) {
            response.Headers.Add("Application-Error", msg);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static int CalculateAge(this DateTime date) {
            var Age = DateTime.Today.Year - date.Year;
            if (date.AddYears(Age) > DateTime.Today) {
                Age--;
            }
            return Age;
        }
    }
}