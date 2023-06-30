namespace Argumentationsframework.RankingSemantiken
{
  #region INTERFACE IRankingSemantik .....................................................................................

  internal interface IRankingSemantik
  {
    #region Oeffentliche Methoden ..........................................................................................

    public void BerechneAlleKnoten();


    public void PrintToConsole(string startsWith = "");

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
  #region INTERFACE IRankingSemantik<T> ..................................................................................

  internal interface IRankingSemantik<T> : IRankingSemantik
  {
    #region Oeffentliche Methoden ..........................................................................................

    public T BerechneKnoten(string name);


    #endregion .............................................................................................................
  }

  #endregion ..............................................................................................................
}
