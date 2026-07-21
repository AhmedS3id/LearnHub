using Mapster;

namespace LearnHub_Api.Mapping
{
    public class MappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, ApplicationUser>()
                .Map(des => des.UserName, src => src.Email);
        }
    }
}
