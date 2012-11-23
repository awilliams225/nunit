﻿// ***********************************************************************
// Copyright (c) 2009 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace NUnit.Framework.Constraints
{
    /// <summary>
    /// Predicate constraint wraps a Predicate in a constraint,
    /// returning success if the predicate is true.
    /// </summary>
    public class PredicateConstraint<TActual> : Constraint
    {
        readonly Predicate<TActual> predicate;

        /// <summary>
        /// Construct a PredicateConstraint from a predicate
        /// </summary>
        public PredicateConstraint(Predicate<TActual> predicate)
        {
            this.predicate = predicate;
        }

        /// <summary>
        /// Gets text describing a constraint
        /// </summary>
        public override string Description
        {
            get
            {
#if NETCF_2_0
                return "value matching predicate";
#else
                return predicate.Method.Name.StartsWith("<")
                    ? "value matching lambda expression"
                    : "value matching " + predicate.Method.Name;
#endif
            }
        }

        /// <summary>
        /// Determines whether the predicate succeeds when applied
        /// to the actual value.
        /// </summary>
        public override ConstraintResult ApplyTo(object actual)
        {
            if (!(actual is TActual))
                throw new ArgumentException("The actual value is not of type " + typeof(TActual).Name, "actual");

            return new ConstraintResult(this, actual, predicate((TActual)actual));
        }
    }
}
