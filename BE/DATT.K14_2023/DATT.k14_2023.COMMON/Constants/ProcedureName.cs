using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.Constants
{
    public class ProcedureName
    {
        /// <summary>
        /// Procedure Get
        /// </summary>
        public static string Get = "Proc_{0}_Get{1}";

        /// <summary>
        /// Procedure GetById
        /// </summary>
        public static string GetById = "Proc_{0}_GetById";

        /// <summary>
        /// Procedure Insert
        /// </summary>
        public static string Insert = "Proc_{0}_InsertOne";

        /// <summary>
        /// Procedure Update
        /// </summary>
        public static string Update = "Proc_{0}_UpdateOne";

        /// <summary>
        /// Procedure Delete
        /// </summary>
        public static string Delete = "Proc_{0}_DeleteOne";

        public static string Check = "Proc_{0}_Check{1}";

        /// <summary>
        /// Procedure Check Code
        /// </summary>
        public static string CheckCode = "Proc_{0}_Check{1}Code";
    }
}
