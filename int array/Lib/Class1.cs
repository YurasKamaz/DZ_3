namespace Lib;

public class Class1
{
    public int n;
    public int[] arr;

    public Class1(int n) {
        this.n = n;
        this.arr = new int[n];
    }

    public void Input(ref int[] arr, params  int[]  values) {
        //arr = new int[this.n];
        for(int index = 0; index<this.n; index++) arr[index] = values[index];
    }

    public void RandomInput(ref int[] arr) {
        //arr = new int[this.n];
        Random random = new Random();
        for(int index = 0; index<this.n; index++) arr[index] = random.Next(10); 
    }

    public void Print(in int first, in int last) {
        for(int index = first; index<=last; index++) Console.WriteLine(this.arr[index]);
    }

    public List<int> findValue(in int value) {
        List<int> res = new List<int>();
        for(int index = 0; index<this.n; index++) {
            if(this.arr[index] == value) res.Add(index);
        }
        return res;
    }

    public void delValue(int value) {
        this.arr = this.arr.Where(val => val != value).ToArray();
        this.n = n-1;
    }

    public int findMax() {
        int res = 0;
        foreach(int item in this.arr) if(item > res) res = item;
        return res;
    }

    public void Add(ref int[] arr, in int[] values) {
        //if(values.Length > this.n || values.Length < this.n) throw new InvalidDataException();
        for(int index = 0; index<this.n; index++) {
            arr[index] += values[index]; 
        }
    }

    public void Sort(ref int[] arr) {
        for(int i=0; i<arr.Length-1; i++) {
            for(int j=i+1; j<arr.Length; j++) {
                if(arr[i] > arr[j]) 
                    (arr[i], arr[j]) = (arr[j], arr[i]);
            }
        }
    }
}
