using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLC_RTUEN01
{
    public class Global
    {
        static private MESAdapter s_objMESAdapter = null;

        static public MESAdapter MESAdpt
        {
            get
            {
                return s_objMESAdapter;
            }

            set
            {
                s_objMESAdapter = value;
            }
        }
    }
}
