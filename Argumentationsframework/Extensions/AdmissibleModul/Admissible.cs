using Argumentationsframework.AFModul;
using System.Diagnostics;

namespace Argumentationsframework.Extensions.AdmissibleModul
{
  #region CLASS Admissible ...............................................................................................

  internal class Admissible : IExtensions
  {
    #region Eigenschaften ..................................................................................................

    private readonly AF _af;

    private List<KnotenSet> _sets = new();

    #endregion .............................................................................................................
    #region Konstruktor ....................................................................................................

    internal Admissible(in AF af)
    {
      Debug.Assert(af != null);

      this._af = af;
    }

    #endregion .............................................................................................................
    #region Getter/Setter ..................................................................................................

    private IReadOnlyList<IKnoten> Knoten => this._af.Knoten;

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    public List<KnotenSet> Calculate()
    {
      this.Calculate(new List<IKnoten>(), 0);
      return this._sets;
    }

    #endregion .............................................................................................................
    #region Private Methoden ...............................................................................................

    private void Calculate(List<IKnoten> aktuelleListe, int startIndex)
    {
      for (int i = startIndex; i < this.Knoten.Count; i++)
      {
        IKnoten knoten = this._af.Knoten[i];

        if (knoten.HasSelfAttack)
        {
          continue;
        }

        if (knoten.IsDefinitivNichtAkzeptiert)
        {
          continue;
        }

        if (aktuelleListe.Contains(knoten))
        {
          continue;
        }


        bool admissible = true;
        foreach (IKnoten knotenAusListe in aktuelleListe)
        {
          if (knotenAusListe.GreiftAn(knoten))
          {
            admissible = false;
            break;
          }
          if (knotenAusListe.WirdAngegriffenVon(knoten))
          {
            admissible = false;
            break;
          }
        }
        if (admissible)
        {
          List<IKnoten> next = new List<IKnoten>(aktuelleListe) { knoten };
          KnotenSet set = new KnotenSet(next);
          if (!this._sets.Contains(set))
          {
            List<IKnoten> rest = new();
            List<IKnoten> adm = new();
            foreach (IKnoten naechster in this.Knoten)
            {
              if (next.Contains(naechster))
              {
                adm.Add(naechster);
              }
              else
              {
                rest.Add(naechster);
              }
            }

            foreach (IKnoten naechster in adm)
            {
              foreach (IKnoten naechster1 in naechster.Ziele)
              {
                rest.Remove(naechster1);
              }
            }

            bool wirdAngegriffen = false;
            foreach (IKnoten pruefling in adm)
            {
              if (rest.Any(k => k.Ziele.Contains(pruefling)))
              {
                wirdAngegriffen = true;
                break;
              }
            }
            if (!wirdAngegriffen)
            {
              this._sets.Add(new KnotenSet(next));
            }

            this.Calculate(next, startIndex + 1);
          }
        }
      }
    }

    #endregion .............................................................................................................
  }

  #endregion ..............................................................................................................
  #region STATIC CLASS AdmissibleExtensionMethods ........................................................................

  internal static class AdmissibleExtensionMethods
  {
    #region STATIC Paket-Interne Methoden ..................................................................................

    internal static List<KnotenSet> GetAdmissibleExtensions(this AF af)
    {
      Admissible adm = new Admissible(af);
      return adm.Calculate();
    }

    #endregion .............................................................................................................

  }
  #endregion ..............................................................................................................
}
