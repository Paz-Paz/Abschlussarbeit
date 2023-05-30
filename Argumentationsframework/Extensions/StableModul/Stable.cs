using Argumentationsframework.AFModul;
using System.Diagnostics;

namespace Argumentationsframework.Extensions.StableModul
{
  #region CLASS Stable ...................................................................................................

  internal class Stable : IExtensions
  {
    #region Eigenschaften ..................................................................................................

    private readonly AF _af;

    #endregion .............................................................................................................
    #region Konstruktor ....................................................................................................

    internal Stable(AF af)
    {
      Debug.Assert(af != null);
      this._af = af;
    }

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    public List<KnotenSet> Calculate()
    {
      List<IKnoten> abgelehnt = new();
      List<IKnoten> akzeptiert = new();
      List<IKnoten> zuBearbeiten = this._af.Knoten.ToList();


      bool wasGeaendert = true;
      while (wasGeaendert || zuBearbeiten.Count == 0)
      {
        wasGeaendert = false;


        foreach (IKnoten knoten in zuBearbeiten)
        {
          if (knoten.IsAktuellNichtAkzeptiert)
          {
            abgelehnt.Add(knoten);
            wasGeaendert = true;
          }
        }

        foreach (IKnoten knoten in abgelehnt)
        {
          zuBearbeiten.Remove(knoten);
        }

        foreach (IKnoten knoten in zuBearbeiten)
        {
          if (knoten.IsAktuellAkzeptiert)
          {
            akzeptiert.Add(knoten);
            wasGeaendert = true;
          }
        }

        foreach (IKnoten knoten in akzeptiert)
        {
          zuBearbeiten.Remove(knoten);
        }
      }


      List<KnotenSet> ergebnis = new();
      this.Calculate(ref ergebnis, akzeptiert, abgelehnt, zuBearbeiten);
      return ergebnis;
    }

    #endregion .............................................................................................................
    #region Private Methoden ...............................................................................................

    private void Calculate(ref List<KnotenSet> ergebnis, in IEnumerable<IKnoten> akzeptiert, in IEnumerable<IKnoten> abgelehnt, in IEnumerable<IKnoten> zuBearbeiten)
    {
      List<IKnoten> ak = new List<IKnoten>(akzeptiert);
      List<IKnoten> ab = new List<IKnoten>(abgelehnt);
      List<IKnoten> zb = new List<IKnoten>(zuBearbeiten);

      if (zb.Count == 0)
      {
        KnotenSet stabil = new KnotenSet(akzeptiert);
        KnotenSet fail = new KnotenSet(abgelehnt);
        //Console.WriteLine($"{stabil} - {fail}");
        if (!ergebnis.Contains(stabil))
        {
          ergebnis.Add(stabil);
        }
        return;
      }

      foreach (Knoten knoten in zb)
      {
        if (ak.Contains(knoten) || ab.Contains(knoten))
        {
          continue;
        }
        List<IKnoten> naechstesZubearbeiten = new List<IKnoten>(zuBearbeiten);

        ak.Add(knoten);
        naechstesZubearbeiten.Remove(knoten);

        foreach (Knoten angreifer in knoten.Angreifer)
        {
          if (!ab.Contains(angreifer))
          {
            ab.Add(angreifer);
          }
          naechstesZubearbeiten.Remove(angreifer);
        }

        foreach (Knoten angegriffen in knoten.Ziele)
        {
          if (!ab.Contains(angegriffen))
          {
            ab.Add(angegriffen);
          }
          naechstesZubearbeiten.Remove(angegriffen);

        }
        this.Calculate(ref ergebnis, ak, ab, naechstesZubearbeiten);
      }

      return;
    }

    #endregion .............................................................................................................
  }

  #endregion ..............................................................................................................
  #region STATIC CLASS StableExtensionMethods ............................................................................

  internal static class StableExtensionMethods
  {
    #region STATIC Paket-Interne Methoden ..................................................................................

    internal static List<KnotenSet> GetStableExtensions(this AF af)
    {
      Stable stable = new Stable(af);
      return stable.Calculate();
    }

    #endregion .............................................................................................................
  }

  #endregion ..............................................................................................................
}
