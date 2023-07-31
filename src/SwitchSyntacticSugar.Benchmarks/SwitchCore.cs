namespace SwitchSyntacticSugar.Benchmarks;

using System;
using System.Collections.Generic;

public static class SwitchCore
{
    public static string GetWorkTimeStatement(DayOfWeek dayOfWeek)
    {
        var workTime = "";
        switch (dayOfWeek)
        {
            case DayOfWeek.Monday:
                workTime = "9-5";
                break;
            case DayOfWeek.Tuesday:
                workTime = "10-3";
                break;
            case DayOfWeek.Wednesday:
                workTime = "10-3";
                break;
            case DayOfWeek.Thursday:
                workTime = "9-5";
                break;
            case DayOfWeek.Friday:
                workTime = "10-3";
                break;
            default:
                break;
        }

        return workTime;
    }

    public static string GetWorkTimeExpression(DayOfWeek dayOfWeek) =>
        dayOfWeek switch
        {
            DayOfWeek.Monday => "9-5",
            DayOfWeek.Tuesday => "10-3",
            DayOfWeek.Wednesday => "10-3",
            DayOfWeek.Thursday => "9-5",
            DayOfWeek.Friday => "10-3",
            _ => ""
        };

    private static readonly Dictionary<DayOfWeek, string> _workSchedule =
        new()
        {
            { DayOfWeek.Monday, "9-5" },
            { DayOfWeek.Tuesday, "10-3" },
            { DayOfWeek.Wednesday, "10-3" },
            { DayOfWeek.Thursday, "9-5" },
            { DayOfWeek.Friday, "10-3" }
        };

    public static string GetWorkTimeDictionary(DayOfWeek dayOfWeek)
    {
        if (_workSchedule.TryGetValue(dayOfWeek, out var workTime))
        {
            return workTime;
        }

        return "";
    }
}
