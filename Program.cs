class Program
{
    static void Main(string[] args)
    {
        // Pseudo Classical

        var Mammal = Prototype.Constructor<string>((that,name_) => that.Name = name_);
        Mammal.Prototype.GetName = (System.Func<dynamic,string>) (that => that.Name);
        Mammal.Prototype.Says = (System.Action<dynamic>)(that => {
            string saying = that.Saying;
            Echo(saying);
        });
        var myMammal = Mammal.New("Herb the Mammal");
        string herb = myMammal.GetName();
        Echo(herb);

        var Cat = Prototype.Constructor<string>((that, name_) => {
            that.Name = name_;
            that.Saying = "meow";
        });

        Cat.Prototype = Mammal.New();

        var myCat = Cat.New("Henrietta");

        string name = myCat.GetName();
        Echo(name);

        dynamic Circle = Prototype.Constructor(that => {});
        Circle.X = 1;
        Echo((int ) Circle.X);

        var Square = Prototype.Constructor<int>(
            (that, width) => {
                that.Width = width;
            });
        dynamic sq = Square.New(5);
        Echo((int ) sq.Width);
        sq.Bang = (System.Action<int>)((x) => Echo(x));
        sq.Bang(9);
        Square.Prototype.Sides = 4;
        Echo((int)sq.Sides);

        // Prototypical

        var Class = Prototype.FromObject( new { 
            X = "Hello", 
            Hey = (System.Func<dynamic, string>)(that => that.X)
        });
       dynamic cool = Class.New();
    }


    private static void Echo(string text)
    {
        System.Diagnostics.Debug.WriteLine(text);
    }

    private static void Echo(int n)
    {
        System.Diagnostics.Debug.WriteLine(n);
    }

}

