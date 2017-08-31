using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinPathEditor
{
    static class GeneralUse
    {
        public static List<string> Split(string str, char delim)
        {
            List<string> data = new List<string>();
            string current = "";

            for (int i = 0; i < str.Length; ++i) {
                if (str[i] == delim) {
                    if (current.Length > 0) {
                        data.Add(current);
                    }
                    current = "";
                }
                else {
                    current += str[i];
                }
            }

            if (current.Length > 0) {
                data.Add(current);
            }

            return data;
        }

        public static string Join(List<string> strList, char delim)
        {
            string result = "";

            for (int i = 0; i < strList.Count - 1; ++i) {
                result += strList[i] + delim;
            }

            if (strList.Count >= 1) {
                result += strList[strList.Count - 1];
            }

            return result;
        }
    }
}
