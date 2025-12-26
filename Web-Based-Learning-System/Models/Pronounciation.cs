using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_Based_Learning_System.Models;

public class Pronunciation
{
    [Key]
    public int Id { get; set; }

    public int CourseId { get; set; }
    [ForeignKey("CourseId")]
    public Course Course { get; set; }

    [Required]
    public string Word { get; set; }

    public string Phonetic { get; set; } // Optional: IPA or phonetic spelling

    public string AudioPath { get; set; } // Recommended over byte[]
}
