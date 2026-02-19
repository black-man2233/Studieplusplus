using System;
using System.Collections;
using StudiePlusPlus.Domain.Academics;
using StudiePlusPlus.Domain.Common;
using StudiePlusPlus.Domain.Common.Enums;

namespace StudiePlusPlus.Domain.Scheduling;

public class WeeklySchedule : Entity<Guid>
{
    // public Guid ClassId { get; set; }
    public Class Class { get; set; }
    public Guid StudentId { get; set; }
    public Guid TeacherId { get; set; }
    public DayOfTheWeek DayOfTheWeek { get; set; } = DayOfTheWeek.None;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    public WeeklySchedule()
    {
    }

    public WeeklySchedule(Guid studentid, Guid teacherid, DayOfTheWeek dayoftheweek, DateTime starttime, DateTime endtime)
    {
        Id = Guid.NewGuid();
        // ClassId = classid;
        StudentId = studentid;
        TeacherId = teacherid;
        DayOfTheWeek = dayoftheweek;
        StartTime = starttime;
        EndTime = endtime;
    }
}