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
    public class RoadPropertyRepository
    {
        private QuanLyGiaoThongContext _context;

        public RoadPropertyRepository(QuanLyGiaoThongContext dbContext)
        {
            _context = dbContext;
        }

        public ApiResponse<RoadPropertyRequest> GetById(uint Id)
        {
            try
            {

                var srcData = _context.RoadProperties.FirstOrDefault(r => r.Id == Id);
                var requestData = ObjectCopier.ConvertCopyObject<RoadProperty, RoadPropertyRequest>(srcData);
                var response = new ApiResponse<RoadPropertyRequest>(requestData);
                response.Success = true;
                response.Data = requestData;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadPropertyRequest>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoadPropertyRequest>> GetAll(int startIndex, int count)
        {
            try
            {
                var srcDatas = _context.RoadProperties.Skip(startIndex).Take(count).ToList();
                var requestDatas = srcDatas.Select(x => ObjectCopier.ConvertCopyObject<RoadProperty, RoadPropertyRequest>(x)).ToList();
                var response = new ApiResponse<List<RoadPropertyRequest>>(requestDatas);
                response.Success = true;
                response.TotalRecord = _context.RoadProperties.Count();
                response.Data = requestDatas;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoadPropertyRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoadPropertyRequest>> AdvancedSearch(RoadPropertyCriteria criteria)
        {
            try
            {
                var query = _context.RoadProperties.AsQueryable();

                if (!string.IsNullOrEmpty(criteria.PropertyCode))
                {
                    query = query.Where(r => r.PropertyCode.Contains(criteria.PropertyCode));
                }
                if (!string.IsNullOrEmpty(criteria.PropertyName))
                {
                    query = query.Where(r => r.PropertyName.Contains(criteria.PropertyName));
                }
                if (!string.IsNullOrEmpty(criteria.Description))
                {
                    query = query.Where(r => r.Description.Contains(criteria.Description));
                }

                var results = query
                    .Skip((int)criteria.StartIndex)
                    .Take((int)criteria.Count)
                    .Select(x => ObjectCopier.ConvertCopyObject<RoadProperty, RoadPropertyRequest>(x)).ToList();

                var response = new ApiResponse<List<RoadPropertyRequest>>(results);
                response.Success = true;
                response.Data = results;
                response.TotalRecord = query.Count();

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoadPropertyRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadPropertyRequest> Create([FromBody] RoadPropertyRequest request)
        {
            try
            {
                request.CreatedDate = DateTime.Now;

                var validateResponse = this.ValidateCode(request);
                if(validateResponse.Success == false) { return validateResponse; }

                validateResponse = this.ValidateName(request);
                if (validateResponse.Success == false) { return validateResponse; }

                var data = new RoadProperty();
                data = ObjectCopier.ConvertCopyObject<RoadPropertyRequest, RoadProperty>(request);
                _context.RoadProperties.Add(data);
                _context.SaveChanges();

                request.Id = data.Id;
                var response = new ApiResponse<RoadPropertyRequest>(request);
                response.Success = true;
                response.Message = "Tạo mới thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadPropertyRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadPropertyRequest> Update([FromBody] RoadPropertyRequest request)
        {
            try
            {
                request.ModifiedDate = DateTime.Now;

                var validateResponse = this.ValidateCode(request);
                if (validateResponse.Success == false) { return validateResponse; }

                validateResponse = this.ValidateName(request);
                if (validateResponse.Success == false) { return validateResponse; }

                var sourceObject = _context.RoadProperties.Where(x => x.Id == request.Id).FirstOrDefault();
                ObjectCopier.CopyProperties(request, sourceObject);
                _context.RoadProperties.Update(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<RoadPropertyRequest>(request);
                response.Success = true;
                response.Message = "Cập nhập thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadPropertyRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadPropertyRequest> Delete([FromBody] RoadPropertyRequest request)
        {
            try
            {
                var sourceObject = _context.RoadProperties.Where(x => x.Id == request.Id).FirstOrDefault();
                _context.RoadProperties.Remove(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<RoadPropertyRequest>(request);
                response.Success = true;
                response.Message = "Xóa thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadPropertyRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        private ApiResponse<RoadPropertyRequest> ValidateCode(RoadPropertyRequest request)
        {
            var findData = _context.RoadProperties.Where(s =>
                s.PropertyCode == request.PropertyCode
                && s.Id != request.Id
            ).FirstOrDefault();
            if (findData != null)
            {
                return new ApiResponse<RoadPropertyRequest>(request)
                {
                    Success = false,
                    Message = $"Mã cột tiêu {request.PropertyCode} đã tồn tại"
                };
            }
            return new ApiResponse<RoadPropertyRequest>() { Success = true };
        }
        private ApiResponse<RoadPropertyRequest> ValidateName(RoadPropertyRequest request)
        {
            var findData = _context.RoadProperties.Where(s =>
                s.PropertyName == request.PropertyName
                && s.Id != request.Id
            ).FirstOrDefault();
            if (findData != null)
            {
                return new ApiResponse<RoadPropertyRequest>(request)
                {
                    Success = false,
                    Message = $"Tên cột tiêu {request.PropertyName} đã tồn tại"
                };
            }
            return new ApiResponse<RoadPropertyRequest>() { Success = true };
        }
    }
}