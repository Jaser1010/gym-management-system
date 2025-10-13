using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
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

        public SessionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var Session = unitOfWork.GetRepository<Session>().GetAll();
            if (!Session.Any()) return [];
            return Session.Select(S => new SessionViewModel
            {
                Id = S.Id,
                Description = S.Description,
                StartDate = S.StartDate,
                EndDate = S.EndDate,
                Capacity = S.Capacity,
                
            });

        }
    }
}
