#if DEBUG
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
	 * <code>PolyDefaultTest</code> is a set of unit tests using JUnit for the
	 * class <code>PolyDefalt</code>.
	 *
	 * @author  Dan Bridenbecker, Solution Engineering, Inc.
	 */
	[TestFixture]
	public class PolyDefaultTest
	{
		// -------------
		// --- Tests ---
		// -------------
		/**
		 * Test the equality operator when empty
		 */
		[Test] public void testEqualEmpty()
		{
			IPoly p0a = new PolyDefault();
			IPoly p0b = new PolyDefault();
      
			Assert.IsTrue( p0a != p0b );
			Assert.AreEqual( p0a, p0b );
			Assert.AreEqual( p0b, p0a );
			Assert.AreEqual( p0a, p0a );
		}
   
		/**
		 * Test the equality operator when same
		 */
		[Test] public void testEqualSame()
		{  
			IPoly p1a = new PolyDefault();
			p1a.Add( 0.0, 0.0 );
			p1a.Add( 4.0, 0.0 );
			p1a.Add( 4.0, 4.0 );
			p1a.Add( 0.0, 4.0 );
      
			IPoly p1b = new PolyDefault();
			p1b.Add( 0.0, 0.0 );
			p1b.Add( 4.0, 0.0 );
			p1b.Add( 4.0, 4.0 );
			p1b.Add( 0.0, 4.0 );
      
			Assert.IsTrue( p1a != p1b );
			Assert.AreEqual( p1a, p1b );
			Assert.AreEqual( p1b, p1a );
			Assert.AreEqual( p1a, p1a );
		}      
   
		/**
		 * Test the equality operator when same but different order
		 */
		[Test] public void testEqualDifferentOrder()
		{  
			IPoly p1a = new PolyDefault();
			p1a.Add( 0.0, 0.0 );
			p1a.Add( 4.0, 0.0 );
			p1a.Add( 4.0, 4.0 );
			p1a.Add( 0.0, 4.0 );
      
			IPoly p1b = new PolyDefault();
			p1b.Add( 0.0, 4.0 );
			p1b.Add( 0.0, 0.0 );
			p1b.Add( 4.0, 0.0 );
			p1b.Add( 4.0, 4.0 );
      
			Assert.IsTrue( p1a != p1b );
			Assert.AreEqual( p1a, p1b );
			Assert.AreEqual( p1b, p1a );
		}
   
		/**
		 * Test the equality operator when same points but not it same order
		 */
		[Test] public void testEqualBadOrder()
		{  
			IPoly p1a = new PolyDefault();
			p1a.Add( 0.0, 0.0 );
			p1a.Add( 4.0, 0.0 );
			p1a.Add( 4.0, 4.0 );
			p1a.Add( 0.0, 4.0 );
      
			IPoly p1b = new PolyDefault();
			p1b.Add( 0.0, 0.0 );
			p1b.Add( 0.0, 4.0 );
			p1b.Add( 4.0, 0.0 );
			p1b.Add( 4.0, 4.0 );
      
			Assert.IsTrue( !p1a.Equals( p1b ) );
			Assert.IsTrue( !p1b.Equals( p1a ) );
		}
   
		/**
		 * Test the equality operator when polys with different number of points
		 */
		[Test] public void testEqualDifferentNumPoints()
		{  
			IPoly p1a = new PolyDefault();
			p1a.Add( 0.0, 0.0 );
			p1a.Add( 4.0, 0.0 );
      
			IPoly p1b = new PolyDefault();
			p1b.Add( 0.0, 0.0 );
			p1b.Add( 4.0, 0.0 );
			p1b.Add( 4.0, 4.0 );
			p1b.Add( 0.0, 4.0 );
      
			Assert.IsTrue( !p1a.Equals( p1b ) );
			Assert.IsTrue( !p1b.Equals( p1a ) );
		}
   
		/**
		 * Test the equality operator when smae number of points but one point value different
		 */
		[Test] public void testEqualDifferentValue()
		{  
			IPoly p1a = new PolyDefault();
			p1a.Add( 0.0, 0.0 );
			p1a.Add( 4.0, 0.0 );
			p1a.Add( 4.0, 4.0 );
			p1a.Add( 0.0, 4.0 );
      
			IPoly p1b = new PolyDefault();
			p1b.Add( 0.0, 0.0 );
			p1b.Add( 4.0, 0.0 );
			p1b.Add( 4.0, 4444444.0 );
			p1b.Add( 0.0, 4.0 );
      
			Assert.IsTrue( !p1a.Equals( p1b ) );
			Assert.IsTrue( !p1b.Equals( p1a ) );
		}
   
		/**
		 * Test the getting the area of a triangle
		 */
		[Test] public void testAreaTriangle()
		{
			IPoly p = new PolyDefault();
			p.Add( 0.0, 0.0 );
			p.Add( 2.0, 0.0 );
			p.Add( 2.0, 2.0 );
      
			Assert.IsTrue( 2.0 == p.GetArea() );
		}
   
		/**
		 * Test the getting the area of a square
		 */
		[Test] public void testAreaSquare()
		{
			IPoly p = new PolyDefault();
			p.Add( 0.0, 0.0 );
			p.Add( 2.0, 0.0 );
			p.Add( 2.0, 2.0 );
			p.Add( 0.0, 2.0 );
      
			Assert.IsTrue( 4.0 == p.GetArea() );
		}
   
		/**
		 * Test the getting the area of a non-convex polygon
		 */
		[Test] public void testAreaNonConvex()
		{
			IPoly p = new PolyDefault();
			p.Add( 0.0, 0.0 );
			p.Add( 0.0, 8.0 );
			p.Add( 6.0, 4.0 );
			p.Add( 4.0, 2.0 );
			p.Add( 2.0, 4.0 );
      
			Assert.IsTrue( 20.0 == p.GetArea() );
		}
   
		/**
		 * Test the getting the area of a polygon with a hole
		 */
		[Test] public void testAreaWithHole()
		{
			IPoly p1 = new PolyDefault();
			p1.Add( 0.0, 0.0 );
			p1.Add( 4.0, 0.0 );
			p1.Add( 4.0, 4.0 );
			p1.Add( 0.0, 4.0 );
      
			IPoly p2 = new PolyDefault(true);
			p2.Add( 1.0, 1.0 );
			p2.Add( 3.0, 1.0 );
			p2.Add( 3.0, 3.0 );
			p2.Add( 1.0, 3.0 );
      
			IPoly p = new PolyDefault();
			p.Add( p1 );
			p.Add( p2 );
      
			Assert.IsTrue( 12.0 == p.GetArea() );
		}
   
		/**
		 * Test the getting the area of a self-intersecting polygon
		 */
		[Test] public void testSelfIntersecting()
		{
			IPoly p = new PolyDefault();
			p.Add( 6.0, 6.0 );
			p.Add( 2.0, 6.0 );
			p.Add( 2.0, 5.0 );
			p.Add( 4.0, 5.0 );
			p.Add( 4.0, 1.0 );
			p.Add( 2.0, 1.0 );
			p.Add( 2.0, 2.0 );
			p.Add( 1.0, 2.0 );
			p.Add( 1.0, 4.0 );
			p.Add( 2.0, 4.0 );
			p.Add( 2.0, 2.0 );
			p.Add( 3.0, 2.0 );
			p.Add( 3.0, 4.0 );
			p.Add( 2.0, 4.0 );
			p.Add( 2.0, 5.0 );
			p.Add( 0.0, 5.0 );
			p.Add( 0.0, 1.0 );
			p.Add( 2.0, 1.0 );
			p.Add( 2.0, 0.0 );
			p.Add( 6.0, 0.0 );
      
			Assert.IsTrue( 24.0 == p.GetArea() );
		}
	}
}
#endif
