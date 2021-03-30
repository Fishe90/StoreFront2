using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreFront2.DATA.EF;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StoreFront2.UI.MVC.Models
{
    public class CartItemViewModel
    {
        [Range(1, byte.MaxValue,ErrorMessage ="*Please enter a Quantity between 1 and 255.")]
        public int Qty { get; set; }

        public Products Product { get; set; }

        public CartItemViewModel(int qty, Products product)
        {
            Qty = qty;
            Product = product;
        }
    }
}