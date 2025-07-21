using System.Text.Json.Serialization;

namespace AionClass.Frontend.Models.Auth
{
    public class AuthResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
