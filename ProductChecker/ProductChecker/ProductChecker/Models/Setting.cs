using SQLite;
using System.Collections.Generic;
using System.IO;

namespace ProductChecker.Models
{
    [Table("Setting")]
    class Setting
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool IsDarkTheme { get; set; }
        public int ChartType { get; set; }

        public static void InitSetting()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatabaseName.txt");
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<Setting>();
            Setting st = new Setting
            {
                IsDarkTheme = false,
                ChartType = Constant.CHART_TYPE_LINE,
                Id = 1
            };
            db.Insert(st);
        }

        public static void Update(Setting st)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatabaseName.txt");
            var db = new SQLiteConnection(dbPath);
            db.Update(st);
        }

        public static Setting GetSetting()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatabaseName.txt");
            var db = new SQLiteConnection(dbPath);
            //List<Setting> ls = db.Query<Setting>("SELECT * FROM Setting LIMIT 1");
            return db.Get<Setting>(1);
            //return ls[0];
        }
    }
}
