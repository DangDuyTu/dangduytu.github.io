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
    public class RoadRepository
    {
        private QuanLyGiaoThongContext _context;

        public RoadRepository(QuanLyGiaoThongContext dbContext)
        {
            _context = dbContext;
        }

        public ApiResponse<RoadRequest> GetById(uint Id)
        {
            try
            {

                var road = _context.Roads.FirstOrDefault(r => r.Id == Id);
                var roadRequest = ObjectCopier.ConvertCopyObject<Road, RoadRequest>(road);
                var response = new ApiResponse<RoadRequest>(roadRequest);
                response.Success = true;
                response.Data = roadRequest;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadRequest>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoadRequest>> GetAll(int startIndex, int count)
        {
            try
            {
                var roads = _context.Roads.Skip(startIndex).Take(count).ToList();
                var roadsRequest = roads.Select(x => ObjectCopier.ConvertCopyObject<Road, RoadRequest>(x)).ToList();
                var response = new ApiResponse<List<RoadRequest>>(roadsRequest);
                response.Success = true;
                response.Data = roadsRequest;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoadRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoadRequest>> AdvancedSearch(RoadCriteria criteria)
        {
            try
            {
                var query = _context.Roads.AsQueryable();

                if (criteria.TypeId.HasValue)
                {
                    query = query.Where(r => r.TypeId == criteria.TypeId);
                }

                if (criteria.ParentId.HasValue)
                {
                    query = query.Where(r => r.ParentId == criteria.ParentId);
                }

                if (!string.IsNullOrEmpty(criteria.RoadCode))
                {
                    query = query.Where(r => r.RoadCode.Contains(criteria.RoadCode));
                }

                if (!string.IsNullOrEmpty(criteria.RoadName))
                {
                    query = query.Where(r => r.RoadName.Contains(criteria.RoadName));
                }

                if (!string.IsNullOrEmpty(criteria.ChainageFrom))
                {
                    query = query.Where(r => r.ChainageFrom.Contains(criteria.ChainageFrom));
                }

                if (!string.IsNullOrEmpty(criteria.ChainageTo))
                {
                    query = query.Where(r => r.ChainageTo.Contains(criteria.ChainageTo));
                }

                if (!string.IsNullOrEmpty(criteria.StartGps))
                {
                    query = query.Where(r => r.StartGps.Contains(criteria.StartGps));
                }

                if (!string.IsNullOrEmpty(criteria.EndGps))
                {
                    query = query.Where(r => r.EndGps.Contains(criteria.EndGps));
                }

                if (!string.IsNullOrEmpty(criteria.Description))
                {
                    query = query.Where(r => r.Description.Contains(criteria.Description));
                }

                var results = query
                    .Skip((int)criteria.StartIndex)
                    .Take((int)criteria.Count)
                    .Select(x => ObjectCopier.ConvertCopyObject<Road, RoadRequest>(x)).ToList();

                var response = new ApiResponse<List<RoadRequest>>(results);
                response.Success = true;
                response.Data = results;
                response.TotalRecord = query.Count();

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoadRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadRequest> Create([FromBody] RoadRequest request)
        {
            try
            {
                request.CreatedDate = DateTime.Now;

                var road = new Road();
                road = ObjectCopier.ConvertCopyObject<RoadRequest, Road>(request);
                _context.Roads.Add(road);
                _context.SaveChanges();

                request.Id = road.Id;
                var response = new ApiResponse<RoadRequest>(request);
                response.Success = true;
                response.Message = "Tạo mới thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadRequest> Update([FromBody] RoadRequest request)
        {
            try
            {
                request.ModifiedDate = DateTime.Now;

                var sourceObject = _context.Roads.Where(x => x.Id == request.Id).FirstOrDefault();
                ObjectCopier.CopyProperties(request, sourceObject);
                _context.Roads.Update(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<RoadRequest>(request);
                response.Success = true;
                response.Message = "Cập nhập thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadRequest> Delete([FromBody] RoadRequest request)
        {
            try
            {
                var sourceObject = _context.Roads.Where(x => x.Id == request.Id).FirstOrDefault();
                _context.Roads.Remove(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<RoadRequest>(request);
                response.Success = true;
                response.Message = "Xóa thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
