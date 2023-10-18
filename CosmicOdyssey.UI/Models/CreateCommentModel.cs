using System.ComponentModel.DataAnnotations;

namespace CosmicOdyssey.UI.Models;

public class CreateCommentModel
{
    [Required]
    [StringLength(1000)]
    [MinLength(1)]
    public string Body { get; set; }
}
