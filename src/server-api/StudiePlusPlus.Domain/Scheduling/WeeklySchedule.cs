using System;
using StudiePlusPlus.Domain.Common;
using StudiePlusPlus.Domain.Common.Enums;

namespace StudiePlusPlus.Domain.Scheduling;

public class WeeklySchedule : Entity<Guid>
{
    public Guid ClassGroupId { get; set; }
    public Guid StudentId { get; set; }
    public Guid TeacherId { get; set; }
    public DayOfTheWeek DayOfTheWeek { get; set; } = DayOfTheWeek.None;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public WeeklySchedule()
    {
    }

    public WeeklySchedule(Guid classgroupid, Guid studentid, Guid teacherid, DayOfTheWeek dayoftheweek,
        DateTime starttime, DateTime endtime)
    {
        Id = Guid.NewGuid();
        ClassGroupId = classgroupid;
        StudentId = studentid;
        TeacherId = teacherid;
        DayOfTheWeek = dayoftheweek;
        StartTime = starttime;
        EndTime = endtime;
    }
}