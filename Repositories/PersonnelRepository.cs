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
    public class PersonnelRepository
    {

        private QuanLyGiaoThongContext _context;

        public PersonnelRepository(QuanLyGiaoThongContext dbContext)
        {
            _context = dbContext;
        }

        public ApiResponse<PersonnelRequest> GetById(uint Id)
        {
            try
            {
                var road = _context.Personnel.FirstOrDefault(r => r.Id == Id);
                var roadRequest = ObjectCopier.ConvertCopyObject<Personnel, PersonnelRequest>(road);
                var response = new ApiResponse<PersonnelRequest>(roadRequest);
                response.Success = true;
                response.Data = roadRequest;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<PersonnelRequest>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<PersonnelRequest>> GetAll(int startIndex, int count)
        {
            try
            {
                var roads = _context.Personnel.Skip(startIndex).Take(count).ToList();
                var roadsRequest = roads.Select(x => ObjectCopier.ConvertCopyObject<Personnel, PersonnelRequest>(x)).ToList();
                var response = new ApiResponse<List<PersonnelRequest>>(roadsRequest);
                response.Success = true;
                response.Data = roadsRequest;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<PersonnelRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<PersonnelRequest>> AdvancedSearch(PersonnelCriteria criteria)
        {
            try
            {
                var query = _context.Personnel.AsQueryable();

                if (!string.IsNullOrEmpty(criteria.PersonelCode))
                {
                    query = query.Where(r => r.PersonelCode.Contains(criteria.PersonelCode));
                }

                if (!string.IsNullOrEmpty(criteria.FullName))
                {
                    query = query.Where(r => r.FullName.Contains(criteria.FullName));
                }

                if (!string.IsNullOrEmpty(criteria.MobilePhoneNumber))
                {
                    query = query.Where(r => r.MobilePhoneNumber.Contains(criteria.MobilePhoneNumber));
                }

                if (!string.IsNullOrEmpty(criteria.Department))
                {
                    query = query.Where(r => r.Department.Contains(criteria.Department));
                }

                if (!string.IsNullOrEmpty(criteria.EmploymentType))
                {
                    query = query.Where(r => r.EmploymentType.Contains(criteria.EmploymentType));
                }

                var results = query
                    .Skip((int)criteria.StartIndex)
                    .Take((int)criteria.Count)
                    .Select(x => ObjectCopier.ConvertCopyObject<Personnel, PersonnelRequest>(x)).ToList();

                var response = new ApiResponse<List<PersonnelRequest>>(results);
                response.Success = true;
                response.Data = results;
                response.TotalRecord = query.Count();

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<PersonnelRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<PersonnelRequest> Create([FromBody] PersonnelRequest request)
        {
            try
            {
                request.CreatedDate = DateTime.Now;

                var validateResponse = this.ValidateCode(request);
                if (validateResponse.Success == false) { return validateResponse; }

                validateResponse = this.ValidateName(request);
                if (validateResponse.Success == false) { return validateResponse; }

                var road = new Personnel();
                road = ObjectCopier.ConvertCopyObject<PersonnelRequest, Personnel>(request);
                _context.Personnel.Add(road);
                _context.SaveChanges();

                request.Id = road.Id;
                var response = new ApiResponse<PersonnelRequest>(request);
                response.Success = true;
                response.Message = "Tạo mới thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<PersonnelRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<PersonnelRequest> Update([FromBody] PersonnelRequest request)
        {
            try
            {
                request.ModifiedDate = DateTime.Now;

                var validateResponse = this.ValidateCode(request);
                if (validateResponse.Success == false) { return validateResponse; }

                validateResponse = this.ValidateName(request);
                if (validateResponse.Success == false) { return validateResponse; }

                var sourceObject = _context.Personnel.Where(x => x.Id == request.Id).FirstOrDefault();
                ObjectCopier.CopyProperties(request, sourceObject);
                _context.Personnel.Update(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<PersonnelRequest>(request);
                response.Success = true;
                response.Message = "Cập nhập thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<PersonnelRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<PersonnelRequest> Delete([FromBody] PersonnelRequest request)
        {
            try
            {
                var sourceObject = _context.Personnel.Where(x => x.Id == request.Id).FirstOrDefault();
                _context.Personnel.Remove(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<PersonnelRequest>(request);
                response.Success = true;
                response.Message = "Xóa thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<PersonnelRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        private ApiResponse<PersonnelRequest> ValidateCode(PersonnelRequest request)
        {
            var sourceData = _context.Personnel.Where(s =>
            s.Id != request.Id
            && s.PersonelCode != request.PersonelCode
            ).FirstOrDefault();
            if (sourceData != null)
            {
                return new ApiResponse<PersonnelRequest>(request)
                {
                    Success = false,
                    Message = $"Mã cán bộ {request.PersonelCode} đã tồn tại"
                };
            }
            return new ApiResponse<PersonnelRequest>() { Success = true };
        }
        private ApiResponse<PersonnelRequest> ValidateName(PersonnelRequest request)
        {
            var sourceData = _context.Personnel.Where(s =>
            s.Id != request.Id
            && s.FullName != request.FullName
            ).FirstOrDefault();
            if (sourceData != null)
            {
                return new ApiResponse<PersonnelRequest>(request)
                {
                    Success = false,
                    Message = $"Tên cán bộ {request.FullName} đã tồn tại"
                };
            }
            return new ApiResponse<PersonnelRequest>() { Success = true };
        }
    }
}
