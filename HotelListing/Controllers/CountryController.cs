using AutoMapper;
using HotelListing.Data;
using HotelListing.DTOs;
using HotelListing.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await _unitOfWork.Countries.GetAll();
                var results = _mapper.Map<IList<CountryDTO>>(countries);
                return Ok(results);
            }
            catch (Exception e)
            {

                _logger.LogError(e, $"Something went wrong:{nameof(GetCountries)}");

                return StatusCode(500);
            }
        }
        [HttpGet("{id:int}",Name ="GetCountry")]
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                var country = await _unitOfWork.Countries.Get(q => q.Id == id, new List<string> {"Hotels"});
                var result = _mapper.Map<CountryDTO>(country);
                return Ok(result);
            }
            catch (Exception e)
            {

                _logger.LogError(e, $"Something went wrong:{nameof(GetCountry)}");

                return StatusCode(500);
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCountry([FromBody]CreateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt int{nameof(CreateCountry)}");
                return BadRequest(ModelState);
            }

            try
            {
                var country = _mapper.Map<Country>(countryDTO);
                await _unitOfWork.Countries.Insert(country);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetCountry", new { id = country.Id }, country);
            }
            catch (Exception e)
            {

                _logger.LogError(e, $"Exception occured at {nameof(CreateCountry)}");
                return StatusCode(500, "INTERNAL SERVER ERROR.");
            }
        }
        [Authorize]
        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int id, UpdateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid || id<1)
            {
                _logger.LogError($"Invalid UPDATE attempt int{nameof(UpdateCountry)}");
                return BadRequest(ModelState);
            }
            try
            {
                var country = await _unitOfWork.Countries.Get(q => q.Id == id);
                if(country == null)
                {
                    return BadRequest();
                }
                _mapper.Map(countryDTO, country);
                _unitOfWork.Countries.Update(country);
               await _unitOfWork.Save();

                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Invalid UPDATE attempt in{nameof(UpdateCountry)}");
                return StatusCode(500, "INTERNAL SERVER ERROR.");

            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if(id<1)
            {
                _logger.LogError($"Invalid DELETE attempt int{nameof(DeleteCountry)}");
                return BadRequest();
            }
            try
            {
                var country = await _unitOfWork.Countries.Get(q => q.Id == id);
                if(id==null)
                {
                    _logger.LogError($"Invalid DELETE attempt int{nameof(DeleteCountry)}");
                    return BadRequest("Submited Data is invalid");
                }
                await _unitOfWork.Countries.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Invalid DELETE attempt in{nameof(DeleteCountry)}");
                return StatusCode(500, "INTERNAL SERVER ERROR.");
            }
        }

    }
}
