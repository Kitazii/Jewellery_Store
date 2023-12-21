using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace K_Burns_JewelleryStore.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        public string Name { get; set; }

        //Navigational Properties
        public virtual ICollection<Product> Products { get; set; }//MANY

        //0 args CONSTRUCTOR
        public Category()
        {
            Products = new List<Product>();
        }
    }
}