using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.Services;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results;
using SchoolApp.Domain.Results.WithData;

namespace SchoolApp.Application.Services;

public class RoleService : GenericService<Role>, IRoleService
{
    private readonly IGenericRepository _genericRepository;
    public RoleService(
        IGenericRepository genericRepository,
        IValidator<Role> validator
    ) : base(genericRepository,validator)
    {
        _genericRepository = genericRepository;
    }

    public async Task<IServiceResultWithData<IEnumerable<Role>>> GetRolesWithIncludesAsync(QueryParameters param)
    {
        try
        {
            var query = _genericRepository.GetAll<Role>();

            if (!string.IsNullOrWhiteSpace(param.Include))
            {
                var includes = param.Include.Split(',',StringSplitOptions.RemoveEmptyEntries);

                foreach (var inc in includes.Select(i => i.Trim().ToLower()))
                {
                    if (inc == "users")
                        query = query.Include(r => r.Users);
                }
            }
            
            var roles = await query.Where(r => !r.IsDeleted)
                            .Where(p => string.IsNullOrEmpty(param.Search) || p.Name.ToLower().Contains(param.Search.ToLower()))
                            .ToListAsync();

            if (!roles.Any())
                return new ErrorResultWithData<IEnumerable<Role>>("There is no role.");

            return new SuccessResultWithData<IEnumerable<Role>>("Roles found.",roles);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<IEnumerable<Role>>(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<Role>> GetRoleByIdWithIncludesAsync(int id, QueryParameters param)
    {
        try
        {
            var query = _genericRepository.GetAll<Role>();

            if (!string.IsNullOrEmpty(param.Include))
            {
                var includes = param.Include.Split(',',StringSplitOptions.RemoveEmptyEntries);

                foreach (var inc in includes.Select(i => i.Trim().ToLower()))
                {
                    if (inc == "users")
                        query = query.Include(r => r.Users);
                }
            }
            
            var role = await query.Where(r => !r.IsDeleted)
                            .Where(p => string.IsNullOrEmpty(param.Search) || p.Name.ToLower().Contains(param.Search.ToLower()))
                            .FirstOrDefaultAsync(r => r.Id == id);

            if (role is null)
                return new ErrorResultWithData<Role>($"There is no role with ID : {id}");

            return new SuccessResultWithData<Role>("Role found.",role);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<Role>(ex.Message);
        }
    }
    public override Task<IServiceResultWithData<IEnumerable<Role>>> GetAllAsync()
    {
        throw new NotSupportedException("Use GetAllRolesWithIncludesAsync instead.");
    }
    public override Task<IServiceResultWithData<Role>> GetByIdAsync(int id)
    {
        throw new NotSupportedException("Use GetRoleByIdWithIncludesAsync instead.");
    }
}