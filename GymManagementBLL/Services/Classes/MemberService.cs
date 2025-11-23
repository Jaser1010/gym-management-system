using AutoMapper;
using GymManagementBLL.Services.Attachmentservice;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMapper mapper;
        private readonly IAttachmentService attachmentService;

        public MemberService(IUnitOfWork UnitOfWork, IMapper mapper, IAttachmentService attachmentService)
        {
            this.UnitOfWork = UnitOfWork;
            this.mapper = mapper;
            this.attachmentService = attachmentService;
        }

        public bool CreateMember(CreateMemberViewModel CreatedMember)
        {
            try
            {
                var Repo = UnitOfWork.GetRepository<Member>();

                if (IsEmailExists(CreatedMember.email))
                    return false;
                if (IsPhoneExists(CreatedMember.phone))
                    return false;


                var PhotName = attachmentService.Upload("members",CreatedMember.PhotoFile);
                if(string.IsNullOrEmpty(PhotName))
                    return false;
                

				var Member = mapper.Map<Member>(CreatedMember);
                Member.Photo = PhotName;
				Repo.Add(Member);
                var IsCreated = UnitOfWork.SaveChanges() > 0;
                if (!IsCreated)
                {
                    attachmentService.Delete( PhotName, "members");
                    return false;
				}
                else
                {
                    return true;
				}
			}
            catch
            {
                return false;
            }


        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = UnitOfWork.GetRepository<Member>().GetAll();
            if (!Members.Any()) return [];
            return mapper.Map<IEnumerable<MemberViewModel>>(Members);
        }
        public MemberViewModel? GetMemberDetails(int MemberId)
        {
            var member = UnitOfWork.GetRepository<Member>().GetById(MemberId);

            if (member is null) return null;

            var viewModel = mapper.Map<MemberViewModel>(member);

            var activeMemberShip = UnitOfWork.GetRepository<MemberShip>()
                .GetAll(MP => MP.MemberId == MemberId && MP.Status == "Active").FirstOrDefault();

            if (activeMemberShip is not null)
            {
                var activePlan = UnitOfWork.GetRepository<Plan>().GetById(activeMemberShip.PlanId);

                viewModel.PlanName = activePlan?.Name;
                viewModel.MembershipStartDate = activeMemberShip.CreatedAt.ToShortDateString();
                viewModel.MembershipEndDate = activeMemberShip.EndDate.ToShortDateString();
            }

            return viewModel;
        }
        public HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId)
        {
            var MemberHealthRecord = UnitOfWork.GetRepository<HealthRecord>().GetById(MemberId);
            if (MemberHealthRecord is null) return null;

            return mapper.Map<HealthRecordViewModel>(MemberHealthRecord);
        }
        public MemberToUpdateViewModel? GetMemberToUpdate(int MemberId)
        {
            var member = UnitOfWork.GetRepository<Member>().GetById(MemberId);
            if (member is null) return null;
            return mapper.Map<MemberToUpdateViewModel>(member);
        }
        public bool RemoveMember(int MemberId)
        {
            var Repo = UnitOfWork.GetRepository<Member>();
            var member = Repo.GetById(MemberId);
            if (member is null) return false;
            var HasActiveMemberSessions = UnitOfWork.GetRepository<MemberSession>()
                .GetAll(ms => ms.MemberId == MemberId && ms.Session.StartDate > DateTime.Now).Any();
            if (HasActiveMemberSessions) return false;
            var memberShips = UnitOfWork.GetRepository<MemberShip>().GetAll(m => m.MemberId == MemberId);
            try
            {
                if (memberShips.Any())
                {
                    foreach (var memberShip in memberShips)
                    {
                        UnitOfWork.GetRepository<MemberShip>().Delete(memberShip);
                    }
                }
                UnitOfWork.GetRepository<Member>().Delete(member);
                var IsDeleted = UnitOfWork.SaveChanges() > 0;
                if (IsDeleted)
                    attachmentService.Delete(member.Photo, "members");
                return IsDeleted;
				
			}
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateMember(int MemberId, MemberToUpdateViewModel UpdatedMember)
        {
            var emailExist = UnitOfWork.GetRepository<Member>().GetAll(
                m => m.Email == UpdatedMember.email && m.Id != MemberId);

            var PhoneExist = UnitOfWork.GetRepository<Member>().GetAll(
                m => m.Phone == UpdatedMember.phone && m.Id != MemberId);

            if (emailExist.Any() || PhoneExist.Any()) return false;

            var Repo = UnitOfWork.GetRepository<Member>();
            var Member = Repo.GetById(MemberId);
            if (Member is null) return false;
            mapper.Map(UpdatedMember, Member);

            Repo.Update(Member);
            return UnitOfWork.SaveChanges() > 0;
        }

        #region Helper Methods
        private bool IsEmailExists(string email)
        {
            var existing = UnitOfWork.GetRepository<Member>().GetAll(
                m => m.Email == email);
            return existing.Any();
        }
        private bool IsPhoneExists(string phone)
        {
            var existing = UnitOfWork.GetRepository<Member>().GetAll(
                m => m.Phone == phone);
            return existing.Any();
        }
        #endregion
    }
}
