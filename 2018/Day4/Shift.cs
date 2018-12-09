using System;
using System.Collections.Generic;

namespace SleepingGuardMonitor
{
  public class Shift
  {
    public DateTime ShiftStart { get; set; }
    public DateTime ShiftEnd { get; set; }
    public int TotalMinutesSlept { get; set; }
    public Dictionary<int, int> SleptOnMinuteCount { get; set; }
    private bool _isSleeping { get; set; }
    private DateTime _startSleep { get; set; }

    public Shift(DateTime shiftStart)
    {
      ShiftStart = shiftStart;
      _isSleeping = false;

      SleptOnMinuteCount = new Dictionary<int, int>();
      for (var i = 0; i < 60; i++)
      {
        SleptOnMinuteCount[i] = 0;
      }
    }

    public void EndShift(DateTime endShift)
    {
      if (_isSleeping)
      {
        CalculateNewSumOfSleep(endShift);
        UpdateSleptOnMinuteBucket(endShift);
      }
    }

    public void FallAsleep(DateTime startSleep)
    {
      if (_isSleeping)
      {
        throw new InvalidOperationException($"Guard is already sleeping!");
      }

      _isSleeping = true;
      _startSleep = startSleep;
    }

    public void WakeUp(DateTime endSleep)
    {
      if (!_isSleeping)
      {
        throw new InvalidOperationException($"Guard is not sleeping!");
      }

      _isSleeping = false;

      CalculateNewSumOfSleep(endSleep);
      UpdateSleptOnMinuteBucket(endSleep);
    }

    private void CalculateNewSumOfSleep(DateTime endSleep)
    {
      // Get sum of sleep
      var minutesSlept = endSleep.Subtract(_startSleep);
      TotalMinutesSlept += ((int)minutesSlept.TotalMinutes);
    }

    private void UpdateSleptOnMinuteBucket(DateTime endSleep)
    {
      var timeIterator = _startSleep;
      while (timeIterator < endSleep)
      {
        var currentMinute = timeIterator.Minute;
        SleptOnMinuteCount[currentMinute]++;

        timeIterator = timeIterator.AddMinutes(1);
      }
    }
  }
}