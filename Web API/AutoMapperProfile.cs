using AutoMapper;
using Entity_Layer.Entities;
using Entity_Layer.Models;
namespace Web_API
{
    //Auto mapper ekledikten sonra profil olarak da eklemek gerekiyor.
    public class AutoMapperProfile : Profile 
    {
        public AutoMapperProfile() 
        {
            CreateMap<User, LoginUserModel>();
            CreateMap<RegisterUserModel,User >();
        }
    }
}
