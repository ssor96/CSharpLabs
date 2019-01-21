using System;
using Lab1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        MyGenericList<int> l;

        [TestInitialize]
        public void Init()
        {
            l = new MyGenericList<int>();
        }

        [TestCleanup]
        public void Clean()
        {
            l = null;
        }

        [TestMethod]
        public void TestAdd()
        {
            l.Add(0);
            l.Add(6);
            Assert.AreEqual("[0, 6]", l.ToString());
            l.Add(-3);
            Assert.AreEqual("[0, 6, -3]", l.ToString());
        }

        [TestMethod]
        public void TestRemove()
        {
            l.Add(1);
            l.Add(2);
            l.Add(3);
            l.Add(1);
            Assert.AreEqual(true, l.Remove(2));
            Assert.AreEqual("[1, 3, 1]", l.ToString());
            Assert.AreEqual(false, l.Remove(4));
            Assert.AreEqual(true, l.Remove(1));
            Assert.AreEqual("[3, 1]", l.ToString());
        }

        [TestMethod]
        public void TestClear()
        {
            l.Clear();
            Assert.AreEqual(0, l.Count);
            l.Add(64);
            l.Insert(0, 0);
            l.Clear();
            Assert.AreEqual(0, l.Count);
        }

        [TestMethod]
        public void TestContains()
        {
            Assert.AreEqual(false, l.Contains(1));
            l.Add(1);
            Assert.AreEqual(true, l.Contains(1));
            l.Add(1);
            Assert.AreEqual(true, l.Contains(1));
            Assert.AreEqual(false, l.Contains(2));
            l.Clear();
            Assert.AreEqual(false, l.Contains(1));
        }

        [TestMethod]
        public void TestIndexOf()
        {
            Assert.AreEqual(-1, l.IndexOf(1));
            l.Add(1);
            Assert.AreEqual(0, l.IndexOf(1));
            l.Add(1);
            Assert.AreEqual(0, l.IndexOf(1));
            l.Insert(0, 0);
            Assert.AreEqual(1, l.IndexOf(1));
            l.Clear();
            Assert.AreEqual(-1, l.IndexOf(0));
        }

        [TestMethod]
        public void TestInsert()
        {
            l.Insert(0, 0);
            Assert.AreEqual("[0]", l.ToString());
            l.Add(7);
            l.Insert(1, -3);
            Assert.AreEqual("[0, -3, 7]", l.ToString());
            l.RemoveAt(0);
            l.Insert(2, 0);
            Assert.AreEqual("[-3, 7, 0]", l.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNegativeIndexInsert()
        {
            l.Insert(-10, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestHugeIndexInsert()
        {
            l.Insert(99999, 1);
        }

        [TestMethod]
        public void TestRemoveAt()
        {
            l.Insert(0, 0);
            l.RemoveAt(0);
            Assert.AreEqual("[]", l.ToString());
            l.Add(7);
            l.Insert(1, -3);
            l.RemoveAt(1);
            Assert.AreEqual("[7]", l.ToString());
            l.Add(-8);
            l.Add(0);
            l.Add(11);
            l.RemoveAt(2);
            Assert.AreEqual("[7, -8, 11]", l.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyToNull()
        {
            l.Add(1);
            l.CopyTo(null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCopyToSmallArr()
        {
            l.Add(1);
            l.Add(2);
            l.Add(3);
            var arr = new int[2];
            l.CopyTo(arr, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCopyToNegativeIdx()
        {
            l.Add(1);
            var arr = new int[2];
            l.CopyTo(arr, -7);
        }

        [TestMethod]
        public void TestCopyToOK()
        {
            l.Add(1);
            l.Add(2);
            l.Add(3);
            var arr = new int[5];
            arr[0] = 76;
            arr[3] = -8;
            l.CopyTo(arr, 1);
            Assert.AreEqual(76, arr[0]);
            Assert.AreEqual(1, arr[1]);
            Assert.AreEqual(2, arr[2]);
            Assert.AreEqual(3, arr[3]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestHugeIndexRemove()
        {
            l.Add(1);
            l.Add(90);
            l.RemoveAt(100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexRemoveFromEmpty()
        {
            l.RemoveAt(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNegativeIndexRemove()
        {
            l.Add(0);
            l.RemoveAt(-2);
        }

        [TestMethod]
        public void TestCount()
        {
            Assert.AreEqual(0, l.Count);
            l.Add(1);
            Assert.AreEqual(1, l.Count);
        }

        [TestMethod]
        public void TestIndexRead()
        {
            l.Add(2);
            l.Add(3);
            Assert.AreEqual(3, l[1]);
            l.Add(4);
            l.Add(5);
            l.Remove(3);
            Assert.AreEqual(4, l[1]);
            Assert.AreEqual(2, l[0]);
        }

        [TestMethod]
        public void TestForeach()
        {
            string s;
            s = "";
            foreach (int v in l)
            {
                s += v.ToString() + '.';
            }
            Assert.AreEqual("", s);

            l.Add(8);
            l.Add(6);
            l.Add(-567);

            s = "";
            foreach (int v in l)
            {
                s += v.ToString() + '.';
            }
            Assert.AreEqual("8.6.-567.", s);

            l.Clear();
            l.Insert(0, -1);

            s = "";
            foreach (int v in l)
            {
                s += v.ToString() + '.';
            }
            Assert.AreEqual("-1.", s);
        }

        [TestMethod]
        public void TestIndexWrite()
        {
            l.Add(2);
            l.Add(3);
            l[0] = -1;
            Assert.AreEqual("[-1, 3]", l.ToString());
            l.Add(10);
            l[2] = 88;
            Assert.AreEqual("[-1, 3, 88]", l.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEmptyListIndexOutOfRangeRead()
        {
            int x = l[0];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEmptyListIndexOutOfRangeWrite()
        {
            l[0] = 1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestTailIndexOutOfRangeRead()
        {
            l.Add(2);
            l.Add(3);
            int x = l[2];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestBigIndexOutOfRangeRead()
        {
            l.Add(2);
            l.Add(3);
            int x = l[42];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestTailIndexOutOfRangeWrite()
        {
            l.Add(2);
            l.Add(3);
            l[2] = 1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestBigIndexOutOfRangeWrite()
        {
            l.Add(2);
            l.Add(3);
            l[42] = 1;
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNegativeRead()
        {
            l.Add(2);
            l.Add(3);
            int x = l[-8];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNegativeWrite()
        {
            l[-42] = 0;
        }
    }
}
