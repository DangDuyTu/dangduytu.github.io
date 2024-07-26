using ApiResponse;
using Azure.Core;
using Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RoadTrafficManagement.AppModels;
using RoadTrafficManagement.Criteria;
using TrafficDataRequest;


namespace RoadTrafficManagement.Repositories
{
    public class RoadInfrastructureRepository
    {

        private QuanLyGiaoThongContext _context;

        public RoadInfrastructureRepository(QuanLyGiaoThongContext dbContext)
        {
            _context = dbContext;
        }

        public ApiResponse<RoadInfrastructureRequest> GetById(uint Id)
        {
            try
            {
                var srcData = _context.RoadInfrastructures.FirstOrDefault(r => r.Id == Id);
                var roadRequest = ObjectCopier.ConvertCopyObject<RoadInfrastructure, RoadInfrastructureRequest>(srcData);
                var response = new ApiResponse<RoadInfrastructureRequest>(roadRequest);
                response.Success = true;
                response.Data = roadRequest;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadInfrastructureRequest>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoadInfrastructureRequest>> GetAll(int startIndex, int count)
        {
            try
            {
                var roads = _context.RoadInfrastructures.Skip(startIndex).Take(count).ToList();
                var roadsRequest = roads.Select(x => ObjectCopier.ConvertCopyObject<RoadInfrastructure, RoadInfrastructureRequest>(x)).ToList();
                var response = new ApiResponse<List<RoadInfrastructureRequest>>(roadsRequest);
                response.Success = true;
                response.Data = roadsRequest;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoadInfrastructureRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoadInfrastructureRequest>> AdvancedSearch(RoadInfrastructureCriteria criteria)
        {
            try
            {
                var query = _context.RoadInfrastructures.AsQueryable();

                if (!string.IsNullOrEmpty(criteria.Location))
                {
                    query = query.Where(r => r.Location.Contains(criteria.Location));
                }

                if (!string.IsNullOrEmpty(criteria.Chainage))
                {
                    query = query.Where(r => r.Chainage.Contains(criteria.Chainage));
                }

                if (criteria.Kilometer.HasValue)
                {
                    query = query.Where(r => r.Kilometer == criteria.Kilometer);
                }

                if (!string.IsNullOrEmpty(criteria.Location))
                {
                    query = query.Where(r => r.Location.Contains(criteria.Location));
                }

                if (criteria.InstallationDate.HasValue)
                {
                    query = query.Where(r => r.InstallationDate == (criteria.InstallationDate));
                }

                if (!string.IsNullOrEmpty(criteria.Status))
                {
                    query = query.Where(r => r.Status.Contains(criteria.Status));
                }

                if (!string.IsNullOrEmpty(criteria.Description))
                {
                    query = query.Where(r => r.Description.Contains(criteria.Description));
                }

                var results = query
                    .Skip((int)criteria.StartIndex)
                    .Take((int)criteria.Count)
                    .Select(x => ObjectCopier.ConvertCopyObject<RoadInfrastructure, RoadInfrastructureRequest>(x)).ToList();

                var response = new ApiResponse<List<RoadInfrastructureRequest>>(results);
                response.Success = true;
                response.Data = results;
                response.TotalRecord = query.Count();

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoadInfrastructureRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadInfrastructureRequest> Create([FromBody] RoadInfrastructureRequest request)
        {
            try
            {
                request.CreatedDate = DateTime.Now;

                var road = new RoadInfrastructure();
                road = ObjectCopier.ConvertCopyObject<RoadInfrastructureRequest, RoadInfrastructure>(request);
                _context.RoadInfrastructures.Add(road);
                _context.SaveChanges();

                request.Id = road.Id;
                var response = new ApiResponse<RoadInfrastructureRequest>(request);
                response.Success = true;
                response.Message = "Tạo mới thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadInfrastructureRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadInfrastructureRequest> Update([FromBody] RoadInfrastructureRequest request)
        {
            try
            {
                request.ModifiedDate = DateTime.Now;

                var sourceObject = _context.RoadInfrastructures.Where(x => x.Id == request.Id).FirstOrDefault();
                ObjectCopier.CopyProperties(request, sourceObject);
                _context.RoadInfrastructures.Update(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<RoadInfrastructureRequest>(request);
                response.Success = true;
                response.Message = "Cập nhập thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadInfrastructureRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadInfrastructureRequest> Delete([FromBody] RoadInfrastructureRequest request)
        {
            try
            {
                var sourceObject = _context.RoadInfrastructures.Where(x => x.Id == request.Id).FirstOrDefault();
                _context.RoadInfrastructures.Remove(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<RoadInfrastructureRequest>(request);
                response.Success = true;
                response.Message = "Xóa thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadInfrastructureRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
