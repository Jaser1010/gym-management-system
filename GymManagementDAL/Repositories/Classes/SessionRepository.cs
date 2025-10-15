using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext dbContext;

        public SessionRepository(GymDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategory()
        {
            return dbContext.Sessions
                .Include(s => s.SessionTrainer)
                .Include(s => s.SessionCategory)
                .ToList();
        }

        public int GetCountOfBookedSlots(int sessionId)
        {
            return dbContext.memberSessions
                .Count(ms => ms.SessionId == sessionId);
        }

        public Session? GetSessionWithTrainerAndCategory(int sessionId)
        {
            return dbContext.Sessions
                .Include(s => s.SessionTrainer)
                .Include(s => s.SessionCategory)
                .FirstOrDefault(s => s.Id == sessionId);
        }
    }
}
