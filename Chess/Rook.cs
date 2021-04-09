using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
	internal class Rook : Piece
	{
		public Rook(string piece, Point point) : base(piece, 'r', point) { }

		public override List<Point> GetMoves()
		{
			//List of moves to be returned
			List<Point> moves = new List<Point>();


			//Iterator items
			Point curPoint;
			Piece curPiece;

			//Row and collumn of this piece
			int row = this.loc.X,
				col = this.loc.Y;

			//If stops[n] == true, stop going in a certain direction
			bool[] stops;

			//y-axis
			stops = new bool[2];
			for (int rMod = 1; rMod < 8; rMod++)
			{
				//if stop == 0, check positive direction
				//else, check negative direction
				for (int stop = 0; stop < 2; stop++)
				{
					//if stops[stop] = true, don't check; stop
					if (!stops[stop])
					{
						int dir = (stop == 0) ? 1 : -1;
						curPoint = new Point(row + rMod * dir, col);
						if(InRange.Invoke(curPoint))
						{
							curPiece = Chess.GetBoard().GetPiece(curPoint);
							if(this.Beats(this, curPiece))
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
			//x-axis
			stops = new bool[2];
			for (int cMod = 1; cMod < 8; cMod++)
			{
				//if stop == 0, check positive direction
				//else, check negative direction
				for (int stop = 0; stop < 2; stop++)
				{
					//if stops[stop] == 0, check positive direction
					//else, check negative direction
					if (!stops[stop])
					{
						int dir = (stop == 0) ? 1 : -1;
						curPoint = new Point(row, col + cMod * dir);
						if (InRange(curPoint))
						{
							curPiece = Chess.GetBoard().GetPiece(curPoint);
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