using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.BookingViewModels;
using GymManagementBLL.ViewModels.MembershipViewModels;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes
{
	public class BookingService : IBookingService
	{
		private readonly IUnitOfWork UnitOfWork;
		private readonly IMapper mapper;

		public BookingService(IUnitOfWork UnitOfWork, IMapper mapper)
		{
			this.UnitOfWork = UnitOfWork;
			this.mapper = mapper;
		}
		public bool CancelBooking(int MemberId, int SessionId)
		{
			try
			{
				var session = UnitOfWork.SessionRepository.GetById(SessionId);
				if (session is null || session.StartDate <= DateTime.Now) return false;

				var Booking = UnitOfWork.BookingRepository.GetAll(X => X.SessionId == SessionId && X.MemberId == MemberId)
														   .FirstOrDefault();
				if (Booking is null) return false;
				UnitOfWork.BookingRepository.Delete(Booking);
				return UnitOfWork.SaveChanges() > 0;
			}
			catch
			{
				return false;
			}
		}
		public bool CreateNewBooking(CreateBookingViewModel createdBooking)
		{
			try
			{
				var session = UnitOfWork.SessionRepository.GetById(createdBooking.SessionId);
				if (session is null || session.StartDate <= DateTime.Now) return false;

				var HasActiveMembership = UnitOfWork.MembershipRepository.GetAll(X => X.MemberId == createdBooking.MemberId && X.Status == "Active").Any();
				if (!HasActiveMembership) return false;

				var HasAvailableSolts = session.Capacity - UnitOfWork.SessionRepository.GetCountOfBookedSlots(createdBooking.SessionId);
				if (HasAvailableSolts == 0) return false;
				UnitOfWork.BookingRepository.Add(new MemberSession()
				{
					MemberId = createdBooking.MemberId,
					SessionId = createdBooking.SessionId,
					IsAttended = false
				});

				return UnitOfWork.SaveChanges() > 0;
			}
			catch
			{
				return false;
			}
		}
		public IEnumerable<SessionViewModel> GetAllSessions()
		{
			var bookings = UnitOfWork.SessionRepository
				.GetAllSessionsWithTrainerAndCategory(X => X.EndDate >= DateTime.Now)
				.OrderByDescending(X => X.StartDate);

			if (!bookings.Any()) return null!;
			var MappedSession = mapper.Map<IEnumerable<SessionViewModel>>(bookings);
			foreach (var item in MappedSession)
			{
				item.AvailableSlots = item.Capacity - UnitOfWork.SessionRepository.GetCountOfBookedSlots(item.Id);
			}
			return MappedSession;
		}
		public IEnumerable<MemberForSessionViewModel> GetMembersForUpcomingBySessionId(int sessionId)
		{
			var MemberForSession = UnitOfWork.BookingRepository.GetBySessionId(sessionId);
			return MemberForSession.Select(X => new MemberForSessionViewModel
			{
				MemberId = X.MemberId,
				SessionId = sessionId,
				MemberName = X.Member.Name,
				BookingDate = X.CreatedAt.ToString()
			});
		}
		public IEnumerable<MemberForSessionViewModel> GetMembersForOngoingBySessionId(int sessionId)
		{
			var MemberForSession = UnitOfWork.BookingRepository.GetBySessionId(sessionId);
			return MemberForSession.Select(X => new MemberForSessionViewModel
			{
				MemberId = X.MemberId,
				SessionId = sessionId,
				MemberName = X.Member.Name,
				BookingDate = X.CreatedAt.ToString(),
				IsAttended = X.IsAttended
			});
		}
		public IEnumerable<MemberSelectListViewModel> GetMembersForDropDown(int sessionId)
		{
			var bookedMemberIds = UnitOfWork.GetRepository<MemberSession>()
								   .GetAll(x => x.SessionId == sessionId)
								   .Select(x => x.MemberId)
								   .ToList();

			var availableMembers = UnitOfWork.GetRepository<Member>()
											  .GetAll(x => !bookedMemberIds.Contains(x.Id));

			return mapper.Map<IEnumerable<MemberSelectListViewModel>>(availableMembers);
		}
		public bool MemberAttended(int MemberId, int SessionId)
		{
			try
			{
				var memberSession = UnitOfWork.GetRepository<MemberSession>()
										   .GetAll(X => X.MemberId == MemberId && X.SessionId == SessionId)
										   .FirstOrDefault();
				if (memberSession is null) return false;

				memberSession.IsAttended = true;
				memberSession.UpdatedAt = DateTime.Now;
				UnitOfWork.GetRepository<MemberSession>().Update(memberSession);
				return UnitOfWork.SaveChanges() > 0;
			}
			catch
			{
				return false;
			}
		}
	}
}
