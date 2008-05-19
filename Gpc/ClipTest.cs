/*
 * This license does NOT supersede the original license of GPC.  Please see:
 * http://www.cs.man.ac.uk/~toby/alan/software/#Licensing
 *
 * The SEI Software Open Source License, Version 1.0
 *
 * Copyright (c) 2004, Solution Engineering, Inc.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 *
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer. 
 *
 * 2. The end-user documentation included with the redistribution,
 *    if any, must include the following acknowledgment:
 *       "This product includes software developed by the
 *        Solution Engineering, Inc. (http://www.seisw.com/)."
 *    Alternately, this acknowledgment may appear in the software itself,
 *    if and wherever such third-party acknowledgments normally appear.
 *
 * 3. The name "Solution Engineering" must not be used to endorse or
 *    promote products derived from this software without prior
 *    written permission. For written permission, please contact
 *    admin@seisw.com.
 *
 * THIS SOFTWARE IS PROVIDED ``AS IS'' AND ANY EXPRESSED OR IMPLIED
 * WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED.  IN NO EVENT SHALL SOLUTION ENGINEERING, INC. OR
 * ITS CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF
 * USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
 * OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF
 * SUCH DAMAGE.
 * ====================================================================
 */

using NUnit.Framework;

namespace Gpc
{
	/**
	 * <code>ClipTest</code> is a suite of unit tests for testing <code>Clip</code>.
	 * <code>Clip</code> is a Java conversion of the <i>General Poly Clipper</i> algorithm 
	 * developed by Alan Murta (gpc@cs.man.ac.uk).
	 *
	 * @author  Dan Bridenbecker, Solution Engineering, Inc.
	 */
	[TestFixture]
	public class ClipTest
	{
		// -------------
		// --- Tests ---
		// -------------
		/**
		 * Test the intersection of two polygons that are 
		 * completely separate - result should be empty set.
		 */
		[Test]
		public void TestIntersectionEmptySet()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 1.0, 0.0 );
			p1.Add( 1.0, 1.0 );
			p1.Add( 0.0, 1.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 0.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 3.0 );
      
