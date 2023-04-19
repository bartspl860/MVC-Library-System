using Library.DAL;
using Library.Model;
using System.Linq;


namespace Library.BLL
{
    public class PublishingHouseService : IPublishingHouseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PublishingHouseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public PublishingHouse? FindPublisher(int id)
        {
            return _unitOfWork.PublishingHousesRepository.GetPublishingHouse(id);
        }
    }
}
