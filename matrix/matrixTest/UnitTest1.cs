namespace matrixTest;

[TestClass]
public class UnitTest1
{
    public Lib.Class1 m = new Lib.Class1(2);
    [TestMethod]
    public void I()
    {
        m.Input(ref m.arr, new string[2]{"12 a  3", " 1 1 1"});
        int[][] exp = new int[2][]{new int[3]{12, 0, 3}, new int[3]{1, 1, 1}};
        bool isEqual = true;
        for(int i=0; i<m.arr.Length; i++) {
            isEqual = m.arr[i].SequenceEqual(exp[i]);
            if (!isEqual) break;
        }
        Assert.IsTrue(isEqual);
    }
    [TestMethod]
    public void Ma() {
        m.Input(ref m.arr, new string[2]{"1 2 a  3", " 1 1 1"});
        int[] ma = m.Max(); 
        bool isEqual = ma.SequenceEqual(new int[2]{3, 1});
        Assert.IsTrue(isEqual);
    }

    [TestMethod]
    public void Mi() {
        m.Input(ref m.arr, new string[2]{"1 2 a  3", " 1 1 1"});
        bool isEqual = m.Min().SequenceEqual(new int[2]{0, 1});
        Assert.IsTrue(isEqual);
    }

    [TestMethod]
    public void S() {
        m.Input(ref m.arr, new string[2]{"1 2 a  3", " 1 1 1"});
        bool isEqual = m.Sum().SequenceEqual(new int[2]{6, 3});
        Assert.IsTrue(isEqual);
    }
}