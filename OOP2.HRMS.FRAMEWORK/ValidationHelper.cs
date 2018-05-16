using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace OOP2.HRMS.FRAMEWORK
{
    public static class ValidationHelper
    {
        public static bool IsIntValid(string value)
        {
            int id;
            return Int32.TryParse(value, out id);
        }

        public static bool IsStringValid(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            return true;
        }

        public static bool IsInputStringValid(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^[a-zA-Z\s]+$", RegexOptions.IgnoreCase))
            {
                return false;
            }

            return true;
        }

        public static bool IsEmailValid(string value)
        {
            if (!Regex.IsMatch(value, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", RegexOptions.IgnoreCase))
            {
                return false;
            }

            return true;
        }

        public static bool IsNameValid(string value)
        {
            if (!Regex.IsMatch(value, @"^([a-zA-Z\s]+\.)*[a-zA-Z\s]+$", RegexOptions.IgnoreCase))
            {
                return false;
            }

            return true;
        }

        public static bool IsContactValid(string value)
        {
            if (!Regex.IsMatch(value, @"^[\+]?[0-9]+$", RegexOptions.IgnoreCase))
            {
                return false;
            }

            return true;
        }

        public static bool IsNumberValid(string value)
        {
            if (!Regex.IsMatch(value, @"^[0-9]+$", RegexOptions.IgnoreCase))
            {
                return false;
            }

            return true;
        }



        public static bool IsDepartmentValid(string value)
        {
            if (!Regex.IsMatch(value, @"^[a-zA-Z\s]*[a-zA-Z\s]+$", RegexOptions.IgnoreCase))
            {
                return false;
            }
            return true;
        }


    }
}