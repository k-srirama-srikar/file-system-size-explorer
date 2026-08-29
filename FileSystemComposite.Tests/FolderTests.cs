/******************************************************************************
 * Filename    = FolderTests.cs
 *
 * Project     = FileSystemComposite.Tests
 * 
 * Author     = K Srirama Srikar
 *
 * Description = Unit tests for the Folder composite, covering recursive
 *               size totals and uniform treatment of nested nodes.
 *****************************************************************************/

using System;
using FileSystemComposite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystemComposite.Tests
{
    /// <summary>
    /// Unit tests for <see cref="Folder"/>.
    /// </summary>
    [TestClass]
    public class FolderTests
    {
        /// <summary>
        /// An empty folder has a size of zero.
        /// </summary>
        [TestMethod]
        public void GetSize_EmptyFolder_IsZero()
        {
            var folder = new Folder("Empty");

            Assert.AreEqual(0, folder.GetSize());
        }

        /// <summary>
        /// A folder containing only files reports the sum of those files.
        /// </summary>
        [TestMethod]
        public void GetSize_SingleLevel_SumsDirectChildren()
        {
            var folder = new Folder("Pictures");
            folder.Add(new FileNode("a.png", 100));
            folder.Add(new FileNode("b.png", 250));

            Assert.AreEqual(350, folder.GetSize());
        }

        /// <summary>
        /// A folder's size recurses through arbitrarily nested sub-folders,
        /// demonstrating the core value of the Composite pattern: the outer
        /// folder never needs to know how deep its children go.
        /// </summary>
        [TestMethod]
        public void GetSize_NestedFolders_SumsRecursively()
        {
            var root = new Folder("root");
            var documents = new Folder("Documents");
            var reports = new Folder("Reports");

            reports.Add(new FileNode("q1.docx", 500));
            reports.Add(new FileNode("q2.docx", 700));
            documents.Add(reports);
            documents.Add(new FileNode("readme.txt", 20));
            root.Add(documents);
            root.Add(new FileNode("root-file.txt", 30));

            Assert.AreEqual(1250, root.GetSize());
        }

        /// <summary>
        /// Children can be a mix of files and folders, and both are treated
        /// uniformly through the shared <see cref="IFileSystemNode"/> contract.
        /// </summary>
        [TestMethod]
        public void Children_ReflectsMixOfFilesAndFolders()
        {
            var root = new Folder("root");
            var child = new FileNode("a.txt", 10);
            var subFolder = new Folder("sub");

            root.Add(child);
            root.Add(subFolder);

            Assert.AreEqual(2, root.Children.Count);
            Assert.AreSame(child, root.Children[0]);
            Assert.AreSame(subFolder, root.Children[1]);
        }

        /// <summary>
        /// Adding a null node is rejected.
        /// </summary>
        [TestMethod]
        public void Add_RejectsNull()
        {
            var folder = new Folder("root");

            Assert.ThrowsException<ArgumentNullException>(() => folder.Add(null!));
        }

        /// <summary>
        /// A folder cannot be added as a child of itself, which would
        /// otherwise create an infinite cycle during size computation.
        /// </summary>
        [TestMethod]
        public void Add_RejectsSelfReference()
        {
            var folder = new Folder("root");

            Assert.ThrowsException<ArgumentException>(() => folder.Add(folder));
        }

        /// <summary>
        /// Construction rejects a null, empty, or whitespace-only name.
        /// </summary>
        [TestMethod]
        public void Constructor_RejectsInvalidName()
        {
            Assert.ThrowsException<ArgumentException>(() => new Folder(string.Empty));
            Assert.ThrowsException<ArgumentException>(() => new Folder("  "));
        }
    }
}
