using System;
using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
	internal class Board
	{
		/*private readonly Piece[,] initialBoard = new Piece[8,8] {
			{new Rook(1, false, new Point(0,0)), new Knight(1, false, new Point(0,1)), new Bishop(1, false, new Point(0,2)), new Queen(1, false, new Point(0,3)), new King(1, false, new Point(0,4)),  new Bishop(2, false, new Point(0,5)), new Knight(2, false, new Point(0,6)), new Rook(2, false, new Point(0,7))},
			{new Pawn(1, false, new Point(1,0)), new Pawn  (1, false, new Point(1,1)), new Pawn  (1, false, new Point(1,2)), new Pawn (1, false, new Point(1,3)), new Pawn(1, false, new Point(1,4)), new Pawn   (1, false, new Point(1,5)), new Pawn  (1, false, new Point(1,6)), new Pawn(1, false, new Point(1,7))},
			{new Empty(new Point(2, 0)), new Empty(new Point(2, 1)), new Empty(new Point(2,2)), new Empty(new Point(2,3)), new Empty(new Point(2,4)), new Empty(new Point(2, 5)), new Empty(new Point(2,6)), new Empty(new Point(2,7))},
			{new Empty(new Point(3, 0)), new Empty(new Point(3, 1)), new Empty(new Point(3,2)), new Empty(new Point(3,3)), new Empty(new Point(3,4)), new Empty(new Point(3, 5)), new Empty(new Point(3,6)), new Empty(new Point(3,7))},
			{new Empty(new Point(4, 0)), new Empty(new Point(4, 1)), new Empty(new Point(4,2)), new Empty(new Point(4,3)), new Empty(new Point(4,4)), new Empty(new Point(4, 5)), new Empty(new Point(4,6)), new Empty(new Point(4,7))},
			{new Empty(new Point(5, 0)), new Empty(new Point(5, 1)), new Empty(new Point(5,2)), new Empty(new Point(5,3)), new Empty(new Point(5,4)), new Empty(new Point(5, 5)), new Empty(new Point(5,6)), new Empty(new Point(5,7))},
			{new Pawn(1, true, new Point(6,0)), new Pawn  (1, true, new Point(6,1)), new Pawn  (1, true, new Point(6,2)), new Pawn (1, true, new Point(6,3)), new Pawn(1, true, new Point(6,4)), new Pawn   (1, true, new Point(6,5)), new Pawn  (1, true, new Point(6,6)), new Pawn(1, true, new Point(6,7))},
			{new Rook(1, true, new Point(7,0)), new Knight(1, true, new Point(7,1)), new Bishop(1, true, new Point(7,2)), new Queen(1, true, new Point(0,3)), new King(1, true, new Point(7,4)),  new Bishop(2, true, new Point(7,5)), new Knight(2, true, new Point(7,6)), new Rook (2, true, new Point(7,7))},

		};
		*/


		private King whiteKing,
					 blackKing;

		//Initial board strings
		private readonly static string[,] initial = new string[8, 8] {

			{ "br1", "bk1", "bb1", "bq1", "b_k", "bb2", "bk2", "br2" },
			{ "bp1", "bp2", "bp3", "bp4", "bp5", "bp6", "bp7", "bp8" },
			{ "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   " },
			{ "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   " },
			{ "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   " },
			{ "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   " },
			{ "wp1", "wp2", "wp3", "wp4", "wp5", "wp6", "wp7", "wp8" },
			{ "wr1", "wk1", "wb1", "wq1", "w_k", "wb2", "wk2", "wr2" }
		
		};


		//Initial board strings
		private readonly static string[,] tial = new string[8, 8]
		{
			{ "bq1", "   ", "   ", "wp7", "   ", "   ", "   ", "   " },
			{ "   ", "   ", "   ", "   ", "   ", "wp1", "   ", "   " },
			{ "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   " },
			{ "   ", "   ", "   ", "wr1", "   ", "   ", "   ", "   " },
			{ "wp2", "   ", "   ", "   ", "   ", "   ", "   ", "   " },
			{ "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   " },
			{ "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   " },
			{ "   ", "w_k", "   ", "   ", "   ", "   ", "b_k", "   " },
		};


		//Private board
		private Piece[][] board;


		//Zero-arg constructor initializes the board with this initial occupying map
		public Board() : this(Board.initial) { }


		public Board(string[,] strBoard)
		{
			//Initializing board
			this.board = new Piece[8][];
			for(int row = 0; row < 8; row++)
			{
				this.board[row] = new Piece[8];
				for(int col = 0; col < 8; col++)
				{
					string piece = strBoard[row, col];
					//depending on type, 
					char type = piece[1];
					if(type == 'r')
					{
						this.board[row][col] = new Rook(piece, new Point(row, col));
					}
					else if(type == 'k')
					{
						this.board[row][col] = new Knight(piece, new Point(row, col));
					}
					else if(type == 'b')
					{
						this.board[row][col] = new Bishop(piece, new Point(row, col));
					}
					else if(type == 'q')
					{
						this.board[row][col] = new Queen(piece, new Point(row, col));
					}
					else if(type == '_')
					{
						this.board[row][col] = new King(piece, new Point(row, col));
						//Setting the private king variables
						if(piece[0] == 'w')
						{
							this.whiteKing = (King) this.board[row][col];
						}
						else
						{
							this.blackKing = (King) this.board[row][col];
						}
					}
					else if(type == 'p')
					{
						this.board[row][col] = new Pawn(piece, new Point(row, col));
					}
					else
					{
						this.board[row][col] = new Empty(new Point(row, col));
					}
				}
			}
		}

		//Sets a piece to a location on the field
		public Piece SetPiece(Piece piece, Point point)
		{
			board[point.X][point.Y] = piece;
			piece.SetLoc(point);
			return piece;
		}

		//moves a piece from its current position to another
		//		returns overwritten piece
		public Piece MovePiece(Piece piece, Point moveTo)
		{
			Piece ret;
			Point oldPoint = piece.GetLoc();
			board[oldPoint.X][oldPoint.Y] = new Empty(oldPoint);
			piece.SetLoc(moveTo);
			ret = board[moveTo.X][moveTo.Y];
			board[moveTo.X][moveTo.Y] = piece;
			return ret;
		}

		//Gets a piece at a row/col location on the map
		public Piece GetPiece(Point point)
		{
			return board[point.X][point.Y];
		}

		//Method for returning a piece on the board based on string
		public Piece GetPiece(string piece)
		{
			//Returns the appropriate type of a piece based on their [1]'th character
			Func<char, Type> GetType = (char type) => {

				if(type == 'p')
				{
					return typeof(Pawn);
				}
				else if (type == 'r')
				{
					return typeof(Rook);
				}
				else if (type == 'b')
				{
					return typeof(Bishop);
				}
				else if (type == 'k')
				{
					return typeof(Knight);
				}
				else if (type == 'q')
				{
					return typeof(Queen);
				}
				else if (type == '_')
				{
					return typeof(King);
				}
				else
				{
					return typeof(Empty);
				}
			};
			
			//Lambda Function for determining if the color, type, and num of a piece matches those of the 
			//		piece we're looking for
			Func<char, Type, int, Piece, bool> FindPiece = (color, type, num, piece) =>
				 (piece.GetColor() == color && piece.GetType() == type && piece.GetNum() == num);

			char color = piece[0];
			Type type  = GetType(piece[1]);
			char num   = piece[2];
			//int  num   = (type == typeof(King)) ? 0 : ((int)piece[2]) - '0';

			//Searches each item in board to see if it matches the attributes of input string;
			foreach(Piece[] row in this.board)
			{
				foreach(Piece p in row)
				{
					if(FindPiece(color, type, num, p))
					{
						return p;
					}
				}
			}
			return null;
		}

		//Returns a list of all pieces on the board
		public List<Piece> GetPieceReport()
		{
			List<Piece> pieces = new List<Piece>();

			foreach(Piece[] row in this.board)
			{
				foreach(Piece element in row)
				{
					if(element.GetType() != typeof(Empty))
					{
						pieces.Add(element);
					}
				}
			}

			return pieces;
		}


		//Returns the two kings
		public (King, King) GetKings()
		{
			return (whiteKing, blackKing);
		}

		//Method for returning formatted board with moves shown
		public string ToString(List<Point> moves)
		{
			char letter = '8';
			string board = "         a    b    c    d    e    f    g    h\n";
			Point curPoint;
			for(int row = 0; row < 8; row++)
			{
				board += "   " + letter + "   ";
				letter--;
				for(int col = 0; col < 8; col++)
				{
					curPoint = new Point(row, col);
					//if curPoint is in movelist, draw an X, else draw the piece at that location
					board += ("[" + ((moves.Exists((point) => point.Equals(curPoint))) ? " X " : this.board[row][col].ToString()) + "]");
				}
				board += "\n";
			}


			return board;
		}

		//Draws the board
		public override string ToString()
		{
			char letter = '8';
			string board = "         a    b    c    d    e    f    g    h\n";
			foreach(Piece[] row in this.board)
			{
				board += "   " + letter + "   ";
				letter--;
				foreach(Piece element in row)
				{
					board += ("[" + element + "]");
				}
				board += "\n";
			}
			return board;
		}
	}
}