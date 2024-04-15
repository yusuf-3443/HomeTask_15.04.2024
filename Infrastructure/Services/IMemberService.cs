using Domain.DTOs.MemberDTO;
using Domain.Entities;
using Domain.Responses;

namespace Infrastructure.Services;

public interface IMemberService
{
    Task<Response<List<GetMemberDTO>>> GetMembers();
    Task<Response<GetMemberDTO>> GetMemberById(int id);
    Task<Response<string>> AddMember(AddMemberDTO member);
    Task<Response<string>> UpdateMember(UpdateMemberDTO member);
    Task<Response<bool>> DeleteMember(int id);
}