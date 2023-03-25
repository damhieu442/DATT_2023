using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.Constants
{
    public class Attribute
    {
        public class NotEmpty : Attribute
        {
            #region Field
            public string ErrorMessage = string.Empty;
            #endregion

            #region Contractor
            public NotEmpty(string errorMessage)
            {
                ErrorMessage = errorMessage;
            }
            #endregion
        }
    }
}
