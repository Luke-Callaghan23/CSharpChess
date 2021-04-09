using System;
using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
   public abstract class Piece
	{

		public static int whiteQueen;
		public static int blackQueen;

		protected char   color;
		protected char   type;
		protected char   num;
		protected Point  loc;
		protected string piece;
		//Predicate lambda to check if a point is in range
		protected Predicate<Point> InRange = (Point point) => (point.X >= 0 && point.X <= 7) && (point.Y >= 0 && point.Y <= 7);
		//Func lambda to determine if the first input piece beats the second one
		protected Func<Piece, Piece, bool> Beats = (Piece self, Piece other) => (self.color != other.color);
		//constructor
		public Piece(string piece, char type, Point point)
		{
			this.piece = piece;
			this.color = piece[0];
			this.type  = type;
			this.num   = piece[2];
			this.loc   = point;
		}

		//abstract method to get the moves of this piece
		public abstract List<Point> GetMoves();
		

		//loc setter
		public void SetLoc(Point loc)
		{
			this.loc = loc;
		}
		//loc getter
		public Point GetLoc()
		{
			return loc;
		}

		//color getter
		public char GetColor()
		{
			return this.color;
		}

		public int GetNum()
		{
			return num;
		}

		//ToString
		public override string ToString()
		{
			return String.Format("{0}{1}{2}", this.color, this.type, this.num);
		}
	}
	//default "empty" piece
	public class Empty : Piece
	{
		public Empty(Point loc) : base("   ", ' ', loc) { }

		public override List<Point> GetMoves() {  return null;  }

		public override string ToString() {  return "   "; }
	}
}