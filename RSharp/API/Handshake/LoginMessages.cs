namespace RSharp.API.Handshake
{
    public enum LoginMessage
    {
        DELAY = 1,
        OK = 2,
        INVALID_CREDS = 3,
        ACCOUNT_DISABLED = 4,
        ACCOUNT_ONLINE = 5,
        SERVER_OFFLINE = 8,
        TOO_MANY_LOGINS = 16
    }
}
