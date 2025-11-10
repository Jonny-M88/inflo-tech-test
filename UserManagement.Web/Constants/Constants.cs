using System.Text.Json;
using System.Text.Json.Serialization;

namespace UserManagement.Web.Constants
{
    public static class Constants
    {
        //Global defaults
        public static readonly string DateTimeToStringImplementation = "yyyy/MM/dd";
        public static readonly string DefaultUsername = "Unknown";
        public static readonly JsonSerializerOptions DefaultSerializerOptions = new()
        {
            PropertyNamingPolicy = null,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };

        //Routes
        public const string BaseUsersRoute = "users";
        public const string FilterUsersRoute = "filter";
        public const string CreateUsersRoute = "create";
        public const string DetailsUserRoute = "/details/{id}/{mode?}";
        public const string DeleteUserRoute = "delete/{id}";

    }
}
