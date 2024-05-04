using Entertainment_web_app.Models.User;

namespace Entertainment_web_app.Repositories;

public interface IApplicationUserRepository
{
    Task<IEnumerable<ApplicationUser>> GetAll();
    Task<ApplicationUser?> GetById(string id);
    void Add(ApplicationUser user);
    void Update(ApplicationUser user);
    void Delete(ApplicationUser user);
}
