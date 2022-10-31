using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entertainment_web_app.Models;

namespace Entertainment_web_app.Data;

public class UserMovies
{
    [Key]
    public string Id { get; set; }
    [Key]
    public int MovieId { get; set; }
    public bool IsBookmarked { get; set; }
    [ForeignKey("Id")]
    public virtual ApplicationUser User { get; set; }
    

}