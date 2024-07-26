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
    public class RoadSectionRepository
    {

        private QuanLyGiaoThongContext _context;

        public RoadSectionRepository(QuanLyGiaoThongContext dbContext)
        {
            _context = dbContext;
        }

        public ApiResponse<RoadSectionRequest> GetById(uint Id)
        {
            try
            {
                var srcData = _context.RoadSections.FirstOrDefault(r => r.Id == Id);
                var roadRequest = ObjectCopier.ConvertCopyObject<RoadSection, RoadSectionRequest>(srcData);
                var response = new ApiResponse<RoadSectionRequest>(roadRequest);
                response.Success = true;
                response.Data = roadRequest;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadSectionRequest>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoadSectionRequest>> GetAll(int startIndex, int count)
        {
            try
            {
                var srcDatas = _context.RoadSections.Skip(startIndex).Take(count).ToList();
                var roadsRequest = srcDatas.Select(x => ObjectCopier.ConvertCopyObject<RoadSection, RoadSectionRequest>(x)).ToList();
                var response = new ApiResponse<List<RoadSectionRequest>>(roadsRequest);
                response.Success = true;
                response.Data = roadsRequest;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoadSectionRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoadSectionRequest>> AdvancedSearch(RoadSectionCriteria criteria)
        {
            try
            {
                var query = _context.RoadSections.AsQueryable();

                if (criteria.RoadId.HasValue)
                {
                    query = query.Where(r => r.RoadId == criteria.RoadId);
                }

                if (criteria.SessionTypeId.HasValue)
                {
                    query = query.Where(r => r.SessionTypeId == criteria.SessionTypeId);
                }

                if (!string.IsNullOrEmpty(criteria.RoadSessionCode))
                {
                    query = query.Where(r => r.RoadSessionCode.Contains(criteria.RoadSessionCode));
                }

                if (!string.IsNullOrEmpty(criteria.RoadSessionName))
                {
                    query = query.Where(r => r.RoadSessionName.Contains(criteria.RoadSessionName));
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
                    .Select(x => ObjectCopier.ConvertCopyObject<RoadSection, RoadSectionRequest>(x)).ToList();

                var response = new ApiResponse<List<RoadSectionRequest>>(results);
                response.Success = true;
                response.Data = results;
                response.TotalRecord = query.Count();

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoadSectionRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadSectionRequest> Create([FromBody] RoadSectionRequest request)
        {
            try
            {
                request.CreatedDate = DateTime.Now;

                var validateResponse = this.ValidateCode(request);
                if (validateResponse.Success == false) { return validateResponse; }

                validateResponse = this.ValidateName(request);
                if (validateResponse.Success == false) { return validateResponse; }

                var road = new RoadSection();
                road = ObjectCopier.ConvertCopyObject<RoadSectionRequest, RoadSection>(request);
                _context.RoadSections.Add(road);
                _context.SaveChanges();

                request.Id = road.Id;
                var response = new ApiResponse<RoadSectionRequest>(request);
                response.Success = true;
                response.Message = "Tạo mới thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadSectionRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadSectionRequest> Update([FromBody] RoadSectionRequest request)
        {
            try
            {
                request.ModifiedDate = DateTime.Now;

                var validateResponse = this.ValidateCode(request);
                if (validateResponse.Success == false) { return validateResponse; }

                validateResponse = this.ValidateName(request);
                if (validateResponse.Success == false) { return validateResponse; }

                var sourceObject = _context.RoadSections.Where(x => x.Id == request.Id).FirstOrDefault();
                ObjectCopier.CopyProperties(request, sourceObject);
                _context.RoadSections.Update(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<RoadSectionRequest>(request);
                response.Success = true;
                response.Message = "Cập nhập thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadSectionRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadSectionRequest> Delete([FromBody] RoadSectionRequest request)
        {
            try
            {
                var sourceObject = _context.RoadSections.Where(x => x.Id == request.Id).FirstOrDefault();
                _context.RoadSections.Remove(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<RoadSectionRequest>(request);
                response.Success = true;
                response.Message = "Xóa thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadSectionRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        private ApiResponse<RoadSectionRequest> ValidateCode(RoadSectionRequest request)
        {
            var sourceData = _context.RoadSections.Where(s =>
            s.Id != request.Id
            && s.RoadSessionCode != request.RoadSessionCode
            ).FirstOrDefault();
            if (sourceData != null)
            {
                return new ApiResponse<RoadSectionRequest>(request)
                {
                    Success = false,
                    Message = $"Mã tuyến đường {request.RoadSessionCode} đã tồn tại"
                };
            }
            return new ApiResponse<RoadSectionRequest>() { Success = true };
        }

        private ApiResponse<RoadSectionRequest> ValidateName(RoadSectionRequest request)
        {
            var sourceData = _context.RoadSections.Where(s =>
            s.Id != request.Id
            && s.RoadSessionName != request.RoadSessionName
            ).FirstOrDefault();
            if (sourceData != null)
            {
                return new ApiResponse<RoadSectionRequest>(request)
                {
                    Success = false,
                    Message = $"Tên tuyến đường {request.RoadSessionName} đã tồn tại"
                };
            }
            return new ApiResponse<RoadSectionRequest>() { Success = true };
        }
    }
}