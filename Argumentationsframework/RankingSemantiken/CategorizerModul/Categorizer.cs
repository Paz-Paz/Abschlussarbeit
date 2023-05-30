using Argumentationsframework.AFModul;

namespace Argumentationsframework.RankingSemantiken.CategorizerModul
{
  #region CLASS Categorizer ..............................................................................................

  internal class Categorizer : ARankingSemantik<double>
  {
    #region Konstruktor ....................................................................................................

    internal Categorizer(AF af)
      : base(af)
    {
      /* nothing */
    }

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................
    public override double BerechneKnoten(string name)
    {
      if (this._af.TryGetKnoten(name, out IKnoten? knoten))
      {
        return this.BerechneKnoten(knoten);
      }
      else
      {
        throw new ArgumentOutOfRangeException(nameof(name), $"Kein Knoten '{name}' bekannt.");
      }
    }

    #endregion .............................................................................................................
    #region Paket-Interne Methoden .........................................................................................

    internal void BerechneAlleKnoten()
    {
      foreach (IKnoten knoten in this._af.Knoten)
      {
        this.BerechneKnoten(knoten);
      }
    }

    #endregion .............................................................................................................
    #region Private Methoden ...............................................................................................

    private double BerechneKnoten(IKnoten knoten)
    {
      IEnumerable<IKnoten> angreifer = knoten.Angreifer;
      double result;

      if (this._knotenwerte.ContainsKey(knoten.Name))
      {
        result = this._knotenwerte[knoten.Name];
      }
      else if (angreifer.Count() == 0)
      {
        result = 1;
      }
      else
      {
        double sum = 0;
        foreach (IKnoten angreiferKnoten in angreifer)
        {
          sum += this.BerechneKnoten(angreiferKnoten);
        }
        result = 1 / (1 + sum);
      }

      if (this._knotenwerte.ContainsKey(knoten.Name))
      {
        this._knotenwerte[knoten.Name] = result;
      }
      else
      {
        this._knotenwerte.Add(knoten.Name, result);
      }
      return result;
    }

    #endregion .............................................................................................................
  }

  #endregion ..............................................................................................................
}
