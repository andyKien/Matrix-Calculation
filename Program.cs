using System;

namespace Mathe.Matrizenrechnung {
    class Program {
        static void Main() {


            // Erzeugen der Matrix-Objekte
            Matrix[] matrices = Import.StringToMatrix(@"File path");

            var m1 = matrices[0];
            var m2 = matrices[1];

            var m3 = m1.Multiplizieren(m2);
            var m4 = m1 * m2;

            var mErg = m1 + m2;

            Console.ReadKey();








            // Lineare Regression
            //var xT = x.Transponieren();
            //var xTx = xT * x;
            //var xTxI = xTx.Invertieren();
            //var xTxI_xT = xTxI * xT;
            //var cp = xTxI_xT * y;

            //Console.WriteLine($"\ny = {cp.werte[0,0]} * x + {cp.werte[1,0]}");

            //   var dimensionen = cp.Dimension;
        }
    }
}
