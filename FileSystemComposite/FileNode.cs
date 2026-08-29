/******************************************************************************
 * Filename    = FileNode.cs
 *
 * Project     = FileSystemComposite
 * 
 * Author      = K Srirama Srikar
 *
 * Description = Leaf implementation of IFileSystemNode. A FileNode has a fixed 
 *               size and no children.
 *****************************************************************************/

using System;
using System.Collections.Generic;

namespace FileSystemComposite
{
    /// <summary>
    /// Represents a single file. This is the "Leaf" role of the Composite
    /// design pattern: it implements the component contract but has noc children, so recursion over the tree naturally terminates here.
    /// </summary>
    public sealed class FileNode : IFileSystemNode
    {
        /// <summary>
        /// The fixed size, in bytes, that this file reports.
        /// </summary>
        private readonly long _sizeInBytes;

        /// <summary>
        /// Creates a file with the given name and size.
        /// </summary>
        /// <param name="name">The name of the file, for example "notes.txt".</param>
        /// <param name="sizeInBytes">The size of the file, in bytes. Must not be negative.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is null, empty, or whitespace.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="sizeInBytes"/> is negative.</exception>
        public FileNode(string name, long sizeInBytes)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("A file must have a non-empty name.", nameof(name));
            }

            if (sizeInBytes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sizeInBytes), sizeInBytes, "A file size cannot be negative.");
            }

            Name = name;
            _sizeInBytes = sizeInBytes;
        }

        /// <inheritdoc/>
        public string Name { get; }

        /// <summary>
        /// Gets the children of this node. A file is a leaf, so this is
        /// always an empty, read-only list.
        /// </summary>
        public IReadOnlyList<IFileSystemNode> Children { get; } = Array.Empty<IFileSystemNode>();

        /// <summary>
        /// Returns the fixed size that this file was created with.
        /// </summary>
        /// <returns>The size of the file, in bytes.</returns>
        public long GetSize()
        {
            return _sizeInBytes;
        }
    }
}
