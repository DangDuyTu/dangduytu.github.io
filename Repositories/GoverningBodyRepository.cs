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
    public class GoverningBodyRepository
    {

        private QuanLyGiaoThongContext _context;

        public GoverningBodyRepository(QuanLyGiaoThongContext dbContext)
        {
            _context = dbContext;
        }

        public ApiResponse<GoverningBodyRequest> GetById(uint Id)
        {
            try
            {
                var sourceData = _context.Roads.FirstOrDefault(r => r.Id == Id);
                var requestData = ObjectCopier.ConvertCopyObject<Road, GoverningBodyRequest>(sourceData);
                var response = new ApiResponse<GoverningBodyRequest>(requestData);
                response.Success = true;
                response.Data = requestData;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<GoverningBodyRequest>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<GoverningBodyRequest>> GetAll(int startIndex, int count)
        {
            try
            {
                var sourceDatas = _context.Roads.Skip(startIndex).Take(count).ToList();
                var roadsRequest = sourceDatas.Select(x => ObjectCopier.ConvertCopyObject<Road, GoverningBodyRequest>(x)).ToList();
                var response = new ApiResponse<List<GoverningBodyRequest>>(roadsRequest);
                response.Success = true;
                response.Data = roadsRequest;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<GoverningBodyRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<GoverningBodyRequest>> AdvancedSearch(GoverningBodyCriteria criteria)
        {
            try
            {
                var query = _context.GoverningBodies.AsQueryable();

                if (!string.IsNullOrEmpty(criteria.GoverningCode))
                {
                    query = query.Where(r => r.GoverningCode.Contains(criteria.GoverningCode));
                }
                if (!string.IsNullOrEmpty(criteria.GoverningName))
                {
                    query = query.Where(r => r.GoverningName.Contains(criteria.GoverningName));
                }
                if (!string.IsNullOrEmpty(criteria.PhoneNumber))
                {
                    query = query.Where(r => r.PhoneNumber.Contains(criteria.PhoneNumber));
                }
                if (!string.IsNullOrEmpty(criteria.Address))
                {
                    query = query.Where(r => r.Address.Contains(criteria.Address));
                }
                if (!string.IsNullOrEmpty(criteria.Description))
                {
                    query = query.Where(r => r.Description.Contains(criteria.Description));
                }

                var results = query
                    .Skip((int)criteria.StartIndex)
                    .Take((int)criteria.Count)
                    .Select(x => ObjectCopier.ConvertCopyObject<GoverningBody, GoverningBodyRequest>(x)).ToList();

                var response = new ApiResponse<List<GoverningBodyRequest>>(results);
                response.Success = true;
                response.Data = results;
                response.TotalRecord = query.Count();

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<GoverningBodyRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<GoverningBodyRequest> Create([FromBody] GoverningBodyRequest request)
        {
            try
            {
                request.CreatedDate = DateTime.Now;

                var sourceData = new Road();
                sourceData = ObjectCopier.ConvertCopyObject<GoverningBodyRequest, Road>(request);
                _context.Roads.Add(sourceData);
                _context.SaveChanges();

                request.Id = sourceData.Id;
                var response = new ApiResponse<GoverningBodyRequest>(request);
                response.Success = true;
                response.Message = "Tạo mới thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<GoverningBodyRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<GoverningBodyRequest> Update([FromBody] GoverningBodyRequest request)
        {
            try
            {
                request.ModifiedDate = DateTime.Now;

                var sourceObject = _context.Roads.Where(x => x.Id == request.Id).FirstOrDefault();
                ObjectCopier.CopyProperties(request, sourceObject);
                _context.Roads.Update(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<GoverningBodyRequest>(request);
                response.Success = true;
                response.Message = "Cập nhập thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<GoverningBodyRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<GoverningBodyRequest> Delete([FromBody] GoverningBodyRequest request)
        {
            try
            {
                var sourceObject = _context.Roads.Where(x => x.Id == request.Id).FirstOrDefault();
                _context.Roads.Remove(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<GoverningBodyRequest>(request);
                response.Success = true;
                response.Message = "Xóa thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<GoverningBodyRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
