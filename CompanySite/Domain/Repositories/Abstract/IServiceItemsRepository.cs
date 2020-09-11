using CompanySite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanySite.Domain.Repositories.Abstract
{
    public interface IServiceItemsRepository
    {
        IQueryable<ServiceItem> GetServiceItems();//метод возвращает все услуги
        ServiceItem GetServiceItemById(Guid id);//выбирает услугу по Id
        void SaveServiceItem(ServiceItem entity);//сохраняет изменения
        void DeleteServiceItem(Guid id);//удаляет услугу
    }
}
