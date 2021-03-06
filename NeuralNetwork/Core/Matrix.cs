﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Core
{
    public class Matrix
    {
        public int rows;
        public int cols;
        public float[,] data;

        public Matrix(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            data = new float[rows, cols];
        }

        public static Matrix fromArray(float[] arr)
        {
            Matrix m = new Matrix(arr.Length, 1);
            for (int i = 0; i < arr.Length; i++)
            {
                m.data[i, 0] = arr[i];
            }
            return m;
        }

        public void randomize(Random r)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    float f = (float)r.NextDouble() * 2 - 1;
                    data[i, j] = f;
                }
            }
        }

        public void randomize(Random r, float min, float max)
        {
            float range = max - min;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    float f = (float)r.NextDouble() * range + min;
                    data[i, j] = f;
                }
            }
        }

        public static Matrix transpose(Matrix m)
        {
            Matrix output = new Matrix(m.cols, m.rows);
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    output.data[j, i] = m.data[i, j];
                }
            }
            return output;
        }

        public void multiply(float n)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    data[i, j] *= n;
                }
            }
        }

        public void hadamard(Matrix n)
        {
            if (!(rows == n.rows && cols == n.cols))
            {
                Console.WriteLine("Matrices must have the same dimentions");
                return;
            }
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    data[i, j] *= n.data[i, j];
                }
            }
        }

        public static Matrix hadamard(Matrix m, Matrix n)
        {

            if (!(m.rows == n.rows && m.cols == n.cols))
            {
                Console.WriteLine("Matrices must have the same dimensions");
                return null;
            }

            Matrix output = new Matrix(m.rows, m.cols);
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    output.data[i, j] = m.data[i, j] * n.data[i, j];
                }
            }
            return output;
        }

        public static Matrix multiply(Matrix a, Matrix b)
        {
            if (a.cols != b.rows)
            {
                throw new Exception("Cols of A and Rows of B must be equaled");
            }
            Matrix output = new Matrix(a.rows, b.cols);
            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < b.cols; j++)
                {
                    float sum = 0;
                    for (int k = 0; k < a.cols; k++)
                    {
                        sum += a.data[i, k] * b.data[k, j];
                    }
                    output.data[i, j] = sum;
                }
            }
            return output;
        }

        public void add(int n)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    data[i, j] += n;
                }
            }
        }

        public void add(Matrix m)
        {
            if (!(rows == m.rows && cols == m.cols))
            {
                throw new Exception("Matrices must have the same dimensions");
            }
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    data[i, j] += m.data[i, j];
                }
            }
        }

        public void subtract(float n)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    data[i, j] -= n;
                }
            }
        }

        public void subtract(Matrix m)
        {
            if (!(rows == m.rows || cols == m.cols))
            {
                throw new Exception("Matrices must have the same dimentions");
            }
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    data[i, j] -= m.data[i, j];
                }
            }
        }

        public static Matrix subtract(Matrix n, float s)
        {
            Matrix m = new Matrix(n.rows, n.cols);

            for (int i = 0; i < n.rows; i++)
            {
                for (int j = 0; j < n.cols; j++)
                {
                    m.data[i, j] = n.data[i, j] - s;
                }
            }
            return m;
        }

        public static Matrix subtract(Matrix a, Matrix b)
        {
            if (a.rows != b.rows || a.cols != b.cols)
            {
                throw new Exception("Matrices must have the same dimensions");
            }
            Matrix output = new Matrix(a.rows, a.cols);
            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < a.cols; j++)
                {
                    output.data[i, j] = a.data[i, j] - b.data[i, j];
                }
            }
            return output;
        }

        public void map(Func<float, float> function)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    data[i, j] = function(data[i, j]);
                }
            }
        }

        public static Matrix map(Func<float, float> function, Matrix m)
        {
            Matrix output = new Matrix(m.rows, m.cols);
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    output.data[i, j] = function(m.data[i, j]);
                }
            }
            return output;
        }

        public static Matrix exp(Matrix m)
        {
            Matrix output = new Matrix(m.rows, m.cols);
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    output.data[i, j] = (float)Math.Exp((float)m.data[i, j]);
                }
            }
            return output;
        }

        public float sum()
        {
            float sum = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sum += data[i, j];
                }
            }
            return sum;
        }

        public float sumSquared()
        {
            float sum = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sum += data[i, j] * data[i, j];
                }
            }
            return sum;
        }

        public static float sum(Matrix m)
        {
            float sum = 0;
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    sum += m.data[i, j];
                }
            }
            return sum;
        }

        public static float max(Matrix m)
        {
            float curMax = m.data[0, 0];
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    curMax = Math.Max(curMax, m.data[i, j]);
                }
            }
            return curMax;
        }

        public Matrix flip()
        {
            Matrix output = new Matrix(rows, cols);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    output.data[i, j] = data[(rows - 1) - i, (cols - 1) - j];
                }
            }
            return output;
        }

        public static Matrix flip(Matrix m)
        {
            Matrix output = new Matrix(m.rows, m.cols);
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    output.data[i, j] = m.data[(m.rows - 1) - i, (m.cols - 1) - j];
                }
            }
            return output;
        }

        public Matrix subMatrix(int xIn, int yIn, int width, int height)
        {
            Matrix output = new Matrix(width, height);
            int xOut = 0;
            int yOut = 0;
            for(int x = xIn; x < xIn + width; x++)
            {
                for (int y = yIn; y < yIn + height; y++)
                {
                    if (x < 0 || x >= rows || y < 0 || y >= cols) output.data[xOut, yOut] = 0;
                    else output.data[xOut, yOut] = data[x, y];
                    yOut++;
                }
                yOut = 0;
                xOut++;
            }

            return output;
        }

        public static Matrix subMatrix(Matrix source, int xIn, int yIn, int width, int height)
        {
            Matrix output = new Matrix(width, height);
            int xOut = 0;
            for (int x = xIn; x < xIn + width; x++)
            {
                int yOut = 0;
                for (int y = yIn; y < yIn + height; y++)
                {
                    if (x < 0 || x >= source.rows || y < 0 || y >= source.cols) output.data[xOut, yOut] = 0;
                    else output.data[xOut, yOut] = source.data[x, y];
                    yOut++;
                }
                xOut++;
            }

            return output;
        }

        public static void table(Matrix m)
        {
            if (m == null) return;
            for (int i = 0; i < m.rows; i++)
            {

                for (int j = 0; j < m.cols; j++)
                {
                    Console.Write("|" + m.data[i, j] + "\t");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("");
        }

        public String table()
        {
            String output = "";
            for (int i = 0; i < rows; i++)
            {

                for (int j = 0; j < cols; j++)
                {
                    output += "| " + data[i, j];
                }
                output += "|| ";
            }
            return output;
        }

        public static Boolean checkForDimensions(Matrix m, int rows, int cols)
        {
            return m.rows == rows && m.cols == cols;
        }
        public static Boolean checkForDimensions(Matrix m, Matrix n)
        {
            return m.rows == n.rows && m.cols == n.cols;
        }
    }
}
