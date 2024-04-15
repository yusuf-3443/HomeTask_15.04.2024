using System.Net;
using Domain.DTOs.MemberDTO;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class MemberService(DataContext context) : IMemberService
{
    public async Task<Response<List<GetMemberDTO>>> GetMembers()
{
    try
    {
        var members = await context.Members.ToListAsync();
        var list = new List<GetMemberDTO>();
        foreach (var member in members)
        {
            var memberDto = new GetMemberDTO()
            {
            FirstName = member.FirstName,
                LastName = member.LastName,
                Loans = member.Loans
            };
            list.Add(memberDto);
        }
        return new Response<List<GetMemberDTO>>(list);
    }
    catch (Exception e)
    {
        return new Response<List<GetMemberDTO>>(HttpStatusCode.InternalServerError, e.Message);
    }
}

public async Task<Response<GetMemberDTO>> GetMemberById(int id)
{
    try
    {
        var member = await context.Members.FirstOrDefaultAsync(x => x.Id == id);
        if (member == null) return new Response<GetMemberDTO>(HttpStatusCode.BadRequest, "Member not found");
        var response = new GetMemberDTO()
        {
            FirstName = member.FirstName,
            LastName = member.LastName,
            Loans = member.Loans
        };
        return new Response<GetMemberDTO>(response);
    }
    catch (Exception e)
    {
        return new Response<GetMemberDTO>(HttpStatusCode.InternalServerError, e.Message);
    }
}

public async Task<Response<string>> AddMember(AddMemberDTO member)
{
    try
    {
        var newMember = new Member()
        {
            FirstName = member.FirstName,
            LastName = member.LastName,
            Loans = member.Loans
        };
        await context.Members.AddAsync(newMember);
        var res = await context.SaveChangesAsync();
        if (res > 0) return new Response<string>("Member successfully added");
        return new Response<string>(HttpStatusCode.BadRequest, "Failed to add member");
    }
    catch (Exception e)
    {
        return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
    }
}

public async Task<Response<string>> UpdateMember(UpdateMemberDTO member)
{
    try
    {
        var updateMember = await context.Members.FirstOrDefaultAsync(x => x.Id == member.Id);
        if (updateMember == null) return new Response<string>("Member not found");
        updateMember.FirstName = member.FirstName;
        updateMember.LastName = member.LastName;
        updateMember.Loans = member.Loans;
        var res = await context.SaveChangesAsync();
        if (res > 0) return new Response<string>("Member successfully updated");
        return new Response<string>("Failed to update member");
    }
    catch (Exception e)
    {
        return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
    }
}

public async Task<Response<bool>> DeleteMember(int id)
{
    try
    {
        var member = await context.Members.FindAsync(id);
        if (member == null) return new Response<bool>(HttpStatusCode.BadRequest, "Member not found");
        context.Members.Remove(member);
        var res = await context.SaveChangesAsync();
        return new Response<bool>(true);
    }
    catch (Exception e)
    {
        return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
    }
}
}