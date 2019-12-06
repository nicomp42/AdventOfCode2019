/*
 * Bill Nicholson
 * nicholdw@ucmail.uc.edu
 * https://adventofcode.com/2019
 */

using System;

namespace AdventOfCode {
    class Day05 {
        public static void Solve() {
            int[] input = new int[] { 1 };
//            SolveDay05Part01(Day05Data.day05Data, input);
            Console.WriteLine("");
            input = new int[] { 5 };
            SolveDay05Part02(Day05Data.day05Data, input);
        }
        private static int[] ComputeParameterModes(int parameterCode) {
            int i = 0;
            int[] parameterModes = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };     // 10 is arbitrary. 0 is the default mode. 
            while (parameterCode > 0) {
                parameterModes[i] = parameterCode - ((parameterCode / 10) * 10);
                i++;
                parameterCode /= 10;
            }
            return parameterModes;
        }
        private static int ComputeValue(int parameterMode, int value1, int value2) {
            int value = 0;
            switch (parameterMode) {
            case 0:     // position mode
            value = value1;
                break;
            case 1:     // immediate mode
            value = value2;
                break;
            case 2:
                throw new Exception("ComputeValue(): Invalid parameterMode: " + parameterMode);
                //break;
            }

            return value;
        }
        public static int SolveDay05Part01(int[] op, int[] input) {
            int inputIdx = 0;
            int[] p = (int[])op.Clone();
            Boolean keepGoing = true;
            for (int i = 0; i < p.Length && keepGoing;) {
                int a, b, c;
                int[] parameterModes;
                parameterModes = ComputeParameterModes(p[i] / 100); // Parameter modes are stored in the digits after removing the op code digits
                switch (p[i] % 100) {               // The op code is in the rightmost 2 digits
                case 1:     // Add
                    a = ComputeValue(parameterModes[2], p[i + 3], i + 3);
                    b = p[ComputeValue(parameterModes[0], p[i + 1], i + 1)];
                    c = p[ComputeValue(parameterModes[1], p[i + 2], i + 2)];
//                    Console.WriteLine("Add: i = " + i + " " + p[i] + " " + p[i + 1] + " " + p[i + 2] + ", a = " + a + ", b = " + b + ", c = " + c);
                    p[a] = b + c;
                    i += 4;
                break;
                case 2:     // Multiply
                    a = ComputeValue(parameterModes[2], p[i + 3], i + 3);
                    b = p[ComputeValue(parameterModes[0], p[i + 1], i + 1)];
                    c = p[ComputeValue(parameterModes[1], p[i + 2], i + 2)];
//                    Console.WriteLine("Multiply: i = " + i + " " + p[i] + " " + p[i + 1] + " " + p[i + 2] + ", a = " + a + ", b = " + b + ", c = " + c);
                    p[a] = b * c;
                    i += 4;
                break;
                case 3:     // takes a single integer as input and saves it to the position given by its only parameter.
                    a = ComputeValue(parameterModes[0], p[i + 1], i + 1);
                    p[a] = input[inputIdx];
                    inputIdx++;
//                    Console.WriteLine("Save: i = " + i + " " + p[i] + " " + p[i + 1] + ", a = " + a);
                    i += 2;
                break;
                case 4:     // outputs the value of its only parameter.
                    a = ComputeValue(parameterModes[0], p[i + 1], i + 1);
                    Console.Write(p[a]);
//                    Console.WriteLine("Output: i = " + i + " " + p[i] + " " + p[i + 1] + ", a = " + a);
                    i += 2;
                break;
                case 99:    // Halt
                    keepGoing = false;
                break;

                default:
                    Console.WriteLine("SolveDay05Part01(): Invalid opcode at index " + i + " (" + p[i] + ")");
                    i++;
                break;
                }
            }
            return p[0];
        }
        public static int SolveDay05Part02(int[] op, int[] input) {
            int inputIdx = 0;
            int[] p = (int[])op.Clone();
            Boolean keepGoing = true;
            for (int i = 0; i < p.Length && keepGoing;) {
                int a, b, c;
                int[] parameterModes;
                parameterModes = ComputeParameterModes(p[i] / 100); // Parameter modes are stored in the digits after removing the op code digits
                switch (p[i] % 100) {               // The op code is in the rightmost 2 digits
                case 1:     // Add
                a = ComputeValue(parameterModes[2], p[i + 3], i + 3);
                b = p[ComputeValue(parameterModes[0], p[i + 1], i + 1)];
                c = p[ComputeValue(parameterModes[1], p[i + 2], i + 2)];
                //                    Console.WriteLine("Add: i = " + i + " " + p[i] + " " + p[i + 1] + " " + p[i + 2] + ", a = " + a + ", b = " + b + ", c = " + c);
                p[a] = b + c;
                i += 4;
                break;
                case 2:     // Multiply
                a = ComputeValue(parameterModes[2], p[i + 3], i + 3);
                b = p[ComputeValue(parameterModes[0], p[i + 1], i + 1)];
                c = p[ComputeValue(parameterModes[1], p[i + 2], i + 2)];
                //                    Console.WriteLine("Multiply: i = " + i + " " + p[i] + " " + p[i + 1] + " " + p[i + 2] + ", a = " + a + ", b = " + b + ", c = " + c);
                p[a] = b * c;
                i += 4;
                break;
                case 3:     // takes a single integer as input and saves it to the position given by its only parameter.
                a = ComputeValue(parameterModes[0], p[i + 1], i + 1);
                p[a] = input[inputIdx];
                inputIdx++;
                //                    Console.WriteLine("Save: i = " + i + " " + p[i] + " " + p[i + 1] + ", a = " + a);
                i += 2;
                break;
                case 4:     // outputs the value of its only parameter.
                a = ComputeValue(parameterModes[0], p[i + 1], i + 1);
                Console.Write(p[a]);
                //                    Console.WriteLine("Output: i = " + i + " " + p[i] + " " + p[i + 1] + ", a = " + a);
                i += 2;
                break;
                case 5: // jump-if-true: if the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                    b = p[ComputeValue(parameterModes[0], p[i + 1], i + 1)];
                    c = p[ComputeValue(parameterModes[1], p[i + 2], i + 2)];
                    if (b != 0) { i = c; } else { i += 3; }
                break;
                case 6: // jump-if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing
                    b = p[ComputeValue(parameterModes[0], p[i + 1], i + 1)];
                    c = p[ComputeValue(parameterModes[1], p[i + 2], i + 2)];
                    if (b == 0) { i = c; } else { i += 3; }
                break;
                case 7: // less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                    a = ComputeValue(parameterModes[2], p[i + 3], i + 3);
                    b = p[ComputeValue(parameterModes[0], p[i + 1], i + 1)];
                    c = p[ComputeValue(parameterModes[1], p[i + 2], i + 2)];
                    if (b < c) { p[a] = 1; } else { p[a] = 0; }
                    i += 4;
                break;
                case 8: // equals: if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                    a = ComputeValue(parameterModes[2], p[i + 3], i + 3);
                    b = p[ComputeValue(parameterModes[0], p[i + 1], i + 1)];
                    c = p[ComputeValue(parameterModes[1], p[i + 2], i + 2)];
                    if (b == c) { p[a] = 1; } else { p[a] = 0; }
                i += 4;
                break;

                case 99:    // Halt
                keepGoing = false;
                break;

                default:
                Console.WriteLine("SolveDay05Part01(): Invalid opcode at index " + i + " (" + p[i] + ")");
                i++;
                break;
                }
            }
            return p[0];
        }
    }
}
