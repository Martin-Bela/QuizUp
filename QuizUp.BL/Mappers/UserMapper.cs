using QuizUp.DAL.Entities;
using Riok.Mapperly.Abstractions;
using QuizUp.BL.Models;

namespace QuizUp.BL.Mappers;

[Mapper]
public static partial class UserMapper
{
    public static partial UserDetailModel MapToUserDetailModel(this ApplicationUser applicationUser);
}
