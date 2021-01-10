namespace api.Contracts
{
    public static class Authorization
    {
        public static readonly string DefaultPassword = "Alexander1%";
        public static string RefreshTokenCookieName { get; } = "refresh-token";
        public static readonly string AccessTokenCookieName = "access-token";
    }
}