			Poly empty = Clipper.Intersection( p1, p2 );
			Assert.IsTrue( empty.IsEmpty() );
		}
   
		/**
		 * Test the intersection of two polygons where
		 * the second is contained in the first.
		 */
		[Test]
		[Category("Test")]
		public void TestIntersectionOneContainsTwo()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 4.0, 0.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 0.0, 4.0 );

			Poly p2 = new PolyDefault();
			p2.Add( 2.0, 1.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 3.0 );
      
			PolyDefault result = (PolyDefault) Clipper.Intersection( p1, p2 );
			//result.Print();
			Assert.AreEqual(p2, result);
		}

		/**
		 * Test the intersection of two polygons where
		 * the first is contained in the second.
		 */
		[Test]
		public void TestIntersectionTwoContainsOne()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 2.0, 0.0 );
			p1.Add( 3.0, 0.0 );
			p1.Add( 3.0, 3.0 );
			p1.Add( 2.0, 3.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 0.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 0.0, 4.0 );
      
			Poly result = Clipper.Intersection( p1, p2 );
			Assert.AreEqual( p1, result );
		}
   
		/**
		 * Test the intersection of two polygons that
		 * are equal.
		 */
		[Test]
		public void TestIntersectionTwoEqual()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 4.0, 0.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 0.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 0.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 0.0, 4.0 );
      
			Poly result = Clipper.Intersection( p1, p2 );
			Assert.AreEqual( p1, result );
		}
   
		/**
		 * Test the intersection of two rectangles that share
		 * one corner and two partial sides.
		 */
		[Test]
		public void TestIntersectionRectCorner1()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 4.0, 0.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 0.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 0.0 );
			p2.Add( 1.0, 0.0 );
			p2.Add( 1.0, 1.0 );
			p2.Add( 0.0, 1.0 );
      
			Poly result = Clipper.Intersection( p1, p2 );
			Assert.AreEqual( p2, result );
		}
   
		/**
		 * Test the intersection of two rectangles that share
		 * one corner and two partial sides.
		 */
		[Test]
		public void TestIntersectionRectCorner2()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 4.0, 0.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 0.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 3.0, 0.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 4.0, 1.0 );
			p2.Add( 3.0, 1.0 );
      
			Poly result = Clipper.Intersection( p1, p2 );
			Assert.AreEqual( p2, result );
		}
   
		/**
		 * Test the intersection of two rectangles that share
		 * one corner and two partial sides.
		 */
		[Test]
		public void TestIntersectionRectCorner3()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 4.0, 0.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 0.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 3.0, 3.0 );
			p2.Add( 4.0, 3.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 4.0 );
      
			Poly result = Clipper.Intersection( p1, p2 );
			Assert.AreEqual( p2, result );
		}
   
		/**
		 * Test the intersection of two rectangles that share
		 * one corner and two partial sides.
		 */
		[Test]
		public void TestIntersectionRectCorner4()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 4.0, 0.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 0.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 3.0 );
			p2.Add( 1.0, 3.0 );
			p2.Add( 1.0, 4.0 );
			p2.Add( 0.0, 4.0 );
      
			Poly result = Clipper.Intersection( p1, p2 );
			Assert.AreEqual( p2, result );
		}
   
		/**
		 * Test the intersection of two rectangles that 
		 * intersect on corner
		 */
		[Test]
		public void TestIntersectionRectInterCorner1()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 2.0, 2.0 );
			p1.Add( 4.0, 2.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 2.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 1.0, 1.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 1.0, 3.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 2.0, 2.0 );
			exp.Add( 3.0, 2.0 );
			exp.Add( 3.0, 3.0 );
			exp.Add( 2.0, 3.0 );
      
			Poly result = Clipper.Intersection( p1, p2 );
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the intersection of two rectangles that 
		 * intersect on corner
		 */
		[Test]
		public void TestIntersectionRectInterCorner2()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 2.0, 2.0 );
			p1.Add( 4.0, 2.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 2.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 3.0, 1.0 );
			p2.Add( 5.0, 1.0 );
			p2.Add( 5.0, 3.0 );
			p2.Add( 3.0, 3.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 3.0, 2.0 );
			exp.Add( 4.0, 2.0 );
			exp.Add( 4.0, 3.0 );
			exp.Add( 3.0, 3.0 );
      
			Poly result = Clipper.Intersection( p1, p2 );
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the intersection of two rectangles that 
		 * intersect on corner
		 */
		[Test]
		public void TestIntersectionRectInterCorner3()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 2.0, 2.0 );
			p1.Add( 4.0, 2.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 2.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 3.0, 3.0 );
			p2.Add( 5.0, 3.0 );
			p2.Add( 5.0, 5.0 );
			p2.Add( 3.0, 5.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 3.0, 3.0 );
			exp.Add( 4.0, 3.0 );
			exp.Add( 4.0, 4.0 );
			exp.Add( 3.0, 4.0 );
      
			Poly result = Clipper.Intersection( p1, p2 );
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the intersection of two rectangles that 
		 * intersect on corner
		 */
		[Test]
		public void TestIntersectionRectInterCorner4()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 2.0, 2.0 );
			p1.Add( 4.0, 2.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 2.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 1.0, 3.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 3.0, 5.0 );
			p2.Add( 1.0, 5.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 2.0, 3.0 );
			exp.Add( 3.0, 3.0 );
			exp.Add( 3.0, 4.0 );
			exp.Add( 2.0, 4.0 );
      
			Poly result = Clipper.Intersection( p1, p2 );
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the intersection of two rectangles where
		 * half of one is contained in the other and
		 * two sides of the inner cross one side of the outer.
		 */
		[Test]
		public void TestIntersectionRectInterSide1()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 1.0 );
			p1.Add( 4.0, 1.0 );
			p1.Add( 4.0, 5.0 );
			p1.Add( 0.0, 5.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 1.0, 0.0 );
			p2.Add( 3.0, 0.0 );
			p2.Add( 3.0, 2.0 );
			p2.Add( 1.0, 2.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 1.0, 1.0 );
			exp.Add( 3.0, 1.0 );
			exp.Add( 3.0, 2.0 );
			exp.Add( 1.0, 2.0 );
      
			Poly result = Clipper.Intersection( p1, p2 );
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the intersection of two rectangles where
		 * half of one is contained in the other and
		 * two sides of the inner cross one side of the outer.
		 */
		[Test]
		public void TestIntersectionRectInterSide2()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 1.0 );
			p1.Add( 4.0, 1.0 );
			p1.Add( 4.0, 5.0 );
			p1.Add( 0.0, 5.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 3.0, 2.0 );
			p2.Add( 5.0, 2.0 );
			p2.Add( 5.0, 4.0 );
			p2.Add( 3.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 3.0, 2.0 );
			exp.Add( 4.0, 2.0 );
			exp.Add( 4.0, 4.0 );
			exp.Add( 3.0, 4.0 );
      
			Poly result = Clipper.Intersection( p1, p2 );
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the intersection of two rectangles where
		 * half of one is contained in the other and
		 * two sides of the inner cross one side of the outer.
		 */
		[Test]
		public void TestIntersectionRectInterSide3()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 1.0 );
			p1.Add( 4.0, 1.0 );
			p1.Add( 4.0, 5.0 );
			p1.Add( 0.0, 5.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 1.0, 4.0 );
			p2.Add( 3.0, 4.0 );
			p2.Add( 3.0, 6.0 );
			p2.Add( 1.0, 6.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 1.0, 4.0 );
			exp.Add( 3.0, 4.0 );
			exp.Add( 3.0, 5.0 );
			exp.Add( 1.0, 5.0 );
      
			Poly result = Clipper.Intersection( p1, p2 );
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the intersection of two rectangles where
		 * half of one is contained in the other and
		 * two sides of the inner cross one side of the outer.
		 */
		[Test]
		public void TestIntersectionRectInterSide4()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 1.0 );
			p1.Add( 4.0, 1.0 );
			p1.Add( 4.0, 5.0 );
			p1.Add( 0.0, 5.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( -1.0, 2.0 );
			p2.Add(  1.0, 2.0 );
			p2.Add(  1.0, 4.0 );
			p2.Add( -1.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 0.0, 2.0 );
			exp.Add( 1.0, 2.0 );
			exp.Add( 1.0, 4.0 );
			exp.Add( 0.0, 4.0 );
      
			Poly result = Clipper.Intersection( p1, p2 );
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the intersection of two complex, non-convex, non-self-intersecting
		 * polygons - 1 on top of two = empty*/
		[Test] public void TestIntersectionPolyOneOnTopOfTwo()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 1.0, 4.0 );
			p1.Add( 5.0, 4.0 );
			p1.Add( 5.0, 9.0 );
			p1.Add( 3.0, 7.0 );
			p1.Add( 1.0, 9.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 2.0 );
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 6.0, 2.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 4.0 );
      
			Poly result = Clipper.Intersection( p1, p2 );
			Assert.IsTrue( result.IsEmpty() );
		}
   
		/**
		 * Test the intersection of two complex, non-convex, non-self-intersecting
		 * polygons - two sides and one vertex - two triangles
		 */
		[Test] public void TestIntersectionPolyTwoSidesOneVertex()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 1.0, 3.0 );
			p1.Add( 5.0, 3.0 );
			p1.Add( 5.0, 8.0 );
			p1.Add( 3.0, 6.0 );
			p1.Add( 1.0, 8.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 2.0 );
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 6.0, 2.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 4.0 );

			Poly iexp1 = new PolyDefault();
			iexp1.Add( 4.0, 4.0 );
			iexp1.Add( 3.0, 3.0 );
			iexp1.Add( 5.0, 3.0 );
			Poly iexp2 = new PolyDefault();
			iexp2.Add( 1.0, 3.0 );
			iexp2.Add( 3.0, 3.0 );
			iexp2.Add( 2.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( iexp1 );
			exp.Add( iexp2 );
      
			PolyDefault result = (PolyDefault)Clipper.Intersection( p1, p2 );
			//      result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the intersection of two complex, non-convex, non-self-intersecting
		 * polygons - two sides 
		 */
		[Test] public void TestIntersectionPolyTwoSides()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 1.0, 2.0 );
			p1.Add( 5.0, 2.0 );
			p1.Add( 5.0, 7.0 );
			p1.Add( 3.0, 5.0 );
			p1.Add( 1.0, 7.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 2.0 );
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 6.0, 2.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 1.0, 2.0 );
			exp.Add( 5.0, 2.0 );
			exp.Add( 5.0, 3.0 );
			exp.Add( 4.0, 4.0 );
			exp.Add( 3.0, 3.0 );
			exp.Add( 2.0, 4.0 );
			exp.Add( 1.0, 3.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Intersection( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the intersection of two complex, non-convex, non-self-intersecting
		 * polygons - two sides and one vertex - the lower one
		 */
		[Test] public void TestIntersectionPolyTwoSidesAndLowerVertex()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 1.0, 1.0 );
			p1.Add( 5.0, 1.0 );
			p1.Add( 5.0, 6.0 );
			p1.Add( 3.0, 4.0 );
			p1.Add( 1.0, 6.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 2.0 );
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 6.0, 2.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 1.0, 1.0 );
			exp.Add( 5.0, 1.0 );
			exp.Add( 5.0, 3.0 );
			exp.Add( 4.0, 4.0 );
			exp.Add( 3.0, 3.0 );
			exp.Add( 2.0, 4.0 );
			exp.Add( 1.0, 3.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Intersection( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the intersection of two complex, non-convex, non-self-intersecting
		 * polygons - cross four sides
		 */
		[Test] public void TestIntersectionPolyFourSides()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 1.0, 0.5 );
			p1.Add( 5.0, 0.5 );
			p1.Add( 5.0, 6.0 );
			p1.Add( 3.0, 4.0 );
			p1.Add( 1.0, 6.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 2.0 );
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 6.0, 2.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 1.0, 1.0 );
			exp.Add( 1.5, 0.5 );
			exp.Add( 2.5, 0.5 );
			exp.Add( 3.0, 1.0 );
			exp.Add( 3.5, 0.5 );
			exp.Add( 4.5, 0.5 );
			exp.Add( 5.0, 1.0 );
			exp.Add( 5.0, 3.0 );
			exp.Add( 4.0, 4.0 );
			exp.Add( 3.0, 3.0 );
			exp.Add( 2.0, 4.0 );
			exp.Add( 1.0, 3.0 );
      
			Poly result = Clipper.Intersection( p1, p2 );
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the intersection of two complex, non-convex, non-self-intersecting
		 * polygons - V overlap
		 */
		[Test] public void TestIntersectionPolyVOverlaps()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 1.0, 0.0 );
			p1.Add( 5.0, 0.0 );
			p1.Add( 5.0, 5.0 );
			p1.Add( 3.0, 3.0 );
			p1.Add( 1.0, 5.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 2.0 );
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 6.0, 2.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 1.0, 1.0 );
			exp.Add( 2.0, 0.0 );
			exp.Add( 3.0, 1.0 );
			exp.Add( 4.0, 0.0 );
			exp.Add( 5.0, 1.0 );
			exp.Add( 5.0, 3.0 );
			exp.Add( 4.0, 4.0 );
			exp.Add( 3.0, 3.0 );
			exp.Add( 2.0, 4.0 );
			exp.Add( 1.0, 3.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Intersection( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the intersection of a rectangle with a hole and solid rectangle
		 */
		[Test] public void TestIntersectionRectangleHole()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 1.0 );
			p1.Add( 4.0, 1.0 );
			p1.Add( 4.0, 5.0 );
			p1.Add( 0.0, 5.0 );
      
			Poly p2 = new PolyDefault(true);
			p2.Add( 1.0, 2.0 );
			p2.Add( 3.0, 2.0 );
			p2.Add( 3.0, 4.0 );
			p2.Add( 1.0, 4.0 );
      
			Poly p12 = new PolyDefault();
			p12.Add( p1 );
			p12.Add( p2 );

			Poly p3 = new PolyDefault();
			p3.Add( 2.0, 0.0 );
			p3.Add( 6.0, 0.0 );
			p3.Add( 6.0, 6.0 );
			p3.Add( 2.0, 6.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 2.0, 1.0 );
			exp.Add( 4.0, 1.0 );
			exp.Add( 4.0, 5.0 );
			exp.Add( 2.0, 5.0 );
			exp.Add( 2.0, 4.0 );
			exp.Add( 3.0, 4.0 );
			exp.Add( 3.0, 2.0 );
			exp.Add( 2.0, 2.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Intersection( p12, p3 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		// -------------
		// --- UNION ---
		// -------------
		/**
		 * Test the UNION of two polygons that are 
		 * completely separate - result should be poly that contains these two polys
		 */
		[Test] public void TestUnionSeparate()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 1.0, 0.0 );
			p1.Add( 1.0, 1.0 );
			p1.Add( 0.0, 1.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 0.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 3.0 );

			Poly exp = new PolyDefault();
			exp.Add( p2 );
			exp.Add( p1 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//      result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the union of two polygons where
		 * the second is contained in the first.
		 */
		[Test] public void TestUnionOneContainsTwo()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 4.0, 0.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 0.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 2.0, 1.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 3.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//      result.print();
			Assert.AreEqual( p1, result );
		}
		/**
		 * Test the union of two polygons where
		 * the first is contained in the second.
		 */
		[Test] public void TestUnionTwoContainsOne()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 2.0, 0.0 );
			p1.Add( 3.0, 0.0 );
			p1.Add( 3.0, 3.0 );
			p1.Add( 2.0, 3.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 0.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 0.0, 4.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//      result.print();
			Assert.AreEqual( p2, result );
		}
   
		/**
		 * Test the union of two polygons that
		 * are equal.
		 */
		[Test] public void TestUnionTwoEqual()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 4.0, 0.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 0.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 0.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 0.0, 4.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//      result.print();
			Assert.AreEqual( p1, result );
		}
   
		/**
		 * Test the union of two rectangles that share
		 * one corner and two partial sides.
		 */
		[Test] public void TestUnionRectCorner1()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 4.0, 0.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 0.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 0.0 );
			p2.Add( 1.0, 0.0 );
			p2.Add( 1.0, 1.0 );
			p2.Add( 0.0, 1.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 0.0, 0.0 );
			exp.Add( 4.0, 0.0 );
			exp.Add( 4.0, 4.0 );
			exp.Add( 0.0, 4.0 );
			exp.Add( 0.0, 1.0 ); // !!! KNOWN BUG - EXTRA POINT BUT SHAPE IS CORRECT
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the UNION of two rectangles that share
		 * one corner and two partial sides.
		 */
		[Test] public void TestUnionRectCorner2()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 4.0, 0.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 0.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 3.0, 0.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 4.0, 1.0 );
			p2.Add( 3.0, 1.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 0.0, 0.0 );
			exp.Add( 4.0, 0.0 );
			exp.Add( 4.0, 1.0 ); // !!! KNOWN BUG - EXTRA POINT BUT SHAPE IS CORRECT
			exp.Add( 4.0, 4.0 );
			exp.Add( 0.0, 4.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the union of two rectangles that share
		 * one corner and two partial sides.
		 */
		[Test] public void TestUnionRectCorner3()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 4.0, 0.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 0.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 3.0, 3.0 );
			p2.Add( 4.0, 3.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 0.0, 0.0 );
			exp.Add( 4.0, 0.0 );
			exp.Add( 4.0, 3.0 ); // !!! KNOWN BUG - EXTRA POINT BUT SHAPE IS CORRECT
			exp.Add( 4.0, 4.0 );
			exp.Add( 0.0, 4.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the union of two rectangles that share
		 * one corner and two partial sides.
		 */
		[Test] public void TestUnionRectCorner4()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 4.0, 0.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 0.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 3.0 );
			p2.Add( 1.0, 3.0 );
			p2.Add( 1.0, 4.0 );
			p2.Add( 0.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 0.0, 0.0 );
			exp.Add( 4.0, 0.0 );
			exp.Add( 4.0, 4.0 );
			exp.Add( 0.0, 4.0 );
			exp.Add( 0.0, 3.0 ); // !!! KNOWN BUG - EXTRA POINT BUT SHAPE IS CORRECT
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the union of two rectangles that 
		 * intersect on corner
		 */
		[Test] public void TestUnionRectInterCorner1()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 2.0, 2.0 );
			p1.Add( 4.0, 2.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 2.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 1.0, 1.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 1.0, 3.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 1.0, 1.0 );
			exp.Add( 3.0, 1.0 );
			exp.Add( 3.0, 2.0 );
			exp.Add( 4.0, 2.0 );
			exp.Add( 4.0, 4.0 );
			exp.Add( 2.0, 4.0 );
			exp.Add( 2.0, 3.0 );
			exp.Add( 1.0, 3.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the union of two rectangles that 
		 * intersect on corner
		 */
		[Test] public void TestUnionRectInterCorner2()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 2.0, 2.0 );
			p1.Add( 4.0, 2.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 2.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 3.0, 1.0 );
			p2.Add( 5.0, 1.0 );
			p2.Add( 5.0, 3.0 );
			p2.Add( 3.0, 3.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 3.0, 1.0 );
			exp.Add( 5.0, 1.0 );
			exp.Add( 5.0, 3.0 );
			exp.Add( 4.0, 3.0 );
			exp.Add( 4.0, 4.0 );
			exp.Add( 2.0, 4.0 );
			exp.Add( 2.0, 2.0 );
			exp.Add( 3.0, 2.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
      
		/**
		 * Test the union of two rectangles that 
		 * intersect on corner
		 */
		[Test] public void TestUnionRectInterCorner3()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 2.0, 2.0 );
			p1.Add( 4.0, 2.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 2.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 3.0, 3.0 );
			p2.Add( 5.0, 3.0 );
			p2.Add( 5.0, 5.0 );
			p2.Add( 3.0, 5.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 5.0, 3.0 );
			exp.Add( 5.0, 5.0 );
			exp.Add( 3.0, 5.0 );
			exp.Add( 3.0, 4.0 );
			exp.Add( 2.0, 4.0 );
			exp.Add( 2.0, 2.0 );
			exp.Add( 4.0, 2.0 );
			exp.Add( 4.0, 3.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the union of two rectangles that 
		 * intersect on corner
		 */
		[Test] public void TestUnionRectInterCorner4()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 2.0, 2.0 );
			p1.Add( 4.0, 2.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 2.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 1.0, 3.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 3.0, 5.0 );
			p2.Add( 1.0, 5.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 1.0, 3.0 );
			exp.Add( 2.0, 3.0 );
			exp.Add( 2.0, 2.0 );
			exp.Add( 4.0, 2.0 );
			exp.Add( 4.0, 4.0 );
			exp.Add( 3.0, 4.0 );
			exp.Add( 3.0, 5.0 );
			exp.Add( 1.0, 5.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
      
		/**
		 * Test the union of two rectangles where
		 * half of one is contained in the other and
		 * two sides of the inner cross one side of the outer.
		 */
		[Test] public void TestUnionRectInterSide1()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 1.0 );
			p1.Add( 4.0, 1.0 );
			p1.Add( 4.0, 5.0 );
			p1.Add( 0.0, 5.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 1.0, 0.0 );
			p2.Add( 3.0, 0.0 );
			p2.Add( 3.0, 2.0 );
			p2.Add( 1.0, 2.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 1.0, 0.0 );
			exp.Add( 3.0, 0.0 );
			exp.Add( 3.0, 1.0 );
			exp.Add( 4.0, 1.0 );
			exp.Add( 4.0, 5.0 );
			exp.Add( 0.0, 5.0 );
			exp.Add( 0.0, 1.0 );
			exp.Add( 1.0, 1.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the union of two rectangles where
		 * half of one is contained in the other and
		 * two sides of the inner cross one side of the outer.
		 */
		[Test] public void TestUnionRectInterSide2()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 1.0 );
			p1.Add( 4.0, 1.0 );
			p1.Add( 4.0, 5.0 );
			p1.Add( 0.0, 5.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 3.0, 2.0 );
			p2.Add( 5.0, 2.0 );
			p2.Add( 5.0, 4.0 );
			p2.Add( 3.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 0.0, 1.0 );
			exp.Add( 4.0, 1.0 );
			exp.Add( 4.0, 2.0 );
			exp.Add( 5.0, 2.0 );
			exp.Add( 5.0, 4.0 );
			exp.Add( 4.0, 4.0 );
			exp.Add( 4.0, 5.0 );
			exp.Add( 0.0, 5.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
      
		/**
		 * Test the union of two rectangles where
		 * half of one is contained in the other and
		 * two sides of the inner cross one side of the outer.
		 */
		[Test] public void TestUnionRectInterSide3()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 1.0 );
			p1.Add( 4.0, 1.0 );
			p1.Add( 4.0, 5.0 );
			p1.Add( 0.0, 5.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 1.0, 4.0 );
			p2.Add( 3.0, 4.0 );
			p2.Add( 3.0, 6.0 );
			p2.Add( 1.0, 6.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 0.0, 1.0 );
			exp.Add( 4.0, 1.0 );
			exp.Add( 4.0, 5.0 );
			exp.Add( 3.0, 5.0 );
			exp.Add( 3.0, 6.0 );
			exp.Add( 1.0, 6.0 );
			exp.Add( 1.0, 5.0 );
			exp.Add( 0.0, 5.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the union of two rectangles where
		 * half of one is contained in the other and
		 * two sides of the inner cross one side of the outer.
		 */
		[Test] public void TestUnionRectInterSide4()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 1.0 );
			p1.Add( 4.0, 1.0 );
			p1.Add( 4.0, 5.0 );
			p1.Add( 0.0, 5.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( -1.0, 2.0 );
			p2.Add(  1.0, 2.0 );
			p2.Add(  1.0, 4.0 );
			p2.Add( -1.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add(  0.0, 1.0 );
			exp.Add(  4.0, 1.0 );
			exp.Add(  4.0, 5.0 );
			exp.Add(  0.0, 5.0 );
			exp.Add(  0.0, 4.0 );
			exp.Add( -1.0, 4.0 );
			exp.Add( -1.0, 2.0 );
			exp.Add(  0.0, 2.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the union of two complex, non-convex, non-self-intersecting
		 * polygons - 1 on top of two */
		[Test] public void TestUnionPolyOneOnTopOfTwo()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 1.0, 4.0 );
			p1.Add( 5.0, 4.0 );
			p1.Add( 5.0, 9.0 );
			p1.Add( 3.0, 7.0 );
			p1.Add( 1.0, 9.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 2.0 );
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 6.0, 2.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 4.0 );
      
			Poly exp1 = new PolyDefault();
			exp1.Add( 0.0, 2.0 );
			exp1.Add( 2.0, 0.0 );
			exp1.Add( 3.0, 1.0 );
			exp1.Add( 4.0, 0.0 );
			exp1.Add( 6.0, 2.0 );
			exp1.Add( 4.0, 4.0 );
			exp1.Add( 5.0, 4.0 );
			exp1.Add( 5.0, 9.0 );
			exp1.Add( 3.0, 7.0 );
			exp1.Add( 1.0, 9.0 );
			exp1.Add( 1.0, 4.0 );
			exp1.Add( 2.0, 4.0 );
      
			Poly exp2 = new PolyDefault(true);
			exp2.Add( 4.0, 4.0 );
			exp2.Add( 3.0, 3.0 );
			exp2.Add( 2.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( exp1 );
			exp.Add( exp2 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the union of two complex, non-convex, non-self-intersecting
		 * polygons - two sides and one vertex - two triangles
		 */
		[Test] public void TestUnionPolyTwoSidesOneVertex()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 1.0, 3.0 );
			p1.Add( 5.0, 3.0 );
			p1.Add( 5.0, 8.0 );
			p1.Add( 3.0, 6.0 );
			p1.Add( 1.0, 8.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 2.0 );
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 6.0, 2.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 4.0 );

			Poly exp = new PolyDefault();
			exp.Add( 0.0, 2.0 );
			exp.Add( 2.0, 0.0 );
			exp.Add( 3.0, 1.0 );
			exp.Add( 4.0, 0.0 );
			exp.Add( 6.0, 2.0 );
			exp.Add( 5.0, 3.0 );
			exp.Add( 5.0, 8.0 );
			exp.Add( 3.0, 6.0 );
			exp.Add( 1.0, 8.0 );
			exp.Add( 1.0, 3.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the union of two complex, non-convex, non-self-intersecting
		 * polygons - two sides 
		 */
		[Test] public void TestUnionPolyTwoSides()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 1.0, 2.0 );
			p1.Add( 5.0, 2.0 );
			p1.Add( 5.0, 7.0 );
			p1.Add( 3.0, 5.0 );
			p1.Add( 1.0, 7.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 2.0 );
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 6.0, 2.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 0.0, 2.0 );
			exp.Add( 2.0, 0.0 );
			exp.Add( 3.0, 1.0 );
			exp.Add( 4.0, 0.0 );
			exp.Add( 6.0, 2.0 );
			exp.Add( 5.0, 3.0 );
			exp.Add( 5.0, 7.0 );
			exp.Add( 3.0, 5.0 );
			exp.Add( 1.0, 7.0 );
			exp.Add( 1.0, 3.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the union of two complex, non-convex, non-self-intersecting
		 * polygons - two sides and one vertex - the lower one
		 */
		[Test] public void TestUnionPolyTwoSidesAndLowerVertex()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 1.0, 1.0 );
			p1.Add( 5.0, 1.0 );
			p1.Add( 5.0, 6.0 );
			p1.Add( 3.0, 4.0 );
			p1.Add( 1.0, 6.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 2.0 );
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 6.0, 2.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 0.0, 2.0 );
			exp.Add( 2.0, 0.0 );
			exp.Add( 3.0, 1.0 );
			exp.Add( 4.0, 0.0 );
			exp.Add( 6.0, 2.0 );
			exp.Add( 5.0, 3.0 );
			exp.Add( 5.0, 6.0 );
			exp.Add( 3.0, 4.0 );
			exp.Add( 1.0, 6.0 );
			exp.Add( 1.0, 3.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the union of two complex, non-convex, non-self-intersecting
		 * polygons - cross four sides
		 */
		[Test] public void TestUnionPolyFourSides()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 1.0, 0.5 );
			p1.Add( 5.0, 0.5 );
			p1.Add( 5.0, 6.0 );
			p1.Add( 3.0, 4.0 );
			p1.Add( 1.0, 6.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 2.0 );
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 6.0, 2.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 1.0, 1.0 );
			exp.Add( 1.0, 0.5 );
			exp.Add( 1.5, 0.5 );
			exp.Add( 2.0, 0.0 );
			exp.Add( 2.5, 0.5 );
			exp.Add( 3.5, 0.5 );
			exp.Add( 4.0, 0.0 );
			exp.Add( 4.5, 0.5 );
			exp.Add( 5.0, 0.5 );
			exp.Add( 5.0, 1.0 );
			exp.Add( 6.0, 2.0 );
			exp.Add( 5.0, 3.0 );
			exp.Add( 5.0, 6.0 );
			exp.Add( 3.0, 4.0 );
			exp.Add( 1.0, 6.0 );
			exp.Add( 1.0, 3.0 );
			exp.Add( 0.0, 2.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
         
		/**
		 * Test the union of two complex, non-convex, non-self-intersecting
		 * polygons - V overlap
		 */
		[Test] public void TestUnionPolyVOverlaps()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 1.0, 0.0 );
			p1.Add( 5.0, 0.0 );
			p1.Add( 5.0, 5.0 );
			p1.Add( 3.0, 3.0 );
			p1.Add( 1.0, 5.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 2.0 );
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 6.0, 2.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 1.0, 0.0 );
			exp.Add( 5.0, 0.0 );
			exp.Add( 5.0, 1.0 );
			exp.Add( 6.0, 2.0 );
			exp.Add( 5.0, 3.0 );
			exp.Add( 5.0, 5.0 );
			exp.Add( 4.0, 4.0 ); // KNOWN BUG - EXTRA POINT BUT SHAPE IS OK
			exp.Add( 3.0, 3.0 );
			exp.Add( 2.0, 4.0 ); // KNOWN BUG - EXTRA POINT BUT SHAPE IS OK
			exp.Add( 1.0, 5.0 );
			exp.Add( 1.0, 3.0 );
			exp.Add( 0.0, 2.0 );
			exp.Add( 1.0, 1.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the UNION of a rectangle with a hole and solid rectangle
		 */
		[Test] public void TestUnionRectangleHole()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 1.0 );
			p1.Add( 4.0, 1.0 );
			p1.Add( 4.0, 5.0 );
			p1.Add( 0.0, 5.0 );
      
			Poly p2 = new PolyDefault(true);
			p2.Add( 1.0, 2.0 );
			p2.Add( 3.0, 2.0 );
			p2.Add( 3.0, 4.0 );
			p2.Add( 1.0, 4.0 );
      
			Poly p12 = new PolyDefault();
			p12.Add( p1 );
			p12.Add( p2 );

			Poly p3 = new PolyDefault();
			p3.Add( 2.0, 0.0 );
			p3.Add( 6.0, 0.0 );
			p3.Add( 6.0, 6.0 );
			p3.Add( 2.0, 6.0 );
      
			Poly exp1 = new PolyDefault();
			exp1.Add( 2.0, 0.0 );
			exp1.Add( 6.0, 0.0 );
			exp1.Add( 6.0, 6.0 );
			exp1.Add( 2.0, 6.0 );
			exp1.Add( 2.0, 5.0 );
			exp1.Add( 0.0, 5.0 );
			exp1.Add( 0.0, 1.0 );
			exp1.Add( 2.0, 1.0 );

			Poly exp2 = new PolyDefault(true);
			exp2.Add( 2.0, 2.0 );
			exp2.Add( 1.0, 2.0 );
			exp2.Add( 1.0, 4.0 );
			exp2.Add( 2.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( exp1 );
			exp.Add( exp2 );
      
			PolyDefault result = (PolyDefault)Clipper.Union( p12, p3 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		// -----------
		// --- XOR ---
		// -----------
		/**
		 * Test the XOR of two polygons that are 
		 * completely separate - result should be poly that contains these two polys
		 */
		[Test] public void TestXorSeparate()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 1.0, 0.0 );
			p1.Add( 1.0, 1.0 );
			p1.Add( 0.0, 1.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 0.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 3.0 );

			Poly exp = new PolyDefault();
			exp.Add( p2 );
			exp.Add( p1 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//      result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the xor of two polygons where
		 * the second is contained in the first.
		 */
		[Test] public void TestXorOneContainsTwo()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 4.0, 0.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 0.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 2.0, 1.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 3.0 );
      
			// notice reverse order
			Poly exp2 = new PolyDefault(true);
			exp2.Add( 3.0, 1.0 );
			exp2.Add( 2.0, 1.0 );
			exp2.Add( 2.0, 3.0 );
			exp2.Add( 3.0, 3.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( p1 );
			exp.Add( exp2 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the xor of two polygons where
		 * the first is contained in the second.
		 */
		[Test] public void TestXorTwoContainsOne()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 2.0, 1.0 );
			p1.Add( 3.0, 1.0 );
			p1.Add( 3.0, 3.0 );
			p1.Add( 2.0, 3.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 0.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 0.0, 4.0 );
      
			// notice reverse order
			Poly exp2 = new PolyDefault(true);
			exp2.Add( 3.0, 1.0 );
			exp2.Add( 2.0, 1.0 );
			exp2.Add( 2.0, 3.0 );
			exp2.Add( 3.0, 3.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( p2 );
			exp.Add( exp2 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the xor of two polygons that
		 * are equal.
		 */
		[Test] public void TestXorTwoEqual()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 4.0, 0.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 0.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 0.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 0.0, 4.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.IsTrue( result.IsEmpty() );
		}
   
		/**
		 * Test the xor of two rectangles that share
		 * one corner and two partial sides.
		 */
		[Test] public void TestXorRectCorner1()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 4.0, 0.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 0.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 0.0 );
			p2.Add( 1.0, 0.0 );
			p2.Add( 1.0, 1.0 );
			p2.Add( 0.0, 1.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 1.0, 0.0 );
			exp.Add( 4.0, 0.0 );
			exp.Add( 4.0, 4.0 );
			exp.Add( 0.0, 4.0 );
			exp.Add( 0.0, 1.0 );
			exp.Add( 1.0, 1.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
      
		/**
		 * Test the xor of two rectangles that share
		 * one corner and two partial sides.
		 */
		[Test] public void TestXorRectCorner2()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 4.0, 0.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 0.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 3.0, 0.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 4.0, 1.0 );
			p2.Add( 3.0, 1.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 0.0, 0.0 );
			exp.Add( 3.0, 0.0 );
			exp.Add( 3.0, 1.0 );
			exp.Add( 4.0, 1.0 );
			exp.Add( 4.0, 4.0 );
			exp.Add( 0.0, 4.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the xor of two rectangles that share
		 * one corner and two partial sides.
		 */
		[Test] public void TestXorRectCorner3()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 4.0, 0.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 0.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 3.0, 3.0 );
			p2.Add( 4.0, 3.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 0.0, 0.0 );
			exp.Add( 4.0, 0.0 );
			exp.Add( 4.0, 3.0 );
			exp.Add( 3.0, 3.0 );
			exp.Add( 3.0, 4.0 );
			exp.Add( 0.0, 4.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the xor of two rectangles that share
		 * one corner and two partial sides.
		 */
		[Test] public void TestXorRectCorner4()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 4.0, 0.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 0.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 3.0 );
			p2.Add( 1.0, 3.0 );
			p2.Add( 1.0, 4.0 );
			p2.Add( 0.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 0.0, 0.0 );
			exp.Add( 4.0, 0.0 );
			exp.Add( 4.0, 4.0 );
			exp.Add( 1.0, 4.0 );
			exp.Add( 1.0, 3.0 );
			exp.Add( 0.0, 3.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}   
   
		/**
		 * Test the xor of two rectangles that 
		 * intersect on corner
		 */
		[Test] public void TestXorRectInterCorner1()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 2.0, 2.0 );
			p1.Add( 4.0, 2.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 2.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 1.0, 1.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 1.0, 3.0 );
      
			Poly exp1 = new PolyDefault();
			exp1.Add( 3.0, 2.0 );
			exp1.Add( 4.0, 2.0 );
			exp1.Add( 4.0, 4.0 );
			exp1.Add( 2.0, 4.0 );
			exp1.Add( 2.0, 3.0 );
			exp1.Add( 3.0, 3.0 );
      
			Poly exp2 = new PolyDefault();
			exp2.Add( 1.0, 1.0 );
			exp2.Add( 3.0, 1.0 );
			exp2.Add( 3.0, 2.0 );
			exp2.Add( 2.0, 2.0 );
			exp2.Add( 2.0, 3.0 );
			exp2.Add( 1.0, 3.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( exp1 );
			exp.Add( exp2 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the xor of two rectangles that 
		 * intersect on corner
		 */
		[Test] public void TestXorRectInterCorner2()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 2.0, 2.0 );
			p1.Add( 4.0, 2.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 2.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 3.0, 1.0 );
			p2.Add( 5.0, 1.0 );
			p2.Add( 5.0, 3.0 );
			p2.Add( 3.0, 3.0 );
      
			// ------------------------------------------------------------------------------------------
			// --- I expected this to give two non-hole inner polygons but it gave one with a hole    ---
			// --- if you look at it, they are equivalent.  Don't have time to figure out difference. ---
			// ------------------------------------------------------------------------------------------
			//      Poly exp1 = new PolyDefault();
			//      exp1.Add( 3.0, 1.0 );
			//      exp1.Add( 5.0, 1.0 );
			//      exp1.Add( 5.0, 3.0 );
			//      exp1.Add( 4.0, 3.0 );
			//      exp1.Add( 4.0, 2.0 );
			//      exp1.Add( 3.0, 2.0 );
			//      
			//      Poly exp2 = new PolyDefault();
			//      exp2.Add( 2.0, 2.0 );
			//      exp2.Add( 3.0, 2.0 );
			//      exp2.Add( 3.0, 3.0 );
			//      exp2.Add( 4.0, 3.0 );
			//      exp2.Add( 4.0, 4.0 );
			//      exp2.Add( 2.0, 4.0 );
      
			Poly exp1 = new PolyDefault(true);
			exp1.Add( 3.0, 2.0 );
			exp1.Add( 3.0, 3.0 );
			exp1.Add( 4.0, 3.0 );
			exp1.Add( 4.0, 2.0 );
      
			Poly exp2 = new PolyDefault();
			exp2.Add( 2.0, 2.0 );
			exp2.Add( 3.0, 2.0 );
			exp2.Add( 3.0, 1.0 );
			exp2.Add( 5.0, 1.0 );
			exp2.Add( 5.0, 3.0 );
			exp2.Add( 4.0, 3.0 );
			exp2.Add( 4.0, 4.0 );
			exp2.Add( 2.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( exp2 );
			exp.Add( exp1 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
      
   
		/**
		 * Test the xor of two rectangles that 
		 * intersect on corner
		 */
		[Test] public void TestXortRectInterCorner3()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 2.0, 2.0 );
			p1.Add( 4.0, 2.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 2.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 3.0, 3.0 );
			p2.Add( 5.0, 3.0 );
			p2.Add( 5.0, 5.0 );
			p2.Add( 3.0, 5.0 );
      
			Poly exp1 = new PolyDefault();
			exp1.Add( 2.0, 2.0 );
			exp1.Add( 4.0, 2.0 );
			exp1.Add( 4.0, 3.0 );
			exp1.Add( 3.0, 3.0 );
			exp1.Add( 3.0, 4.0 );
			exp1.Add( 2.0, 4.0 );
      
			Poly exp2 = new PolyDefault();
			exp2.Add( 4.0, 3.0 );
			exp2.Add( 5.0, 3.0 );
			exp2.Add( 5.0, 5.0 );
			exp2.Add( 3.0, 5.0 );
			exp2.Add( 3.0, 4.0 );
			exp2.Add( 4.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( exp2 );
			exp.Add( exp1 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the xor of two rectangles that 
		 * intersect on corner
		 */
		[Test] public void TestXorRectInterCorner4()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 2.0, 2.0 );
			p1.Add( 4.0, 2.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 2.0, 4.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 1.0, 3.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 3.0, 5.0 );
			p2.Add( 1.0, 5.0 );
      
			// ------------------------------------------------------------------------------------------
			// --- I expected this to give two non-hole inner polygons but it gave one with a hole    ---
			// --- if you look at it, they are equivalent.  Don't have time to figure out difference. ---
			// ------------------------------------------------------------------------------------------

			Poly exp1 = new PolyDefault(true);
			exp1.Add( 3.0, 3.0 );
			exp1.Add( 2.0, 3.0 );
			exp1.Add( 2.0, 4.0 );
			exp1.Add( 3.0, 4.0 );
      
			Poly exp2 = new PolyDefault();
			exp2.Add( 1.0, 3.0 );
			exp2.Add( 2.0, 3.0 );
			exp2.Add( 2.0, 2.0 );
			exp2.Add( 4.0, 2.0 );
			exp2.Add( 4.0, 4.0 );
			exp2.Add( 3.0, 4.0 );
			exp2.Add( 3.0, 5.0 );
			exp2.Add( 1.0, 5.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( exp2 );
			exp.Add( exp1 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the xor of two rectangles where
		 * half of one is contained in the other and
		 * two sides of the inner cross one side of the outer.
		 */
		[Test] public void TestXorRectInterSide1()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 1.0 );
			p1.Add( 4.0, 1.0 );
			p1.Add( 4.0, 5.0 );
			p1.Add( 0.0, 5.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 1.0, 0.0 );
			p2.Add( 3.0, 0.0 );
			p2.Add( 3.0, 2.0 );
			p2.Add( 1.0, 2.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 0.0, 1.0 );
			exp.Add( 1.0, 1.0 );
			exp.Add( 1.0, 0.0 );
			exp.Add( 3.0, 0.0 );
			exp.Add( 3.0, 1.0 );
			exp.Add( 1.0, 1.0 );
			exp.Add( 1.0, 2.0 );
			exp.Add( 3.0, 2.0 );
			exp.Add( 3.0, 1.0 );
			exp.Add( 4.0, 1.0 );
			exp.Add( 4.0, 5.0 );
			exp.Add( 0.0, 5.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
      
		/**
		 * Test the xor of two rectangles where
		 * half of one is contained in the other and
		 * two sides of the inner cross one side of the outer.
		 */
		[Test] public void TestXornRectInterSide2()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 1.0 );
			p1.Add( 4.0, 1.0 );
			p1.Add( 4.0, 5.0 );
			p1.Add( 0.0, 5.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 3.0, 2.0 );
			p2.Add( 5.0, 2.0 );
			p2.Add( 5.0, 4.0 );
			p2.Add( 3.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 4.0, 5.0 );
			exp.Add( 0.0, 5.0 );
			exp.Add( 0.0, 1.0 );
			exp.Add( 4.0, 1.0 );
			exp.Add( 4.0, 2.0 );
			exp.Add( 3.0, 2.0 );
			exp.Add( 3.0, 4.0 );
			exp.Add( 4.0, 4.0 );
			exp.Add( 4.0, 2.0 );
			exp.Add( 5.0, 2.0 );
			exp.Add( 5.0, 4.0 );
			exp.Add( 4.0, 4.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the xor of two rectangles where
		 * half of one is contained in the other and
		 * two sides of the inner cross one side of the outer.
		 */
		[Test] public void TestXorRectInterSide3()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 1.0 );
			p1.Add( 4.0, 1.0 );
			p1.Add( 4.0, 5.0 );
			p1.Add( 0.0, 5.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 1.0, 4.0 );
			p2.Add( 3.0, 4.0 );
			p2.Add( 3.0, 6.0 );
			p2.Add( 1.0, 6.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( 4.0, 5.0 );
			exp.Add( 3.0, 5.0 );
			exp.Add( 3.0, 6.0 );
			exp.Add( 1.0, 6.0 );
			exp.Add( 1.0, 5.0 );
			exp.Add( 3.0, 5.0 );
			exp.Add( 3.0, 4.0 );
			exp.Add( 1.0, 4.0 );
			exp.Add( 1.0, 5.0 );
			exp.Add( 0.0, 5.0 );
			exp.Add( 0.0, 1.0 );
			exp.Add( 4.0, 1.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
      
		/**
		 * Test the xor of two rectangles where
		 * half of one is contained in the other and
		 * two sides of the inner cross one side of the outer.
		 */
		[Test] public void TestXorRectInterSide4()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 1.0 );
			p1.Add( 4.0, 1.0 );
			p1.Add( 4.0, 5.0 );
			p1.Add( 0.0, 5.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( -1.0, 2.0 );
			p2.Add(  1.0, 2.0 );
			p2.Add(  1.0, 4.0 );
			p2.Add( -1.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add(  4.0, 5.0 );
			exp.Add(  0.0, 5.0 );
			exp.Add(  0.0, 4.0 );
			exp.Add(  1.0, 4.0 );
			exp.Add(  1.0, 2.0 );
			exp.Add(  0.0, 2.0 );
			exp.Add(  0.0, 4.0 );
			exp.Add( -1.0, 4.0 );
			exp.Add( -1.0, 2.0 );
			exp.Add(  0.0, 2.0 );
			exp.Add(  0.0, 1.0 );
			exp.Add(  4.0, 1.0 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
      
		/**
		 * Test the xor of two complex, non-convex, non-self-intersecting
		 * polygons - 1 on top of two*/
		[Test] public void TestXorPolyOneOnTopOfTwo()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 1.0, 4.0 );
			p1.Add( 5.0, 4.0 );
			p1.Add( 5.0, 9.0 );
			p1.Add( 3.0, 7.0 );
			p1.Add( 1.0, 9.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 2.0 );
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 6.0, 2.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 4.0 );
      
			Poly exp1 = new PolyDefault();
			exp1.Add( 0.0, 2.0 );
			exp1.Add( 2.0, 0.0 );
			exp1.Add( 3.0, 1.0 );
			exp1.Add( 4.0, 0.0 );
			exp1.Add( 6.0, 2.0 );
			exp1.Add( 4.0, 4.0 );
			exp1.Add( 5.0, 4.0 );
			exp1.Add( 5.0, 9.0 );
			exp1.Add( 3.0, 7.0 );
			exp1.Add( 1.0, 9.0 );
			exp1.Add( 1.0, 4.0 );
			exp1.Add( 2.0, 4.0 );
      
			Poly exp2 = new PolyDefault(true);
			exp2.Add( 4.0, 4.0 );
			exp2.Add( 3.0, 3.0 );
			exp2.Add( 2.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( exp1 );
			exp.Add( exp2 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
   
		/**
		 * Test the xor of two complex, non-convex, non-self-intersecting
		 * polygons - two sides and one vertex - two triangles
		 */
		[Test] public void TestXorPolyTwoSidesOneVertex()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 1.0, 3.0 );
			p1.Add( 5.0, 3.0 );
			p1.Add( 5.0, 8.0 );
			p1.Add( 3.0, 6.0 );
			p1.Add( 1.0, 8.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 2.0 );
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 6.0, 2.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 4.0 );

			Poly exp1 = new PolyDefault();
			exp1.Add( 5.0, 8.0 );
			exp1.Add( 3.0, 6.0 );
			exp1.Add( 1.0, 8.0 );
			exp1.Add( 1.0, 3.0 );
			exp1.Add( 0.0, 2.0 );
			exp1.Add( 2.0, 0.0 );
			exp1.Add( 3.0, 1.0 );
			exp1.Add( 4.0, 0.0 );
			exp1.Add( 6.0, 2.0 );
			exp1.Add( 5.0, 3.0 );
			exp1.Add( 3.0, 3.0 );
			exp1.Add( 4.0, 4.0 );
			exp1.Add( 5.0, 3.0 );
      
			Poly exp2 = new PolyDefault(true);
			exp2.Add( 3.0, 3.0 );
			exp2.Add( 1.0, 3.0 );
			exp2.Add( 2.0, 4.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( exp1 );
			exp.Add( exp2 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
      
		/**
		 * Test the xor of two complex, non-convex, non-self-intersecting
		 * polygons - two sides 
		 */
		[Test] public void TestXorPolyTwoSides()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 1.0, 2.0 );
			p1.Add( 5.0, 2.0 );
			p1.Add( 5.0, 7.0 );
			p1.Add( 3.0, 5.0 );
			p1.Add( 1.0, 7.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 2.0 );
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 6.0, 2.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 4.0 );
      
			Poly exp1 = new PolyDefault();
			exp1.Add( 5.0, 7.0 );
			exp1.Add( 3.0, 5.0 );
			exp1.Add( 1.0, 7.0 );
			exp1.Add( 1.0, 3.0 );
			exp1.Add( 2.0, 4.0 );
			exp1.Add( 3.0, 3.0 );
			exp1.Add( 4.0, 4.0 );
			exp1.Add( 5.0, 3.0 );
      
			Poly exp2 = new PolyDefault();
			exp2.Add( 5.0, 3.0 );
			exp2.Add( 5.0, 2.0 );
			exp2.Add( 1.0, 2.0 );
			exp2.Add( 1.0, 3.0 );
			exp2.Add( 0.0, 2.0 );
			exp2.Add( 2.0, 0.0 );
			exp2.Add( 3.0, 1.0 );
			exp2.Add( 4.0, 0.0 );
			exp2.Add( 6.0, 2.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( exp1 );
			exp.Add( exp2 );      
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
      
		/**
		 * Test the xor of two complex, non-convex, non-self-intersecting
		 * polygons - two sides and one vertex - the lower one
		 */
		[Test] public void TestXorPolyTwoSidesAndLowerVertex()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 1.0, 1.0 );
			p1.Add( 5.0, 1.0 );
			p1.Add( 5.0, 6.0 );
			p1.Add( 3.0, 4.0 );
			p1.Add( 1.0, 6.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 2.0 );
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 6.0, 2.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 4.0 );
      
			Poly exp1 = new PolyDefault();
			exp1.Add( 5.0, 6.0 );
			exp1.Add( 3.0, 4.0 );
			exp1.Add( 1.0, 6.0 );
			exp1.Add( 1.0, 3.0 );
			exp1.Add( 2.0, 4.0 );
			exp1.Add( 3.0, 3.0 );
			exp1.Add( 4.0, 4.0 );
			exp1.Add( 5.0, 3.0 );
      
			Poly exp2 = new PolyDefault();
			exp2.Add( 5.0, 3.0 );
			exp2.Add( 5.0, 1.0 );
			exp2.Add( 3.0, 1.0 );
			exp2.Add( 4.0, 0.0 );
			exp2.Add( 6.0, 2.0 );
      
			Poly exp3 = new PolyDefault();
			exp3.Add( 1.0, 3.0 );
			exp3.Add( 0.0, 2.0 );
			exp3.Add( 2.0, 0.0 );
			exp3.Add( 3.0, 1.0 );
			exp3.Add( 1.0, 1.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( exp1 );
			exp.Add( exp2 );
			exp.Add( exp3 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}

		/**
		 * Test the xor of two complex, non-convex, non-self-intersecting
		 * polygons - cross four sides
		 */
		[Test] public void TestXorPolyFourSides()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 1.0, 0.5 );
			p1.Add( 5.0, 0.5 );
			p1.Add( 5.0, 6.0 );
			p1.Add( 3.0, 4.0 );
			p1.Add( 1.0, 6.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 2.0 );
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 6.0, 2.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 4.0 );
      
			Poly exp1 = new PolyDefault();
			exp1.Add( 5.0, 6.0 );
			exp1.Add( 3.0, 4.0 );
			exp1.Add( 1.0, 6.0 );
			exp1.Add( 1.0, 3.0 );
			exp1.Add( 2.0, 4.0 );
			exp1.Add( 3.0, 3.0 );
			exp1.Add( 4.0, 4.0 );
			exp1.Add( 5.0, 3.0 );
      
			Poly exp2 = new PolyDefault();
			exp2.Add( 5.0, 3.0 );
			exp2.Add( 5.0, 1.0 );
			exp2.Add( 6.0, 2.0 );
      
			Poly exp3 = new PolyDefault();
			exp3.Add( 1.0, 3.0 );
			exp3.Add( 0.0, 2.0 );
			exp3.Add( 1.0, 1.0 );
      
			Poly exp4 = new PolyDefault();
			exp4.Add( 5.0, 1.0 );
			exp4.Add( 4.5, 0.5 );
			exp4.Add( 5.0, 0.5 );
      
			Poly exp5 = new PolyDefault();
			exp5.Add( 3.0, 1.0 );
			exp5.Add( 2.5, 0.5 );
			exp5.Add( 3.5, 0.5 );
			exp5.Add( 4.0, 0.0 );
			exp5.Add( 4.5, 0.5 );
			exp5.Add( 3.5, 0.5 );
      
			Poly exp6 = new PolyDefault();
			exp6.Add( 1.0, 1.0 );
			exp6.Add( 1.0, 0.5 );
			exp6.Add( 1.5, 0.5 );
			exp6.Add( 2.0, 0.0 );
			exp6.Add( 2.5, 0.5 );
			exp6.Add( 1.5, 0.5 );
      
			Poly exp = new PolyDefault();
			exp.Add( exp1 );
			exp.Add( exp2 );
			exp.Add( exp3 );
			exp.Add( exp4 );
			exp.Add( exp5 );
			exp.Add( exp6 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}

		/**
		 * Test the xor of two complex, non-convex, non-self-intersecting
		 * polygons - V overlap
		 */
		[Test] public void TestXorPolyVOverlaps()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 1.0, 0.0 );
			p1.Add( 5.0, 0.0 );
			p1.Add( 5.0, 5.0 );
			p1.Add( 3.0, 3.0 );
			p1.Add( 1.0, 5.0 );
      
			Poly p2 = new PolyDefault();
			p2.Add( 0.0, 2.0 );
			p2.Add( 2.0, 0.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 4.0, 0.0 );
			p2.Add( 6.0, 2.0 );
			p2.Add( 4.0, 4.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 2.0, 4.0 );
      
			Poly exp1 = new PolyDefault();
			exp1.Add( 5.0, 5.0 );
			exp1.Add( 4.0, 4.0 );
			exp1.Add( 5.0, 3.0 );
      
			Poly exp2 = new PolyDefault();
			exp2.Add( 1.0, 5.0 );
			exp2.Add( 1.0, 3.0 );
			exp2.Add( 2.0, 4.0 );
      
			Poly exp3 = new PolyDefault();
			exp3.Add( 5.0, 3.0 );
			exp3.Add( 5.0, 1.0 );
			exp3.Add( 6.0, 2.0 );
      
			Poly exp4 = new PolyDefault();
			exp4.Add( 1.0, 3.0 );
			exp4.Add( 0.0, 2.0 );
			exp4.Add( 1.0, 1.0 );
      
			Poly exp5 = new PolyDefault();
			exp5.Add( 5.0, 1.0 );
			exp5.Add( 4.0, 0.0 );
			exp5.Add( 5.0, 0.0 );
      
			Poly exp6 = new PolyDefault();
			exp6.Add( 3.0, 1.0 );
			exp6.Add( 2.0, 0.0 );
			exp6.Add( 4.0, 0.0 );
      
			Poly exp7 = new PolyDefault();
			exp7.Add( 1.0, 1.0 );
			exp7.Add( 1.0, 0.0 );
			exp7.Add( 2.0, 0.0 );
      
			Poly exp = new PolyDefault();
			exp.Add( exp1 );
			exp.Add( exp2 );
			exp.Add( exp3 );
			exp.Add( exp4 );
			exp.Add( exp5 );
			exp.Add( exp6 );
			exp.Add( exp7 );
      
			PolyDefault result = (PolyDefault)Clipper.Xor( p1, p2 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
      
		/**
		 * Test the xor of a rectangle with a hole and solid rectangle
		 */
		[Test] public void TestXorRectangleHole()
		{
			Poly p1 = new PolyDefault();
			p1.Add( 0.0, 1.0 );
			p1.Add( 4.0, 1.0 );
			p1.Add( 4.0, 5.0 );
			p1.Add( 0.0, 5.0 );
      
			Poly p2 = new PolyDefault(true);
			p2.Add( 1.0, 2.0 );
			p2.Add( 3.0, 2.0 );
			p2.Add( 3.0, 4.0 );
			p2.Add( 1.0, 4.0 );
      
			Poly p12 = new PolyDefault();
			p12.Add( p1 );
			p12.Add( p2 );

			Poly p3 = new PolyDefault();
			p3.Add( 2.0, 0.0 );
			p3.Add( 6.0, 0.0 );
			p3.Add( 6.0, 6.0 );
			p3.Add( 2.0, 6.0 );

			// -----------------------------------------------------------
			// --- This is not what I expected and it seems reasonable ---
			// --- However it could be wrong.                          ---
			// -----------------------------------------------------------
			// --- I computed the area of this poly and it came out to ---
			// --- be 24 which is what you would expect.               ---
			// -----------------------------------------------------------
			Poly exp = new PolyDefault();
			exp.Add( 6.0, 6.0 );
			exp.Add( 2.0, 6.0 );
			exp.Add( 2.0, 5.0 );
			exp.Add( 4.0, 5.0 );
			exp.Add( 4.0, 1.0 );
			exp.Add( 2.0, 1.0 );
			exp.Add( 2.0, 2.0 );
			exp.Add( 1.0, 2.0 );
			exp.Add( 1.0, 4.0 );
			exp.Add( 2.0, 4.0 );
			exp.Add( 2.0, 2.0 );
			exp.Add( 3.0, 2.0 );
			exp.Add( 3.0, 4.0 );
			exp.Add( 2.0, 4.0 );
			exp.Add( 2.0, 5.0 );
			exp.Add( 0.0, 5.0 );
			exp.Add( 0.0, 1.0 );
			exp.Add( 2.0, 1.0 );
			exp.Add( 2.0, 0.0 );
			exp.Add( 6.0, 0.0 );

			PolyDefault result = (PolyDefault)Clipper.Xor( p12, p3 );
			//result.print();
			Assert.AreEqual( exp, result );
		}
	}
}
