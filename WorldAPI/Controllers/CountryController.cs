using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Dtos;
using WorldAPI.Data;
using WorldAPI.Entities;
using WorldAPI.Interfaces;

namespace WorldAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryRepository countryRepository, IMapper mapper, ILogger<CountryController> logger)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetAll()
        {
            try
            {
                var countries = await _countryRepository.GetAll();

                var countriesDto = _mapper.Map<List<CountryDto>>(countries);

                if (countriesDto == null)
                {
                    return NoContent();
                }

                return Ok(countriesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CountryDto>> GetById(int id)
        {
            try
            {
                var country = await _countryRepository.GetById(id);

                if (country == null)
                {
                    _logger.LogError($"Error while try to get record id:{id}");
                    return NoContent();
                }

                var countryDto = _mapper.Map<CountryDto>(country);

                return Ok(countryDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateCountryDto>> Create([FromBody] CreateCountryDto countryDto)
        {
            try
            {
                var result = _countryRepository.IsRecordExist(x => x.Name == countryDto.Name);

                if (result)
                {
                    return Conflict("Country Already Exist");
                }

                var country = _mapper.Map<Country>(countryDto);

                await _countryRepository.Create(country);

                return CreatedAtAction("GetById", new { id = country.Id }, country);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Country>> Update(int id, [FromBody] UpdateCountryDto countryDto)
        {
            try
            {
                if (id != countryDto.Id || countryDto == null)
                {
                    return BadRequest();
                }

                var country = _mapper.Map<Country>(countryDto);

                await _countryRepository.Update(country);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Country>> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var Country = await _countryRepository.GetById(id);

            if (Country == null)
            {
                return NotFound();
            }

            await _countryRepository.Delete(Country);

            return NoContent();
        }
    }
}