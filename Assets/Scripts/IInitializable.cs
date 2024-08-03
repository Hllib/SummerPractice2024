using System.Threading.Tasks;

namespace HlStudio
{
    public interface IInitializable
    {
        public Task Init();
    }
}