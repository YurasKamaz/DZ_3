using System.Reflection.Metadata;

namespace Lib;

public class Class1
{
    int n;
    public int[][] arr;

    public Class1(int n) {
        this.arr = new int[n][];
        this.n = n;
    }

    public void Input(ref int[][] arr, in string[] str){
        int index = 0;
        foreach(string item in str) {
            var s = item.Split(' ').ToArray();
            List<int> res = new List<int>();
            foreach(var it in s) {
                if(String.IsNullOrEmpty(it)) continue;
                int result;
                bool isParsed = int.TryParse(it, out result);
                if(isParsed) res.Add(result);
                else res.Add(0);
            }
            arr[index] = res.ToArray();
            index++;
        }
    }

    public int[] Max() {
        int[] res = new int[this.arr.Length];
        int index = 0;
        foreach(int[] row in this.arr) {
            int ma = Int32.MinValue;
            foreach(int item in row) {
                if(item > ma) ma = item;
            }
            res[index] = ma;
            index++; 
        }   
        return res;
    }

    public int[] Min() {
        int[] res = new int[this.arr.Length];
        int index = 0;
        foreach(int[] row in this.arr) {
            int mi = Int32.MaxValue;
            foreach(int item in row) {
                if(item < mi) mi = item;
            }
            res[index] = mi;
            index++; 
        }   
        return res;
    }

    public int[] Sum() {
        int[] res = new int[this.arr.Length];
        int index = 0;
        foreach(int[] row in this.arr) {
            int sum = 0;
            foreach(int item in row) sum += item;
            res[index] = sum;
            index++;
        }
        return res;
    }
}
