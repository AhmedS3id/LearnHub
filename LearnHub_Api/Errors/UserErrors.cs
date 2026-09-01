namespace LearnHub_Api.Errors;

public static class UserErrors
{
    public static readonly Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "Invalid email or password.",
        StatusCodes.Status401Unauthorized);

    public static readonly Error UserDisabled = new(
        "User.Disabled",
        "Your account has been disabled. Please contact the administrator.",
        StatusCodes.Status403Forbidden);

    public static readonly Error UserLockedOut = new(
        "User.LockedOut",
        "Your account has been locked. Please try again later or contact the administrator.",
        StatusCodes.Status423Locked);

    public static readonly Error InvalidJwtToken = new(
        "User.InvalidJwtToken",
        "The access token is invalid.",
        StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidRefreshToken = new(
        "User.InvalidRefreshToken",
        "The refresh token is invalid or expired.",
        StatusCodes.Status401Unauthorized);

    public static readonly Error EmailAlreadyExists = new(
        "User.EmailAlreadyExists",
        "A user with this email already exists.",
        StatusCodes.Status409Conflict);

    public static readonly Error AlreadyInstructor = new(
        "User.AlreadyInstructor",
        "This user with this Role already exists.",
        StatusCodes.Status409Conflict);

    public static readonly Error EmailNotConfirmed = new(
        "User.EmailNotConfirmed",
        "Please confirm your email address before signing in.",
        StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidConfirmationCode = new(
        "User.InvalidConfirmationCode",
        "The confirmation code is invalid.",
        StatusCodes.Status400BadRequest);

    public static readonly Error EmailAlreadyConfirmed = new(
        "User.EmailAlreadyConfirmed",
        "The email address has already been confirmed.",
        StatusCodes.Status409Conflict);

    public static readonly Error UserNotFound = new(
        "User.NotFound",
        "User not found.",
        StatusCodes.Status404NotFound);

    public static readonly Error InvalidCurrentPassword = new(
        "User.InvalidCurrentPassword",
        "The current password is incorrect.",
        StatusCodes.Status400BadRequest);

        public static readonly Error RoleAssignmentFailed = new(
        "User.RoleAssignmentFailed",
        "Role Assignment Failed.",
        StatusCodes.Status400BadRequest);

    public static readonly Error PasswordMismatch = new(
        "User.PasswordMismatch",
        "The new password and confirmation password do not match.",
        StatusCodes.Status400BadRequest);
}