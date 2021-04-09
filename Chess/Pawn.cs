using System;
using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
	internal class Pawn : Piece
	{
		private bool moved;
		public Pawn(string piece, Point point) : base(piece, 'p', point) { this.moved = false; }

		public Pawn(string piece, Point point, bool moved) : base(piece, 'p', point) { this.moved = moved; }

		public override List<Point> GetMoves()
		{
			List<Point> moves = new List<Point>();

			//gets the direction in the row-axis that this pawn moves (white -> -1, black -> 1)
			int rDir = (this.color == 'w') ? -1 : 1;

			//variables for target point/piece
			Point targPoint; 
			Piece targPiece;

			//Checking tile in front of pawn
			targPoint = new Point(this.loc.X + (1 * rDir), this.loc.Y);
			if (InRange(targPoint))
			{
				targPiece = Chess.GetBoard().GetPiece(targPoint);
				if (targPiece.GetType() == typeof(Empty))
				{
					moves.Add(targPoint);

					//Checking tile two steps ahead of pawn (if this pawn has never moved)
					if(!moved)
					{
						targPoint = new Point(this.loc.X + (2 * rDir), this.loc.Y);
						if (InRange(targPoint))
						{

							targPiece = Chess.GetBoard().GetPiece(targPoint);
							if(targPiece.GetType() == typeof(Empty))
							{
								moves.Add(targPoint);
							}
						}
					}
				}
			}
			//Checking tile diagonally right and left from this pawn
			//		loop 0 -> left
			//		loop 1 -> right
			for(int loop = 0; loop < 2; loop++)
			{
				//calculating column direction
				int cDir = (loop == 0) ? -1 : 1;

				targPoint = new Point(this.loc.X + (1 * rDir), this.loc.Y + (1 * cDir));
				if(InRange(targPoint))
				{
					targPiece = Chess.GetBoard().GetPiece(targPoint);
					//checks if targPiece is enemy
					if(this.GetColor() != targPiece.GetColor() && targPiece.GetColor() != ' ')
					{
						moves.Add(targPoint);
					}
				}
			}
			return moves;
		}

		public void SetMoved(bool moved)
		{
			this.moved = moved;
		}
	}
}