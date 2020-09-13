using CompanySite.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanySite.Domain
{
    public class DataManager//удобное управление данными через этот класс
    {
        public ITextFieldsRepositoriy TextFields { get; set; }
        public IServiceItemsRepository ServiceItems { get; set; }
        public DataManager(ITextFieldsRepositoriy textFieldsRepositoriy, IServiceItemsRepository serviceItemsRepository)
        {
            TextFields = textFieldsRepositoriy;
            ServiceItems = serviceItemsRepository;
        }
    }
}
