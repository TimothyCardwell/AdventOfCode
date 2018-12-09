using System;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace SleepingGuardMonitor
{
  class Program
  {
    static void Main(string[] args)
    {
      var orderedGuardStatusLog = GetSortedGaurdStatusLog();
      var guardShifts = new Dictionary<string, GuardShifts>();

      GuardShifts currentGuardShift = null;
      for (var i = 0; i < orderedGuardStatusLog.Count; i++)
      {
        var dateTime = (DateTime)orderedGuardStatusLog.GetKey(i);
        var action = GetAction((string)orderedGuardStatusLog.GetByIndex(i));

        Debug.WriteLine($"DateTime: {dateTime}, Action: {action}");
        switch (action)
        {
          case "falls asleep":
            currentGuardShift.FallAsleep(dateTime);
            break;
          case "wakes up":
            currentGuardShift.WakeUp(dateTime);
            break;
          default:
            if (currentGuardShift != null)
            {
              currentGuardShift.EndShift(dateTime);
            }

            var guardId = GetGuardId((string)orderedGuardStatusLog.GetByIndex(i));
            if (!guardShifts.ContainsKey(guardId))
            {
              guardShifts.Add(guardId, new GuardShifts(guardId));
            }

            currentGuardShift = guardShifts[guardId];
            currentGuardShift.StartShift(dateTime);
            break;
        }
      }

      // Part 1
      //var sleepiestGuard = guardShifts.OrderByDescending(g => g.Value.GetTotalMinutesSlept()).First();
      //Debug.WriteLine(sleepiestGuard.Key);
      //Debug.WriteLine(sleepiestGuard.Value.GetFavoriteMinuteSlept());

      // Part 2 
      var sleepiestGuardMinute = guardShifts.OrderByDescending(g => g.Value.GetFavoriteMinuteSlept().Value).First();
      Debug.WriteLine(sleepiestGuardMinute.Key);
      Debug.WriteLine(sleepiestGuardMinute.Value.GetFavoriteMinuteSlept());
    }

    private static SortedList GetSortedGaurdStatusLog()
    {
      var sortedGuardStatusLog = new SortedList();

      foreach (var guardStatus in File.ReadAllLines("input.txt"))
      {
        var dateTimeString = guardStatus.Substring(1, 16);
        var dateTime = DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
        sortedGuardStatusLog.Add(dateTime, guardStatus);
      }

      return sortedGuardStatusLog;
    }

    private static string GetAction(string magicString)
    {
      // [1518-05-23 23:58] Guard #263 begins shift
      return magicString.Substring(19);
    }

    private static string GetGuardId(string magicString)
    {
      // [1518-05-23 23:58] Guard #263 begins shift
      var indexOfHash = magicString.IndexOf("#");
      var indexOfRelevantWhitespace = magicString.IndexOf(" ", indexOfHash) - 1;
      return magicString.Substring(indexOfHash + 1, indexOfRelevantWhitespace - indexOfHash);
    }
  }
}
