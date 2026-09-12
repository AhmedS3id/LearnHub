namespace LearnHub_API.Abstractions.Consts
{
    public static class Permissions
    {
        public static string Type { get; } = "permissions";

        public const string ManageUsers = "users:manage";
        // Category
        public const string GetCategories = "categories:read";
        public const string AddCategories = "categories:add";
        public const string UpdateCategories = "categories:update";
        public const string DeleteCategories = "categories:delete";

        // Course
        public const string GetCourses = "courses:read";
        public const string AddCourses = "courses:add";
        public const string UpdateCourses = "courses:update";
        public const string DeleteCourses = "courses:delete";

        // Enrollment
        public const string GetEnrollments = "enrollments:read";
        public const string AddEnrollments = "enrollments:add";
        public const string UpdateEnrollments = "enrollments:update";

        // Lesson
        public const string GetLessons = "lessons:read";
        public const string AddLessons = "lessons:add";
        public const string UpdateLessons = "lessons:update";
        public const string DeleteLessons = "lessons:delete";

        // Section
        public const string GetSections = "sections:read";
        public const string AddSections = "sections:add";
        public const string UpdateSections = "sections:update";
        public const string DeleteSections = "sections:delete";

        // Review
        public const string GetReviews = "reviews:read";
        public const string AddReviews = "reviews:add";
        public const string UpdateReviews = "reviews:update";
        public const string DeleteReviews = "reviews:delete";

        // Me (Profile)
        public const string GetProfile = "profile:read";
        public const string UpdateProfile = "profile:update";

        public static IList<string> GetAllPermissions() => [.. typeof(Permissions)
        .GetFields()
        .Select(x => (string)x.GetValue(null)!)];
    }
}