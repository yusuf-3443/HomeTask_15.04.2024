using Domain.DTOs.MemberDTO;
using Domain.Responses;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

    [Route("/api/Member")]
    [ApiController]
    public class MemberController(IMemberService service) : ControllerBase
    {
        [HttpGet("Get-All-Members")]
        public async Task<Response<List<GetMemberDTO>>> GetMembers()
        {
            return await service.GetMembers();
        }

        [HttpGet("MemberId:int")]
        public async Task<Response<GetMemberDTO>> GetMemberById(int MemberId)
        {
            return await service.GetMemberById(MemberId);
        }

        [HttpPost("Add-Member")]
        public async Task<Response<string>> AddMember(AddMemberDTO add)
        {
            return await service.AddMember(add);
        }

        [HttpPut("Update-Member")]
        public async Task<Response<string>> UpdateMember(UpdateMemberDTO update)
        {
            return await service.UpdateMember(update);
        }

        [HttpDelete("MemberId:int")]
        public async Task<Response<bool>> DeleteMember(int MemberId)
        {
            return await service.DeleteMember(MemberId);
        }
    }

