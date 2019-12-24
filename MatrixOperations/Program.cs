using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixOperations
{
    class Matrix
    {
        private decimal[,] _matrix;

        public int Rows { get; private set; }

        public int Cols { get; private set; }

        public Matrix(int rows, int cols)
        {
            this._matrix = new decimal[rows, cols];
            this.Rows = rows;
            this.Cols = cols;
        }

        public Matrix(decimal[,] matrix)
        {
            this._matrix = matrix;

            this.Rows = this._matrix.GetUpperBound(0) + 1;

            this.Cols = this._matrix.Length / this.Rows;
        }

        public decimal[,] GetMatrix()
        {
            return (decimal[,])_matrix.Clone();
        }

        public void MatrixRandomFill()
        {
            Random rnd = new Random();

            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Cols; j++)
                {
                    this._matrix[i, j] = rnd.Next();
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    sb.Append(String.Format("{0,5}", _matrix[i, j]));
                }
                sb.Append("\n");
            }

            return sb.ToString();
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Rows || m1.Cols != m2.Cols)
            {
                throw new Exception("Складываемые матрицы должны быть одного размера");
            }

            decimal[,] resultMatrix = m2.GetMatrix();

            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Cols; j++)
                {
                    resultMatrix[i, j] += m1._matrix[i, j];
                }
            }

            return new Matrix(resultMatrix);
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            decimal[,] resultMatrix = new decimal[m1.Rows, m2.Cols];
            decimal[,] firstMatrix = m1.GetMatrix();
            decimal[,] secondMatrix = m2.GetMatrix();

            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m2.Cols; j++)
                {
                    for (int k = 0; k < m1.Cols; k++)
                    {
                        resultMatrix[i, j] += firstMatrix[i, k] * secondMatrix[k, j];
                    }
                }
            }

            return new Matrix(resultMatrix);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            decimal[,] mat1 = new decimal[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            decimal[,] mat2 = new decimal[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            Matrix m1 = new Matrix(mat1);

            Matrix m2 = new Matrix(mat2);

            Matrix m3 = m1 + m2;

            Console.WriteLine(m3.ToString());

            Matrix m4 = m1 * m2;

            Console.WriteLine(m4.ToString());

            Console.Read();
        }
    }
}
