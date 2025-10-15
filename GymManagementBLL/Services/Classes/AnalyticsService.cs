using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.AnalyticsViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork unitOfWork;

        public AnalyticsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public AnalyticsViewModel GetAnalyticsData()
        {
            var Session = unitOfWork.SessionRepository.GetAll();
            return new AnalyticsViewModel
            {
                ActiveMembers = unitOfWork.GetRepository<MemberShip>().GetAll(m => m.Status == "Active").Count(),
                TotalMembers = unitOfWork.GetRepository<MemberShip>().GetAll().Count(),
                TotalTrainers = unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                UpcomingSessions = Session.Count(s => s.StartDate > DateTime.Now),
                OngoingSessions = Session.Count(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now),
                CompletedSessions = Session.Count(s => s.EndDate < DateTime.Now)
            };
        }
    }
}
