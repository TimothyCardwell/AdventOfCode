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

      var currentNode = polymerList.First;
      while (currentNode != null)
      {
        var nextNode = CheckForReactions(polymerList, currentNode);
        currentNode = nextNode;
      }

      //Console.WriteLine(RebuildPolymerString());
      Console.WriteLine(RebuildPolymerString(polymerList).Length);
    }

    public static LinkedList<char> BuildPolymerList()
    {
      var input = File.ReadAllText("input.txt");
      Console.WriteLine(input.Length);
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
        if (nextNodeTemp == null) {
          return null;
        }

        // Reached the beginning of the linked list, return the new first node
        if (nextNodeTemp.Previous == null) {
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
  }
}
