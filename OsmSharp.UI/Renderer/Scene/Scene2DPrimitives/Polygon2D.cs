// OsmSharp - OpenStreetMap (OSM) SDK
// Copyright (C) 2013 Abelshausen Ben
// 
// This file is part of OsmSharp.
// 
// OsmSharp is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// OsmSharp is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with OsmSharp. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OsmSharp.Math.Primitives;

namespace OsmSharp.UI.Renderer.Scene.Scene2DPrimitives
{
	/// <summary>
	/// A simple 2D polygon.
	/// </summary>
	public class Polygon2D : IScene2DPrimitive
	{
        /// <summary>
        /// Creates a new Polygon2D.
        /// </summary>
        public Polygon2D()
        {
            
        }

        /// <summary>
        /// Creates a new Polygon2D.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        /// <param name="width"></param>
        /// <param name="fill"></param>
		public Polygon2D(double[] x, double[] y, int color, double width, bool fill)
        {
            this.X = x;
            this.Y = y;
            this.Color = color;
            this.Width = width;
            this.Fill = fill;

            MinX = int.MaxValue;
            MaxX = int.MinValue;
            for (int idx = 0; idx < x.Length; idx++)
            {
                if (x[idx] > MaxX)
                {
                    MaxX = x[idx];
                }
                if (x[idx] < MinX)
                {
                    MinX = x[idx];
                }
            }
            MinY = int.MaxValue;
            MaxY = int.MinValue;
            for (int idx = 0; idx < y.Length; idx++)
            {
                if (y[idx] > MaxY)
                {
                    MaxY = y[idx];
                }
                if (y[idx] < MinY)
                {
                    MinY = y[idx];
                }
            }

            this.MinZoom = float.MinValue;
            this.MaxZoom = float.MaxValue;
        }

	    /// <summary>
        /// Creates a new Polygon2D.
	    /// </summary>
	    /// <param name="x"></param>
	    /// <param name="y"></param>
	    /// <param name="color"></param>
	    /// <param name="width"></param>
	    /// <param name="fill"></param>
	    /// <param name="minZoom"></param>
	    /// <param name="maxZoom"></param>
		public Polygon2D(double[] x, double[] y, int color, double width, bool fill, float minZoom, float maxZoom)
            : this()
        {
            this.X = x;
            this.Y = y;
            this.Color = color;
            this.Width = width;
            this.Fill = fill;

            MinX = int.MaxValue;
            MaxX = int.MinValue;
            for (int idx = 0; idx < x.Length; idx++)
            {
                if (x[idx] > MaxX)
                {
                    MaxX = x[idx];
                }
                if (x[idx] < MinX)
                {
                    MinX = x[idx];
                }
            }
            MinY = int.MaxValue;
            MaxY = int.MinValue;
            for (int idx = 0; idx < y.Length; idx++)
            {
                if (y[idx] > MaxY)
                {
                    MaxY = y[idx];
                }
                if (y[idx] < MinY)
                {
                    MinY = y[idx];
                }
            }

            this.MinZoom = minZoom;
            this.MaxZoom = maxZoom;
        }

        /// <summary>
        /// Creates a new Polygon2D.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        /// <param name="width"></param>
        /// <param name="fill"></param>
        /// <param name="minX"></param>
        /// <param name="maxX"></param>
        /// <param name="minY"></param>
        /// <param name="maxY"></param>
		public Polygon2D(double[] x, double[] y, int color, double width, bool fill,
            int minX, int maxX, int minY, int maxY)
            : this()
        {
            this.X = x;
            this.Y = y;
            this.Color = color;
            this.Width = width;
            this.Fill = fill;

            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;

            this.MinZoom = float.MinValue;
            this.MaxZoom = float.MaxValue;
		}

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public uint Id { get; set; }
		
		/// <summary>
		/// Gets or sets the tag.
		/// </summary>
		/// <value>The tag.</value>
		public object Tag {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the x.
		/// </summary>
		/// <value>The x.</value>
		public double[] X {
			get;
			set;
		}
		
