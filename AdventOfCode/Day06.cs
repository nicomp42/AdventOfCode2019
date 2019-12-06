using System;
using System.Collections.Generic;

namespace AdventOfCode {
    class Day06 {
        public static void Solve() {
            SolveDay06Part01();
            SolveDay06Part02();
        }
        public static void SolveDay06Part02() {
        }

        public static void SolveDay06Part01(){
            List<String> YOU = new List<String>(), SAN = new List<string>(), dummy = new List<string>();
           String[] o = (String[])Day06Data.day06Data.Clone();
            Dictionary<String, int> dd = new Dictionary<string, int>();
            // Build a unique list of orbiting objects
            foreach (String s in o) {
                String[] pair = s.Split(')');
                if (pair[0] != "COM") { try { dd.Add(pair[0], 0); } catch (Exception ex) { } }
                if (pair[1] != "COM") { try { dd.Add(pair[1], 0); } catch (Exception ex) { } }
            }
            Dictionary<String, int> d = CloneDictionary(dd);
            foreach (KeyValuePair<string, int> entry in d) {
                List<String> current;
                Boolean keepGoing; keepGoing = true;
                String target; target = entry.Key;
//              Console.WriteLine("Processing " + target);
                current = dummy;
                if (target == "YOU") { current = YOU; } // current.Add(target); }
                if (target == "SAN") { current = SAN; } // current.Add(target); }
                while (keepGoing) {
                    foreach (String s in o) {
                        String[] pair; pair = s.Split(')');
                        if (pair[1] == target) {
                            Increment(dd, entry.Key);
                            current.Add(pair[0]);
                            if (pair[0] == "COM") {
//                                Console.WriteLine("found the end of " + target);
                                keepGoing = false;
                                break;
                            }
                            target = pair[0];
                            break;
                        }
                    }
                }
            }
            int count = 0;
            foreach (KeyValuePair<string, int> entry in dd) {
                count += entry.Value;
            }
            Console.WriteLine("Part 01: " + count);
            // Find where the YOU and SAN lists intersect
            int youCount = 0, sanCount = 0;
            Boolean keepGoing01 = true;
            foreach(String you in YOU) {
                if (!keepGoing01) { break; }
                sanCount = 0;
                foreach (String san in SAN) {
                    if (you == san) {
                        Console.WriteLine("Match found at " + you);
                        keepGoing01 = false;
                        break;
                    }
                    sanCount++;
                }
                youCount++;
            }
            Console.WriteLine("Part 02: " + youCount + " + " + sanCount + " - 1 = " + (youCount + sanCount - 1));
        }
        private static void Increment(Dictionary<String, int> d, String key) {
            d[key] = d[key] + 1;
        }
        public static Dictionary<String, int> CloneDictionary(Dictionary<String, int> original) {
            Dictionary<String, int> ret = new Dictionary<String, int>();
            foreach (KeyValuePair<String, int> entry in original) {
                ret.Add(entry.Key, entry.Value);
            }
            return ret;
        }
    }
}
