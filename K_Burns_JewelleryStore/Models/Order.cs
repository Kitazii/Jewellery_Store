using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace K_Burns_JewelleryStore.Models
{
    public class Order
    {
        [Display(Name = "Order Number")]
        public int OrderId { get; set; }

        [Display(Name = "Date")]
        public DateTime? OrderDate { get; set; }

        [Display(Name = "Date Cancelled")]
        public DateTime? DateCancelled { get; set; }

        [Display(Name = "Total")]
        public decimal OrderTotal { get; set; }

        [Display(Name = "Paid")]
        public bool IsPaid { get; set; }

        public OrderStatus OrderStatus { get; set; }

        //Navigational Properties
        //ORDERLINES***
        public virtual ICollection<OrderLine> OrderLines { get; set; }//MANY

        //CUSTOMER***
        [ForeignKey("Customer")]
        public string UserId { get; set; }
        public Customer Customer { get; set; }//ONE

        //PAYMENT***
        public Payment Payment { get; set; }//ONE

        public Order()
        {
            OrderLines = new List<OrderLine>();
        }
    }

    //ENUM
    public enum OrderStatus { Started, Placed, Delivery, Dispatched, Completed}
}