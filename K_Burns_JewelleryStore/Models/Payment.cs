using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace K_Burns_JewelleryStore.Models
{
    public class Payment
    {
        [Key, ForeignKey("Order")]
        public int PaymentId { get; set; }

        [Display(Name = "Payment Total")]
        public decimal PaymentTotal { get; set; }

        [Display(Name = "Payment Date")]
        public DateTime? PaymentDate { get; set; }

        public bool IsSuccessful { get; set; }
        public bool IsRefunded { get; set; }

        //Navigational Properties
        //ORDER***
        public Order Order { get; set; }//ONE

        //CARD***
        [ForeignKey("Card")]
        public int CardId { get; set; }
        public Card Card { get; set; }//ONE
    }
}