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
    internal class MemberService : IMemberService
    {
        private readonly IUnitOfWork unitOfWork;

        public MemberService(IUnitOfWork UnitOfWork)
        {
            unitOfWork = UnitOfWork;
        }

        public bool CreateMember(CreateMemberViewModel createMember)
        {
            try
            {
                var emailExists = unitOfWork.GetRepository<Member>().GetAll(m => m.Email == createMember.email).Any();
                var PhoneExists = unitOfWork.GetRepository<Member>().GetAll(m => m.Phone == createMember.phone).Any();
                if (PhoneExists || emailExists) return false;
                var member = new Member()
                {
                    Name = createMember.name,
                    Email = createMember.email,
                    Phone = createMember.phone,
                    DateOfBirth = createMember.DateOfBirth,
                    Gender = createMember.Gender,
                    Address = new Address
                    {
                        BuildingNumber = createMember.BuildingNumber,
                        Street = createMember.Street,
                        City = createMember.City
                    },
                    HealthRecord = new HealthRecord
                    {
                        Weight = createMember.HealthRecordViewModel.Weight,
                        Height = createMember.HealthRecordViewModel.Height,
                        BloodType = createMember.HealthRecordViewModel.BloodType,
                        Note = createMember.HealthRecordViewModel.note
                    }
                };
                unitOfWork.GetRepository<Member>().Add(member);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = unitOfWork.GetRepository<Member>().GetAll();
            if (members is null || !members.Any()) return Enumerable.Empty<MemberViewModel>();
            var memberViewModels = members.Select(m => new MemberViewModel
            {
                Id = m.Id,
                Photo = m.Photo,
                Name = m.Name,
                Email = m.Email,
                Phone = m.Phone,
                Gender = m.Gender.ToString()

            }).ToList();
            return memberViewModels;
        }

        public MemberViewModel? GetMemberDetails(int MemberId)
        {
            var member = unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (member is null) return null;
            var ViewModel = new MemberViewModel
            {
                Photo = member.Photo,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Address = $"{member.Address.BuildingNumber}- {member.Address.Street}- {member.Address.City}"

            };

            var ActiveMemberShip = unitOfWork.GetRepository<MemberShip>().GetAll(m => m.MemberId == MemberId && m.Status == "Active").FirstOrDefault();
            if (ActiveMemberShip is not null)
            {
                var plan = unitOfWork.GetRepository<Plan>().GetById(ActiveMemberShip.PlanId);
                ViewModel.PlanName = plan?.Name;
                ViewModel.MembershipStartDate = ActiveMemberShip.CreatedAt.ToShortDateString();
                ViewModel.MembershipEndDate = ActiveMemberShip.EndDate.ToShortDateString();
            }
            return ViewModel;
        }

        public HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId)
        {
            var MemberHealthRecord = unitOfWork.GetRepository<HealthRecord>().GetById(MemberId);
            if (MemberHealthRecord is null) return null;
            return new HealthRecordViewModel
            {
                Weight = MemberHealthRecord.Weight,
                Height = MemberHealthRecord.Height,
                BloodType = MemberHealthRecord.BloodType,
                note = MemberHealthRecord.Note
            };
        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int MemberId)
        {
            var member = unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (member is null) return null;
            return new MemberToUpdateViewModel
            {
                Name = member.Name,
                Photo = member.Photo,
                email = member.Email,
                phone = member.Phone,
                BuildingNumber = member.Address.BuildingNumber,
                Street = member.Address.Street,
                City = member.Address.City
            };
        }

        public bool RemoveMember(int MemberId)
        {
            var member = unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (member is null) return false;
            var HasActiveMemberSessions = unitOfWork.GetRepository<MemberSession>()
                .GetAll(ms => ms.MemberId == MemberId && ms.Session.StartDate > DateTime.Now).Any();
            if (HasActiveMemberSessions) return false;
            var memberShips = unitOfWork.GetRepository<MemberShip>().GetAll(m => m.MemberId == MemberId);
            try
            {
                if (memberShips.Any())
                {
                    foreach (var memberShip in memberShips)
                    {
                        unitOfWork.GetRepository<MemberShip>().Delete(memberShip);
                    }
                }
                unitOfWork.GetRepository<Member>().Delete(member);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateMember(int MemberId, MemberToUpdateViewModel UpdatedMember)
        {
            try
            {
                var EmailExists = unitOfWork.GetRepository<Member>().GetAll(m => m.Email == UpdatedMember.email).Any();
                var phoneExists = unitOfWork.GetRepository<Member>().GetAll(m => m.Phone == UpdatedMember.phone).Any();
                if (EmailExists || phoneExists) return false;
                var member = unitOfWork.GetRepository<Member>().GetById(MemberId);
                if (member is null) return false;
                member.Email = UpdatedMember.email;
                member.Phone = UpdatedMember.phone;
                member.Address.BuildingNumber = UpdatedMember.BuildingNumber;
                member.Address.Street = UpdatedMember.Street;
                member.Address.City = UpdatedMember.City;
                member.UpdatedAt = DateTime.Now;
                unitOfWork.GetRepository<Member>().Update(member);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
