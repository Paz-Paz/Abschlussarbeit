using Argumentationsframework.AFModul;
using Argumentationsframework.Extensions;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

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

    public void BerechneAlleKnoten()
    {
      foreach (Knoten knoten in this._af.Knoten)
      {
        this.BerechneKnoten(knoten.Name);
      }
    }


    public abstract T BerechneKnoten(string name);

    public virtual void PrintToConsole(string startsWith = "")
    {
      List<KeyValuePair<string, T>> kvpListe = this._knotenwerte.Where(k => k.Key.StartsWith(startsWith)).ToList();
      List<string> knotenNamen = new();
      foreach (KeyValuePair<string, T> kvp in kvpListe)
      {
        knotenNamen.Add(kvp.Key);
      }
      knotenNamen.Sort();

      Console.WriteLine();
      Console.WriteLine($"========== {this.GetType().Name} ==========");
      foreach (string namen in knotenNamen)
      {
        Console.WriteLine($"{namen}: {this._knotenwerte[namen]}");
      }
      //foreach (KeyValuePair<string, T> knotenwert in this._knotenwerte.Where(k => k.Key.StartsWith(startsWith)) ?? Enumerable.Empty<KeyValuePair<string, T>>())
      //{
      //  Console.WriteLine($"{knotenwert.Key}: {knotenwert.Value}");
      //}

    }

    #endregion .............................................................................................................
  }

  #endregion ..............................................................................................................
}
