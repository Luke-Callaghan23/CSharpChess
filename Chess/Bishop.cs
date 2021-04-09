using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
	internal class Bishop : Piece
	{
		public Bishop(string piece, Point point) : base(piece, 'b', point) { }

		public override List<Point> GetMoves()
		{
			Point curPoint;
			List<Point> moves = new List<Point>();

			Piece curPiece;
			//Piece[][] board = Chess.GetBoard().GetBoard();

			int row = this.loc.X,
				col = this.loc.Y;


			//4-directions
			//	0-> up-left
			//	1-> up-right
			//	2-> down-left
			//	3-> down-right
			bool[] stops = new bool[4];

			//double for-loop checks all 4 directions of movement (inner)
			//		at each (outer) iteration of increasing distance (1-4)
			
			//each iteration of mod is another tile of distance from current piece
			for (int mod = 1; mod <= 4; mod++)
			{
				//checks 4 directions
				for(int stop = 0; stop < 4; stop++)
				{
					if(!stops[stop])
					{
						//converts iteration of inner loop to -1/1 for rows/cols
						//	0 -> -1, -1
						//	1 -> -1,  1
						//	2 ->  1, -1
						//	3 ->  1,  1
						int dirModRow = (stop / 2 != 0) ? 1 : -1;
						int dirModCol = (stop % 2 != 0) ? 1 : -1;

						//Gets current point on map
						curPoint = new Point(row + mod * dirModRow, col + mod * dirModCol);
						if(InRange(curPoint))
						{
							//Gets piece at current point
							curPiece = Chess.GetBoard().GetPiece(curPoint);

							//Checks if this piece at current point
							if (this.Beats(this, curPiece))
							{
								moves.Add(curPoint);
								stops[stop] = (curPiece.GetColor() != ' ');
							}
							else
							{
								stops[stop] = true;
							}
						}
						else
						{
							stops[stop] = true;
						}
					}
				}
			}
			return moves;
		}
	}
}