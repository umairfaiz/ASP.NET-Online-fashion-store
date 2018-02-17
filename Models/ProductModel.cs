using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorrectTemp.Models
{
    
    public partial class Product
    {
        private int quantity =1;
        public int Quantity
        {
            get { return quantity; }
            set {quantity = value; }
        }
    }
}