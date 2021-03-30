using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront2.DATA.EF//.Metadata - Namespace Must Match
                          //the location of the original classes
{
    //class DBStoreFront2Metadata
    //{
    //    //public partial Department()
    //    //{

    //    //}
    //}

    #region Department
    public class DepartmentMetadata
    {
        [Display(Name = "Department")]
        [Required]
        [StringLength(20, ErrorMessage = "Department must not exceed 20 characters.")]
        public string DepName { get; set; }
    }
    //[MetadataType(typeof(Department))]
    //public partial class Department
    //{

    //}
    #endregion
    #region Employees
    public class EmployeesMetadata
    {
        [Display(Name = "Department ID")]
        [Required]
        public int DepID { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [StringLength(20, ErrorMessage = "First name must not exceed 20 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(20, ErrorMessage = "Last name must not exceed 20 characters")]
        public string LastName { get; set; }

        [Display(Name = "Reports To")]
        [StringLength(20, ErrorMessage = "Reports To must not exceed 20 characters")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string ReportsTo { get; set; }

        [Display(Name = "Birth Date")]
        [DisplayFormat(NullDisplayText = "[N/A]", DataFormatString = "{0:MM/dd/yy}")]
        public Nullable<System.DateTime> BirthDate { get; set; }

        //[Display(Name = "Position")]
        [Required]
        [StringLength(20, ErrorMessage = "Position must not exceed 20 characters")]
        public string Position { get; set; }

        [Display(Name = "Hire Date")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}")]
        public System.DateTime HireDate { get; set; }

        //[Display(Name = "Address")]
        [StringLength(50, ErrorMessage = "Address must not exceed 50 characters")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string Address { get; set; }

        //[Display(Name = "City")]
        [StringLength(20, ErrorMessage = "City must not exceed 20 characters")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string City { get; set; }

        //[Display(Name = "State")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "State must be 2 characters")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string State { get; set; }

        [Display(Name = "Postal Code")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string PostalCode { get; set; }
    }

    //[MetadataType(typeof(EmployeesMetadata))]
    //public partial class Employees
    //{
    //    [Display(Name = "Name")]
    //    public string FullName = $"{FirstName}";
    //}

    #endregion
    #region Locations
    public class LocationsMetadata
    {
        [Display(Name = "Location")]
        [Required]
        [StringLength(50, ErrorMessage = "Location must not exceed 50 characters")]
        public string LocationName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Address must not exceed 20 characters")]
        public string Address { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "City must be not exceed 20 characters")]
        public string City { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "State must be 2 characters")]
        public string State { get; set; }

        [Display(Name = "Postal Code")]
        [Required]
        [StringLength(10, ErrorMessage = "Address must not exceed 10 characters")]
        public string PostalCode { get; set; }
    }
    #endregion
    #region Orders
    public class OrdersMetadata
    {
        [Display(Name = "Product ID")]
        [Required]
        public int ProductID { get; set; }

        [Required]
        public short Quantity { get; set; }

        [Display(Name = "Total Cost")]
        [Required]
        public decimal TotalCost { get; set; }

        [Display(Name = "Date Ordered")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}")]
        public System.DateTime DateOrdered { get; set; }

        [Display(Name = "Date Shipped")]
        [DisplayFormat(NullDisplayText = "[N/A]", DataFormatString = "{0:MM/dd/yy}")]
        public Nullable<System.DateTime> DateShipped { get; set; }

        [Display(Name = "Date Arrived")]
        [DisplayFormat(NullDisplayText = "[N/A]", DataFormatString = "{0:MM/dd/yy}")]
        public Nullable<System.DateTime> DateReceived { get; set; }

        [Display(Name = "Location ID")]
        [Required]
        public int LocationID { get; set; }
    }
    #endregion
    #region Products
    public class ProductsMetadata
    {
        [Required]
        [Display(Name = "Product")]
        [StringLength(50, ErrorMessage = "Product name must not exceed 50 characters.")]
        public string ProdName { get; set; }

        [StringLength(100, ErrorMessage = "Product name must not exceed 100 characters.")]
        public string Description { get; set; }

        [Display(Name = "Unit Price")]
        [Required]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Unit Cost")]
        [Required]
        public decimal UnitCost { get; set; }

        public string ImgURL { get; set; }

        [Display(Name = "Department ID")]
        [Required]
        public int DepID { get; set; }

        [Display(Name = "Vendor ID")]
        [Required]
        public int VendorID { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "SKU must be 8 characters.")]
        public string SKU { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "UPC must be 12 characters.")]
        public string UPC { get; set; }
    }
    [MetadataType(typeof(ProductsMetadata))]
    public partial class Products{}
    #endregion
    #region ProductStatus
    public class ProductStatusMetadata
    {
        [Display(Name = "Product ID")]
        [Required]
        public int ProductID { get; set; }

        [Display(Name = "Location ID")]
        [Required]
        public int LocationID { get; set; }

        [Required]
        public short InStock { get; set; }

        [Display(Name = "Order ID")]
        public Nullable<int> OrderID { get; set; }

        //[StringLength(12, MinimumLength = 12, ErrorMessage = "Must input 'Discontinued' if product is no longer in production.")]
        public Nullable<bool> IsDiscontinued { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Section must not exceed 10 characters.")]
        public string Section { get; set; }

        [Required]
        public short Col { get; set; }

        [Required]
        public short Row { get; set; }
    }
    #endregion
    #region Vendors
    public class VendorsMetadata
    {
        [Display(Name = "Vendor")]
        [Required]
        [StringLength(30, ErrorMessage = "Vendor Name must not exceed 30 characters.")]
        public string VenName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Address must not exceed 50 characters.")]
        public string Address { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "City must not exceed 20 characters.")]
        public string City { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "State must be 2 characters.")]
        public string State { get; set; }

        [Display(Name = "Postal Code")]
        [Required]
        [StringLength(10, ErrorMessage = "Postal Code must not exceed 10 characters.")]
        public string PostalCode { get; set; }
    }
    #endregion
}
