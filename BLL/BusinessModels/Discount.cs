﻿using System;

namespace BLL.BusinessModels
{
   public class Discount
   {
       private decimal _value = 0;
       public decimal Value {
           get { return _value; }
       }

        public Discount(decimal val)
        {
            _value = val;
        }

        public  decimal GetDiscountedPrice(decimal sum)
        {
            if (DateTime.Now.Day == 1)
                return sum - sum*_value;
            return sum;
        }
    }
}
