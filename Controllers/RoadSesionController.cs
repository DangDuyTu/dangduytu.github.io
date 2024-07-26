
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using RoadTrafficManagement.Repositories;
using RoadTrafficManagement.AppModels;

namespace RoadTrafficManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoadSesionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        //private readonly MyDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private QuanLyGiaoThongContext _dbContext;

        public RoadSesionController(QuanLyGiaoThongContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("Create", Name = "CreateRoadSesion")]
        public IActionResult Create([FromBody] RoadSection roadSession)
        {
            _dbContext.RoadSections.Add(roadSession);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPost("Update", Name = "UpdateRoadSesion")]
        public IActionResult Update([FromBody] RoadSection roadSession)
        {
            var sourceObject = _dbContext.RoadSections.Where(x => x.Id == roadSession.Id).FirstOrDefault();

            sourceObject.RoadSessionCode = roadSession.RoadSessionCode;
            sourceObject.RoadSessionName = roadSession.RoadSessionName;

            _dbContext.RoadSections.Update(sourceObject);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPost("Delete", Name = "DeleteRoadSesion")]
        public IActionResult Delete([FromBody] RoadSection roadSession)
        {
            var sourceObject = _dbContext.RoadSections.Where(x => x.Id == roadSession.Id).FirstOrDefault();
            _dbContext.RoadSections.Remove(sourceObject);
            _dbContext.SaveChanges();
            return Ok();
        }
    }

}