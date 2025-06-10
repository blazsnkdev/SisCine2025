using SistemaCineMVC.Models;

namespace SistemaCineMVC.Data.Repo
{
    public interface IRepository<T> where T : class
    {

        Task<int> CountEntityAsync();



    }
}
