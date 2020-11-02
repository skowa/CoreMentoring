using System.Collections.Generic;

namespace Northwind.Business.Interfaces.Services
{
    public interface IImagesService
    {
        bool VerifyImage(IEnumerable<byte> image);

        IEnumerable<byte> RestoreImage(IEnumerable<byte> image);
    }
}
