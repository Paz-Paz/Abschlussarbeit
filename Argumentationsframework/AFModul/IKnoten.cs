namespace Argumentationsframework.AFModul
{
  internal interface IKnoten : IComparable, IComparable<IKnoten>
  {
    internal IReadOnlyList<IKnoten> Angreifer { get; }

    internal string Name { get; }

    internal bool HasSelfAttack { get; }

    internal bool IsAktuellAkzeptiert { get; }

    internal bool IsAktuellNichtAkzeptiert { get; }

    internal bool IsDefinitivAkzeptiert { get; }

    internal bool IsDefinitivNichtAkzeptiert { get; }

    internal IReadOnlyList<IKnoten> Ziele { get; }


    internal bool GreiftAn(IKnoten ziel);

    internal bool WirdAngegriffenVon(IKnoten angreifer);

  }
}
