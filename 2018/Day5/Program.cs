using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace PolymerScanner
{
  class Program
  {
    static void Main(string[] args)
    {
      var polymerList = BuildPolymerList();
      PartOne(polymerList);
      PartTwo(polymerList);
    }


    private static void PartOne(LinkedList<char> polymerList)
    {
      var currentNode = polymerList.First;
      while (currentNode != null)
      {
        var nextNode = CheckForReactions(polymerList, currentNode);
        currentNode = nextNode;
      }

      Console.WriteLine($"Part One: {RebuildPolymerString(polymerList).Length}");
    }

    private static void PartTwo(LinkedList<char> polymerList)
    {
      var polymerReactionResults = new Dictionary<char, int>();
      List<char> characters = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

      foreach (var character in characters)
      {
        var characterlessPolymerList = RemoveCharacterFromPolymer(polymerList, character);
        var currentNode = characterlessPolymerList.First;
        while (currentNode != null)
        {
          var nextNode = CheckForReactions(characterlessPolymerList, currentNode);
          currentNode = nextNode;
        }

        Console.WriteLine($"Part Two ({character}): {RebuildPolymerString(characterlessPolymerList).Length}");
        polymerReactionResults.Add(character, RebuildPolymerString(characterlessPolymerList).Length);
      }

      Console.WriteLine($"Lowest reaction length: {polymerReactionResults.OrderBy(x => x.Value).First().Value}");
    }

    public static LinkedList<char> BuildPolymerList()
    {
      var input = File.ReadAllText("input.txt");
      //var input = "dabAcCaCBAcCcaDA";

      var polymerList = new LinkedList<char>();
      foreach (var character in input.ToCharArray())
      {
        var currentNode = new LinkedListNode<char>(character);
        polymerList.AddLast(currentNode);
      }

      return polymerList;
    }

    private static LinkedListNode<char> CheckForReactions(LinkedList<char> polymerList, LinkedListNode<char> currentNode)
    {
      if (currentNode.Next == null)
      {
        return null;
      }

      char leftChar = currentNode.Value;
      char rightChar = currentNode.Next.Value;

      if (
          (Char.ToLower(leftChar) == Char.ToLower(rightChar)) &&
          ((Char.IsLower(leftChar) && Char.IsUpper(rightChar)) || (Char.IsUpper(leftChar) && Char.IsLower(rightChar))))
      {
        //Debug.WriteLine($"Polymer reaction! Left: {leftChar}, Right: {rightChar}");

        var nextNodeTemp = currentNode.Next.Next;
        polymerList.Remove(currentNode.Next);
        polymerList.Remove(currentNode);

        // No nodes left to process
        if (nextNodeTemp == null)
        {
          return null;
        }

        // Reached the beginning of the linked list, return the new first node
        if (nextNodeTemp.Previous == null)
        {
          return nextNodeTemp;
        }

        return CheckForReactions(polymerList, nextNodeTemp.Previous);
      }

      else
      {
        //Debug.WriteLine($"No polymer reaction! Left: {leftChar}, Right: {rightChar}");
        return currentNode.Next;
      }
    }

    private static string RebuildPolymerString(LinkedList<char> polymerList)
    {
      var sb = new StringBuilder();
      var currentNode = polymerList.First;
      while (currentNode != null)
      {
        sb.Append(currentNode.Value);
        currentNode = currentNode.Next;
      }
      return sb.ToString();
    }

    private static LinkedList<char> RemoveCharacterFromPolymer(LinkedList<char> polymerList, char character)
    {
      var newList = new LinkedList<char>();

      var currentNode = polymerList.First;
      while (currentNode != null)
      {
        if (Char.ToLower(currentNode.Value) != Char.ToLower(character))
        {
          newList.AddLast(new LinkedListNode<char>(currentNode.Value));
        }

        currentNode = currentNode.Next;
      }

      return newList;
    }
  }
}
