using System;
using System.Collections.Generic;
using System.Text;

namespace ProductChecker.Models
{
    class InitDatabase
    {
        public InitDatabase()
        {
            Item.InitItem();
            History.InitHistory();
            Setting.InitSetting();
            Console.WriteLine("Co init");
        }
    }
}
