using GymManagementBLL.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMembers();
        bool CreateMember(CreateMemberViewModel createMember);
        MemberViewModel? GetMemberDetails(int MemberId);
        HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId);
        MemberToUpdateViewModel? GetMemberToUpdate(int MemberId);
        bool UpdateMember(int MemberId, MemberToUpdateViewModel UpdatedMember);
        bool RemoveMember(int MemberId);
    }
}
