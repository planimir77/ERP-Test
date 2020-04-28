namespace CustomERP.Data.Configurations
{
    // ReSharper disable once RedundantNameQualifier
    using CustomERP.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ScheduleDayConfiguration :IEntityTypeConfiguration<ScheduleDay>
    {
        public void Configure(EntityTypeBuilder<ScheduleDay> builder)
        {
            builder
                .Property(e => e.Name)
                .IsRequired();
            builder
                .Property(e => e.WorkingMode)
                .IsRequired();
            builder
                .Property(e => e.Begins)
                .IsRequired(false);
            builder
                .Property(e => e.IncludingRest)
                .IsRequired(false);
            builder
                .Property(e => e.Duration)
                .IsRequired(false);
        }
    }
}
