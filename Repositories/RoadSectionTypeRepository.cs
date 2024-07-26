using ApiResponse;
using Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoadTrafficManagement.AppModels;
using RoadTrafficManagement.Criteria;
using TrafficDataRequest;
using TrafficDataRequest.Criteria;


namespace RoadTrafficManagement.Repositories
{
    public class RoadSectionTypeRepository
    {

        private QuanLyGiaoThongContext _context;

        public RoadSectionTypeRepository(QuanLyGiaoThongContext dbContext)
        {
            _context = dbContext;
        }

        public ApiResponse<RoadSesionTypeRequest> GetById(uint Id)
        {
            try
            {
                var srcData = _context.RoadSesionTypes.FirstOrDefault(r => r.Id == Id);
                var roadRequest = ObjectCopier.ConvertCopyObject<RoadSesionType, RoadSesionTypeRequest>(srcData);
                var response = new ApiResponse<RoadSesionTypeRequest>(roadRequest);
                response.Success = true;
                response.Data = roadRequest;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadSesionTypeRequest>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoadSesionTypeRequest>> GetAll(int startIndex, int count)
        {
            try
            {
                var srcDatas = _context.RoadSesionTypes.Skip(startIndex).Take(count).ToList();
                var roadsRequest = srcDatas.Select(x => ObjectCopier.ConvertCopyObject<RoadSesionType, RoadSesionTypeRequest>(x)).ToList();
                var response = new ApiResponse<List<RoadSesionTypeRequest>>(roadsRequest);
                response.Success = true;
                response.Data = roadsRequest;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoadSesionTypeRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoadSesionTypeRequest>> AdvancedSearch(RoadSesionTypeCriteria criteria)
        {
            try
            {
                var query = _context.RoadSesionTypes.AsQueryable();

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
                    .Select(x => ObjectCopier.ConvertCopyObject<RoadSesionType, RoadSesionTypeRequest>(x)).ToList();

                var response = new ApiResponse<List<RoadSesionTypeRequest>>(results);
                response.Success = true;
                response.Data = results;
                response.TotalRecord = query.Count();

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoadSesionTypeRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadSesionTypeRequest> Create([FromBody] RoadSesionTypeRequest request)
        {
            try
            {
                request.CreatedDate = DateTime.Now;

                var validateResponse = this.ValidateCode(request);
                if (validateResponse.Success == false) { return validateResponse; }

                validateResponse = this.ValidateName(request);
                if (validateResponse.Success == false) { return validateResponse; }

                var road = new RoadSesionType();
                road = ObjectCopier.ConvertCopyObject<RoadSesionTypeRequest, RoadSesionType>(request);
                _context.RoadSesionTypes.Add(road);
                _context.SaveChanges();

                request.Id = road.Id;
                var response = new ApiResponse<RoadSesionTypeRequest>(request);
                response.Success = true;
                response.Message = "Tạo mới thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadSesionTypeRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadSesionTypeRequest> Update([FromBody] RoadSesionTypeRequest request)
        {
            try
            {
                request.ModifiedDate = DateTime.Now;

                var validateResponse = this.ValidateCode(request);
                if (validateResponse.Success == false) { return validateResponse; }

                validateResponse = this.ValidateName(request);
                if (validateResponse.Success == false) { return validateResponse; }

                var sourceObject = _context.RoadSesionTypes.Where(x => x.Id == request.Id).FirstOrDefault();
                ObjectCopier.CopyProperties(request, sourceObject);
                _context.RoadSesionTypes.Update(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<RoadSesionTypeRequest>(request);
                response.Success = true;
                response.Message = "Cập nhập thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadSesionTypeRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadSesionTypeRequest> Delete([FromBody] RoadSesionTypeRequest request)
        {
            try
            {
                var validateResponse = this.ValidateDelete(request);
                if (validateResponse.Success == false) { return validateResponse; }

                var sourceObject = _context.RoadSesionTypes.Where(x => x.Id == request.Id).FirstOrDefault();
                _context.RoadSesionTypes.Remove(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<RoadSesionTypeRequest>(request);
                response.Success = true;
                response.Message = "Xóa thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadSesionTypeRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        private ApiResponse<RoadSesionTypeRequest> ValidateCode(RoadSesionTypeRequest request)
        {
            var sourceData = _context.RoadSesionTypes.Where(s => 
            s.Id != request.Id
            && s.TypeCode != request.TypeCode
            ).FirstOrDefault();
            if (sourceData != null)
            {
                return new ApiResponse<RoadSesionTypeRequest>(request) { 
                    Success = false ,
                    Message = $"Mã loại {request.TypeCode} đã tồn tại"
                };
            }
            return new ApiResponse<RoadSesionTypeRequest>() { Success = true };
        }
        private ApiResponse<RoadSesionTypeRequest> ValidateName(RoadSesionTypeRequest request)
        {
            var sourceData = _context.RoadSesionTypes.Where(s =>
            s.Id != request.Id
            && s.TypeName != request.TypeName
            ).FirstOrDefault();
            if (sourceData != null)
            {
                return new ApiResponse<RoadSesionTypeRequest>(request)
                {
                    Success = false,
                    Message = $"Tên loại {request.TypeName} đã tồn tại"
                };
            }
            return new ApiResponse<RoadSesionTypeRequest>() { Success = true };
        }
        private ApiResponse<RoadSesionTypeRequest> ValidateDelete(RoadSesionTypeRequest request)
        {
            var sourceData = _context.RoadSections.Where(s => s.SessionTypeId != request.Id).FirstOrDefault();
            if (sourceData != null)
            {
                return new ApiResponse<RoadSesionTypeRequest>(request)
                {
                    Success = false,
                    Message = $"Loại đoạn đường đã có phát sinh đoạn đường, không thể xóa"
                };
            }
            return new ApiResponse<RoadSesionTypeRequest>() { Success = true };
        }

    }
}