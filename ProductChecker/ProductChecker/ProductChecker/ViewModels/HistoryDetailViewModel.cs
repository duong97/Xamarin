using ProductChecker.Models;

namespace ProductChecker.ViewModels
{
    public class HistoryDetailViewModel : BaseViewModel
    {
        public History Item { get; set; }
        public HistoryDetailViewModel(History item = null)
        {
            Title = item?.ItemName;
            Item = item;
        }
    }
}
