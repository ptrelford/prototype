class Program
{
    static void Main(string[] args)
    {
        dynamic Circle = Prototype.Define(that => {});
        Circle.X = 1;
        System.Diagnostics.Debug.WriteLine((int ) Circle.X);

        var Square = Prototype.Define<int>(
            (that, width) => {
                that.Width = width;
            });
        dynamic sq = Square.New(5);
        System.Diagnostics.Debug.WriteLine((int ) sq.Width);
        sq.Bang = (System.Action<int>)((x) => System.Diagnostics.Debug.WriteLine(x));
        sq.Bang(9);
    }
}

