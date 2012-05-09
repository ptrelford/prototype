class Program
{
    static void Main(string[] args)
    {
        var Mammal = Prototype.Constructor<string>((that,name_) => that.Name = name_);
        Mammal.Prototype.GetName = (System.Func<dynamic,string>) (that => that.Name);
        var myMammal = Mammal.New("Herb the Mammal");
        string name = myMammal.GetName();
        System.Diagnostics.Debug.WriteLine(name);

        var Cat = Prototype.Constructor<string>((that, name_) => {
            that.Name = name_;
            that.Saying = "meow";
        });

        Cat.Prototype = Mammal.New();

        var myCat = Cat.New("Henrietta");
        string n = myCat.GetName();
        System.Diagnostics.Debug.WriteLine(n);


        dynamic Circle = Prototype.Constructor(that => {});
        Circle.X = 1;
        System.Diagnostics.Debug.WriteLine((int ) Circle.X);

        var Square = Prototype.Constructor<int>(
            (that, width) => {
                that.Width = width;
            });
        dynamic sq = Square.New(5);
        System.Diagnostics.Debug.WriteLine((int ) sq.Width);
        sq.Bang = (System.Action<int>)((x) => System.Diagnostics.Debug.WriteLine(x));
        sq.Bang(9);
        Square.Prototype.Sides = 4;
        System.Diagnostics.Debug.WriteLine((int)sq.Sides);      
    }
}

