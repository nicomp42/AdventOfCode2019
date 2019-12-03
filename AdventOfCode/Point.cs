﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Point {
        public int x;
        public int y;
        public Point(int x, int y) { this.x = x; this.y = y; }
        public Point(Point p) { this.x = p.x; this.y = p.y; }
    }
}
