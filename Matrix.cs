using System;

namespace Mathe.Matrizenrechnung {

    class Matrix {


        // Felder
        int dim1;
        int dim2;
	    public double[,] werte;
	

	
	    // Eigenschaften
	    public int[] Dimension {
            get {
                int[] dim = new int[2] { dim1, dim2 };
                return dim;
            }
        }



        // Konstruktoren
        public Matrix() {
            dim1 = 1;
            dim2 = 1;
            werte = new double[1, 1];
        }

        public Matrix(double[,] w) {

            dim1 = w.GetUpperBound(0) + 1;
            dim2 = w.GetUpperBound(1) + 1;
            werte = w;
            
        }



        // Operatoren
        public static Matrix operator+(Matrix m1, Matrix m2) {
            return Matrix.Addieren(m1, m2);
        }

        public static Matrix operator-(Matrix m1, Matrix m2) {
            return Matrix.Subtrahieren(m1, m2);
        }

        public static Matrix operator*(Matrix m1, Matrix m2) {
            return Matrix.Multiplizieren(m1, m2);
        }



        // Methoden public

        public Matrix Kofaktor() {
            return Kofaktor(werte);
        }

        public double Determinante() {
            return Determinante(werte);
        }

        public Matrix Invertieren() {
            return Invertieren(werte);
        }

        public Matrix Transponieren() {
            return Transponieren(werte);
        }

        public Matrix Addieren(Matrix m) {
            return Matrix.Addieren(this, m);
        }

        public Matrix Subtrahieren(Matrix m) {
            return Matrix.Subtrahieren(this, m);
        }

        public Matrix Multiplizieren(Matrix m) {
            return Matrix.Multiplizieren(this, m);
        }



        // Methoden private
        Matrix Kofaktor(double[,] w) {

            int d1 = w.GetUpperBound(0) + 1, d2 = w.GetUpperBound(1) + 1;
            double[,] cof = new double[d1, d2];
            double[,] uMat = new double[d1 - 1, d2 - 1];
            
            for (int is_cof = 0;  is_cof < d2;  is_cof++) {
                for (int iz_cof = 0; iz_cof < d1; iz_cof++) {

                    int z = 0, s = 0;
                    uMat.Initialize();

                    for (int is_umat = 0; is_umat < d2; is_umat++) {
                        for (int iz_umat = 0; iz_umat < d1; iz_umat++) {

                            if (is_umat == is_cof | iz_umat == iz_cof) {
                                // Nichts tun
                            } else {
                                uMat[z, s] = w[iz_umat, is_umat];
                                if (z == uMat.GetUpperBound(0)) {
                                    z = 0;
                                    s++;
                                } else {
                                    z++;
                                }
                            }

                        }
                    }

                    cof[iz_cof, is_cof] = Math.Pow(-1.0, (iz_cof + is_cof)) * Determinante(uMat);


                }

            }

            Matrix mCof = new Matrix(cof);

            return mCof;

        }

        double Determinante(double[,] w) {

            int d1 = w.GetUpperBound(0) + 1, d2 = w.GetUpperBound(1) + 1;

            if (d1 != d2) {
                Console.WriteLine("Die Determinante ist für diese Matrix nicht definert!");
                return 0;
            }
            if (d1 == d2 && d1 == 1) {
                double det = w[0, 0];
                return det;
            }
            if (d1 == d2 && d1 == 2) {
                double det = w[0, 0] * w[1, 1] - w[1, 0] * w[0, 1];
                return det;
            }
            if (d1 == d2 && d1 == 3) {
                double det = w[0, 0] * w[1, 1] * w[2, 2] + w[0, 1] * w[1, 2] * w[2, 0] + w[0, 2] * w[1, 0] * w[2, 1]
                             - w[2, 0] * w[1, 1] * w[0, 2] - w[2, 1] * w[1, 2] * w[0, 0] - w[2, 2] * w[1, 0] * w[0, 1];
                return det;
            } else {
                double[,] cof = new double[Kofaktor(w).dim1, Kofaktor(w).dim2];
                cof = Kofaktor(w).werte;

                double det = 0; ;
                for (int s = 0; s < d2; s++) {
                    det += w[0, s] * cof[0, s];
                }
                return det;
            }
        }

