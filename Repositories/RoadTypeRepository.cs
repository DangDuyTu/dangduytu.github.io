using ApiResponse;
using Azure.Core;
using Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoadTrafficManagement.AppModels;
using RoadTrafficManagement.Criteria;
using TrafficDataRequest;


namespace RoadTrafficManagement.Repositories
{
    public class RoadTypeRepository
    {
        private QuanLyGiaoThongContext _context;

        public RoadTypeRepository(QuanLyGiaoThongContext dbContext)
        {
            _context = dbContext;
        }

        public ApiResponse<RoadTypeRequest> GetById(uint Id)
        {
            try
            {

                var road = _context.RoadTypes.FirstOrDefault(r => r.Id == Id);
                var roadRequest = ObjectCopier.ConvertCopyObject<RoadType, RoadTypeRequest>(road);
                var response = new ApiResponse<RoadTypeRequest>(roadRequest);
                response.Success = true;
                response.Data = roadRequest;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadTypeRequest>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoadTypeRequest>> GetAll(int startIndex, int count)
        {
            try
            {
                var roads = _context.RoadTypes.Skip(startIndex).Take(count).ToList();
                var roadsRequest = roads.Select(x => ObjectCopier.ConvertCopyObject<RoadType, RoadTypeRequest>(x)).ToList();
                var response = new ApiResponse<List<RoadTypeRequest>>(roadsRequest);
                response.Success = true;
                response.Data = roadsRequest;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoadTypeRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoadTypeRequest>> AdvancedSearch(RoadTypeCriteria criteria)
        {
            try
            {
                var query = _context.RoadTypes.AsQueryable();

                if (!string.IsNullOrEmpty(criteria.TypeCode))
                {
                    query = query.Where(r => r.TypeCode.Contains(criteria.TypeCode));
                }

                if (!string.IsNullOrEmpty(criteria.TypeName))
                {
                    query = query.Where(r => r.TypeName.Contains(criteria.TypeName));
                }

                if (!string.IsNullOrEmpty(criteria.Description))
                {
                    query = query.Where(r => r.Description.Contains(criteria.Description));
                }

                var results = query
                    .Skip((int)criteria.StartIndex)
                    .Take((int)criteria.Count)
                    .Select(x => ObjectCopier.ConvertCopyObject<RoadType, RoadTypeRequest>(x)).ToList();

                var response = new ApiResponse<List<RoadTypeRequest>>(results);
                response.Success = true;
                response.Data = results;
                response.TotalRecord = query.Count();

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoadTypeRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadTypeRequest> Create([FromBody] RoadTypeRequest request)
        {
            try
            {
                request.CreatedDate = DateTime.Now;

                var road = new RoadType();
                road = ObjectCopier.ConvertCopyObject<RoadTypeRequest, RoadType>(request);
                _context.RoadTypes.Add(road);
                _context.SaveChanges();

                request.Id = road.Id;
                var response = new ApiResponse<RoadTypeRequest>(request);
                response.Success = true;
                response.Message = "Tạo mới thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadTypeRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadTypeRequest> Update([FromBody] RoadTypeRequest request)
        {
            try
            {
                request.ModifiedDate = DateTime.Now;

                var sourceObject = _context.RoadTypes.Where(x => x.Id == request.Id).FirstOrDefault();
                ObjectCopier.CopyProperties(request, sourceObject);
                _context.RoadTypes.Update(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<RoadTypeRequest>(request);
                response.Success = true;
                response.Message = "Cập nhập thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadTypeRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadTypeRequest> Delete([FromBody] RoadTypeRequest request)
        {
            try
            {
                var sourceObject = _context.RoadTypes.Where(x => x.Id == request.Id).FirstOrDefault();
                _context.RoadTypes.Remove(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<RoadTypeRequest>(request);
                response.Success = true;
                response.Message = "Xóa thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadTypeRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
