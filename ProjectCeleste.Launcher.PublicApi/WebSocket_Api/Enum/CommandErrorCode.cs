
namespace ProjectCeleste.Launcher.PublicApi.WebSocket_Api.WebSocket.Enum
{
    public enum CommandErrorCode
    {
        None,
        InvalidEmail,
        InvalidUsername,
        InvalidUsernameLength,
        InvalidPassword,
        InvalidPasswordLength,
        InvalidNewPassword,
        InvalidNewPasswordLength,
        InvalidPasswordMatch,
        InvalidVerifyKey,
        InvalidRequestSpam,
        Unknow,
    }
}