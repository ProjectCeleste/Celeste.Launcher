namespace ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Enum
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
        InternalServerError,
        UsernameAlreadyUsed,
        EmailAlreadyUsed,
        UserNotFound,
        YouAreBanned,
        Unknow
    }
}