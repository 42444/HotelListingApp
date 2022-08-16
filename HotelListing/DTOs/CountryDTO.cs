using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.DTOs
{
    public class CountryDTO:CreateCountryDTO
    {
        public int Id { get; set; }

        public IList<HotelDTO> Hotels { get; set; }
       
    }
    public class UpdateCountryDTO:CreateCountryDTO
    {
        public IList<PutHotelDTO> Hotels{ get; set; }
    }
    public class CreateCountryDTO
    {
       
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Country name is too long")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 3, ErrorMessage = "Short Country name is too long")]
        public string ShortName { get; set; }
    }
}
