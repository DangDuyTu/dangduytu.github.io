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
    public class RoadwayIssuesTypeRepository
    {
        private QuanLyGiaoThongContext _context;

        public RoadwayIssuesTypeRepository(QuanLyGiaoThongContext dbContext)
        {
            _context = dbContext;
        }

        public ApiResponse<RoadwayIssuesTypeRequest> GetById(uint Id)
        {
            try
            {

                var srcData = _context.RoadwayIssuesTypes.FirstOrDefault(r => r.Id == Id);
                var requestData = ObjectCopier.ConvertCopyObject<RoadwayIssuesType, RoadwayIssuesTypeRequest>(srcData);
                var response = new ApiResponse<RoadwayIssuesTypeRequest>(requestData);
                response.Success = true;
                response.Data = requestData;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadwayIssuesTypeRequest>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoadwayIssuesTypeRequest>> GetAll(int startIndex, int count)
        {
            try
            {
                var srcDatas = _context.RoadwayIssuesTypes.Skip(startIndex).Take(count).ToList();
                var requestDatas = srcDatas.Select(x => ObjectCopier.ConvertCopyObject<RoadwayIssuesType, RoadwayIssuesTypeRequest>(x)).ToList();
                var response = new ApiResponse<List<RoadwayIssuesTypeRequest>>(requestDatas);
                response.Success = true;
                response.Data = requestDatas;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoadwayIssuesTypeRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoadwayIssuesTypeRequest>> AdvancedSearch(RoadwayIssuesTypeCriteria criteria)
        {
            try
            {
                var query = _context.RoadwayIssuesTypes.AsQueryable();

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
                    .Select(x => ObjectCopier.ConvertCopyObject<RoadwayIssuesType, RoadwayIssuesTypeRequest>(x)).ToList();

                var response = new ApiResponse<List<RoadwayIssuesTypeRequest>>(results);
                response.Success = true;
                response.Data = results;
                response.TotalRecord = query.Count();

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoadwayIssuesTypeRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadwayIssuesTypeRequest> Create([FromBody] RoadwayIssuesTypeRequest request)
        {
            try
            {
                request.CreatedDate = DateTime.Now;

                var data = new RoadwayIssuesType();
                data = ObjectCopier.ConvertCopyObject<RoadwayIssuesTypeRequest, RoadwayIssuesType>(request);
                _context.RoadwayIssuesTypes.Add(data);
                _context.SaveChanges();

                request.Id = data.Id;
                var response = new ApiResponse<RoadwayIssuesTypeRequest>(request);
                response.Success = true;
                response.Message = "Tạo mới thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadwayIssuesTypeRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadwayIssuesTypeRequest> Update([FromBody] RoadwayIssuesTypeRequest request)
        {
            try
            {
                request.ModifiedDate = DateTime.Now;

                var sourceObject = _context.RoadwayIssuesTypes.Where(x => x.Id == request.Id).FirstOrDefault();
                ObjectCopier.CopyProperties(request, sourceObject);
                _context.RoadwayIssuesTypes.Update(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<RoadwayIssuesTypeRequest>(request);
                response.Success = true;
                response.Message = "Cập nhập thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadwayIssuesTypeRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadwayIssuesTypeRequest> Delete([FromBody] RoadwayIssuesTypeRequest request)
        {
            try
            {
                var sourceObject = _context.RoadwayIssuesTypes.Where(x => x.Id == request.Id).FirstOrDefault();
                _context.RoadwayIssuesTypes.Remove(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<RoadwayIssuesTypeRequest>(request);
                response.Success = true;
                response.Message = "Xóa thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadwayIssuesTypeRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
