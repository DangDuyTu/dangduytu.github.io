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
    public class RoadwayIssuesRepository
    {
        private QuanLyGiaoThongContext _context;

        public RoadwayIssuesRepository(QuanLyGiaoThongContext dbContext)
        {
            _context = dbContext;
        }

        public ApiResponse<RoadwayIssueRequest> GetById(uint Id)
        {
            try
            {

                var srcData = _context.RoadwayIssues.FirstOrDefault(r => r.Id == Id);
                var requestData = ObjectCopier.ConvertCopyObject<RoadwayIssue, RoadwayIssueRequest>(srcData);
                var response = new ApiResponse<RoadwayIssueRequest>(requestData);
                response.Success = true;
                response.Data = requestData;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadwayIssueRequest>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoadwayIssueRequest>> GetAll(int startIndex, int count)
        {
            try
            {
                var srcDatas = _context.RoadwayIssues.Skip(startIndex).Take(count).ToList();
                var requestDatas = srcDatas.Select(x => ObjectCopier.ConvertCopyObject<RoadwayIssue, RoadwayIssueRequest>(x)).ToList();
                var response = new ApiResponse<List<RoadwayIssueRequest>>(requestDatas);
                response.Success = true;
                response.Data = requestDatas;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoadwayIssueRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoadwayIssueRequest>> AdvancedSearch(RoadwayIssueCriteria criteria)
        {
            try
            {
                var query = _context.RoadwayIssues.AsQueryable();

                if (!string.IsNullOrEmpty(criteria.IssueCode))
                {
                    query = query.Where(r => r.IssueCode.Contains(criteria.IssueCode));
                }
                if (!string.IsNullOrEmpty(criteria.IssueName))
                {
                    query = query.Where(r => r.IssueName.Contains(criteria.IssueName));
                }
                if (!string.IsNullOrEmpty(criteria.Description))
                {
                    query = query.Where(r => r.Description.Contains(criteria.Description));
                }

                var results = query
                    .Skip((int)criteria.StartIndex)
                    .Take((int)criteria.Count)
                    .Select(x => ObjectCopier.ConvertCopyObject<RoadwayIssue, RoadwayIssueRequest>(x)).ToList();

                var response = new ApiResponse<List<RoadwayIssueRequest>>(results);
                response.Success = true;
                response.Data = results;
                response.TotalRecord = query.Count();

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoadwayIssueRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadwayIssueRequest> Create([FromBody] RoadwayIssueRequest request)
        {
            try
            {
                request.CreatedDate = DateTime.Now;

                var data = new RoadwayIssue();
                data = ObjectCopier.ConvertCopyObject<RoadwayIssueRequest, RoadwayIssue>(request);
                _context.RoadwayIssues.Add(data);
                _context.SaveChanges();

                request.Id = data.Id;
                var response = new ApiResponse<RoadwayIssueRequest>(request);
                response.Success = true;
                response.Message = "Tạo mới thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadwayIssueRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadwayIssueRequest> Update([FromBody] RoadwayIssueRequest request)
        {
            try
            {
                request.ModifiedDate = DateTime.Now;

                var sourceObject = _context.RoadwayIssues.Where(x => x.Id == request.Id).FirstOrDefault();
                ObjectCopier.CopyProperties(request, sourceObject);
                _context.RoadwayIssues.Update(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<RoadwayIssueRequest>(request);
                response.Success = true;
                response.Message = "Cập nhập thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadwayIssueRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoadwayIssueRequest> Delete([FromBody] RoadwayIssueRequest request)
        {
            try
            {
                var sourceObject = _context.RoadwayIssues.Where(x => x.Id == request.Id).FirstOrDefault();
                _context.RoadwayIssues.Remove(sourceObject);
                _context.SaveChanges();

                var response = new ApiResponse<RoadwayIssueRequest>(request);
                response.Success = true;
                response.Message = "Xóa thành công";

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoadwayIssueRequest>(request);
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
