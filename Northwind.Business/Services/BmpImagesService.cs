using System.Collections.Generic;
using System.Linq;
using Northwind.Business.Interfaces.Services;

namespace Northwind.Business.Services
{
    public class BmpImagesService : IImagesService
    {
        private const int BrokenBytesAmount = 78;

        private readonly byte[] _bmpHeaders = new byte[] { 66, 77 };

        public bool VerifyImage(IEnumerable<byte> image)
        {
            var imageHeaders = image.Take(_bmpHeaders.Length).ToArray();
            for (var i = 0; i < _bmpHeaders.Length; i++)
            {
                if (_bmpHeaders[i] != imageHeaders[i])
                {
                    return false;
                }
            }

            return true;
        }

        public IEnumerable<byte> RestoreImage(IEnumerable<byte> image)
        {
            if (!VerifyImage(image))
            {
                return image.Skip(BrokenBytesAmount);
            }

            return image;
        }
    }
}
