using System.ComponentModel.DataAnnotations;

namespace CosmicOdyssey.UI.Models;

public class CreateBlogModel
{
    [Required]
    [StringLength(1000)]
    public string Body { get; set; }
}
