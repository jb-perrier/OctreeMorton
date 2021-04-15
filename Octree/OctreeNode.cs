using System;

namespace Octree
{
    public class OctreeNode<T> where T : struct
    {
        public object Node;

        public OctreeNode(T value)
        {
            Node = value;
        }
        
        public OctreeNode(int depth, T defaultValue)
        {
            if (depth == 0)
            {
                var children = new OctreeNode<T>[8];
                for (int i = 0; i < 8; i++)
                {
                    children[i] = new OctreeNode<T>(defaultValue);
                }
                Node = children;
            }
            else
            {
                var children = new OctreeNode<T>[8];
                for (int i = 0; i < 8; i++)
                {
                    children[i] = new OctreeNode<T>(depth - 1, defaultValue);
                }
                Node = children;
            }
        }
        
        public void Simply()
        {
            if (IsLeaf())
                return;

            OctreeNode<T>[] Children = (OctreeNode<T>[]) Node;
            
            // Simply children
            for (var i = 0; i < Children.Length; i++)
            {
                Children[i].Simply();
            }
            
            // Can be simplified ?
            for (var i = 1; i < Children.Length; i++)
            {
                if (Children[i - 1].IsLeaf() && Children[i].IsLeaf())
                {
                    if (Children[i - 1].Node.GetHashCode() != Children[i].Node.GetHashCode())
                        return;
                }
                else
                    return;
            }
            
            // Simplify
            Node = Children[0].Node;
        }

        public T Value()
        {
            return (T) Node;
        }

        public OctreeNode<T>[] Children()
        {
            return ((OctreeNode<T>[]) Node);
        }
        
        public bool IsLeaf()
        {
            return Node is T;
        }

        public OctreeNode<T> GetChild(int morton)
        {
            return ((OctreeNode<T>[]) Node)[morton];
        }

        public void Divide()
        {
            Console.WriteLine("Dividing !");
            if (!IsLeaf())
            {
                Console.WriteLine("Trying to divide while not a leaf !");
                return;
            }
            T value = (T) Node;
            var children = new OctreeNode<T>[8];
            for (var i = 0; i < children.Length; i++)
            {
                children[i] = new OctreeNode<T>(value);
            }

            Node = children;
        }
    }
}