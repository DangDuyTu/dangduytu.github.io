
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

namespace RoadTrafficManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoadwayIssuesTypeController : ControllerBase
    {
        private QuanLyGiaoThongContext _dbContext;

        public RoadwayIssuesTypeController(QuanLyGiaoThongContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(uint Id)
        {
            var sourceData = _dbContext.RoadwayIssuesTypes.FirstOrDefault(r => r.Id == Id);
            var roadRequest = ObjectCopier.ConvertCopyObject<RoadwayIssuesType, RoadwayIssuesTypeRequest>(sourceData);
            return Ok(sourceData);
        }

        [HttpGet("GetRoadwayIssues/{Id}")]
        public IActionResult GetRoadwayIssues(uint Id)
        {
            var sourceData = _dbContext.RoadwayIssuesTypes.Include(r => r.RoadwayIssues).FirstOrDefault(r => r.Id == Id);
            var requestData = sourceData.RoadwayIssues.Select(x => ObjectCopier.ConvertCopyObject<RoadwayIssue, RoadwayIssueRequest>(x)).ToList();
            return Ok(requestData);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] RoadwayIssuesTypeRequest request)
        {
            var road = new RoadwayIssuesType();
            ObjectCopier.CopyProperties(request, road);
            _dbContext.RoadwayIssuesTypes.Add(road);
            _dbContext.SaveChanges();
            return Ok(road);
        }

        [HttpPost("Update")]
        public IActionResult Update([FromBody] RoadwayIssuesType requestData)
        {
            var sourceObject = _dbContext.RoadwayIssuesTypes.Where(x => x.Id == requestData.Id).FirstOrDefault();

            ObjectCopier.CopyProperties<RoadwayIssuesType, RoadwayIssuesType>(requestData, sourceObject);

            _dbContext.RoadwayIssuesTypes.Update(sourceObject);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] RoadwayIssuesType roadSession)
        {
            var sourceObject = _dbContext.RoadwayIssuesTypes.Where(x => x.Id == roadSession.Id).FirstOrDefault();
            _dbContext.RoadwayIssuesTypes.Remove(sourceObject);
            _dbContext.SaveChanges();
            return Ok();
        }
    }

}