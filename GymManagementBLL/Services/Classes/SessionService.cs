using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public bool CreateSession(SessionViewModel CreatedSession)
        {
            try
            {
                if (!IsTrainerExist(CreatedSession.Id)) return false;
                if (!IsCategoryExist(CreatedSession.Id)) return false;
                if (!IsValidDateRange(CreatedSession.StartDate, CreatedSession.EndDate)) return false;
                if (CreatedSession.Capacity > 25 || CreatedSession.Capacity < 0) return false;
                var MappedSession = mapper.Map<Session>(CreatedSession);
                unitOfWork.GetRepository<Session>().Add(MappedSession);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create Session Failed : {ex}");
                return false;
            }
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var Session = unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory();
            if (!Session.Any()) return [];
            
            var MappedSessions = mapper.Map< IEnumerable <Session> ,IEnumerable <SessionViewModel>>(Session);
            foreach (var session in MappedSessions)
            {
                var bookedSlots = unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id);
                session.AvailableSlots = session.Capacity - bookedSlots;
            }
            return MappedSessions;

        }
        public SessionViewModel? GetSessionById(int sessionId)
        {
            var Session = unitOfWork.SessionRepository.GetSessionWithTrainerAndCategory(sessionId);
            if (Session == null) return null;
            
            var MappedSession = mapper.Map<Session, SessionViewModel>(Session);
            var bookedSlots = unitOfWork.SessionRepository.GetCountOfBookedSlots(MappedSession.Id);
            MappedSession.AvailableSlots = MappedSession.Capacity - bookedSlots;
            return MappedSession;
        }
        public UpdateSessionViewModel? GetSessionToUpdate(int SessionId)
        {
            var Session = unitOfWork.GetRepository<Session>().GetById(SessionId);
            if (!IsSessionAvailableForUpdate(Session!)) return null;
            var MappedSession = mapper.Map<UpdateSessionViewModel>(Session!);
            return MappedSession;
        }
        public bool UpdateSession(UpdateSessionViewModel UpdatedSession, int sessionId)
        {
            try
            {
                var Session = unitOfWork.GetRepository<Session>().GetById(sessionId);
                if (!IsSessionAvailableForUpdate(Session!)) return false;
                if (!IsTrainerExist(UpdatedSession.TrainerId)) return false;
                if (!IsValidDateRange(UpdatedSession.StartDate, UpdatedSession.EndDate)) return false;
                mapper.Map(UpdatedSession, Session);
                Session!.UpdatedAt = DateTime.Now;
                unitOfWork.GetRepository<Session>().Update(Session!);
                return unitOfWork.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Session Failed : {ex}");
                return false;
            }


        }
        public bool RemoveSession(int sessionId)
        {
            try
            {
                var Session = unitOfWork.SessionRepository.GetById(sessionId);
                if (!IsSessionAvailableForRemoving(Session!)) return false;
                unitOfWork.GetRepository<Session>().Delete(Session!);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Remove Session Failed : {ex}");
                return false;
            }
        }



		public IEnumerable<TrainerSelectViewModel> GetTrainersForDropDown()
		{
			var trainers = unitOfWork.GetRepository<Trainer>().GetAll();
			return mapper.Map<IEnumerable<TrainerSelectViewModel>>(trainers);
		}
		public IEnumerable<CategorySelectViewModel> GetCategoriesForDropDown()
		{

			var categories = unitOfWork.GetRepository<Category>().GetAll();
			return mapper.Map<IEnumerable<CategorySelectViewModel>>(categories);
		}

		#region Private Methods
		private bool IsTrainerExist(int TrainerId)
        {
             return unitOfWork.GetRepository<Trainer>().GetById(TrainerId) is not null;

        }
        private bool IsCategoryExist(int CategoryId)
        {
            return unitOfWork.GetRepository<Category>().GetById(CategoryId) is not null;

        }
        private bool IsValidDateRange(DateTime StartDate, DateTime EndDate)
        {
            return StartDate < EndDate;
        }
        private bool IsSessionAvailableForUpdate(Session session)
        {
            if (session == null) return false;
            if (session.EndDate < DateTime.Now) return false;
            if (session.StartDate <= DateTime.Now) return false;
            var bookedSlots = unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id);
            if (bookedSlots > 0) return false;
            return true;
        }
        private bool IsSessionAvailableForRemoving(Session session)
        {
            if (session == null) return false;
            if (session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now) return false;
            if (session.StartDate > DateTime.Now) return false;
            var bookedSlots = unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id);
            if (bookedSlots > 0) return false;
            return true;
        }
        #endregion

    }
}
