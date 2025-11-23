using BookstoreManagerAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace BookstoreManagerAPI.Communication.Requests;

public class RequestUpdateBookJson
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 120 caracteres")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "O autor é obrigatório.")]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "O autor deve ter entre 2 e 120 caracteres")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "O gênero é obrigatório.")]
    public Genre Genre { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser maior ou igual a 0.")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "O estoque deve ser maior ou igual a 0.")]
    public int Stock { get; set; }
}
