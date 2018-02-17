using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorrectTemp
{
    public class random_numberGen
    {
        public static int randNumber()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 1000);
            return randomNumber;

        }
    }
}