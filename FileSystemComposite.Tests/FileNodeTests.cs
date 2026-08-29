/******************************************************************************
 * Filename    = FileNodeTests.cs
 *
 * Project     = FileSystemComposite.Tests
 *
 * Author      = K Srirama Srikar
 *
 * Description = Unit tests for the FileNode leaf.
 *****************************************************************************/

using System;
using FileSystemComposite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystemComposite.Tests
{
    /// <summary>
    /// Unit tests for <see cref="FileNode"/>.
    /// </summary>
    [TestClass]
    public class FileNodeTests
    {
        /// <summary>
        /// A file reports the size it was created with.
        /// </summary>
        [TestMethod]
        public void GetSize_ReturnsConstructorValue()
        {
            var file = new FileNode("photo.png", 2048);

            Assert.AreEqual(2048, file.GetSize());
        }

        /// <summary>
        /// A file exposes the name it was created with.
        /// </summary>
        [TestMethod]
        public void Name_ReturnsConstructorValue()
        {
            var file = new FileNode("photo.png", 2048);

            Assert.AreEqual("photo.png", file.Name);
        }

        /// <summary>
        /// A file, being a leaf, always reports zero children.
        /// </summary>
        [TestMethod]
        public void Children_IsAlwaysEmpty()
        {
            var file = new FileNode("photo.png", 2048);

            Assert.AreEqual(0, file.Children.Count);
        }

        /// <summary>
        /// A zero-byte file is valid and reports a size of zero.
        /// </summary>
        [TestMethod]
        public void GetSize_AllowsZero()
        {
            var file = new FileNode("empty.txt", 0);

            Assert.AreEqual(0, file.GetSize());
        }

        /// <summary>
        /// Construction rejects a null, empty, or whitespace-only name.
        /// </summary>
        [TestMethod]
        public void Constructor_RejectsInvalidName()
        {
            Assert.ThrowsException<ArgumentException>(() => new FileNode(string.Empty, 10));
            Assert.ThrowsException<ArgumentException>(() => new FileNode("   ", 10));
        }

        /// <summary>
        /// Construction rejects a negative size.
        /// </summary>
        [TestMethod]
        public void Constructor_RejectsNegativeSize()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new FileNode("notes.txt", -1));
        }
    }
}
