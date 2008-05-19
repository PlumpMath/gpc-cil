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
	 * <code>PolySimple</code> is a simple polygon - contains only one inner polygon.
	 * <p>
	 * <strong>WARNING:</strong> This type of <code>Poly</code> cannot be used for an
	 * inner polygon that is a hole.
	 *
	 * @author  Dan Bridenbecker, Solution Engineering, Inc.
	 */
	public class PolySimple
	: Poly
	{
		// -----------------
		// --- Constants ---
		// -----------------
   
		// ------------------------
		// --- Member Variables ---
		// ------------------------
		/**
		 * The list of PointF objects in the polygon.
		 */
		protected ArrayList m_List = new ArrayList();

		/** Flag used by the Clip algorithm */
		private bool m_Contributes = true ;
   
		// --------------------
		// --- Constructors ---
		// --------------------
		/** Creates a new instance of PolySimple */
		public PolySimple()
		{
		}
   
		// ----------------------
		// --- Object Methods ---
		// ----------------------
		/**
		 * Return true if the given object is equal to this one.
		 * <p>
		 * <strong>WARNING:</strong> This method failse if the first point
		 * appears more than once in the list.
		 */
		public override bool Equals( object obj )
		{
			// See if we can cast the object
			if( !(obj is PolySimple) )
				return false;

			PolySimple that = (PolySimple)obj;

			// Compare the sizees
			int this_num = this.m_List.Count;
			int that_num = that.m_List.Count;

			if( this_num != that_num )
				return false ;

			// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			// !!! WARNING: This is not the greatest algorithm.  It fails if !!!
			// !!! the first point in "this" poly appears more than once.    !!!
			// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			if( this_num > 0 )
			{
				double this_x = this.GetX(0);
				double this_y = this.GetY(0);
				int that_first_index = -1 ;
				int that_index = 0;
				for( that_index = 0 ; (that_first_index == -1) && (that_index < that_num) ; that_index++ )
				{
					double that_x = that.GetX(that_index);
					double that_y = that.GetY(that_index);
					if( (this_x == that_x) && (this_y == that_y) )
					{
						that_first_index = that_index ;
					}
				}
				if( that_first_index == -1 ) return false ;
				that_index = that_first_index ;
				for( int this_index = 0 ; this_index < this_num ; this_index++ )
				{
					this_x = this.GetX(this_index);
					this_y = this.GetY(this_index);
					double that_x = that.GetX(that_index);
					double that_y = that.GetY(that_index);
            
					if( (this_x != that_x) || (this_y != that_y) ) return false;
               
					that_index++ ;
					if( that_index >= that_num )
					{
						that_index = 0 ;
					}
				}
			}
			return true ;
		}
   
		/**
		 * Return the hashCode of the object.
		 * <p>
		 * <strong>WARNING:</strong>Hash and Equals break contract.
		 *
		 * @return an integer value that is the same for two objects
		 * whenever their internal representation is the same (equals() is true)
		 */
		public override int GetHashCode()
		{
			// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			// !!! WARNING:  This hash and equals break the contract. !!!
			// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			int result = 17 ;
			result = 37*result + m_List.GetHashCode();
			return result;
		}
   
		/**
		 * Return a string briefly describing the polygon.
		 */
		public override string ToString()
		{
			return "PolySimple: num_points="+GetNumPoints();
		}
   
		// --------------------
		// --- Poly Methods ---
		// --------------------
		/**
		 * Remove all of the points.  Creates an empty polygon.
		 */
		public void Clear()
		{
			m_List.Clear();
		}
   
		/**
		 * Add a point to the first inner polygon.
		 */
		public void Add(double x, double y)
		{
			Add( new PointF( (float) x, (float) y ) );
		}
   
		/**
		 * Add a point to the first inner polygon.
		 */
		public void Add(PointF p)
		{
			m_List.Add( p );
		}
   
		/**
		 * Throws exception if called
		 */
		public void Add(Poly p)
		{
			throw new Exception("Cannot add poly to a simple poly.");
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
		 */
		public RectangleF GetBounds()
		{
			double xmin =  Double.MaxValue ;
			double ymin =  Double.MaxValue ;
			double xmax = -Double.MaxValue ;
			double ymax = -Double.MaxValue ;
      
			for( int i = 0 ; i < m_List.Count ; i++ )
			{
				double x = GetX(i);
				double y = GetY(i);
				if( x < xmin ) xmin = x;
				if( x > xmax ) xmax = x;
				if( y < ymin ) ymin = y;
				if( y > ymax ) ymax = y;
			}
      
			return new RectangleF( (float) xmin, (float) ymin,
				(float) (xmax-xmin), (float) (ymax-ymin) );
		}
   
		/**
		 * Returns <code>this</code> if <code>polyIndex = 0</code>, else it throws
		 * Exception.
		 */
		public Poly GetInnerPoly(int polyIndex)
		{
			if( polyIndex != 0 )
			{
				throw new Exception("PolySimple only has one poly");
			}
			return this ;
		}
   
		/**
		 * Always returns 1.
		 */
		public int GetNumInnerPoly()
		{
			return 1 ;
		}
   
		/**
		 * Return the number points of the first inner polygon
		 */
		public int GetNumPoints()
		{
			return m_List.Count;
		}   

		/**
		 * Return the X value of the point at the index in the first inner polygon
		 */
		public double GetX(int index)
		{
			return ((PointF)m_List[index]).X;
		}
   
		/**
		 * Return the Y value of the point at the index in the first inner polygon
		 */
		public double GetY(int index)
		{
			return ((PointF)m_List[index]).Y;
		}
   
		/**
		 * Always returns false since PolySimples cannot be holes.
		 */
		public bool IsHole()
		{
			return false ;
		}
   
		/**
		 * Throws Exception if called.
		 */
		public void SetIsHole(bool isHole)
		{
			throw new Exception("PolySimple cannot be a hole");
		}
   
		/**
		 * Return true if the given inner polygon is contributing to the set operation.
		 * This method should NOT be used outside the Clip algorithm.
		 *
		 * @throws Exception if <code>polyIndex != 0</code>
		 */
		public bool IsContributing( int polyIndex )
		{
			if( polyIndex != 0 )
			{
				throw new Exception("PolySimple only has one poly");
			}
			return m_Contributes ;
		}
   
		/**
		 * Set whether or not this inner polygon is constributing to the set operation.
		 * This method should NOT be used outside the Clip algorithm.
		 *
		 * @throws Exception if <code>polyIndex != 0</code>
		 */
		public void SetContributing( int polyIndex, bool contributes )
		{
			if( polyIndex != 0 )
			{
				throw new Exception("PolySimple only has one poly");
			}
			m_Contributes = contributes ;
		}
   
		/**
		 * Return a Poly that is the intersection of this polygon with the given polygon.
		 * The returned polygon is simple.
		 *
		 * @return The returned Poly is of type PolySimple
		 */
		public Poly Intersection(Poly p)
		{
			return Clipper.Intersection( this, p, GetType() );
		}
   
		/**
		 * Return a Poly that is the union of this polygon with the given polygon.
		 * The returned polygon is simple.
		 *
		 * @return The returned Poly is of type PolySimple
		 */
		public Poly Union(Poly p)
		{
			return Clipper.Union( this, p, GetType() );
		}
   
		/**
		 * Return a Poly that is the exclusive-or of this polygon with the given polygon.
		 * The returned polygon is simple.
		 *
		 * @return The returned Poly is of type PolySimple
		 */
		public Poly Xor(Poly p)
		{
			return Clipper.Xor( p, this, GetType() );
		}
         
		/**
		 * Returns the area of the polygon.
		 * <p>
		 * The algorithm for the area of a complex polygon was take from
		 * code by Joseph O'Rourke author of " Computational Geometry in C".
		 */
		public double GetArea()
		{
			if( GetNumPoints() < 3 )
			{
				return 0.0 ;
			}
			double ax = GetX(0);
			double ay = GetY(0);
			double area = 0.0 ;
			for( int i = 1 ; i < (GetNumPoints()-1) ; i++ )
			{
				double bx = GetX(i);
				double by = GetY(i);
				double cx = GetX(i+1);
				double cy = GetY(i+1);
				double tarea = ((cx - bx)*(ay - by)) - ((ax - bx)*(cy - by));
				area += tarea ;
			}
			area = 0.5*Math.Abs(area);
			return area ;
		}
   
		// -----------------------
		// --- Package Methods ---
		// -----------------------
	}
}
