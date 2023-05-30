using Argumentationsframework.AFModul;

namespace Argumentationsframework.RankingSemantiken.SocialModelModul
{
  #region CLASS SozialModel ..............................................................................................

  internal class SozialModel : ARankingSemantik<double>
  {
    #region Konstruktor ....................................................................................................

    internal SozialModel(AF af)
      : base(af)
    {
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
    #region Private Methoden ...............................................................................................

    private double BerechneKnoten(IKnoten knoten)
    {
      IEnumerable<IKnoten> angreifer = knoten.Angreifer;
      const double tauA = 1 / 1.1;
      double result;

      if (this._knotenwerte.ContainsKey(knoten.Name))
      {
        result = this._knotenwerte[knoten.Name];
      }
      else if (angreifer.Count() == 0)
      {
        result = tauA;
      }
      else
      {
        List<double> werte = new();
        foreach (Knoten angreiferKnoten in angreifer)
        {
          werte.Add(this.BerechneKnoten(angreiferKnoten));
        }

        double zwischenergebnis;
        if (werte.Count > 0)
        {
          zwischenergebnis = werte[0];
          for (int i = 1; i < werte.Count(); i++)
          {
            double x1 = zwischenergebnis;
            double x2 = werte[i];
            zwischenergebnis = x1 + x2 - x1 * x2;
          }
        }
        else
        {
          zwischenergebnis = 1;
        }


        result = tauA * (1 - zwischenergebnis);
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
