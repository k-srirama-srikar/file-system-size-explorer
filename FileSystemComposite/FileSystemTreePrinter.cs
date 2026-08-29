/******************************************************************************
 * Filename    = FileSystemTreePrinter.cs
 *
 * Project     = FileSystemComposite
 * 
 * Author      = K Srirama Srikar
 *
 * Description = Renders a file-system tree as indented text. It relies
 *               solely on IFileSystemNode, so it never needs to know or
 *               check whether a given node is a file or a folder, this is the 
 *               payoff of the Composite pattern.
 *****************************************************************************/

using System.Collections.Generic;
using System.Text;

namespace FileSystemComposite
{
    /// <summary>
    /// Formats an <see cref="IFileSystemNode"/> tree into a human-readable,
    /// indented string. The printer is deliberately kept outside of
    /// <see cref="FileNode"/> and <see cref="Folder"/> so that presentation
    /// concerns stay separate from the domain model (Single Responsibility
    /// Principle), while remaining fully generic over the component
    /// contract (Open/Closed Principle: a new kind of node needs no change
    /// here).
    /// </summary>
    public static class FileSystemTreePrinter
    {
        /// <summary>
        /// Builds a multi-line, indented text representation of the tree
        /// rooted at <paramref name="root"/>, showing every node's name and
        /// its own <see cref="IFileSystemNode.GetSize"/> alongside it.
        /// </summary>
        /// <param name="root">The node, file or folder, to start printing from.</param>
        /// <returns>The formatted tree, with one node per line.</returns>
        public static string Print(IFileSystemNode root)
        {
            var builder = new StringBuilder();
            AppendNode(builder, root, string.Empty, true);
            return builder.ToString();
        }

        /// <summary>
        /// Appends a single node, followed recursively by its children,
        /// using box-drawing characters to depict tree structure.
        /// </summary>
        /// <param name="builder">The buffer being written to.</param>
        /// <param name="node">The node being rendered.</param>
        /// <param name="prefix">The indentation carried over from ancestor nodes.</param>
        /// <param name="isLast">Whether this node is the last child among its siblings.</param>
        private static void AppendNode(StringBuilder builder, IFileSystemNode node, string prefix, bool isLast)
        {
            builder.Append(prefix);
            builder.Append(isLast ? "'-- " : "|-- ");
            builder.Append(node.Name);
            builder.Append(" (");
            builder.Append(node.GetSize());
            builder.AppendLine(" bytes)");

            string childPrefix = prefix + (isLast ? "    " : "|   ");
            IReadOnlyList<IFileSystemNode> children = node.Children;
            for (int i = 0; i < children.Count; i++)
            {
                AppendNode(builder, children[i], childPrefix, i == children.Count - 1);
            }
        }
    }
}
