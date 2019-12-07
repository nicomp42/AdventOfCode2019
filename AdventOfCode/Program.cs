/*
 * Bill Nicholson
 * nicholdw@ucmail.uc.edu
 * https://adventofcode.com/2019
 */
using System;
using System.Collections.Generic;

namespace AdventOfCode {
    class Program {
        static void Main(string[] args) {
            Day07.Solve();
//          Day06.Solve();
//          Day05.Solve();
            /*Console.WriteLine("Day 04 Part 01: " + SolveDay04Part01());
              Console.WriteLine("Day 03 Part 01: " + SolveDay03Part01());
              Console.WriteLine("Day 02 Part 02: "); SolveDay02Part02(Day02Data.op);
              Console.WriteLine("Day 02 Part 01: " + SolveDay02Part01(Day02Data.op, true));
              Console.WriteLine("Day 01 Part 01: " + SolveDay01Part01());
              Console.WriteLine("Day 01 Part 02: " + SolveDay01Part02());*/

            //            Console.ReadLine();
        }
        public static int SolveDay04Part01() {
            int result = 0;
            int start = 168630; int stop = 718098;
            for (int i = start; i <= stop; i++)
            {
                if (checkDay04Part01(i)) { result++; }
            }
            return result;
        }
        public static Boolean checkDay04Part01(int num) {
            String s = Convert.ToString(num);
            Boolean matchFound = false, ascending = true;
            int matchCount = 1; // c matches itself so we start with 1
            Char prevChar = '!';
            foreach(Char c in s) {
                if (prevChar == c) {
                    matchCount++;
                } else {
//                    if (matchCount > 1) { matchFound = true; }    // For Part 01 of the problem.
                     if (matchCount == 2 ) { matchFound = true; }  // for Part 02 of the problem
                    matchCount = 1;
                }
                if (c < prevChar) { ascending = false; }
                prevChar = c;
            }
//          if (matchCount > 1) { matchFound = true; }    // For Part 01 of the problem.
            if (matchCount == 2 ) { matchFound = true; }  // for Part 02 of the problem
            return (ascending && matchFound);
        }
        public static int SolveDay03Part01() {
            Point centralPort = new Point(1, 1);
            List<Point> w1 = new List<Point>();
            List<Point> w2 = new List<Point>();
            // Start both wires at the Central Port
            w1.Add(new Point(1, 1));
            w2.Add(new Point(1, 1));
            foreach (String s in Day03Data.path1) {
                int distance;
                distance = Convert.ToInt32(s.Substring(1));
                Point startPoint = new Point(w1[w1.Count - 1]);
                switch (s.Substring(0,1)) {
                    case "U":
                        for (int i = 1; i <= distance; i++) { w1.Add(new Point(startPoint.x, startPoint.y + i)); }
                        break;
                    case "D":
                        for (int i = 1; i <= distance; i++) { w1.Add(new Point(startPoint.x, startPoint.y - i)); }
                        break;
                    case "L":
                       for (int i = 1; i <= distance; i++) { w1.Add(new Point(startPoint.x - i, startPoint.y)); }
                        break;
                    case "R":
                       for (int i = 1; i <= distance; i++) { w1.Add(new Point(startPoint.x + i, startPoint.y)); }
                        break;
                }
            }
            foreach (String s in Day03Data.path2) {
                int distance;
                distance = Convert.ToInt32(s.Substring(1));
                Point startPoint = new Point(w2[w2.Count - 1]);
                switch (s.Substring(0, 1)) {
                case "U":
                for (int i = 1; i <= distance; i++) { w2.Add(new Point(startPoint.x, startPoint.y + i)); }
                break;
                case "D":
                for (int i = 1; i <= distance; i++) { w2.Add(new Point(startPoint.x, startPoint.y - i)); }
                break;
                case "L":
                for (int i = 1; i <= distance; i++) { w2.Add(new Point(startPoint.x - i, startPoint.y)); }
                break;
                case "R":
                for (int i = 1; i <= distance; i++) { w2.Add(new Point(startPoint.x + i, startPoint.y)); }
                break;
                }
            }
            Console.WriteLine("Day 03 Part 01: " + w1.Count + " points in wire 01");
            Console.WriteLine("Day 03 Part 01: " + w2.Count + " points in wire 02");
            Console.WriteLine("We will be processing " + (long)w1.Count * w2.Count + " points...");
            int minDist = Int32.MaxValue;
            int minTotalSteps = Int32.MaxValue;
            int stepsToP1Point, stepsToP2Point;
            stepsToP1Point = 0; stepsToP2Point = 0;
            foreach (Point p1 in w1) {
                stepsToP2Point = 0;
                foreach(Point p2 in w2) {
                    if ((p1.x == p2.x) && (p1.y == p2.y)) {
                        // We have an intersection
                        int dist;
                        dist = Math.Abs(p1.x - centralPort.x) + Math.Abs(p1.y - centralPort.y);
                        if ((dist > 0) && minDist > dist) { minDist = dist; }
                        if ((dist > 0) && minTotalSteps > (stepsToP1Point + stepsToP2Point)) { minTotalSteps = stepsToP1Point + stepsToP2Point; }
                    }
                    stepsToP2Point++;
                }
                stepsToP1Point++;
            }
            Console.WriteLine("Day 03 Part 02: Min total steps = " + minTotalSteps);
            return minDist;
        }
        public static void SolveDay02Part02(int[] op) {
            int goal = 19690720;
            for (int i = 0; i <= 99; i++) {
                for (int j = 0; j <= 99; j++) {
                    int[] p = (int[])op.Clone();
                    p[1] = i; p[2] = j;
                    int result = SolveDay02Part01(p, false);
                    if (result == goal) { Console.WriteLine(p[1] + ", " + p[2]); }
                }
            }
        }
        public static int SolveDay02Part01(int[] op, Boolean replace) {
            int[] p = (int[])op.Clone();
            if (replace) {p[1] = 12; p[2] = 2; }
            Boolean keepGoing = true;
            for (int i = 0; i < p.Length && keepGoing; i+= 4) {
                switch (p[i]) {
                    case 1:     // Add
                        p[p[i + 3]] = p[p[i + 1]] + p[p[i + 2]];
                        break;
                    case 2:     // Multiply
                        p[p[i + 3]] = p[p[i + 1]] * p[p[i + 2]];
                        break;

                    case 99:    // Halt
                        keepGoing = false;
                        break;

                    default:
                        Console.WriteLine("SolveDay02Part01(): Invalid opcode at index " + i + " (" + p[i] + ")");
                        break;
                }
            }
            return p[0];
        }
        public static int SolveDay01Part01() {
            int fuel = 0;
            foreach (int num in Day01Data.data) {
                fuel += (num / 3) - 2;
            }
            return fuel;
        }
        public static int SolveDay01Part02() {
            int fuel = 0;
            foreach (int numX in Day01Data.data) {
                int num; num = numX;
                while (true) {
                    int tmp;
                    tmp = (num / 3) - 2;
                    if (tmp > 0) { fuel += tmp; num = tmp; } else { break; }
                }
            }
            return fuel;
        }
    }
}
