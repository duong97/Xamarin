using ProductChecker.Models;
using ProductChecker.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace ProductChecker.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        public ObservableCollection<History> Htr { get; set; }
        public Command LoadItemsCommand { get; set; }
        public String PageTitle { get; set; }

        public HistoryViewModel(int type)
        {
            PageTitle = Constant.MENU_LIST[type];
            Htr = new ObservableCollection<History>();

            LoadItemsCommand = new Command(() => ExecuteLoadItemsCommand(type));

            // Binding new item when press save on create (listen from MessagingCenter.send)
            MessagingCenter.Subscribe<NewIECPage, History>(this, "modifyHistory", (obj, item) =>
            {
                refreshList(type);
            });
            MessagingCenter.Subscribe<HistoryDetailPage, History>(this, "modifyHistory", (obj, item) =>
            {
                refreshList(type);
            });
        }

        // Refresh list view
        void ExecuteLoadItemsCommand(int type)
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                Htr.Clear();
                var items = History.GetAllByType(type);
                foreach (var item in items)
                {
                    Htr.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void refreshList(int type)
        {
            Htr.Clear();
            var items = History.GetAllByType(type);
            foreach (var item in items)
            {
                Htr.Add(item);
            }
        }
    }
}
