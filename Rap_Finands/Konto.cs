using System;
using System.Collections.Generic;
namespace Rap_Finands
{
    class Konto
    {
        public string registreringsnr;
        public string kontonr;
        public string ejer;
        public List<Transaktion> transaktioner;
        public Konto(string ejer)
        {
            transaktioner = new List<Transaktion>();
            registreringsnr = Program.reginummer; //Sæt registreringsnummer på kontoen!
            kontonr = Program.LavEtKontoNummer(); //Lav et nyt (tilfældigt shh!) kontonummer
            this.ejer = ejer;  // Assign the owner of the account from the constructor argument.
        }

    }
}
/** 
Koden er lavet til undervisningbrug på TECHCOLLEGE
Voldum Bank og nævnte personer er fiktive.
~Simon Hoxer Bønding
**/