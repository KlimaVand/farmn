using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testOfEverything
{
    class CalculateingOld
    {
        private List<string> getRotationNames()
        {
            List<string> rotationNames = new List<string>();
            //for (int i = 1; i < 14; i++)
            //{
            //    string tmp = "k" + i.ToString();
            //    rotationNames.Add(tmp);
            //}
            for (int i = 1; i < 3; i++)
            {
                string tmp = "s" + i.ToString();
            rotationNames.Add(tmp);
            }
            //for (int i = 1; i < 11; i++)
            //{
            //    string tmp = "g" + i.ToString();
            //    rotationNames.Add(tmp);
            //}

            return rotationNames;
        }
        private void callOld(decimal ind, string indput, int i)
        {
            XPressCalculation1.RotationSoapClient rotationService = new XPressCalculation1.RotationSoapClient();
            double returnValue = rotationService.CalcRotAftercropXPress(indput, ind, i);
           
        }
        public void calc()
        {
            List<string> ListOfRotationName = getRotationNames();
            //for (decimal i = 0.19m; i < 5.01m; i = i + 0.01m)
            for (decimal i = 7.76m; i < 8.01m; i = i + 0.01m)
            {
                //for (int j = 0; j < 36; j++)
                for (int j = 0; j < ListOfRotationName.Count(); j++)
                //for (int j = 0; j < 1; j++)
                {
                    callOld(i, ListOfRotationName.ElementAt(j), 1);
                    callOld(i, ListOfRotationName.ElementAt(j), 2);
                }
            }
            //callOld(21.61m, "G10", 2);
        }
    }
}
