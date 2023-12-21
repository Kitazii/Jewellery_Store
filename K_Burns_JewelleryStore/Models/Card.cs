using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace K_Burns_JewelleryStore.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        [Display(Name = "Name on card")]
        public string CardholderName { get; set; }

        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Display(Name = "CVV2")]
        public string Cvv2 { get; set; }

        [Display(Name = "Expiry Month")]
        public int ExpiryMonth { get; set; }

        [Display(Name = "Expiry Year")]
        public int ExpiryYear { get; set; }

        //Navigational Properties
        //CUSTOMER***
        [ForeignKey("Customer")]
        public string UserId { get; set; }
        public Customer Customer { get; set; }//ONE

        //PAYMENT***
        public virtual ICollection<Payment> Payments { get; set;}//MANY

        //0 args CONSTRUCTOR
        public Card()
        {
            Payments = new List<Payment>();
        }
    }
}