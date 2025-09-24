using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using VS_Monopoly.Properties;

namespace VS_Monopoly
{
    internal class BoardGenerator
    {
        public static List<Property> Generate()
        {
            List<Property> propertyData;
            string[] properties = Resources.standard_uk.Split("\r\n",StringSplitOptions.RemoveEmptyEntries);
            propertyData = Property.Setup(properties);
            return propertyData;
        }

        public static void Assemble(List<Property> propertyData)
        {
            // TODO: add code to make the board look less like a variable and more like a board cause no one wants to play monopoly on a list of variables
            // TODO: add train station logic in property.cs cause currently it detects if theres a station but does nothing about it
            // TODO: fix the horrendous sight that is program.cs
            // TODO: make it work (aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaah)
        }

    }

}
