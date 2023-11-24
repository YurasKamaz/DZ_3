namespace intArrTest;

[TestClass]
public class UnitTest1
{
    public Lib.Class1 a = new Lib.Class1(3);
    [TestMethod]
    public void Input()
    {
        a.Input(ref a.arr, 1, 2, 3);
        Assert.AreEqual(2, a.arr[1]);
        a.Input(ref a.arr, 3, 2, 1);
        Assert.AreEqual(3, a.arr[0]);
    }

    // [TestMethod]
    // public void Print() {
    //     a.Input(ref a.arr, 1, 2, 3);
        
    // }

    [TestMethod]
    public void Finding() {
        a.Input(ref a.arr, 1, 3, 3);
        int[] res = a.findValue(3).ToArray();
        bool isEqual = res.SequenceEqual(new int[2]{1, 2});
        Assert.IsTrue(isEqual);
    }

    [TestMethod]
    public void Deleting() {
        a.Input(ref a.arr, 1, 3, 3);
        a.delValue(3);
        bool isEqual = a.arr.SequenceEqual(new int[1]{1});
        Assert.IsTrue(isEqual);
    }

    [TestMethod]
    public void findingMax() {
        a.Input(ref a.arr, 1, 2, 3);
        Assert.AreEqual(3, a.findMax());
    }

    [TestMethod]
    public void Adding() {
        a.Input(ref a.arr, 1, 2, 3);
        a.Add(ref a.arr, new int[3]{3, 2, 1});
        bool isEqual = a.arr.SequenceEqual(new int[3]{4, 4, 4});
        Assert.IsTrue(isEqual);
    }

    [TestMethod]
    public void Sort() {
        a.Input(ref a.arr, 3, 2, 5);
        a.Sort(ref a.arr);
        bool isEqual = a.arr.SequenceEqual(new int[3]{2, 3, 5});
        Assert.IsTrue(isEqual);
    }
}