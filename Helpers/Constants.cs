using System.ComponentModel;

namespace TherapEase.Helpers
{
    public class Constants
    {
        public enum Cookies
        {
            [Description("X-Access-Token")]
            AccessToken,
            [Description("X-Refresh-Token")]
            RefreshToken,
            [Description("X-User-Id")]
            UserId
        }
    }
}
