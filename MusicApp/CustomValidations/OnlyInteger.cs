using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicApp.CustomValidations
{
    public class OnlyInteger : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int x = 2;
            return Object.ReferenceEquals(x.GetType(), value.GetType());
        }
    }
}