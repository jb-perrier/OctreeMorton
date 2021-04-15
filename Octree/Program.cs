using System;

namespace Octree
{
    class Program
    {
        static void Main(string[] args)
        {
            double depth = 4;
            Octree<int> octree = new Octree<int>((int) depth, 10);
            Int3 mpos = new Int3(0, 0, 0);
            octree.Set(mpos, 99);
            octree.Simply();
            octree.PrintRoot();
            int size = (int) Math.Pow(2.0, depth);
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    Console.Write(octree.Get(new Int3(x, y, 0)) + " ");
                }
                Console.Write("\n");
            }
        }
    }
}