using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.DL.DashBoardDL
{
    public interface IDashBoardDL
    {
        dynamic getDashBoard();
        dynamic getDashBoardChart();
        dynamic getDashBoarPrice();
        dynamic getDashBoarCategory();
    }
}
