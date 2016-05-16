﻿// Accord Statistics Library
// The Accord.NET Framework
// http://accord-framework.net
//
// Copyright © César Souza, 2009-2015
// cesarsouza at gmail.com
//
//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or (at your option) any later version.
//
//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public
//    License along with this library; if not, write to the Free Software
//    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//

namespace Accord.MachineLearning
{
    using Accord.Math;
    using Accord.Statistics;
    using System;

    /// <summary>
    /// Base class for binary classifiers.
    /// </summary>
    /// <typeparam name="TInput">The data type for the input data. Default is double[].</typeparam>
    [Serializable]
    public abstract class BinaryClassifierBase<TInput> :
        ClassifierBase<TInput, bool>,
        IBinaryClassifier<TInput>
    {


        // Input

        double IClassifier<TInput, double>.Decide(TInput input)
        {
            return Decide(input).ToZeroOne();
        }

        
        int IClassifier<TInput, int>.Decide(TInput input)
        {
            return Decide(input).ToZeroOne();
        }

        
        bool[] IClassifier<TInput, bool[]>.Decide(TInput input)
        {
            return Decide(input, new bool[NumberOfOutputs]);
        }

        
        int[] IClassifier<TInput, int[]>.Decide(TInput input)
        {
            return ToMulticlass().Decide(input, new int[NumberOfOutputs]);
        }

        
        double[] IClassifier<TInput, double[]>.Decide(TInput input)
        {
            return ToMulticlass().Decide(input, new double[NumberOfOutputs]);
        }



        // Input[]

        double[] IClassifier<TInput, double>.Decide(TInput[] input)
        {
            return ToMulticlass().Decide(input, new double[input.Length]);
        }

        int[] IClassifier<TInput, int>.Decide(TInput[] input)
        {
            return ToMulticlass().Decide(input, new int[input.Length]);
        }

        bool[][] IClassifier<TInput, bool[]>.Decide(TInput[] input)
        {
            return ToMulticlass().Decide(input, create<bool>(input));
        }

        double[][] IClassifier<TInput, double[]>.Decide(TInput[] input)
        {
            return ToMulticlass().Decide(input, create<double>(input));
        }

        int[][] IClassifier<TInput, int[]>.Decide(TInput[] input)
        {
            return ToMulticlass().Decide(input, create<int>(input));
        }




        // Input, result

        /// <summary>
        /// Computes class-label decisions for the given <paramref name="input" />.
        /// </summary>
        /// <param name="input">The input vectors that should be classified as
        /// any of the <see cref="ITransform.NumberOfOutputs" /> possible classes.</param>
        /// <param name="result">The location where to store the class-labels.</param>
        /// <returns>
        /// A set of class-labels that best describe the <paramref name="input" />
        /// vectors according to this classifier.
        /// </returns>
        public bool[] Decide(TInput input, bool[] result)
        {
            return Vector.OneHot<bool>(Decide(input), result);
        }

        int[] IMultilabelClassifier<TInput, int[]>.Decide(TInput input, int[] result)
        {
            return Vector.OneHot<int>(Decide(input), result);
        }

        double[] IMultilabelClassifier<TInput, double[]>.Decide(TInput input, double[] result)
        {
            return Vector.OneHot<double>(Decide(input), result);
        }



        // Input[], result[]

        int[] IClassifier<TInput, int>.Decide(TInput[] input, int[] result)
        {
            for (int i = 0; i < input.Length; i++)
                result[i] = Decide(input[i]).ToZeroOne();
            return result;
        }

        double[] IClassifier<TInput, double>.Decide(TInput[] input, double[] result)
        {
            for (int i = 0; i < input.Length; i++)
                result[i] = Decide(input[i]).ToZeroOne();
            return result;
        }

        
        bool[][] IClassifier<TInput, bool[]>.Decide(TInput[] input, bool[][] result)
        {
            return Jagged.OneHot<bool>(Decide(input), result);
        }


        int[][] IClassifier<TInput, int[]>.Decide(TInput[] input, int[][] result)
        {
            return Jagged.OneHot<int>(Decide(input), result);
        }

        double[][] IClassifier<TInput, double[]>.Decide(TInput[] input, double[][] result)
        {
            return Jagged.OneHot<double>(Decide(input), result);
        }






        // Transform

        int ITransform<TInput, int>.Transform(TInput input)
        {
            return ((IClassifier<TInput, int>)this).Decide(input);
        }

        
        double ITransform<TInput, double>.Transform(TInput input)
        {
            return ((IClassifier<TInput, double>)this).Decide(input);
        }

        
        double[] ITransform<TInput, double>.Transform(TInput[] input)
        {
            return Transform(input, new double[input.Length]);
        }

        
        double[] ITransform<TInput, double[]>.Transform(TInput input)
        {
            return Transform(input, create<double>(input));
        }


        int[] ITransform<TInput, int>.Transform(TInput[] input)
        {
            return Transform(input, new int[input.Length]);
        }

        double[][] ITransform<TInput, double[]>.Transform(TInput[] input)
        {
            return Transform(input, create<double>(input));
        }

        bool[] ITransform<TInput, bool[]>.Transform(TInput input)
        {
            return Transform(input, create<bool>(input));
        }

        bool[][] ITransform<TInput, bool[]>.Transform(TInput[] input)
        {
            return Transform(input, create<bool>(input));
        }

        int[] ITransform<TInput, int[]>.Transform(TInput input)
        {
            return Transform(input, create<int>(input));
        }

        int[][] ITransform<TInput, int[]>.Transform(TInput[] input)
        {
            return Transform(input, create<int>(input));
        }



        /// <summary>
        /// Applies the transformation to an input, producing an associated output.
        /// </summary>
        /// <param name="input">The input data to which the transformation should be applied.</param>
        /// <param name="result">A location to store the output, avoiding unnecessary memory allocations.</param>
        /// <returns>
        /// The output generated by applying this transformation to the given input.
        /// </returns>

        virtual public bool[] Transform(TInput input, bool[] result)
        {
            return Decide(input, result);
        }

        /// <summary>
        /// Applies the transformation to an input, producing an associated output.
        /// </summary>
        /// <param name="input">The input data to which the transformation should be applied.</param>
        /// <param name="result">A location to store the output, avoiding unnecessary memory allocations.</param>
        /// <returns>
        /// The output generated by applying this transformation to the given input.
        /// </returns>

        virtual public int[] Transform(TInput input, int[] result)
        {
            return ToMulticlass().Decide(input, result);
        }

        /// <summary>
        /// Applies the transformation to an input, producing an associated output.
        /// </summary>
        /// <param name="input">The input data to which the transformation should be applied.</param>
        /// <param name="result">A location to store the output, avoiding unnecessary memory allocations.</param>
        /// <returns>
        /// The output generated by applying this transformation to the given input.
        /// </returns>

        virtual public double[] Transform(TInput input, double[] result)
        {
            return ToMulticlass().Decide(input, result);
        }

        /// <summary>
        /// Applies the transformation to an input, producing an associated output.
        /// </summary>
        /// <param name="input">The input data to which the transformation should be applied.</param>
        /// <param name="result">A location to store the output, avoiding unnecessary memory allocations.</param>
        /// <returns>
        /// The output generated by applying this transformation to the given input.
        /// </returns>
        virtual public int[] Transform(TInput[] input, int[] result)
        {
            return ToMulticlass().Decide(input, result);
        }

        /// <summary>
        /// Applies the transformation to an input, producing an associated output.
        /// </summary>
        /// <param name="input">The input data to which the transformation should be applied.</param>
        /// <param name="result">A location to store the output, avoiding unnecessary memory allocations.</param>
        /// <returns>
        /// The output generated by applying this transformation to the given input.
        /// </returns>

        virtual public double[] Transform(TInput[] input, double[] result)
        {
            return ToMulticlass().Decide(input, result);
        }

        /// <summary>
        /// Applies the transformation to an input, producing an associated output.
        /// </summary>
        /// <param name="input">The input data to which the transformation should be applied.</param>
        /// <param name="result">A location to store the output, avoiding unnecessary memory allocations.</param>
        /// <returns>
        /// The output generated by applying this transformation to the given input.
        /// </returns>

        virtual public bool[][] Transform(TInput[] input, bool[][] result)
        {
            return ToMulticlass().Decide(input, result);
        }

        /// <summary>
        /// Applies the transformation to an input, producing an associated output.
        /// </summary>
        /// <param name="input">The input data to which the transformation should be applied.</param>
        /// <param name="result">A location to store the output, avoiding unnecessary memory allocations.</param>
        /// <returns>
        /// The output generated by applying this transformation to the given input.
        /// </returns>

        virtual public int[][] Transform(TInput[] input, int[][] result)
        {
            return ToMulticlass().Decide(input, result);
        }

        /// <summary>
        /// Applies the transformation to an input, producing an associated output.
        /// </summary>
        /// <param name="input">The input data to which the transformation should be applied.</param>
        /// <param name="result">A location to store the output, avoiding unnecessary memory allocations.</param>
        /// <returns>
        /// The output generated by applying this transformation to the given input.
        /// </returns>

        virtual public double[][] Transform(TInput[] input, double[][] result)
        {
            return ToMulticlass().Decide(input, result);
        }



        /// <summary>
        /// Views this instance as a multi-class classifier,
        /// giving access to more advanced methods, such as the prediction
        /// of integer labels.
        /// </summary>
        /// <returns>
        /// This instance seen as an <see cref="IMulticlassClassifier{TInput}" />.
        /// </returns>
        public IMulticlassClassifier<TInput> ToMulticlass()
        {
            return (IMulticlassClassifier<TInput>)this;
        }

        /// <summary>
        /// Views this instance as a multi-label classifier,
        /// giving access to more advanced methods, such as the prediction
        /// of one-hot vectors.
        /// </summary>
        /// <returns>
        /// This instance seen as an <see cref="IMultilabelGenerativeClassifier{TInput}" />.
        /// </returns>
        public IMultilabelClassifier<TInput> ToMultilabel()
        {
            return (IMultilabelClassifier<TInput>)this;
        }
    }

}