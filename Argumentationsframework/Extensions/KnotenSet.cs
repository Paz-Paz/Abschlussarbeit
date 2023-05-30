using Argumentationsframework.AFModul;

namespace Argumentationsframework.Extensions
{
  #region CLASS KnotenSet ................................................................................................

  internal class KnotenSet
  {
    #region Eigenschaften ..................................................................................................

    private readonly List<IKnoten> _set;

    #endregion .............................................................................................................
    #region Konstruktor ....................................................................................................

    internal KnotenSet(IEnumerable<IKnoten> knoten)
    {
      this._set = new List<IKnoten>();
      foreach (IKnoten k in knoten)
      {
        this._set.Add(k);
      }
      this._set.Sort();
    }

    #endregion .............................................................................................................
    #region Getter/Setter ..................................................................................................

    internal IReadOnlyList<IKnoten> Knoten => this._set;

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    public static bool operator ==(KnotenSet a, KnotenSet b)
    {
      if (a._set.Count != b._set.Count)
      {
        return false;
      }
      foreach (IKnoten knoten in a._set)
      {
        if (!b._set.Contains(knoten))
        {
          return false;
        }
      }
      return true;
    }

    public static bool operator !=(KnotenSet a, KnotenSet b) => !(a == b);

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
      if (obj.GetType() != this.GetType())
      {
        return false;
      }

      return this == (KnotenSet)obj;
    }


    /// <inheritdoc/>
    public override int GetHashCode()
    {
      throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public override string ToString()
    {
      string zeile = "{ ";

      foreach (IKnoten knoten in this._set)
      {
        zeile += knoten.Name + ", ";
      }
      zeile = zeile.Substring(0, zeile.Length - 2);
      zeile += " }";
      return zeile;
    }

    #endregion .............................................................................................................
    #region Paket-Interne Methoden .........................................................................................

    internal void Add(IKnoten knoten)
    {
      if (!this._set.Contains(knoten))
      {
        this._set.Add(knoten);
      }
      this._set.Sort();
    }

    internal bool Contains(IKnoten knoten) => this._set.Contains(knoten);

    internal bool IstTeilMengeVon(KnotenSet anderesSet)
    {
      if (this == anderesSet)
      {
        return true;
      }
      else
      {
        return this.IstEchteTeilMengeVon(anderesSet);
      }
    }

    internal bool IstEchteTeilMengeVon(KnotenSet anderesSet)
    {
      if (this._set.Count == anderesSet._set.Count)
      {
        return false;
      }

      bool istTeilmenge = true;
      foreach (IKnoten aKnoten in this._set)
      {
        if (!anderesSet.Contains(aKnoten))
        {
          istTeilmenge = false;
          break;
        }
      }
      return istTeilmenge;
    }

    #endregion .............................................................................................................
  }

  #endregion ..............................................................................................................
  #region STATIC CLASS KnotenSetExtensionMethods .........................................................................

  internal static class KnotenSetExtensionMethods
  {
    #region STATIC Paket-Interne Methoden ..................................................................................

    internal static void PrintToConsole(this AF _, List<KnotenSet> knotenSets, string title)
    {
      Console.WriteLine();
      Console.WriteLine($"========== {title} ==========");

      foreach (KnotenSet set in knotenSets)
      {
        string zeile = "{ ";

        foreach (IKnoten knoten in set.Knoten)
        {
          zeile += knoten.Name + ", ";
        }
        zeile = zeile.Substring(0, zeile.Length - 2);
        zeile += " }";
        Console.WriteLine(zeile);
      }

    }

    #endregion .............................................................................................................
  }

  #endregion ..............................................................................................................
}
