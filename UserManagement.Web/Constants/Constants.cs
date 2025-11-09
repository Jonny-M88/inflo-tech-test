using System.Text.Json;
using System.Text.Json.Serialization;

namespace UserManagement.Web.Constants
{
    public static class Constants
    {
        public static readonly string DateTimeToStringImplementation = "yyyy/MM/dd";
        public static readonly JsonSerializerOptions DefaultSerializerOptions;

        static Constants()
        {
            //Camelcases json properties
            DefaultSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            };

            //Converts enum values to string
            DefaultSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }
    }
}
