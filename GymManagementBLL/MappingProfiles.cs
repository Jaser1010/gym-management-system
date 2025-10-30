using AutoMapper;
using GymManagementBLL.ViewModels.MembershipViewModels;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementBLL.ViewModels.PlanViewModels;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.Entities;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL
{
    public class MappingProfiles : Profile
    {


        public MappingProfiles()
        {

			MapTrainer();
			MapSession();
			MapMember();
			MapPlan();
			MapMemberSession();

		}
        private void MapTrainer()
        {
			#region MapTrainer
			CreateMap<CreateTrainerViewModel, Trainer>()
				.ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
				{
					BuildingNumber = src.BuildingNumber,
					Street = src.Street,
					City = src.City
				}));
			CreateMap<Trainer, TrainerViewModel>()
							.ForMember(dest => dest.Address,
							opt => opt.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.Street} - {src.Address.City}"));

			CreateMap<Trainer, TrainerToUpdateViewModel>()
				.ForMember(dist => dist.Street, opt => opt.MapFrom(src => src.Address.Street))
				.ForMember(dist => dist.City, opt => opt.MapFrom(src => src.Address.City))
				.ForMember(dist => dist.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber));

			CreateMap<TrainerToUpdateViewModel, Trainer>()
			.ForMember(dest => dest.Name, opt => opt.Ignore())
			.AfterMap((src, dest) =>
			{
				dest.Address.BuildingNumber = src.BuildingNumber;
				dest.Address.City = src.City;
				dest.Address.Street = src.Street;
				dest.UpdatedAt = DateTime.Now;
			});
			#endregion
		}
		private void MapSession()
		{
			#region MapSession

			CreateMap<CreateSessionViewModel, Session>();
			CreateMap<Session, SessionViewModel>()
						.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.SessionCategory.CategoryName))
						.ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.SessionTrainer.Name))
						.ForMember(dest => dest.AvailableSlots, opt => opt.Ignore()); // Will Be Calculated After Map
			CreateMap<UpdateSessionViewModel, Session>().ReverseMap();

			#endregion
		}
		private void MapMember()
		{
			#region MapMember

			CreateMap<CreateMemberViewModel, Member>()
				  .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
				  {
					  BuildingNumber = src.BuildingNumber,
					  City = src.City,
					  Street = src.Street
				  })).ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => src.HealthRecordViewModel));






			CreateMap<HealthRecordViewModel, HealthRecord>().ReverseMap();
			CreateMap<Member, MemberViewModel>()
		   .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
			.ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()))
			.ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.Street} - {src.Address.City}"));

			CreateMap<Member, MemberToUpdateViewModel>()
			.ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
			.ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
			.ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street));

			CreateMap<MemberToUpdateViewModel, Member>()
				.ForMember(dest => dest.Name, opt => opt.Ignore())
				.ForMember(dest => dest.Photo, opt => opt.Ignore())
				.AfterMap((src, dest) =>
				{
					dest.Address.BuildingNumber = src.BuildingNumber;
					dest.Address.City = src.City;
					dest.Address.Street = src.Street;
					dest.UpdatedAt = DateTime.Now;
				});



			#endregion
		}
		private void MapPlan()
		{
			#region MapPlan

			CreateMap<Plan, PlanViewModel>();
			CreateMap<Plan, UpdatePlanViewModel>().ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Name));
			CreateMap<UpdatePlanViewModel, Plan>()
		   .ForMember(dest => dest.Name, opt => opt.Ignore())
		   .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

			#endregion
		}
		private void MapMemberSession()
		{
			#region MapMemberSession

			CreateMap<MemberShip, MemberShipForMemberViewModel>()
					 .ForMember(dist => dist.MemberName, Option => Option.MapFrom(Src => Src.Member.Name))
					 .ForMember(dist => dist.PlanName, Option => Option.MapFrom(Src => Src.Plan.Name))
					 .ForMember(dist => dist.StartDate, Option => Option.MapFrom(X => X.CreatedAt));

			CreateMap<MemberShip, MemberShipViewModel>()
					 .ForMember(dist => dist.MemberName, Option => Option.MapFrom(Src => Src.Member.Name))
					 .ForMember(dist => dist.PlanName, Option => Option.MapFrom(Src => Src.Plan.Name))
										  .ForMember(dist => dist.StartDate, Option => Option.MapFrom(X => X.CreatedAt));

			CreateMap<CreateMemberShipViewModel, MemberShip>();
			CreateMap<Member, MemberSelectListViewModel>();
			CreateMap<Plan, PlanSelectListViewModel>();

			#endregion
		}

	}
}
