
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using RoadTrafficManagement.Repositories;
using RoadTrafficManagement.AppModels;
using TrafficDataRequest;
using Helper;
using Microsoft.AspNetCore.Http.HttpResults;
using Azure.Core;
using RoadTrafficManagement.Criteria;

namespace RoadTrafficManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoadTypeController : ControllerBase
    {
        private readonly RoadRepository _repository;

        public RoadTypeController(RoadRepository repository)
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

        [HttpPost("Search")]
        public IActionResult Search([FromBody] RoadCriteria criteria)
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
        public IActionResult Create([FromBody] RoadRequest request)
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
        public IActionResult Update([FromBody] RoadRequest request)
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
        public IActionResult Delete([FromBody] RoadRequest request)
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