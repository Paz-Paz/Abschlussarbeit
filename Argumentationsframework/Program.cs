using Argumentationsframework.AFModul;
using Argumentationsframework.Extensions;
using Argumentationsframework.Extensions.AdmissibleModul;
using Argumentationsframework.Extensions.InitialSetModul;
using Argumentationsframework.RankingSemantiken;
using Argumentationsframework.RankingSemantiken.CategorizerModul;

namespace Argumentationsframework
{
  internal class Program
  {
    static void Main(string[] args)
    {
      AF af = AF.BeispielAusSerialisierer;

      Console.WriteLine(af);

      //IRankingSemantik<double> categorizer = new Categorizer(af);
      ////categorizer.BerechneKnoten("b");
      //categorizer.BerechneKnoten("h");
      //categorizer.PrintToConsole();

      //SozialModel sozialModel = new SozialModel(af);
      ////sozialModel.BerechneKnoten("b");
      //sozialModel.BerechneKnoten("h");
      //sozialModel.PrintToConsole();

      //.AddAngriff("a", "b1")
      //.AddAngriff("a", "b2")
      //.AddAngriff("b1", "c")
      //.AddAngriff("b2", "c")
      //.AddAngriff("c", "d")
      //.AddAngriff("c", "e")
      //.AddAngriff("d", "a")
      //.AddAngriff("e", "f")
      //.AddAngriff("g", "h")
      //.AddAngriff("h", "f");

      int anzahlKnoten = 100;
      af = new AF();
      for (int i = 0; i < anzahlKnoten + 1; i++)
      {
        af.AddAngriff(NummerZuName(i, anzahlKnoten), NummerZuName(i + 1, anzahlKnoten));
      }

      af.AddAngriff("b", NummerZuName(anzahlKnoten / 2, anzahlKnoten));

      IRankingSemantik categorizer = new Categorizer2(af);
      categorizer.BerechneAlleKnoten();
      categorizer.PrintToConsole();


      categorizer = new Categorizer(af);
      categorizer.BerechneAlleKnoten();
      categorizer.PrintToConsole();

      //af = new AF()
      //.AddKnoten("a")
      //.AddKnoten("b1")
      //.AddKnoten("b2")
      //.AddKnoten("c")
      //.AddKnoten("d")
      //.AddKnoten("e")
      //.AddKnoten("f")
      //.AddAngriff("a", "b1")
      //.AddAngriff("a", "b2")
      //.AddAngriff("b1", "c")
      //.AddAngriff("b2", "c")
      //.AddAngriff("c", "d")
      //.AddAngriff("c", "e")
      //.AddAngriff("d", "a")
      //.AddAngriff("e", "f");

      //List<KnotenSet> a = af.GetAdmissibleExtensions();
      //af.PrintToConsole(a, "Admisible");

      //List<KnotenSet> b = af.GetInitialSet();
      //af.PrintToConsole(b, "Initial Set");





      Console.WriteLine("[ENTER] beendet...");
      Console.ReadLine();
    }

    private static string NummerZuName(in int nummer, in int maxNummer)
    {
      int laenge = maxNummer.ToString().Length;
      string  nummerString = nummer.ToString();
      string  name = $"a{nummerString.PadLeft(laenge, '0')}";
      return name;
    }
  }
}