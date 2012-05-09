class Program
{
    static void Main(string[] args)
    {
        var Mammal = Prototype.Define<string>((that,name_) => that.Name = name_);
        Mammal.prototype.GetName = (System.Func<dynamic,string>) (that => that.Name);
        var myMammal = Mammal.New("Herb the Mammal");
        string name = myMammal.GetName();
        System.Diagnostics.Debug.WriteLine(name);

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
        Square.prototype.Sides = 4;
        System.Diagnostics.Debug.WriteLine((int)sq.Sides);      
    }
}

