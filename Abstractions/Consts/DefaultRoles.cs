namespace LearnHub_Api.Abstractions.Consts
{
    public static class DefaultRoles
    {
        public const string Admin = nameof(Admin);
        public const string AdminRoleId = "01a095ed-eff4-7d35-917c-b136c6dec33f";
        public const string AdminRoleConcurrencyStamp = "01a095ed-eff4-798c-93e9-02ebcf95f00b";

        public const string Instructor = nameof(Instructor);
        public const string InstructorRoleId = "01a095ed-eff4-7f1e-9b09-951b5cafecde";
        public const string InstructorRoleConcurrencyStamp = "01a095ed-eff4-76fd-bc6a-6b2661ccb43d";

        public const string Member = nameof(Member);
        public const string MemberRoleId = "01a095ed-eff4-708d-94d3-fcd6dda415e4";
        public const string MemberRoleConcurrencyStamp = "01a095ed-eff4-7872-a394-da5724758a8a";

    }
}
