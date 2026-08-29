/******************************************************************************
 * Filename    = FileSystemTreePrinterTests.cs
 *
 * Project     = FileSystemComposite.Tests
 *
 * Author      = K Srirama Srikar
 * 
 * Description = Unit tests for FileSystemTreePrinter, verifying that
 *               traversal reaches every node uniformly.
 *****************************************************************************/

using FileSystemComposite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystemComposite.Tests
{
    /// <summary>
    /// Unit tests for <see cref="FileSystemTreePrinter"/>.
    /// </summary>
    [TestClass]
    public class FileSystemTreePrinterTests
    {
        /// <summary>
        /// Printing a single file shows its name and size on one line.
        /// </summary>
        [TestMethod]
        public void Print_SingleFile_ShowsNameAndSize()
        {
            var file = new FileNode("notes.txt", 42);

            string tree = FileSystemTreePrinter.Print(file);

            StringAssert.Contains(tree, "notes.txt");
            StringAssert.Contains(tree, "42 bytes");
        }

        /// <summary>
        /// Printing a nested tree visits every node, files and folders
        /// alike, without the printer ever needing to check node types.
        /// </summary>
        [TestMethod]
        public void Print_NestedTree_VisitsEveryNode()
        {
            var root = new Folder("root");
            var sub = new Folder("sub");
            sub.Add(new FileNode("deep.txt", 5));
            root.Add(sub);
            root.Add(new FileNode("top.txt", 3));

            string tree = FileSystemTreePrinter.Print(root);

            StringAssert.Contains(tree, "root");
            StringAssert.Contains(tree, "sub");
            StringAssert.Contains(tree, "deep.txt");
            StringAssert.Contains(tree, "top.txt");
        }

        /// <summary>
        /// Each node in the printed tree appears on its own line.
        /// </summary>
        [TestMethod]
        public void Print_NestedTree_UsesOneLinePerNode()
        {
            var root = new Folder("root");
            root.Add(new FileNode("a.txt", 1));
            root.Add(new FileNode("b.txt", 2));

            string tree = FileSystemTreePrinter.Print(root);
            string[] lines = tree.Split(new[] { "\r\n", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);

            Assert.AreEqual(3, lines.Length);
        }
    }
}
