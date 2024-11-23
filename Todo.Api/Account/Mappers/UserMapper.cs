using Riok.Mapperly.Abstractions;

using Todo.Api.Account.Models;

namespace Todo.Api.Account.Mappers;
[Mapper(EnumMappingIgnoreCase = true, EnumMappingStrategy = EnumMappingStrategy.ByValue)]
public static partial class UserMapper
{
    [MapPropertyFromSource(nameof(UserEntity.Detail), Use = nameof(CreateDetail))]
    [MapPropertyFromSource(nameof(UserEntity.Security), Use = nameof(CreateSecurity))]
    [MapperIgnoreTarget(nameof(UserEntity.Key))]
    [MapPropertyFromSource(nameof(UserEntity.RegisterDate), Use = nameof(CreateDateOnly))]
    public static partial UserEntity Map(SignUpRequest request);
    [MapProperty(nameof(SecurityAccount.Password), nameof(SignUpRequest.Password))]
    [MapProperty(nameof(SecurityAccount.UserName), nameof(SignUpRequest.UserName))]
    [MapperIgnoreSource(nameof(SignUpRequest.FirstName))]
    [MapperIgnoreSource(nameof(SignUpRequest.LastName))]
    private static partial SecurityAccount CreateSecurity(SignUpRequest request);
    [MapProperty(nameof(DetailAccount.FirstName), nameof(SignUpRequest.FirstName))]
    [MapProperty(nameof(DetailAccount.LastName), nameof(SignUpRequest.LastName))]
    [MapperIgnoreSource(nameof(SignUpRequest.UserName))]
    [MapperIgnoreSource(nameof(SignUpRequest.Password))]
    private static partial DetailAccount CreateDetail(SignUpRequest request);
    private static DateOnly CreateDateOnly(SignUpRequest request) => DateOnly.FromDateTime(DateTimeOffset.UtcNow.DateTime);
}