namespace Argumentationsframework.AFModul
{
  #region CLASS Knoten ...................................................................................................

  internal class Knoten : IKnoten
  {
    #region Eigenschaften ..................................................................................................

    private Dictionary<string, Knoten> _greiftAn = new();
    private Dictionary<string, Knoten> _angegriffenVon = new();

    #endregion .............................................................................................................
    #region Konstruktor ....................................................................................................

    public Knoten(in string name)
    {
      if (string.IsNullOrWhiteSpace(name))
      {
        throw new ArgumentNullException(nameof(name));
      }

      this.Name = name;
    }

    #endregion .............................................................................................................
    #region Getter/Setter ..................................................................................................

    public IReadOnlyList<IKnoten> Angreifer
    {
      get
      {
        List<Knoten> knoten = this._angegriffenVon.Values.ToList();
        knoten.Sort();
        return knoten;
      }
    }

    /// <inheritdoc/>
    public string Name { get; }

    /// <inheritdoc/>
    public bool HasSelfAttack { get; private set; } = false;

    /// <inheritdoc/>
    public bool IsAktuellAkzeptiert => this.IsDefinitivAkzeptiert || this.Angreifer.All(a => a.IsDefinitivNichtAkzeptiert);

    /// <inheritdoc/>
    public bool IsAktuellNichtAkzeptiert => this.IsDefinitivNichtAkzeptiert || this.Angreifer.Any(a => a.IsAktuellAkzeptiert);

    /// <inheritdoc/>
    public bool IsDefinitivAkzeptiert => this.Angreifer.Count() == 0;

    /// <inheritdoc/>
    public bool IsDefinitivNichtAkzeptiert => this.HasSelfAttack || this.Angreifer.Any(a => a.IsDefinitivAkzeptiert);

    public IReadOnlyList<IKnoten> Ziele
    {
      get
      {
        List<Knoten> knoten = this._greiftAn.Values.ToList();
        knoten.Sort();
        return knoten;
      }
    }

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    /// <inheritdoc/>
    public int CompareTo(IKnoten? other)
    {
      if (other == null)
      {
        return 1;
      }
      return this.Name.CompareTo(other.Name);
    }

    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
      if (obj == null)
      {
        return 1;
      }
      return this.CompareTo(obj as IKnoten);

    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
      if (obj == null)
      {
        return false;
      }
      else if (obj.GetType() != this.GetType())
      {
        return false;
      }
      else
      {
        return this == ((Knoten)obj);
      }

    }

    /// <inheritdoc/>
    public override int GetHashCode() => base.GetHashCode();


    /// <inheritdoc/>
    public bool GreiftAn(IKnoten ziel) => this.GreiftAn(ziel.Name);

    /// <inheritdoc/>
    public bool WirdAngegriffenVon(IKnoten angreifer) => this.WirdAngegriffenVon(angreifer.Name);

    public static bool operator ==(Knoten a, Knoten b) => a.Name == b?.Name;

    public static bool operator !=(Knoten a, Knoten b) => !(a == b);

    #endregion .............................................................................................................
    #region Paket-Interne Methoden .........................................................................................

    internal void FuegeAngriffHinzu(Knoten knoten)
    {
      if (knoten == this)
      {
        this.HasSelfAttack = true;
      }

      if (!this._greiftAn.ContainsKey(knoten.Name))
      {
        this._greiftAn.Add(knoten.Name, knoten);
        knoten.FuegeAngegriffenVonHinzu(this);
      }
    }

    #endregion .............................................................................................................
    #region Private Methoden ...............................................................................................

    private void FuegeAngegriffenVonHinzu(Knoten knoten)
    {
      if (!this._angegriffenVon.ContainsKey(knoten.Name))
      {
        this._angegriffenVon.Add(knoten.Name, knoten);
      }
    }

    private bool GreiftAn(string ziel) => this._greiftAn.ContainsKey(ziel);

    private bool WirdAngegriffenVon(string angreifer) => this._angegriffenVon.ContainsKey(angreifer);

    #endregion .............................................................................................................
  }

  #endregion ..............................................................................................................
}
