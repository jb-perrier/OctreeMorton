using System;

namespace Octree
{
    public class Octree<T> where T : struct
    {
        private readonly int _depth;

        private OctreeNode<T> _root;

        public Octree(int depth, T defaultValue)
        {
            if (depth <= 0)
                depth = 1;
            _depth = depth - 1;
            _root = new OctreeNode<T>(_depth, defaultValue);
        }
        
        public void Simply()
        {
            Console.WriteLine("-- Simplifying data !");
            _root.Simply();
        }

        public T Get(Int3 pos)
        {
            //Console.WriteLine("-- Searching at position : " + pos.ToString());
            OctreeNode<T> curr = _root;
            int e0 = _depth;
            while (e0 >= 0)
            {
                //Console.WriteLine("---- Depth : " + (_depth - e0 + 1));
                if (!curr.IsLeaf() && e0 == 0)
                {
                    return curr.GetChild(((pos.X >> e0) & 1) + (((pos.Y >> e0) & 1) << 1) + (((pos.Z >> e0) & 1) << 2))
                        .Value();
                }
                if (curr.IsLeaf())
                {
                    //Console.WriteLine("-- Found !  Value : " + e0);
                    return curr.Value();
                }
                curr = curr.GetChild(((pos.X >> e0) & 1) + (((pos.Y >> e0) & 1) << 1) + (((pos.Z >> e0) & 1) << 2));
                e0--;
            }
            
            //Console.WriteLine("-- Not Found ! ");
            return default;
        }

        public void Set(Int3 pos, T value)
        {
            Set(pos, _depth, value);
        }
        
        public void Set(Int3 pos, int depth, T value)
        {
            if (depth > _depth)
            {
                Console.WriteLine("Requested depth too high !");
                return;
            }
            
            Console.WriteLine("-- Changing value at position : " + pos.ToString());
            OctreeNode<T> curr = _root;
            int e0 = _depth;
            while (e0 >= 0)
            {
                Console.WriteLine("---- Depth : " + e0);
                if ((_depth - e0) == depth)
                {
                    
                    curr.GetChild(((pos.X >> e0) & 1) + (((pos.Y >> e0) & 1) << 1) + (((pos.Z >> e0) & 1) << 2)).Node = value;
                    Console.WriteLine("-- Found !  New Value : " + curr.GetChild(((pos.X >> e0) & 1) + (((pos.Y >> e0) & 1) << 1) + (((pos.Z >> e0) & 1) << 2)).Node);
                    return;
                }
                if (curr.IsLeaf())
                {
                    if (curr.Value().GetHashCode() == value.GetHashCode())
                    {
                        Console.WriteLine("-- Found !  New Value : " + curr.Value());
                        return;
                    }
                    else
                    {
                        curr.Divide();
                    }
                }
                curr = curr.GetChild(((pos.X >> e0) & 1) + (((pos.Y >> e0) & 1) << 1) + (((pos.Z >> e0) & 1) << 2));
                e0--;
            }
            Console.WriteLine("-- Not Found ! ");
        }

        public void PrintRoot()
        {
            if (_root.IsLeaf())
            {
                Console.WriteLine("Root Value : " + _root.Value());
            }
            else
            {
                var children = _root.Children();
                for (var i = 0; i < children.Length; i++)
                {
                    if (children[i].IsLeaf())
                        Console.WriteLine(" " + i + "  Value : " + children[i].Value());
                    else
                        Console.WriteLine(" " + i + "  Subdivision");
                }
            }
        }
    }
}