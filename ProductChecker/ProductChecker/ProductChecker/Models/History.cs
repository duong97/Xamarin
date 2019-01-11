using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace ProductChecker.Models
{
    [Table("History")]
    public class History
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ItemName { get; set; }
        public long ItemBarcode { get; set; }
        public string Date { get; set; }
        public int Type { get; set; } // import, export
        public int Amount { get; set; }

        public static void InitHistory()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatabaseName.txt");
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<History>();
        }

        public static void Add(History htr)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatabaseName.txt");
            var db = new SQLiteConnection(dbPath);
            db.Insert(htr);
            UpdateNumberItem(htr);
        }

        public static void Update(History oldItem)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatabaseName.txt");
            var db = new SQLiteConnection(dbPath);
            History prevHtr = db.Get<History>(oldItem.Id);
            UpdateNumberItem(oldItem, prevHtr.Amount);
            db.Update(oldItem);
        }

        public static void Delete(int id)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatabaseName.txt");
            var db = new SQLiteConnection(dbPath);
            History itemDelete = db.Get<History>(id);
            UpdateNumberItem(itemDelete, itemDelete.Amount);
            db.Delete(itemDelete);
        }

        public static void UpdateNumberItem(History htr, int prevAmount = 0)
        {
            Item item = Item.Find("Barcode", htr.ItemBarcode.ToString());
            int number = htr.Amount - prevAmount;
            if (item != null)
            {
                switch (htr.Type)
                {
                    case Constant.MENU_TYPE_IMPORT:
                        item.Remain      += number;
                        item.TotalImport += number;
                        break;
                    case Constant.MENU_TYPE_EXPORT:
                        item.Remain      -= number;
                        item.TotalExport += number;
                        break;
                    case Constant.MENU_TYPE_CHECK:
                        item.Remain      = number;
                        break;
                };
                Item.Update(item);
            }
        }

        public static List<History> GetAll()
        {
            //ObservableCollection<History> ItemCollection = new ObservableCollection<History>();
            List<History> ItemCollection = new List<History>();
            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatabaseName.txt");
                var db = new SQLiteConnection(dbPath);
                var tb = db.Table<History>();
                if (tb == null) return ItemCollection;
                foreach (var item in tb)
                {
                    ItemCollection.Add(item);
                }
                return ItemCollection;
            } catch (SQLiteException s)
            {
                Console.WriteLine("SQL error: " + s.ToString());
                return ItemCollection;
            }
        }

        public static List<History> GetInRangeAll(DateTime from, DateTime to)
        {
            List<History> ItemCollection = new List<History>();

            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatabaseName.txt");
            var db = new SQLiteConnection(dbPath);
            var tb = db.Table<History>();
            foreach (var item in tb)
            {
                ItemCollection.Add(item);
            }
            return ItemCollection;
        }

        public static List<History> GetAllByType(int type)
        {
            List<History> ItemCollection = new List<History>();

            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatabaseName.txt");
            var db = new SQLiteConnection(dbPath);

            var listHrtByType = db.Query<History>("SELECT * FROM History WHERE Type = ?", type);
            foreach (var item in listHrtByType)
            {
                ItemCollection.Add(item);
            }
            return ItemCollection;
        }

        public static History Find(String field, String value)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatabaseName.txt");
            var db = new SQLiteConnection(dbPath);
            var item = db.Query<History>("SELECT * FROM History WHERE " + field + " = ? LIMIT 1", value);
            if (item.Count == 0)
            {
                return null;
            }
            else
            {
                return item[0];
            }
        }


    }
}
