using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace K_Burns_JewelleryStore.Models
{
    public class Employee : User
    {
        [Display(Name = "Employement Status")]
        public EmploymentStatus EmploymentStatus { get; set; }
    }

    public enum EmploymentStatus
    {
        FullTime,
        PartTime
    }
}