using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.MemberViewModels
{
    internal class HealthRecordViewModel
    {
        [Required (ErrorMessage = "Weight Is Required")]
        [Range(1,500, ErrorMessage = "Weight Must Be Between 1 And 500 Kg")]
        public decimal Weight { get; set; }
        [Required (ErrorMessage = "Height Is Required")]
        [Range(0.1,300, ErrorMessage = "Height Must Be Between 0.1 And 3 Meters")]
        public decimal Height { get; set; }
        [Required (ErrorMessage = "Blood Type Is Required")]
        [StringLength(3, ErrorMessage = "Blood Type Must Be Between 3 Characters or less")]
        public string BloodType { get; set; } = null!;
        public string? note { get; set; }
    }
}
