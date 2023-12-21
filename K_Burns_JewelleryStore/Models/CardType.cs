using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace K_Burns_JewelleryStore.Models
{
    public class CardType
    {
        [Key]
        public int CardTypeId { get; set; }

        public string CardTypeName { get; set; }
    }
}