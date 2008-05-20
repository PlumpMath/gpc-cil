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

using System;
using System.Collections;
using System.Drawing;

namespace Gpc
{
	/**
	 * <code>PolyDefault</code> is a default <code>Poly</code> implementation.  
	 * It provides support for both complex and simple polygons.  A <i>complex polygon</i> 
	 * is a polygon that consists of more than one polygon.  A <i>simple polygon</i> is a 
	 * more traditional polygon that contains of one inner polygon and is just a 
	 * collection of points.
	 * <p>
	 * <b>Implementation Note:</b> If a point is added to an empty <code>PolyDefault</code>
	 * object, it will create an inner polygon of type <code>PolySimple</code>.
	 *
	 * @see PolySimple
	 *
	 * @author  Dan Bridenbecker, Solution Engineering, Inc.
	 */
	public class PolyDefault
	: IPoly
	{
		// -----------------
		// --- Constants ---
		// -----------------
   
		// ------------------------
		// --- Member Variables ---
		// ------------------------
		/**
		 * Only applies to the first poly and can only be used with a poly that contains one poly
		 */
		private   bool m_IsHole = false ;
		protected ArrayList m_List = new ArrayList();
   
		// --------------------
		// --- Constructors ---
		// --------------------
		/** Creates a new instance of PolyDefault */
		public PolyDefault()
			: this(false)
		{
		}
   
		public PolyDefault( bool isHole )
		{
			m_IsHole = isHole ;
		}
   
		// ----------------------
		// --- Object Methods ---
		// ----------------------
		/**
		 * Return true if the given object is equal to this one.
		 */
		public override bool Equals( Object obj )
		{
			// Don't bother if we aren't the same type of object
			if(!(obj is PolyDefault))
				return false;

			// Cast it to this object
			PolyDefault that = (PolyDefault) obj;

			// We need to have the same hole type
			if (this.m_IsHole != that.m_IsHole)
				return false;

			// Go through the elements to match
			if (m_List.Count != that.m_List.Count)
				return false;

			// Loop through and make sure each item is equal
			for (int i = 0; i < m_List.Count; i++)
			{
				IPoly p1 = (IPoly) m_List[i];
				IPoly p2 = (IPoly) that.m_List[i];
				
				if (!p1.Equals(p2))
					return false;
			}

			// We have a match
			return true;
		}
   
		/**
		 * Return the hashCode of the object.
		 *
		 * @return an integer value that is the same for two objects
		 * whenever their internal representation is the same (equals() is true)
		 **/
		public override int GetHashCode()
		{
			int result = 17 ;
			result = 37*result + m_List.GetHashCode();
			return result;
		}
   
		// ----------------------
		// --- Public Methods ---
		// ----------------------
		/**
		 * Remove all of the points.  Creates an empty polygon.
		 */
		public void Clear()
		{
			m_List.Clear();
		}
   
		/**
		 * Add a point to the first inner polygon.
		 * <p>
		 * <b>Implementation Note:</b> If a point is added to an empty PolyDefault object,
		 * it will create an inner polygon of type <code>PolySimple</code>.
		 */
		public void Add(double x, double y)
		{
			Add( new PointF( (float) x, (float) y ) );
		}
   
		/**
		 * Add a point to the first inner polygon.
		 * <p>
		 * <b>Implementation Note:</b> If a point is added to an empty PolyDefault object,
		 * it will create an inner polygon of type <code>PolySimple</code>.
		 */
		public void Add( PointF p )
		{
			if( m_List.Count == 0 )
			{
				m_List.Add( new PolySimple() );
			}
			((IPoly)m_List[0]).Add(p);
		}
   
		/**
		 * Add an inner polygon to this polygon - assumes that adding polygon does not
		 * have any inner polygons.
		 *
		 * @throws IllegalStateException if the number of inner polygons is greater than
		 * zero and this polygon was designated a hole.  This would break the assumption
		 * that only simple polygons can be holes.
		 */
		public void Add( IPoly p )
		{
			if( (m_List.Count > 0) && m_IsHole )
			{
				throw new Exception("Cannot add polys to something designated as a hole.");
			}
			m_List.Add( p );
		}
   
		/**
		 * Return true if the polygon is empty
		 */
		public bool IsEmpty()
		{
			return m_List.Count == 0;
		}
   
		/**
		 * Returns the bounding rectangle of this polygon.
		 * <strong>WARNING</strong> Not supported on complex polygons.
		 */
		public RectangleF GetBounds()
		{
			if( m_List.Count == 0 )
			{
				return new RectangleF();
			}
			else if( m_List.Count == 1 )
			{
				IPoly ip = GetInnerPoly(0);
				return ip.GetBounds();
			}
			else
			{
				throw new Exception("getBounds not supported on complex poly.");
			}
		}
   
		/**
		 * Returns the polygon at this index.
		 */
		public IPoly GetInnerPoly(int polyIndex)
		{
			return (IPoly)m_List[polyIndex];
		}
   
		/// <summary>
		/// Returns the number of inner polygons. Inner polygons are
		/// assumed to always return 1 here.
		/// </summary>
		public int InnerPolygonCount
		{
			get
			{
				if (m_List == null || m_List.Count == 0)
					return 0;
				else
					return m_List.Count;
			}
		}
   
		/// <summary>
		/// Returns the number of points in the first inner polygon.
		/// </summary>
		public int PointCount
		{
			get
			{
				if (m_List == null || m_List.Count == 0)
					return 0;
				else
					return ((IPoly) m_List[0]).PointCount;
			}
		}
   
		/**
		 * Return the X value of the point at the index in the first inner polygon
		 */
		public double GetX(int index)
		{
			return ((IPoly)m_List[0]).GetX(index) ;
		}
   
		/**
		 * Return the Y value of the point at the index in the first inner polygon
		 */
		public double GetY(int index)
		{
			return ((IPoly)m_List[0]).GetY(index) ;
		}
   
		/**
		 * Return true if this polygon is a hole.  Holes are assumed to be inner polygons of
		 * a more complex polygon.
		 *
		 * @throws Exception if called on a complex polygon.
		 */
		public bool IsHole()
		{
			if( m_List.Count > 1 )
			{
				throw new Exception( "Cannot call on a poly made up of more than one poly." );
			}
			return m_IsHole ;
		}
   
		/**
		 * Set whether or not this polygon is a hole.  Cannot be called on a complex polygon.
		 *
		 * @throws Exception if called on a complex polygon.
		 */
		public void SetIsHole( bool isHole )
		{
			if( m_List.Count > 1 )
			{
				throw new Exception( "Cannot call on a poly made up of more than one poly." );
			}
			m_IsHole = isHole ;
		}
   
		/**
		 * Return true if the given inner polygon is contributing to the set operation.
		 * This method should NOT be used outside the Clip algorithm.
		 */
		public bool IsContributing( int polyIndex)
		{
			return ((IPoly)m_List[polyIndex]).IsContributing(0);
		}
   
		/**
		 * Set whether or not this inner polygon is constributing to the set operation.
		 * This method should NOT be used outside the Clip algorithm.
		 *
		 * @throws Exception if called on a complex polygon
		 */
		public void SetContributing( int polyIndex, bool contributes)
		{
			if( m_List.Count != 1 )
			{
				throw new Exception( "Only applies to polys of size 1" );
			}
			((IPoly)m_List[polyIndex]).SetContributing( 0, contributes );
		}
   
		/**
		 * Return a IPoly that is the difference of this polygon with the given polygon.
		 * The returned polygon could be complex.
		 *
		 * @return the returned IPoly will be an instance of PolyDefault.
		 */
		public IPoly Difference(IPoly p)
		{
			return Clipper.Difference( p, this, GetType() );
		}
   
		/**
		 * Return a IPoly that is the intersection of this polygon with the given polygon.
		 * The returned polygon could be complex.
		 *
		 * @return the returned IPoly will be an instance of PolyDefault.
		 */
		public IPoly Intersection(IPoly p)
		{
			return Clipper.Intersection( p, this, GetType() );
		}
   
		/**
		 * Return a IPoly that is the union of this polygon with the given polygon.
		 * The returned polygon could be complex.
		 *
		 * @return the returned IPoly will be an instance of PolyDefault.
		 */
		public IPoly Union(IPoly p)
		{
			return Clipper.Union( p, this, GetType() );
		}
   
		/**
		 * Return a IPoly that is the exclusive-or of this polygon with the given polygon.
		 * The returned polygon could be complex.
		 *
		 * @return the returned IPoly will be an instance of PolyDefault.
		 */
		public IPoly Xor(IPoly p)
		{
			return Clipper.Xor( p, this, GetType() );
		}
   
		/**
		 * Return the area of the polygon in square units.
		 */
		public double GetArea()
		{
			double area = 0.0 ;
			for( int i = 0 ; i < InnerPolygonCount ; i++ )
			{
				IPoly p = GetInnerPoly(i);
				double tarea = p.GetArea() * (p.IsHole() ? -1.0 : 1.0);
				area += tarea ;
			}
			return area ;
		}

		// -----------------------
		// --- Package Methods ---
		// -----------------------
		public void Print()
		{
			for( int i = 0 ; i < m_List.Count ; i++ )
			{
				IPoly p = GetInnerPoly(i);
				Console.WriteLine("InnerPoly("+i+").hole="+p.IsHole());
				for( int j = 0 ; j < p.PointCount; j++ )
				{
					Console.WriteLine(p.GetX(j)+"  "+p.GetY(j));
				}
			}
		}
   
		public override string ToString()
		{
			return String.Format("PolyDefault({0})", m_List.Count);
		}
	}
}
