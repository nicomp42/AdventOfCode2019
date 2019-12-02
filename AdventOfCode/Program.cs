/*
 * Bill Nicholson
 * nicholdw@ucmail.uc.edu
 * https://adventofcode.com/2019
 */
using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("Day 02 Part 02: "); SolveDay02Part02(Day02Data.op);
            Console.WriteLine("Day 02 Part 01: " + SolveDay02Part01(Day02Data.op, true));
            Console.WriteLine("Day 01 Part 01: " + SolveDay01Part01());
            Console.WriteLine("Day 01 Part 02: " + SolveDay01Part02());
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
