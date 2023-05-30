namespace Argumentationsframework.RankingSemantiken
{
  #region INTERFACE IRankingSemantik<T> ..................................................................................

  internal interface IRankingSemantik<T>
  {
    #region Oeffentliche Methoden ..........................................................................................

    public T BerechneKnoten(string name);

    public void PrintToConsole(string startsWith = "");

    #endregion .............................................................................................................
  }

  #endregion ..............................................................................................................
}
