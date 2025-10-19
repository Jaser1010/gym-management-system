using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModels;
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
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PlanService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans = unitOfWork.GetRepository<Plan>().GetAll();
            if (!plans.Any()) return [];
            return mapper.Map<IEnumerable<PlanViewModel>>(plans);
        }
        public PlanViewModel? GetPlanById(int planId)
        {
            var plan = unitOfWork.GetRepository<Plan>().GetById(planId);

            if (plan == null)
                return null;

            return mapper.Map<PlanViewModel>(plan);
        }
        public UpdatePlanViewModel? GetPlanToUpdate(int planId)
        {
            var plan = unitOfWork.GetRepository<Plan>().GetById(planId);

            if (plan == null || plan.IsActive == false || HasActiveMembers(planId))
                return null;

            return mapper.Map<UpdatePlanViewModel>(plan);
        }
        public bool TogglePlanStatus(int PlanId)
        {
            try
            {
                var Repo = unitOfWork.GetRepository<Plan>();
                var Plan = Repo.GetById(PlanId);
                if (Plan is null || HasActiveMembers(PlanId)) return false;
                Plan.IsActive = Plan.IsActive == true ? false : true;
                Plan.UpdatedAt = DateTime.Now;
                Repo.Update(Plan);
                return unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdatePlan(int Id, UpdatePlanViewModel updatePlanViewModel)
        {
            try
            {
                var Repo = unitOfWork.GetRepository<Plan>();
                var Plan = Repo.GetById(Id);
                if (Plan is null || HasActiveMembers(Id)) return false;
                mapper.Map(updatePlanViewModel, Plan);
                Repo.Update(Plan);
                return unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        #region Helpers
        private bool HasActiveMembers(int PlanId)
        {
            var activeMembers = unitOfWork.GetRepository<MemberShip>()
                .GetAll(m => m.PlanId == PlanId && m.Status == "Active");
            return activeMembers.Any();
        }
        #endregion
    }
}
