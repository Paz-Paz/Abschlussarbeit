using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Argumentationsframework.AFModul
{
  #region CLASS AF .......................................................................................................

  internal class AF
  {
    #region Eigenschaften ..................................................................................................

    private Dictionary<string, Knoten> _knoten = new();

    #endregion .............................................................................................................
    #region Konstruktor ....................................................................................................

    internal IReadOnlyList<IKnoten> Knoten
    {
      get
      {
        List<Knoten> knoten = this._knoten.Values.ToList();
        knoten.Sort();
        return knoten;
      }
    }

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    public override string ToString()
    {
      StringBuilder sb = new();
      sb.AppendLine("========== AF ==========");
      foreach (IKnoten knoten in this.Knoten)
      {

        sb.AppendLine($"------- {knoten.Name} -------");
        for (int i = 0; i < Math.Max(knoten.Angreifer.Count, knoten.Ziele.Count); i++)
        {
          if (knoten.Angreifer.Count > i)
          {
            sb.Append($"{knoten.Angreifer[i].Name,3} -->");
          }
          else
          {
            sb.Append("".PadLeft(7, ' '));
          }
          sb.Append($" {knoten.Name} ");
          if (knoten.Ziele.Count > i)
          {
            sb.Append($"--> {knoten.Ziele[i].Name,3}");
          }
          sb.AppendLine();
        }
        sb.AppendLine();
      }
      return sb.ToString();
    }

    #endregion .............................................................................................................
    #region Paket-Interne Methoden .........................................................................................

    internal AF AddAngriff(in string angreifer, in string angegriffener)
    {
      if (string.IsNullOrWhiteSpace(angreifer))
      {
        throw new ArgumentNullException(nameof(angreifer));
      }
      if (string.IsNullOrWhiteSpace(angegriffener))
      {
        throw new ArgumentNullException(nameof(angegriffener));
      }

      if (!this._knoten.ContainsKey(angreifer))
      {
        this._knoten.Add(angreifer, new Knoten(angreifer));
      }
      if (!this._knoten.ContainsKey(angegriffener))
      {
        this._knoten.Add(angegriffener, new Knoten(angegriffener));
      }

      this._knoten[angreifer].FuegeAngriffHinzu(this._knoten[angegriffener]);

      return this;
    }

    internal AF AddKnoten(in string knotenName)
    {
      if (!this._knoten.ContainsKey(knotenName))
      {
        this._knoten.Add(knotenName, new AFModul.Knoten(knotenName));
      }
      return this;
    }

    internal bool TryGetKnoten(in string name, [NotNullWhen(true)] out IKnoten? knoten)
    {
      if (this._knoten.ContainsKey(name))
      {
        knoten = this._knoten[name];
      }
      else
      {
        knoten = null;
      }
      return knoten != null;
    }

    #endregion .............................................................................................................
    #region STATIC Paket-Interne Methoden ..................................................................................

    internal static AF BeispielAusAA => new AF()
       .AddAngriff("a", "e")
       .AddAngriff("b", "c")
       .AddAngriff("b", "a")
       .AddAngriff("c", "e")
       .AddAngriff("d", "a")
       .AddAngriff("e", "d");

    internal static AF BeispielAusSerialisierer => new AF()
      .AddKnoten("a")
      .AddKnoten("b1")
      .AddKnoten("b2")
      .AddKnoten("c")
      .AddKnoten("d")
      .AddKnoten("e")
      .AddKnoten("f")
      .AddKnoten("g")
      .AddKnoten("h")
      .AddAngriff("a", "b1")
      .AddAngriff("a", "b2")
      .AddAngriff("b1", "c")
      .AddAngriff("b2", "c")
      .AddAngriff("c", "d")
      .AddAngriff("c", "e")
      .AddAngriff("d", "a")
      .AddAngriff("e", "f")
      .AddAngriff("g", "h")
      .AddAngriff("h", "f");

    internal static AF ProposalAbbildung1 => new AF()
      .AddAngriff("a", "b")
      .AddAngriff("a", "c")
      .AddAngriff("b", "c")
      .AddAngriff("d", "c")
      .AddAngriff("d", "d");

    internal static AF ProposalSerialisierung => new AF()
      .AddAngriff("a", "b")
      .AddAngriff("b", "c")
      .AddAngriff("c", "d")
      .AddAngriff("d", "a")
      .AddAngriff("b", "e");

    #endregion .............................................................................................................
    #region Private Methoden ...............................................................................................
    #endregion .............................................................................................................
  }

  #endregion ..............................................................................................................
}
