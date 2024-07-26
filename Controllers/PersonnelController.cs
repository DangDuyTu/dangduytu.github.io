
using Microsoft.AspNetCore.Mvc;
using RoadTrafficManagement.Criteria;
using RoadTrafficManagement.Repositories;
using TrafficDataRequest;

namespace RoadTrafficManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelController : ControllerBase
    {
        private readonly PersonnelRepository _repository;

        public PersonnelController(PersonnelRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(uint Id)
        {
            var response = _repository.GetById(Id);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("GetAll/{startIndex}/{count}")]
        public IActionResult GetAll(uint startIndex, uint count)
        {
            var response = _repository.GetAll((int)startIndex, (int)count);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("Search")]
        public IActionResult Search([FromBody] PersonnelCriteria criteria)
        {
            var response = _repository.AdvancedSearch(criteria);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] PersonnelRequest request)
        {
            var response = _repository.Create(request);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("Update")]
        public IActionResult Update([FromBody] PersonnelRequest request)
        {
            var response = _repository.Update(request);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] PersonnelRequest request)
        {
            var response = _repository.Delete(request);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

    }

}