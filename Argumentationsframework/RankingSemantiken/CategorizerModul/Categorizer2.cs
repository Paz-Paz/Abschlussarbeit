using Argumentationsframework.AFModul;

namespace Argumentationsframework.RankingSemantiken.CategorizerModul
{
  #region CLASS Categorizer2 ..............................................................................................

  internal class Categorizer2 : ARankingSemantik<double>
  {
    #region Konstruktor ....................................................................................................

    internal Categorizer2(AF af)
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
        double value = this.BerechneKnoten(knoten);
        if (!this._knotenwerte.ContainsKey(name))
        {
          this._knotenwerte.Add(name, value);
        }
        return value;
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


      if (angreifer.Count() == 0)
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
        result = 1 / (1 + (sum / 2));
      }



      return result;
    }

    #endregion .............................................................................................................
  }

  #endregion ..............................................................................................................
}
