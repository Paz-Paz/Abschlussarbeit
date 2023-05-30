using Argumentationsframework.AFModul;
using System.Diagnostics;

namespace Argumentationsframework.RankingSemantiken
{
  #region ABSTRACT CLASS ARankingSemantik<T> .............................................................................

  internal abstract class ARankingSemantik<T> : IRankingSemantik<T>
  {

    #region Eigenschaften ..................................................................................................

    protected readonly AF _af;
    protected readonly Dictionary<string, T> _knotenwerte = new();

    #endregion .............................................................................................................
    #region Konstruktor ....................................................................................................

    internal ARankingSemantik(AF af)
    {
      Debug.Assert(af != null);

      this._af = af;
    }

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    public abstract T BerechneKnoten(string name);

    public virtual void PrintToConsole(string startsWith = "")
    {
      Console.WriteLine();
      Console.WriteLine($"========== {this.GetType().Name} ==========");
      foreach (KeyValuePair<string, T> knotenwert in this._knotenwerte.Where(k => k.Key.StartsWith(startsWith)) ?? Enumerable.Empty<KeyValuePair<string, T>>())
      {
        Console.WriteLine($"{knotenwert.Key}: {knotenwert.Value}");
      }

    }

    #endregion .............................................................................................................
  }

  #endregion ..............................................................................................................
}
