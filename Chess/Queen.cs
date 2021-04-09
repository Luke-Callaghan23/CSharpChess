using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
	internal class Queen : Piece
	{
		private Rook   tempRook;
		private Bishop tempBish;
		public Queen(string piece, Point point) : base(piece, 'q', point) 
		{
			//creates new instances of Bishops/Rooks for getting this piece's moveset
			tempRook = new Rook  (this.color + "  ", this.loc);
			tempBish = new Bishop(this.color + "  ", this.loc);
		}


		public Queen(char color, Point loc) : base(color + "  ", 'q', loc)
		{
			//creates new instances of Bishops/Rooks for getting this piece's moveset
			tempRook = new Rook  (this.color + "  ", this.loc);
			tempBish = new Bishop(this.color + "  ", this.loc);
			if(this.color == 'w')
			{
				this.num = (char) (Piece.whiteQueen + '0');
				Piece.whiteQueen++;
			}
			else
			{
				this.num = (char) (Piece.blackQueen + '0');
				Piece.blackQueen++;
			}
		}

		public override List<Point> GetMoves()
		{
			//Since a Queen's moves are just a rooks moves + a bishop's moves,
			//      moves location of tempBish/tempRook to current position
			tempBish.SetLoc(this.loc);
			tempRook.SetLoc(this.loc);

			//Adds moveset of bishop and rook to one list
			List<Point> moves = tempRook.GetMoves();
			moves.AddRange(tempBish.GetMoves());

			//returns list
			return moves;

		}
	}
}