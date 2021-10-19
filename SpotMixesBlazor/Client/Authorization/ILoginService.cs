using System.Threading.Tasks;

namespace SpotMixesBlazor.Client.Authorization
{
    public interface ILoginService
    {
        Task Login(string token);
        Task Logout();
    }
}