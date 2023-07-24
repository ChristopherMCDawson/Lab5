// Prediction.cs
using System.ComponentModel.DataAnnotations;

public class Prediction
{
    // Primary key
    [Key]
    public int PredictionId { get; set; }

    [Required]
    [MaxLength(100)] // Change the max length accordingly
    public string FileName { get; set; }

    [Required]
    [Url]
    public string Url { get; set; }

    [Required]
    public Question Question { get; set; }
}
