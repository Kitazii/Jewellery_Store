using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace K_Burns_JewelleryStore.Models
{
    public class OrderLine
    {
        [Key]
        public int OrderLineId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [Display(Name = "Amount")]
        public decimal LineTotal { get; set; }

        //Navigational properties
        //ORDER***
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }//ONE

        //PRODUCT***
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }//ONE
    }
}