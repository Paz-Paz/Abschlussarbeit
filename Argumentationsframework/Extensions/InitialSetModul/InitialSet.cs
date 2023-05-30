using Argumentationsframework.AFModul;
using Argumentationsframework.Extensions.AdmissibleModul;
using System.Diagnostics;

namespace Argumentationsframework.Extensions.InitialSetModul
{
  #region CLASS InitialSet ...............................................................................................

  internal class InitialSet : IExtensions
  {

    #region Eigenschaften ..................................................................................................

    private readonly AF _af;

    #endregion .............................................................................................................
    #region Konstruktor ....................................................................................................

    internal InitialSet(in AF af)
    {
      Debug.Assert(af != null);

      this._af = af;
    }

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    public List<KnotenSet> Calculate()
    {

      List<KnotenSet> adm = this._af.GetAdmissibleExtensions();

      List<KnotenSet> result = new();
      foreach (KnotenSet i in adm)
      {
        bool hatTeilmenge = false;
        foreach (KnotenSet j in adm)
        {
          if (i == j)
          {
            continue;
          }

          if (j.IstTeilMengeVon(i))
          {
            hatTeilmenge = true;
            break;
          }
        }

        if (!hatTeilmenge)
        {
          result.Add(i);
        }
      }

      return result;
    }

    #endregion .............................................................................................................
  }

  #endregion ..............................................................................................................


  #region STATIC CLASS InitialSetExtensionMethods ........................................................................

  internal static class InitialSetExtensionMethods
  {
    #region STATIC Paket-Interne Methoden ..................................................................................

    internal static List<KnotenSet> GetInitialSet(this AF af)
    {
      InitialSet initialSet = new InitialSet(af);
      return initialSet.Calculate();
    }

    #endregion .............................................................................................................
  }

  #endregion ..............................................................................................................
}
