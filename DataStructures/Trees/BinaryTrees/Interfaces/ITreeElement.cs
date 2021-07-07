using System;

namespace DataStructures.Trees.BinaryTrees
{
    public interface ITreeElement<T>
        where T : IComparable<T>
    {
        T Content { get; set; }
        ITreeElement<T> Right { get; set; }
        ITreeElement<T> Parent { get; set; }
        ITreeElement<T> Left { get; set; }
    }
}