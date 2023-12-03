using System.ComponentModel.DataAnnotations;

namespace Entertainment_web_app.Data;

public class Trending
{
    [Key]
    public int MovieId { get; set; }
    [Required]
    [StringLength(80)]
    public string ImgSmall { get; set; }
    [Required]
    [StringLength(80)]
    public string ImgLarge { get; set; }
}