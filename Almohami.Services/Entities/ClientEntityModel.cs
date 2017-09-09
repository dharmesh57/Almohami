using Almohami.Data.AlmohamiModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Entities
{
    public class ClientEntityModel : BaseViewModel
    {
        public ClientEntityModel()
        {
            ClientList = new List<Client>();
           
        }
        [Key]
        public long ClientID { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use only letters")]
        [Display(Name="Name")]
        [StringLength(100)]
        public string ClientName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use only letters")]
         [Display(Name = "Name-2")]
        [StringLength(100)]
        public string ClientName2 { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Civil ID No must be numeric")]
        [Display(Name = "Civil ID NO")]
        [StringLength(50)]
        public string ClientCivilId { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [Display(Name = "Company Name")]
        [StringLength(100)]
        public string ClientCompany { get; set; }

         [Display(Name = "Notes")]
        public string ClientNotes { get; set; }

         [Display(Name = "Profile Picture")]
        public string ClientAvtar { get; set; }

        [Required]
         [Display(Name = "Email ID")]
         [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Enter Valid Email Id")]
        [StringLength(100)]
        public string ClientEmailId { get; set; }

        public string CountryCode { get; set; }

        [Required]
         [Display(Name = "Mobile No")]
         [RegularExpression(@"^\d{8}$", ErrorMessage = "Not a valid Phone number Format(xxxxxxxx)")]
        [StringLength(50)]
        public string ClientMobileNo { get; set; }

        [Display(Name = "Office No")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Not a valid Phone number Format(xxxxxxxx)")]
        [StringLength(50)]
        public string ClientOfficeNo { get; set; }

        [Display(Name = "Fax No")]
        [StringLength(50)]
        public string ClientFaxNo { get; set; }

        [Display(Name = "Address")]
        [StringLength(500)]
        public string ClientAddress { get; set; }

        [Display(Name = "Website")]
        [StringLength(100)]
        public string ClientWebsite { get; set; }

        public bool? ClientStatus { get; set; }

        public bool? ClientDelete { get; set; }

        public DateTime? ClientCreatedDate { get; set; }

        public long? ClientCreatedBy { get; set; }

        public DateTime? ClientLastModifiedDate { get; set; }

        public long? ClientLastModifiedBy { get; set; }

      
        public List<Client> ClientList{get;set;}

        public string getseterror { get; set; }

        public int ar { get; set; }

        public decimal filesize { get; set; }
    }
}
