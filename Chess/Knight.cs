using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
	internal class Knight : Piece
	{
		public Knight(string piece, Point point) : base(piece, 'k', point) { }

		public override List<Point> GetMoves()
		{
			Point curPoint;
			List<Point> moves = new List<Point>();

			Piece curPiece;
			//Piece[][] board = Chess.GetBoard().GetBoard();

			int row = this.loc.X,
				col = this.loc.Y;

			//y-axis
			for (int rMod = 0; rMod < 2; rMod++)
			{
				//x-axis
				for (int cMod = 0; cMod < 2; cMod++)
				{
					//rot 90 deg
					for (int inner = 0; inner < 2; inner++)
					{
						int fRow = (rMod * 4) - 2,
							fCol = (cMod * 2) - 1,
							temp;
						//If inner == 1, swap row/col modifiers
						//		(rotates current piece target)
						if (inner == 1)
						{
							temp = fCol;
							fCol = fRow;
							fRow = temp;
						}

						//get current point
						curPoint = new Point(row + fRow, col + fCol);
						//check if current point is in range
						if (InRange(curPoint))
						{
							//get piece at current point
							curPiece = Chess.GetBoard().GetPiece(curPoint);

							//check if this piece beats the piece at current point
							if (this.Beats(this, curPiece))
							{
								moves.Add(curPoint);
							}
						}
					}
				}
			}
			return moves;
		}
	}
}