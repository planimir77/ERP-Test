﻿namespace CustomERP.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CustomERP.Data.Common.Models;

    public class Shift : BaseDeletableModel<int>
    {
        public Shift()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            this.Employees = new HashSet<ApplicationUser>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public int ScheduleId { get; set; }

        public Schedule Schedule { get; set; }

        public virtual ICollection<ApplicationUser> Employees { get; set; }
    }
}
