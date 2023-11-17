// See https://aka.ms/new-console-template for more information

using System.Collections.Generic;
using System.Numerics;

class Program
{
    static void Main()
    {
        
        PrintRoots(PolyRoots(new double[] { 1, -1.5 }));     
        PrintRoots(PolyRoots(new double[] { 1, -2, 1 }));    
        PrintRoots(PolyRoots(new double[] { 1, 2, 2 }));       
        PrintRoots(PolyRoots(new double[] { 2, 2, -4, 0 }));    
    }

    static void PrintRoots(IEnumerable<Complex> roots)
    {
        foreach (var root in roots)
        {
            Console.Write(root.Real + " ");
        }
        Console.WriteLine();
    }

    static IEnumerable<Complex> PolyRoots(double[] coefficients)
    {
        // Using Newton-Raphson method to find roots
        int degree = coefficients.Length - 1;

        // Convert coefficients to complex numbers
        Complex[] polyCoefficients = new Complex[coefficients.Length];
        for (int i = 0; i < coefficients.Length; i++)
        {
            polyCoefficients[i] = new Complex(coefficients[i], 0);
        }

        // Initial guesses for the roots
        Complex[] roots = new Complex[degree];

        // Perform Newton-Raphson iterations
        for (int i = 0; i < degree; i++)
        {
            roots[i] = NewtonRaphson(polyCoefficients, roots, i);
        }

        return roots;
    }

    static Complex NewtonRaphson(Complex[] coefficients, Complex[] roots, int i)
    {
        const int maxIterations = 1000;
        const double tolerance = 1e-10;

        Complex root = roots[i];
        Complex numerator = Polynomial.Evaluate(coefficients, root);
        Complex derivative = Polynomial.Derivative(coefficients, root);

        int iteration = 0;
        while (numerator.Magnitude > tolerance && iteration < maxIterations)
        {
            root = root - numerator / derivative;
            numerator = Polynomial.Evaluate(coefficients, root);
            derivative = Polynomial.Derivative(coefficients, root);
            iteration++;
        }

        return root;
    }

    static class Polynomial
    {
        public static Complex Evaluate(Complex[] coefficients, Complex x)
        {
            Complex result = 0;
            for (int i = coefficients.Length - 1; i >= 0; i--)
            {
                result = result * x + coefficients[i];
            }
            return result;
        }

        public static Complex Derivative(Complex[] coefficients, Complex x)
        {
            Complex result = 0;
            for (int i = coefficients.Length - 1; i > 0; i--)
            {
                result = result * x + i * coefficients[i];
            }
            return result;
        }
    }
}


