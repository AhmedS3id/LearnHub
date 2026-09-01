namespace LearnHub_Api.Authentication.Filter
{
    public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
    {
    }
}
