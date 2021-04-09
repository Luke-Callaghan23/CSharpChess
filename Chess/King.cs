using System;
using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
	internal class King : Piece
	{
		public King(string piece, Point point) : base(piece, '_', point) { }

		public override List<Point> GetMoves()
		{
			Point curPoint;
			List<Point> moves = new List<Point>();

			Piece curPiece;
			//Piece[][] board = Chess.GetBoard().GetBoard();

			int row = this.loc.X, 
				col = this.loc.Y;
			
			//x-axis
			for(int rMod = -1; rMod < 2; rMod++)
			{
				//y-axis
				for(int cMod = -1; cMod < 2; cMod++)
				{
					//gets current point
					curPoint = new Point(row + rMod, col + cMod);

					//checks if current point is in range
					if (InRange(curPoint))
					{
						//gets piece at current point
						curPiece = Chess.GetBoard().GetPiece(curPoint);

						//checks if this piece beats the piece at the current point
						if (this.Beats(this, curPiece)) 
						{
							moves.Add(curPoint);
						}
					}
				}
			}
			return moves;
		}

		//Method to check if this king is checked
		public bool CheckCheck()
		{
			//Get all pieces
			List<Piece> pieceReport = Chess.GetBoard().GetPieceReport();

			//Filter out all ally pieces
			List<Piece> enemies = pieceReport.FindAll((piece) => piece.GetColor() != this.GetColor());

			//Foreach enemy, searches all moves in .GetMoves() for one that matches this king's location
			//		if any do, return that piece, store in Piece killer
			bool check = enemies.Exists( piece => piece.GetMoves().Exists( move => move.Equals(this.loc) ) );

			//If killer exists, king is checked
			return (check);
		}

		//Method to check if this king is check mated
		public bool CheckMate()
		{
			//If the king is not checked, then it is not checkmated
			if(!this.CheckCheck())
			{
				return false;
			}


			List<Point> moves = this.GetMoves();
			Point oldLoc = this.GetLoc();

			//Adds all moves that match the predicate function to validMoves list
			List<Point> validMoves = moves.FindAll( (nextMove) => {

				//retrieves overwritten piece at nextMove
				Piece overwritten = Chess.GetBoard().MovePiece(this, nextMove);

				//Checking if king is checked in new position returning to old board state 
				bool check = this.CheckCheck();

				//Returning to old board state
				Chess.GetBoard().MovePiece(this, oldLoc);
				Chess.GetBoard().MovePiece(overwritten, nextMove);

				//Return !checked
				return !check;
			});

			//If no valid moves, king is check mated
			return validMoves.Count == 0;
		}
	}
}