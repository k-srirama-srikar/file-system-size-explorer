/******************************************************************************
 * Filename    = Folder.cs
 *
 * Project     = FileSystemComposite
 * 
 * Author      = K Srirama Srikar
 *
 * Description = Composite implementation of IFileSystemNode. A Folder owns
 *               zero or more child nodes, which may themselves be files or 
 *               folders, and reports its size as the recursive sum of its 
 *               descendants.
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FileSystemComposite
{
    /// <summary>
    /// Represents a folder that can contain files and other folders. This is
    /// the "Composite" role of the Composite design pattern: it implements
    /// the same component contract as a file, while also delegating
    /// operations (such as computing a size) to its children.
    /// </summary>
    /// <remarks>
    /// Following the "safe" variant of the Composite pattern, child
    /// management (<see cref="Add"/>) is declared only on <see cref="Folder"/>
    /// and not on <see cref="IFileSystemNode"/>. This keeps the component
    /// contract meaningful for leaves too: a <see cref="FileNode"/> is never
    /// forced to implement an "Add" operation that would make no sense for
    /// it, which keeps the design consistent with the Liskov Substitution
    /// Principle.
    /// </remarks>
    public sealed class Folder : IFileSystemNode
    {
        /// <summary>
        /// The child nodes directly contained in this folder.
        /// </summary>
        private readonly List<IFileSystemNode> _children;

        /// <summary>
        /// Creates an empty folder with the given name.
        /// </summary>
        /// <param name="name">The name of the folder, for example "Documents".</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is null, empty, or whitespace.</exception>
        public Folder(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("A folder must have a non-empty name.", nameof(name));
            }

            Name = name;
            _children = new List<IFileSystemNode>();
        }

        /// <inheritdoc/>
        public string Name { get; }

        /// <summary>
        /// Gets a read-only, live view of the child nodes directly contained
        /// in this folder. The order reflects the order in which children
        /// were added.
        /// </summary>
        public IReadOnlyList<IFileSystemNode> Children => new ReadOnlyCollection<IFileSystemNode>(_children);

        /// <summary>
        /// Computes the total size of this folder as the recursive sum of
        /// the sizes of every node nested underneath it, however deep.
        /// A newly created, empty folder has a size of zero.
        /// </summary>
        /// <returns>The recursive size of the folder, in bytes.</returns>
        public long GetSize()
        {
            long total = 0;
            foreach (IFileSystemNode child in _children)
            {
                total += child.GetSize();
            }

            return total;
        }

        /// <summary>
        /// Adds a file or folder as a direct child of this folder.
        /// </summary>
        /// <param name="node">The node to add. May itself be a <see cref="FileNode"/> or a <see cref="Folder"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="node"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="node"/> is this same folder, which would create a cycle.</exception>
        public void Add(IFileSystemNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (ReferenceEquals(node, this))
            {
                throw new ArgumentException("A folder cannot be added as a child of itself.", nameof(node));
            }

            _children.Add(node);
        }
    }
}
