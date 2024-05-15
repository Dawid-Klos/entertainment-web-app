using Entertainment_web_app.Data;

namespace Entertainment_web_app.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<ApplicationUser>> GetAll();
    Task<ApplicationUser?> GetById(string id);
    Task Add(ApplicationUser user);
    Task Update(ApplicationUser user);
    Task Delete(ApplicationUser user);
}
