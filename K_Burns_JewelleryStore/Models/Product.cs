using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace K_Burns_JewelleryStore.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Unit Stock")]
        public int UnitsInStock { get; set; }

        [Display(Name = "Stock last updated")]
        public DateTime StockUpdatedOn { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }//storing path to product image in the database

        public bool Discontinued { get; set; }

        [Display(Name = "Sale")]
        public bool OnSale { get; set; }

        [NotMapped]
        public string Status { get; set; }//planning to use this for low stock

        //Navigational Properties
        //CATEGORY***
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }//ONE

        //ORDERLINES***
        public virtual ICollection<OrderLine> OrderLines { get; set; }//MANY


        //METHODS
        //updates units when new stock arrives
        public void ReStock(int quantity)
        {
            UnitsInStock += quantity;
        }

        //updates units when product is sold
        public void UpdateStock(int quantity)
        {
            UnitsInStock -= quantity;
        }

        //0 args CONSTRUCTOR
        public Product()
        {
            OrderLines = new List<OrderLine>();
        }
    }
}