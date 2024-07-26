
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using RoadTrafficManagement.Repositories;
using RoadTrafficManagement.AppModels;
using RoadTrafficManagement.Request;
using Helper;
using Microsoft.AspNetCore.Http.HttpResults;
using Azure.Core;
using RoadTrafficManagement.Criteria;
using RoadTrafficManagement.DbServices;

namespace RoadTrafficManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleServices _service;

        public RoleController(RoleServices service)
        {
            _service = service;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(uint id)
        {
            var response = _service.GetById(id);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var response = _service.GetAll();
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("GetDetails")]
        public IActionResult GetDetails(uint id)
        {
            var response = _service.GetRoleDetails(id);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("GetDetailByUser/{userId}")]
        public IActionResult GetDetailByUser(uint userId)
        {
            var response = _service.GetDetailByUser(userId);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("GetByUser/{id}")]
        public IActionResult GetByUser(uint id)
        {
            var response = _service.GetByUser(id);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("Authorization")]
        public IActionResult Authorization([FromBody] URCommandRequest request)
        {
            var response = _service.Authorization(request);
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