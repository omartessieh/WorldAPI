using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using WorldAPI.Entities;
using WorldAPI.Interfaces;
using WorldAPI.Repository;

namespace WorldAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly IStatesRepository _statesRepository;
        private readonly IMapper _mapper;

        public StatesController(IStatesRepository statesRepository, IMapper mapper)
        {
            _statesRepository = statesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StatesDto>>> GetAll()
        {
            try
            {
                var states = await _statesRepository.GetAll();

                var statesDto = _mapper.Map<List<StatesDto>>(states);

                if (statesDto == null)
                {
                    return NoContent();
                }

                return Ok(statesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<StatesDto>> GetById(int id)
        {
            try
            {
                var state = await _statesRepository.GetById(id);

                var stateDto = _mapper.Map<StatesDto>(state);

                if (state == null)
                {
                    return NoContent();
                }

                return Ok(stateDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateStatesDto>> Create([FromBody] CreateStatesDto statesDto)
        {
            try
            {
                var result = _statesRepository.IsRecordExist(x => x.Name == statesDto.Name);

                if (result)
                {
                    return Conflict("States Already Exist");
                }

                var states = _mapper.Map<States>(statesDto);

                await _statesRepository.Create(states);

                return CreatedAtAction("GetById", new { id = states.Id }, states);
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
        public async Task<ActionResult<States>> Update(int id, [FromBody] UpdateStatesDto statesDto)
        {
            try
            {
                if (id != statesDto.Id || statesDto == null)
                {
                    return BadRequest();
                }

                var states = _mapper.Map<States>(statesDto);

                await _statesRepository.Update(states);

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
        public async Task<ActionResult<States>> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var states = await _statesRepository.GetById(id);

            if (states == null)
            {
                return NotFound();
            }

            await _statesRepository.Delete(states);

            return NoContent();
        }
    }
}