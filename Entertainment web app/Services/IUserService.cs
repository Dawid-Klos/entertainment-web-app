using Entertainment_web_app.Data;

namespace Entertainment_web_app.Services;

public interface IApplicationUserService
{
    Task<IEnumerable<ApplicationUser>> GetAll();
    Task<ApplicationUser> GetById(int id);
    Task Update(ApplicationUser user, string password = null);
    Task Delete(int id);
}
