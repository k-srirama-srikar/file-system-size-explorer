/******************************************************************************
 * Filename    = IFileSystemNode.cs
 *
 * Project     = FileSystemComposite
 * 
 * Author      = K Srirama Srikar
 *
 * Description = Component contract of the Composite pattern. Both leaf
 *               (file) and composite (folder) nodes implement this single
 *               interface so that callers can work with either kind of node
 *               without ever checking or casting to a concrete type.
 *****************************************************************************/

using System.Collections.Generic;

namespace FileSystemComposite
{
    /// <summary>
    /// Represents a single node, either a file or a folder, in a
    /// file-system tree. This is the "Component" role of the Composite
    /// design pattern: every operation a client needs (reading the name,
    /// computing a size, listing children) is exposed here, so a client can
    /// treat a single file and an entire folder tree identically.
    /// </summary>
    public interface IFileSystemNode
    {
        /// <summary>
        /// Gets the display name of the node (for example "photo.png" or
        /// "Documents"). The name never includes path separators.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the child nodes contained directly inside this node.
        /// A leaf node (a file) has no children and always returns an empty,
        /// read-only list, so callers never need to know whether a given
        /// node is a leaf or a composite before traversing it.
        /// </summary>
        IReadOnlyList<IFileSystemNode> Children { get; }

        /// <summary>
        /// Computes the size, in bytes, that this node occupies. For a file
        /// this is its own size; for a folder this is the recursive sum of
        /// the sizes of every file nested underneath it, however deep.
        /// </summary>
        /// <returns>The size of the node, in bytes.</returns>
        long GetSize();
    }
}
