using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace K_Burns_JewelleryStore.Models
{
    public class Customer : User
    {
        [Display(Name = "Customer Type")]
        public CustomerType CustomerType { get; set; }

        //Navigational Properties
        //ORDER***
        public virtual ICollection<Order> Orders { get; set; }//MANY

        //CARD***
        public virtual ICollection<Card> Cards { get; set; }//MANY

        //0 args CONSTRUCTOR
        public Customer():base()
        {
            Orders = new List<Order>();
            Cards = new List<Card>();
        }
    }

    //private customers - refers to users that will place orders and pay at the point of sale
    //trade customers - refers to users that will buy in bulk and might pay by invoice
    public enum CustomerType
    {
        Private,
        Trade
    }
}