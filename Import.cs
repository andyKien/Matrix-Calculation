using System;
namespace Mathe.Matrizenrechnung {

    class Import {

        public static Matrix[] StringToMatrix(string file) {

            string[] strA = System.IO.File.ReadAllLines(file);
            int numLines = 0;

            foreach (string str in strA) {
                if (str.Contains('[') && str.Contains(']')) {
                    numLines++;
                }
            }

            Matrix[] matrizen = new Matrix[numLines];
            int z = 0;

            foreach (string str in strA) {
                if (str.Contains('[') && str.Contains(']')) {



                    string tSub = str.Substring(str.IndexOf('[') + 1);
                    tSub = tSub.Remove(tSub.IndexOf(']'));

                    string[] tSplitD1 = tSub.Split(';');
                    int dim1 = tSplitD1.Length;
                    for (int i = 0; i < dim1; i++) {
                        tSplitD1[i] = tSplitD1[i].Trim();
                    }

                    int dim2 = tSplitD1[0].Split(' ').Length;

                    double[,] matrix = new double[dim1, dim2];

                    for (int i = 0; i < dim1; i++) {
                        string[] strs = tSplitD1[i].Split();

                        for (int j = 0; j < dim2; j++) {
                            matrix[i, j] = Convert.ToDouble(strs[j]);
                        }
                    }

                    matrizen[z] = new Matrix(matrix);

                    z++;

                }
            }

            return matrizen;

        }
    }
}