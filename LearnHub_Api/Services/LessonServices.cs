namespace LearnHub_Api.Services
{
    public class LessonServices(ApplicationDbContext context) : ILessonService
    {
        private readonly ApplicationDbContext _context = context;

    }
}
