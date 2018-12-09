using System;
using System.Collections.Generic;
using System.Linq;

namespace SleepingGuardMonitor
{
  public class GuardShifts
  {
    public string GuardId { get; set; }

    private List<Shift> _shifts { get; set; }

    public GuardShifts(string guardId)
    {
      GuardId = guardId;
      _shifts = new List<Shift>();
    }

    public void StartShift(DateTime startShift)
    {
      _shifts.Add(new Shift(startShift));
    }

    public void EndShift(DateTime endShift)
    {
      GetActiveShift().EndShift(endShift);
    }

    public void FallAsleep(DateTime startSleep)
    {
      GetActiveShift().FallAsleep(startSleep);
    }

    public void WakeUp(DateTime endSleep)
    {
      GetActiveShift().WakeUp(endSleep);
    }

    public int GetTotalMinutesSlept()
    {
      var totalMinutesSlept = 0;
      foreach (var shift in _shifts)
      {
        totalMinutesSlept += shift.TotalMinutesSlept;
      }

      return totalMinutesSlept;
    }

    public int GetFavoriteMinuteSlept()
    {
      var aggregate = new List<KeyValuePair<int, int>>();
      foreach (var shift in _shifts)
      {
        aggregate.AddRange(shift.SleptOnMinuteCount.ToList());
      }

      var favoriteMinute = aggregate
        .GroupBy(m => m.Key)
        .ToDictionary(item => item.Key, item => item.Select(kvp => kvp.Value).Sum())
        .OrderByDescending(x => x.Value)
        .Select(x => x.Key)
        .First();

      return favoriteMinute;
    }

    private Shift GetActiveShift()
    {
      return _shifts.Last();
    }
  }
}