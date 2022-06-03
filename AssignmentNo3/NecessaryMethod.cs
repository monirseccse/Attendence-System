using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentNo3
{
    public static class NecessaryMethod
    {
       public static int ConvertTypedValue(string typedvalue)
        {
            int.TryParse(typedvalue, out int value);
            return value;
        }
        public static (string username,string password)LoginInformation()
        {
            Console.Write("User Name :");
            string username = Console.ReadLine();
            Console.Write("Password :");
            string password = Console.ReadLine();
            return (username,password);

        }
    }
}