		/// <summary>
		/// Gets or sets the y.
		/// </summary>
		/// <value>The y.</value>
		public double[] Y {
			get;
			set;
		}
		
		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public int Color {
			get;
			set;
		}
		
		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>The size.</value>
		public double Width {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="OsmSharp.UI.Renderer.Scene2DPrimitives.Polygon2D"/> is to be filled.
		/// </summary>
		/// <value><c>true</c> if fill; otherwise, <c>false</c>.</value>
		public bool Fill {
			get;
			set;
        }

        /// <summary>
        /// The minimum zoom.
        /// </summary>
        public float MinZoom { get; set; }

        /// <summary>
        /// The maximum zoom.
        /// </summary>
        public float MaxZoom { get; set; }

		#region IScene2DPrimitive implementation

        internal double MinX { get; set; }
        internal double MaxX { get; set; }
        internal double MinY { get; set; }
        internal double MaxY { get; set; }

	    /// <summary>
	    /// Returns true if the object is visible on the view.
	    /// </summary>
	    /// <returns>true</returns>
	    /// <c>false</c>
	    /// <param name="view">View.</param>
	    /// <param name="zoom"></param>
	    public bool IsVisibleIn (View2D view, float zoom)
        {
            if (this.MinZoom > zoom || this.MaxZoom < zoom)
            { // outside of zoom bounds!
                return false;
            }

			if (view.OverlapsWithBox (MinX, MinY, MaxX, MaxY)) {
				return true; // maybe a better hittest?
			}
			return false;
		}

        /// <summary>
        /// Returns the bounding box for this primitive.
        /// </summary>
        /// <returns></returns>
		public BoxF2D GetBox()
        {
			return new BoxF2D(MinX, MinY, MaxX, MaxY);
        }
		
		#endregion

        #region Calculations

        /// <summary>
        /// Returns true if the given vertex is convex.
        /// </summary>
        /// <param name="vertexIdx"></param>
        /// <returns></returns>
        public bool IsEar(int vertexIdx)
        {
            int previousIdx = vertexIdx == 0 ? this.X.Length - 1 : vertexIdx - 1;
            int nextIdx = vertexIdx == this.X.Length - 1 ? 0 : vertexIdx + 1;

            return (this.Contains(
                new double[] { 
                    (this.X[previousIdx] + this.X[nextIdx]) / 2, 
                    (this.Y[previousIdx] + this.Y[nextIdx]) / 2 }));
        }

        /// <summary>
        /// Returns the neighbours of the given vertex.
        /// </summary>
        /// <returns></returns>
        public double[][] GetNeigbours(int vertexIdx)
        {
            int previousIdx = vertexIdx == 0 ? this.X.Length - 1 : vertexIdx - 1;
            int nextIdx = vertexIdx == this.X.Length - 1 ? 0 : vertexIdx + 1;

            double[] previous = new double[] { this.X[previousIdx], this.Y[previousIdx] };
            double[] next = new double[] { this.X[nextIdx], this.Y[nextIdx] };
            return new double[][] { previous, next };
        }

        /// <summary>
        /// Returns true if the given coordinate is contained in this ring.
        /// 
        /// See: http://geomalgorithms.com/a03-_inclusion.html
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public bool Contains(double[] coordinate)
        {
            int number = 0;
            if (this.X[0] == coordinate[0] && 
                this.Y[0] == coordinate[1])
            { // the given point is one of the corners.
                return true;
            }
            // loop over all edges and calculate if they possibly intersect.
            for (int idx = 0; idx < this.X.Length - 1; idx++)
            {
                if (this.X[idx + 1] == coordinate[0] &&
                    this.Y[idx + 1] == coordinate[1])
                { // the given point is one of the corners.
                    return true;
                }
                bool idxRight = this.X[idx] > coordinate[0];
                bool idx1Right = this.X[idx + 1] > coordinate[0];
                if (idxRight || idx1Right)
                { // at least one of the coordinates is to the right of the point to calculate for.
                    if ((this.Y[idx] <= coordinate[1] &&
                        this.Y[idx + 1] >= coordinate[1]) &&
                        !(this.Y[idx] == coordinate[1] &&
                        this.Y[idx + 1] == coordinate[1]))
                    { // idx is lower than idx+1
                        if (idxRight && idx1Right)
                        { // no need for the left/right algorithm the result is already known.
                            number++;
                        }
                        else
                        { // one of the coordinates is not to the 'right' now we need the left/right algorithm.
                            LineF2D localLine = new LineF2D(
                                new PointF2D(this.X[idx], this.Y[idx]),
                                new PointF2D(this.X[idx + 1], this.Y[idx + 1]));
                            if (localLine.PositionOfPoint(new PointF2D(coordinate)) == LinePointPosition.Left)
                            {
                                number++;
                            }
                        }
                    }
                    else if ((this.Y[idx] >= coordinate[1] &&
                        this.Y[idx + 1] <= coordinate[1]) &&
                        !(this.Y[idx] == coordinate[1] &&
                        this.Y[idx + 1] == coordinate[1]))
                    { // idx is higher than idx+1
                        if (idxRight && idx1Right)
                        { // no need for the left/right algorithm the result is already known.
                            number--;
                        }
                        else
                        { // one of the coordinates is not to the 'right' now we need the left/right algorithm.
                            LineF2D localLine = new LineF2D(
                                new PointF2D(this.X[idx], this.Y[idx]),
                                new PointF2D(this.X[idx + 1], this.Y[idx + 1]));
                            if (localLine.PositionOfPoint(new PointF2D(coordinate)) == LinePointPosition.Right)
                            {
                                number--;
                            }
                        }
                    }
                }
            }
            return number != 0;
        }

        #endregion
    }
}