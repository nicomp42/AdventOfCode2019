using System;
// HCGFE Oh my goodness
namespace AdventOfCode {
    class Day08 {
        private static int[] image;
        private static int imageSize, layers, imageWidth, imageHeight;
        public static void Solve() {
            SolveDay08Part01();
            SolveDay08Part02();
        }
        public static void SolveDay08Part02() {
            ProcessRawImageData();
            int num, idx;
            int[] message = new int[imageSize];
            for (int pixel = 0; pixel < imageSize; pixel++) {
                for (int layer = 0;  layer < layers; layer++) {
                    idx =  (imageSize * layer)
                         + (pixel);
                    num = image[idx];
                    if (num != 2) {
                        message[pixel] = num;
                        break;
                    }
                }
            }
            for (int i = 0; i < message.Length; i++) { Console.Write(message[i]); }
            Console.WriteLine();
            // The solution is a string of characters. black = *, white = space, 25 chars wide by 6 chars tall
            for (int i = 0; i < imageHeight; i++) {
                for (int j = 0; j < imageWidth; j++) {
                    Console.Write(message[(i * imageWidth) + j] == 0 ? " " : "*");  // HCGFE is the code
                }
                Console.WriteLine();
            }
        }
        private static void ProcessRawImageData() {
            image = new int[Day08Data.day08Data.Length];
            for (int i = 0; i < Day08Data.day08Data.Length; i++) {
                image[i] = Convert.ToInt32(Day08Data.day08Data.Substring(i, 1));
            }
            // Now we have an array of integers we can work with
            imageWidth = 25; imageHeight = 6;
            imageSize = imageWidth * imageHeight;
            layers = image.Length / (imageSize);
            Console.WriteLine("Day 08 Part 01 total layers = " + layers);

        }
        public static void SolveDay08Part01() {
            ProcessRawImageData();
            int[,] digitCount = new int[layers, imageSize];
            for (int i = 0; i < layers; i++) {
                for (int j = 0; j < imageSize; j++) {
                    digitCount[i, image[i * (imageSize) + j]]++;
                }
            }
            // Find the layer with the fewest zero digits
            int fewest = int.MaxValue;
            int layerFewest = int.MaxValue;
            for (int i = 0; i < layers; i++) {
                if (digitCount[i,0] < fewest) { fewest = digitCount[i, 0]; layerFewest = i; }
            }
            Console.WriteLine("Day 08 Part 01 layer with fewest zeros = " + layerFewest);
            for (int i = 0; i < imageSize; i++) { Console.Write(image[layerFewest * imageSize + i] + " "); }
            Console.WriteLine();
            int result = digitCount[layerFewest, 1] * digitCount[layerFewest, 2];
            Console.WriteLine("Day 08 Part 01 = " + result);
        }
    }
}
// 010011100001111000101111011110100111101110010010110001100010011000110111111001011111000011011101111101111011000111110110100101011110000101100001101110