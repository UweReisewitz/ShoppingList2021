using System.IO;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using ShoppingList2021.Core.Types;

namespace ShoppingList2021.Core
{
    public class Photo : IPhoto
    {
        public async Task<byte[]> PickPhoto()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return null;
            }

            var pickMediaOptions = new Plugin.Media.Abstractions.PickMediaOptions()
            {
                PhotoSize = PhotoSize.Full
            };

            var file = await CrossMedia.Current.PickPhotoAsync();

            return file == null ? null : SetImageProperties(file);
        }

        public async Task<byte[]> TakePhoto()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return null;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "ShoppingList",
                SaveToAlbum = true
            });

            return file == null ? null : SetImageProperties(file);
        }

        private byte[] SetImageProperties(MediaFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