        Matrix Invertieren(double[,] w) {

            int d1 = w.GetUpperBound(0) + 1, d2 = w.GetUpperBound(1) + 1;
            if (d1 != d2) {
                Console.WriteLine("Inverse Matrix nicht definiert!");
                Matrix mat = new Matrix();
                return mat;
            } else {
                double[,] cof = Kofaktor(w).werte;
                double[,] cofT = Transponieren(cof).werte;
                double det = Determinante(w);
                double[,] invMat = new double[d1, d2];

                if (det != 0) {
                    for (int i_s = 0; i_s < d2; i_s++) {
                        for (int i_z = 0; i_z < d1; i_z++) {
                            invMat[i_z, i_s] = cofT[i_z, i_s] / det;
                        }
                    }
                }

                Matrix mat = new Matrix(invMat);
                return mat;
            }

        }

        Matrix Transponieren(double[,] w) {

            int d1 = w.GetUpperBound(1)+1, d2 = w.GetUpperBound(0) + 1;
            double[,] wNeu = new double[d1, d2];

            for (int i = 0; i < d1; i++) {
                for (int j = 0; j < d2; j++) {
                    wNeu[i, j] = w[j, i];
                }
            }

            Matrix mNeu = new Matrix(wNeu);

            return mNeu;
        }

        static Matrix Addieren(Matrix m1, Matrix m2) {
            Matrix mOut = null;

            int[] dm1 = m1.Dimension;
            int[] dm2 = m2.Dimension;
            bool[] flag = { false, false };

            for (int i = 0; i < 2; i++) {
                flag[i] = dm1[i] == dm2[i];
            }

            if (flag[0] & flag[1]) {

                double[,] wOut = new double[dm1[0], dm1[1]];

                for (int i = 0; i < dm1[0]; i++) {
                    for (int j = 0; j < dm1[1]; j++) {
                        wOut[i, j] = m1.werte[i, j] + m2.werte[i, j];
                    }
                }

                mOut = new Matrix(wOut);

            }
            return mOut;
        }

        static Matrix Subtrahieren(Matrix m1, Matrix m2) {
            Matrix mOut = null;

            int[] dm1 = m1.Dimension;
            int[] dm2 = m2.Dimension;
            bool[] flag = { false, false };

            for (int i = 0; i < 2; i++) {
                flag[i] = dm1[i] == dm2[i];
            }

            if (flag[0] & flag[1]) {

                double[,] wOut = new double[dm1[0], dm1[1]];

                for (int i = 0; i < dm1[0]; i++) {
                    for (int j = 0; j < dm1[1]; j++) {
                        wOut[i, j] = m1.werte[i, j] - m2.werte[i, j];
                    }
                }

                mOut = new Matrix(wOut);

            }
            return mOut;
        }

        static Matrix Multiplizieren(Matrix m1, Matrix m2) {

            if (m1.dim2 != m2.dim1) {
                Console.WriteLine("\n\nMatrixmultiplikation nicht möglich! Unpassende Dimensionen.");
                Matrix mNeu = new Matrix();
                return mNeu;
            }
            else {

                double[,] wNeu = new double[m1.dim1, m2.dim2];

                for (int i_z = 0; i_z < m1.dim1; i_z++) {
                    for (int i_s = 0; i_s < m2.dim2; i_s++) {

                        double erg_i = 0;

                        for (int i = 0; i < m1.dim2; i++) {
                            erg_i += m1.werte[i_z, i] * m2.werte[i, i_s];
                        }

                        wNeu[i_z, i_s] = erg_i;

                    }
                }

                Matrix mNeu = new Matrix(wNeu);

                return mNeu;

            }

        }


    }

}
