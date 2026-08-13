using LearnHub_Api.Contracts.Course;
using LearnHub_Api.Contracts.Lesson;
using Mapster;

namespace LearnHub_Api.Mapping
{
    public class MappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, ApplicationUser>()
                .Map(des => des.UserName, src => src.Email);

            config.NewConfig<Course, CourseResponse>()
               .Map(des => des.CategoryName, src => src.Category.Name)
               .Map(des => des.InstructorName, src => src.Instructor.FirstName + " " + src.Instructor.LastName);

        }
    }
}
