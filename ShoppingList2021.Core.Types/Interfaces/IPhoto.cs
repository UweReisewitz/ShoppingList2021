using System.Threading.Tasks;

namespace ShoppingList2021.Core.Types
{
    public interface IPhoto
    {
        Task<byte[]> TakePhoto();
        Task<byte[]> PickPhoto();
    }
}
