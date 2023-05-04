using DATT.k14_2023.DL.DashBoardDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.K14_2023.BL.DashBoardBL
{
    public class DashBoardBL : IDashBoardBL
    {
        private IDashBoardDL _dashBoardDL;
        public DashBoardBL(IDashBoardDL dashBoardDL)
        {
            _dashBoardDL = dashBoardDL;
        }

        public dynamic getDashBoard()
        {
            return _dashBoardDL.getDashBoard();
        }

        public dynamic getDashBoardChart()
        {
            return _dashBoardDL.getDashBoardChart();
        }
    }
}
