using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DictionaryVisualizer.Utilities
{
    /// <summary>
    /// Tree data structure.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class TreeNode<T>
    {
        /// <summary>
        /// Gets a child <see cref="TreeNode{T}"/> from a zero-based index.
        /// </summary>
        /// <param name="index">The zero-based index of the child node.</param>
        /// <returns>The child node at the specified index.</returns>
        public TreeNode<T> this[int index] => children[index];

        /// <summary>
        /// The value of the <see cref="TreeNode{T}"/>.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// The parent of the <see cref="TreeNode{T}"/>; can be null!
        /// </summary>
        public TreeNode<T> Parent { get; private set; }

        /// <summary>
        /// The children of the <see cref="TreeNode{T}"/>.
        /// </summary>
        public ReadOnlyCollection<TreeNode<T>> Children => children.AsReadOnly();
        
        /// <summary>
        /// Internal collection of this <see cref="TreeNode{T}"/> children.
        /// </summary>
        private readonly List<TreeNode<T>> children;

        /// <summary>
        /// Initializes a <see cref="TreeNode{T}"/> with a value.
        /// </summary>
        /// <param name="value">The value to initialize the <see cref="TreeNode{T}"/> with.</param>
        public TreeNode(T value)
        {
            Value = value;
            children = new List<TreeNode<T>>();
        }

        /// <summary>
        /// Adds a child to the <see cref="TreeNode{T}"/>.
        /// </summary>
        /// <param name="value">The value of the child node.</param>
        /// <returns>The child node added.</returns>
        public TreeNode<T> AddChild(T value)
        {
            TreeNode<T> node = new TreeNode<T>(value) {Parent = this};
            children.Add(node);

            return node;
        }

        /// <summary>
        /// Add multiple children to the <see cref="TreeNode{T}"/>.
        /// </summary>
        /// <param name="values">The values of the children nodes.</param>
        /// <returns>The children nodes added.</returns>
        public TreeNode<T>[] AddChildren(params T[] values) => values.Select(AddChild).ToArray();

        /// <summary>
        /// Remove a child from the <see cref="TreeNode{T}"/>.
        /// </summary>
        /// <param name="node">The <see cref="TreeNode{T}"/> to remove.</param>
        /// <returns>A boolean indicating whether operation was successful.</returns>
        public bool RemoveChild(TreeNode<T> node) => children.Remove(node);

        /// <summary>
        /// Traverse the <see cref="TreeNode{T}"/> and it's children, executing an operation on each node.
        /// The traversal operation works recursively on all nodes.
        /// </summary>
        /// <param name="operation">The operation to execute on each <see cref="TreeNode{T}"/>.</param>
        /// <param name="runOnThisNode">Indicates whether the traversal operation should run on this node.</param>
        public void Traverse(Action<TreeNode<T>> operation, bool runOnThisNode = true)
        {
            if (runOnThisNode)
            {
                operation(this);
            }

            foreach (TreeNode<T> child in children)
            {
                child.Traverse(operation);
            }
        }

        /// <summary>
        /// Flatten the <see cref="TreeNode{T}"/> and it's children into one collection.
        /// </summary>
        /// <returns>The flattened version of the <see cref="TreeNode{T}"/>.</returns>
        public IEnumerable<T> Flatten() => new[] {Value}.Concat(children.SelectMany(x => x.Flatten()));
    }
}