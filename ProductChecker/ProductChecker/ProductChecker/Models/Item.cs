using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;

namespace ProductChecker.Models
{
    [Table("Item")]
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public long Barcode { get; set; }
        public string Exp { get; set; }
        public string Image { get; set; }
        public int Remain { get; set; }
        public int TotalImport { get; set; }
        public int TotalExport { get; set; }

        public static void InitItem()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatabaseName.txt");
            var db        = new SQLiteConnection(dbPath);
            db.CreateTable<Item>();
        }

        public static void Add(Item it)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatabaseName.txt");
            var db        = new SQLiteConnection(dbPath);
            db.Insert(it);
        }

        public static void Update(Item newItem)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatabaseName.txt");
            var db        = new SQLiteConnection(dbPath);
            db.Update(newItem);
        }

        public static void Delete(int id)
        {
            string dbPath   = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatabaseName.txt");
            var db          = new SQLiteConnection(dbPath);
            Item itemDelete = db.Get<Item>(id);
            db.Delete(itemDelete);
        }

        public static List<Item> GetAll()
        {
            List<Item> ItemCollection = new List<Item>();

            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatabaseName.txt");
            var db        = new SQLiteConnection(dbPath);
            var tb        = db.Table<Item>();
            foreach (var item in tb)
            {
                ItemCollection.Add(item);
            }
            return ItemCollection;
        }

        public static ObservableCollection<Item> GetAllObs()
        {
            ObservableCollection<Item> ItemCollection = new ObservableCollection<Item>();

            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatabaseName.txt");
            var db = new SQLiteConnection(dbPath);
            var tb = db.Table<Item>();
            foreach (var item in tb)
            {
                ItemCollection.Add(item);
            }
            return ItemCollection;
        }

        public static Item Find(String field, String value)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatabaseName.txt");
            var db        = new SQLiteConnection(dbPath);
            var item      = db.Query<Item>("SELECT * FROM Item WHERE " + field + " = ? LIMIT 1", value);
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