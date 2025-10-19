using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.PlanViewModels
{
    public class UpdatePlanViewModel
    {
        [Required (ErrorMessage = "Plan Name Is Required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Plan Name Must Be Between 2 And 50 Characters")]
        public string PlanName { get; set; } = null!;
        [Required(ErrorMessage = "Plan Price Is Required")]
        [Range(0.1, 10000, ErrorMessage = "Plan Price Must Be Between 0.1 And 10000")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Plan Duration In Days Is Required")]
        [Range(1, 365, ErrorMessage = "Plan Duration In Days Must Be Between 1 And 365 Days")]
        public int DurationInDays { get; set; }
        [Required(ErrorMessage = "Plan Description Is Required")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Plan Description Must Be Between 5 And 200 Characters")]
        public string Description { get; set; } = null!;    }
}
