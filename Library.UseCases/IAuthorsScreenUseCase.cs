using Library.Core.Models;

namespace Library.UseCases
{
    public interface IAuthorsScreenUseCase
    {
        Task<IEnumerable<Author>> ViewAuthorsAsync();
    }
}