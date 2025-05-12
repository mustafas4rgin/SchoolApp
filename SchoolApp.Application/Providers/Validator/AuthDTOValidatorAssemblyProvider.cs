using SchoolApp.Application.DTOValidators.Auth;

namespace SchoolApp.Application.Providers.Validator;

public class AuthDTOValidatorAssemblyProvider
{
    public static Type[] GetValidatorAssemblies()
    {
        return new[]
        {
            typeof(LoginDTOValidator)
        };
    }
}