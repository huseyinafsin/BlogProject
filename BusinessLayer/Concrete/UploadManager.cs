using BusinessLayer.Abstract;
using BusinessLayer.Helpers;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UploadManager : IUploadService
    {
        private IGenericDal<Image> _fileDal;

        public UploadManager(IGenericDal<Image> fileDal)
        {
            _fileDal = fileDal;
        }

        public List<Image> GetAllCarImages()
        {
            return _fileDal.GetListAll(); ;
        }


        public bool AddCarImage(Image image, IFormFile file)
        {


            var uploadResult = FileHelperManager.Upload(file, "wwwroot/Images");

  
            var path = uploadResult.Remove(0, 8);
            image.ImagePath = path;

            image.Date = DateTime.Now;
            _fileDal.Insert(image);
            
            return true;


        }



        //public IResult DeleteCarImage(Image image)
        //{

        //    var deleteResult = FileHelperManager.Delete(image.ImagePath);

        //    if (!deleteResult.Success)
        //    {
        //        return new ErrorResult();
        //    }
        //    _carImageDal.Delete(carImage);
        //    return new SuccessResult();
        //}

        //public IResult UpdateCarImage(Image image, IFormFile file)
        //{

        //    var filePath = _carImageDal.Get(i => i.Id == image.Id);

        //    var updateResult = FileHelperManager.Update(file, filePath.ImagePath, Paths.CarImages);

        //    if (!updateResult.Success)
        //    {
        //        return new ErrorResult(Messages.FileDeletionException);
        //    }

        //    image.ImagePath = updateResult.Data;
        //    image.Date = DateTime.Now;

        //    _carImageDal.Update(image);

        //    return new SuccessResult();

        //}

     
        //public IDataResult<CarImage> GetCarImageById(int carImageId)
        //{
        //    return new SuccessDataResult<CarImage>(_carImageDal.Get(i => i.Id == carImageId));
        //}

        //public IDataResult<List<Image>> GetAllCarImagesByCarId(int carId)
        //{
        //    var result = new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(x => x.CarId == carId));
        //    if (result.Data.Count == 0)
        //    {
        //        result.Data.Add(_carImageDal.Get(x => x.Id == 1));

        //        return result;
        //    }

        //    return result;
        //}


        // ------ Business Rules ca
        //private IResult CheckImageRestriction(int carID)
        //{
        //    var result = _carImageDal.GetAll(c => c.CarId == carID).Count;

        //    if (result >= 5)
        //    {
        //        return new ErrorResult(Messages.CheckImageRestriction);
        //    }

        //    return new SuccessResult();
        //}


    }
}
