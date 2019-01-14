using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tinastest4
{
    class BSLibs
    {
        public static void SetEnable(Control ctrlTarget, bool bSet, bool bEdit)
        {
            foreach (Control c in ctrlTarget.Controls)
            {
                if ((c.GetType() != typeof(Label)))
                {
                    c.Enabled = bSet;
                    if (!bEdit && (c.GetType() != typeof(Button)))
                    {
                        c.Text = "";
                    }
                }
            }
        }

        public static string CheckString(object obj)
        {
            string szRet = "";
            try
            {
                szRet = obj.ToString();
            }
            catch (Exception e)
            {
                szRet = "";
            }
            return szRet;
        }

    }
}