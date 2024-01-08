using GemensamService;
using System;

namespace Valvetwebb.Kontroller
{
    /// <summary>
    /// Summary description for HookerException.
    /// </summary>
    [Serializable]
    public class ValvetException : System.Exception
    {
        FelKod _felkod = FelKod.Default;

        /// <summary>
        /// Egenskapen FelKod
        /// </summary>
        public FelKod FelKod
        {
            get
            {
                return _felkod;
            }
            set
            {
                _felkod = value;
            }
        }

        /// <summary>
        /// Defaultkonstruktor
        /// </summary>
        public ValvetException() : base("[[Felkod: 0]]")
        {

        }

        /// <summary>
        /// Konstruktor att användas av bla subklasser.
        /// </summary>
        /// <param name="felkod">Felkod</param>
        public ValvetException(FelKod felkod) : base("[[Felkod: " + ((int)felkod).ToString() + ", ]]")
        {
            _felkod = felkod;

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="ex">Inner exception</param>
        public ValvetException(Exception ex) : base(ex.Message, ex)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="ex">Inner exception</param>
        /// <param name="felkod">Felkod</param>
        public ValvetException(FelKod felkod, Exception ex)
            : base("[[Felkod: " + ((int)felkod).ToString() + ", " + ex.Message + "]]", ex)
        {
            _felkod = felkod;
        }

        /// <summary>
        /// Konstruktor med fler överlagrade parametrar
        /// </summary>
        /// <param name="meddelande">Meddelande</param>
        /// <param name="ex">Inner Exception</param>
        public ValvetException(string meddelande, Exception ex) : base("[[Felkod: 0, " + meddelande + "]]", ex)
        {
        }
    }
}
