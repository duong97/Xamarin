using System;
using System.Collections.Generic;
using System.Text;

namespace ProductChecker.Models
{
    class Constant
    {
        public const int MENU_TYPE_IMPORT = 1;
        public const int MENU_TYPE_EXPORT = 2;
        public const int MENU_TYPE_CHECK  = 3;

        public const int CHART_TYPE_DONUT = 1;
        public const int CHART_TYPE_LINE  = 2;
        public const int CHART_TYPE_BAR   = 3;

        public static Dictionary<int, String> MENU_LIST = new Dictionary<int, String>
        {
            { MENU_TYPE_IMPORT, "Nhập Kho" },
            { MENU_TYPE_EXPORT, "Xuất Kho" },
            { MENU_TYPE_CHECK,  "Kiểm Kho" },
        };

        public static Dictionary<int, String> CHART_LIST = new Dictionary<int, String>
        {
            { Constant.CHART_TYPE_DONUT , "Biểu Đồ Tròn"},
            { Constant.CHART_TYPE_LINE , "Biểu Đồ Đưòng"},
            { Constant.CHART_TYPE_BAR , "Biểu Đồ Cột"},
        };
    }
}
