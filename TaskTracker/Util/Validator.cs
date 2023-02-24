using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskTracker.Util
{
    class Validator
    {
        public static bool CheckEmpty(String str, Control cnt, ErrorProvider errorProvider)
        {
            bool isOk = true;
            if (str.Equals(""))
            {
                errorProvider.SetError(cnt, "Field could not be empty!");
                isOk = false;
            }
            return isOk;
        }

        public static bool CheckEmail(String str, Control cnt, ErrorProvider errorProvider)
        {
            bool isOk = true;
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase))
            {
                errorProvider.SetError(cnt, "Field must be an email address!");
                isOk = false;
            }
            return isOk;
        }

        public static bool CheckSelection(ComboBox cb, ErrorProvider errorProvider)
        {
            bool isOk = true;
            if (cb.SelectedIndex == -1)
            {
                errorProvider.SetError(cb, "Value must be selected!");
                isOk = false;
            }
            return isOk;
        }
    }
}
