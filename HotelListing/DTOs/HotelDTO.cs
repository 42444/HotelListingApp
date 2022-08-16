using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.DTOs
{
    public class CreateHotelDTO
    {
        [Required]
        [StringLength(maximumLength:50,ErrorMessage ="Hotel name is too long")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength:250,ErrorMessage ="Adress name is too long")]
        public string Adress { get; set; }
        [Required]
        [Range(1,5)]
        public double Rating { get; set; }
        [Required]
        public int CountryId { get; set; }
    }
    public class UpdateHotelDTO:CreateHotelDTO
    {

    }

    public class HotelDTO:CreateHotelDTO
    {
        public int Id { get; set; }
        public CountryDTO Country { get; set; }
    }

    public class PutHotelDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Hotel name is too long")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "Adress name is too long")]
        public string Adress { get; set; }
        [Required]
        [Range(1, 5)]
        public double Rating { get; set; }
        
        public int CountryId { get; set; }
    }
}
