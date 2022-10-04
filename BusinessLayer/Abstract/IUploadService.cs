using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IUploadService
    {
        //List<Image> GetAllCarImages();
        bool AddCarImage(Image carImage, IFormFile file);
        //IResult DeleteCarImage(Image carImage);
        //IResult UpdateCarImage(Image carImage, IFormFile file);
        //Image GetCarImageById(int carImageId);
        //List<Image> GetAllCarImagesByCarId(int carId);
    }
}
