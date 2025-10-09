using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    internal class PlanService : IPlanService
    {
        private readonly IUnitOfWork unitOfWork;

        public PlanService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans = unitOfWork.GetRepository<Plan>().GetAll();
            if (plans is null || !plans.Any()) return Enumerable.Empty<PlanViewModel>();
            return plans.Select(p => new PlanViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                DurationDays = p.DurationDays,
                IsActive = p.IsActive
            });
        }

        public PlanViewModel? GetPlanById(int PlanId)
        {
            var plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan is null) return null;
            return new PlanViewModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                DurationDays = plan.DurationDays,
                IsActive = plan.IsActive
            };
        }

        public UpdatePIanViewModel? GetPlanToUpdate(int PlanId)
        {
            var plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan is null || plan.IsActive == false || HasActiveMembers(PlanId)) return null;
            return new UpdatePIanViewModel
            {
                PlanName = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                DurationInDays = plan.DurationDays
            };
        }

        public bool TogglePlanStatus(int PlanId)
        {
            var plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan is null || HasActiveMembers(PlanId)) return false;
            plan.IsActive = plan.IsActive == true ? false : true;
            plan.UpdatedAt = DateTime.Now;
            try
            {
                unitOfWork.GetRepository<Plan>().Update(plan);
                return unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdatePlan(int PlanId, UpdatePIanViewModel UpdatedPlan)
        {
            var plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan is null || plan.IsActive == false || HasActiveMembers(PlanId)) return false;
            try
            {
                plan.Name = UpdatedPlan.PlanName;
                plan.Description = UpdatedPlan.Description;
                plan.Price = UpdatedPlan.Price;
                plan.DurationDays = UpdatedPlan.DurationInDays;
                plan.UpdatedAt = DateTime.Now;
                unitOfWork.GetRepository<Plan>().Update(plan);
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
