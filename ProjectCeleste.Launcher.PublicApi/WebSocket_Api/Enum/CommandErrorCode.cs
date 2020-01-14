
namespace ProjectCeleste.Launcher.PublicApi.WebSocket_Api.Enum
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