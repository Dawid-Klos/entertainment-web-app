using Entertainment_web_app.Models.Auth;

namespace Entertainment_web_app.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<ApplicationUser>> GetAll();
    Task<ApplicationUser?> GetById(string id);
    void Add(ApplicationUser user);
    void Update(ApplicationUser user);
    void Delete(ApplicationUser user);
}
