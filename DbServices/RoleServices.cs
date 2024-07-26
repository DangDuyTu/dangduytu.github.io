using Helper;
using RoadTrafficManagement.AppModels;
using RoadTrafficManagement.Request;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RoadTrafficManagement.DbServices
{
    public class RoleServices
    {
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly QuanLyGiaoThongContext _context;
        private readonly uint _userId = 1;

        public RoleServices(QuanLyGiaoThongContext context, IHttpContextAccessor httpAccessor)
        {
            _httpAccessor = httpAccessor;
            _context = context;
            //_userId = (uint)Convert.ToInt32(_httpAccessor.HttpContext.Items["UserId"]);
        }

        public ApiResponse<RoleRequest> GetById(uint id)
        {
            try
            {
                var role = _context.Roles.FirstOrDefault(r => r.Id == id);
                var roleRequest = ObjectCopier.ConvertCopyObject<Role, RoleRequest>(role);
                var response = new ApiResponse<RoleRequest>(roleRequest);
                response.Success = true;
                response.Data = roleRequest;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoleRequest>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoleRequest> GetByUser(uint id)
        {
            try
            {
                var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == id);
                var roleRequest = ObjectCopier.ConvertCopyObject<Role, RoleRequest>(user.Role);
                var response = new ApiResponse<RoleRequest>(roleRequest);
                response.Success = true;
                response.Data = roleRequest;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoleRequest>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoleRequest>> GetAll()
        {
            try
            {
                var roles = _context.Roles.ToList();
                var rolesRequest = roles.Select(x => ObjectCopier.ConvertCopyObject<Role, RoleRequest>(x)).ToList();
                var response = new ApiResponse<List<RoleRequest>>(rolesRequest);
                response.Success = true;
                response.Data = rolesRequest;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoleRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoleDetailRequest>> GetRoleDetails(uint id)
        {
            try
            {
                var role = _context.Roles.Include(r => r.RoleDetails).FirstOrDefault(x => x.Id == id);
                var details = role.RoleDetails.Select(s => ObjectCopier.ConvertCopyObject<RoleDetail, RoleDetailRequest>(s)).ToList();
                var response = new ApiResponse<List<RoleDetailRequest>>();
                response.Success = true;
                response.Data = details;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoleDetailRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<List<RoleDetailRequest>> GetDetailByUser(uint id)
        {
            try
            {
                var user = _context.Users.Include(s => s.Role).FirstOrDefault(x => x.Id == id);
                var role = _context.Roles.Include(s => s.RoleDetails).FirstOrDefault(r => r.Id == user.RoleId);
                var details = role.RoleDetails.Select(s => ObjectCopier.ConvertCopyObject<RoleDetail, RoleDetailRequest>(s)).ToList();
                var response = new ApiResponse<List<RoleDetailRequest>>();
                response.Success = true;
                response.Data = details;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<RoleDetailRequest>>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<RoleDetailRequest> UpdateDetail(RoleDetailRequest roleDetail)
        {
            try
            {
                var detail = _context.RoleDetails.FirstOrDefault(s => s.Id == roleDetail.Id);
                ObjectCopier.CopyProperties(roleDetail, detail);
                _context.RoleDetails.Update(detail);
                _context.SaveChanges();

                var response = new ApiResponse<RoleDetailRequest>();
                response.Success = true;
                response.Data = roleDetail;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<RoleDetailRequest>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponse<string> Authorization(URCommandRequest request)
        {
            try
            {
                var user = _context.Users.Include(s => s.Role).FirstOrDefault(x => x.Id == _userId);
                var role = user.Role;
                var details = _context.RoleDetails.Include(s => s.Command)
                    .Include(s => s.SubSystem).Where(s =>
                    s.RoleId == role.Id
                    && s.SubSystem.SubSystemCode == request.SubSystemCode
                    && s.Command.CommandCode == request.CommandCode
                );
                var response = new ApiResponse<string>();
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>();
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}