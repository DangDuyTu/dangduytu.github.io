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
    public class RoadGuardrailRepository
    {
        private QuanLyGiaoThongContext _context;

        public RoadGuardrailRepository(QuanLyGiaoThongContext dbContext)
        {
            _context = dbContext;
        }

        public ApiResponse<RoadGuardrailRequest> GetById(uint Id)
        {
            try
            {

                var srcData = _context.RoadGuardrails.FirstOrDefault(r => r.Id == Id);
                var requestData = ObjectCopier.ConvertCopyObject<RoadGuardrail, RoadGuardrailRequest>(srcData);
                var response = new ApiResponse<RoadGuardrailRequest>(requestData);
                response.Success = true;
                response.Data = requestData;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadGuardrailRequest>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoadGuardrailRequest>> GetAll(int startIndex, int count)
        {
            try
            {
                var srcDatas = _context.RoadGuardrails.Skip(startIndex).Take(count).ToList();
                var requestDatas = srcDatas.Select(x => ObjectCopier.ConvertCopyObject<RoadGuardrail, RoadGuardrailRequest>(x)).ToList();
                var response = new ApiResponse<List<RoadGuardrailRequest>>(requestDatas);
                response.Success = true;
                response.Data = requestDatas;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoadGuardrailRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoadGuardrailRequest>> AdvancedSearch(RoadGuardrailCriteria criteria)
        {
            try
            {
                var query = _context.RoadGuardrails.AsQueryable();

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
                    .Select(x => ObjectCopier.ConvertCopyObject<RoadGuardrail, RoadGuardrailRequest>(x)).ToList();

                var response = new ApiResponse<List<RoadGuardrailRequest>>(results);
                response.Success = true;
                response.Data = results;
                response.TotalRecord = query.Count();

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoadGuardrailRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadGuardrailRequest> Create([FromBody] RoadGuardrailRequest request)
        {
            try
            {
                request.CreatedDate = DateTime.Now;

                var data = new RoadGuardrail();
                data = ObjectCopier.ConvertCopyObject<RoadGuardrailRequest, RoadGuardrail>(request);
                _context.RoadGuardrails.Add(data);
                _context.SaveChanges();

                request.Id = data.Id;
                var response = new ApiResponse<RoadGuardrailRequest>(request);
                response.Success = true;
                response.Message = "Tạo mới thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadGuardrailRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadGuardrailRequest> Update([FromBody] RoadGuardrailRequest request)
        {
            try
            {
                request.ModifiedDate = DateTime.Now;

                var sourceObject = _context.RoadGuardrails.Where(x => x.Id == request.Id).FirstOrDefault();
                ObjectCopier.CopyProperties(request, sourceObject);
                _context.RoadGuardrails.Update(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<RoadGuardrailRequest>(request);
                response.Success = true;
                response.Message = "Cập nhập thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadGuardrailRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadGuardrailRequest> Delete([FromBody] RoadGuardrailRequest request)
        {
            try
            {
                var sourceObject = _context.RoadGuardrails.Where(x => x.Id == request.Id).FirstOrDefault();
                _context.RoadGuardrails.Remove(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<RoadGuardrailRequest>(request);
                response.Success = true;
                response.Message = "Xóa thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadGuardrailRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
