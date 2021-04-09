using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Chess
{
	class Chess
	{
		private static Board board;

		//Stores player strings in easily-accessible array
		private readonly static string[] PLAYERS = new string[2] { "White", "Black" };

		private readonly static char[] PLAYTYPES = new char[2] { 'w', 'b' };

		//Stores input split regex in regex
		private readonly static Regex verify = new Regex("^(([wb][qp][0-9])|([wb][krb][12])|([wb]_k))(,[a-h]-[1-8])?$");
		
		//Stores turns
		private static int turns;

		private static King whiteKing,
						    blackKing;

		public static void Main(string[] args)
		{
			int    wWins   = 0, 
				   bWins   = 0;
			bool   playing = true,
				   inGame  = true;
			string winner  = null;
			string input   = null;

			//Outer loop restarts game if players want to keep going
			while(playing)
			{
				Piece.whiteQueen = 2;
				Piece.blackQueen = 2;

				//Setting original game state
				board = new Board();
				(whiteKing, blackKing) = board.GetKings();
				turns = 0;
				inGame = true;

				//Inner loop progresses turns
				while(inGame)
				{
					inGame = turn();
					Console.WriteLine("turned");
				}
				
				//Setting winner and incrementing wWins or bWins
				winner = PLAYERS[turns % 2];    
				_ = (turns % 2 == 0) ? wWins++ : bWins++;
				
				//Displaying win/loss message
				Console.Clear();
				Console.WriteLine("Check mate!");
				Console.WriteLine(winner + "wins!");
				Console.WriteLine(wWins + "-" + bWins);
				
				//Play again prompt
				Console.WriteLine("Play again?");
				Console.Write("y / n : ");
				input = Console.ReadLine();
				playing = (input.ToUpper()[0] == 'Y');

			}
		}
		//Turn function
		public static bool turn()
		{
			//determine current player
			string player = PLAYERS[turns % 2];

			//Draw board and prompt
			Console.Clear();
			Console.WriteLine(board);
			Console.WriteLine(String.Format("Where would you like to move, {0}?", player));
			Console.WriteLine("\tType 'h' for help");

			//request input
			string input = Console.ReadLine();

			//Lambda for printing help menu;
			Action printH = () => {
				Console.WriteLine("Type [piece],[location] to move your piece.");
				Console.WriteLine("\tEx: 'wp1,a-3'");
				Console.WriteLine("Type [piece] to see a piece's moves.");
				Console.WriteLine("\tEx: 'wp1'");
				//After, printing instructions, request another input and
				//		return to original logic flow
				input = Console.ReadLine();
			};

			//Check for 'h' (or 'H')
			if (input.ToUpper().Equals("H"))
			{
				printH();
			}

			//loop variables
			bool validMove = false;
			string message = null;

			//Do, while the user has not entered a valid input, or moved
			do
			{

				while (!verify.Match(input).Success || message != null)
				{
					if (message != null)
					{
						Console.WriteLine(message);
					}
					Console.WriteLine("Invalid input!  Try again!");
					message = null;
					input = Console.ReadLine();
					//Check for 'h' (or 'H')
					if (input.ToUpper().Equals("H"))
					{
						printH();
					}
				}
				//If input contains a "," and it passed the Regex match, then it must be a movement action
				if (input.Contains(","))
				{
					string piece;
					string move;
					string[] splits = input.Split(",");
					piece = splits[0];
					move = splits[1];

					//Retrieving piece from board
					Piece p = Chess.GetBoard().GetPiece(piece);
					if (p != null)
					{
						if (p.GetColor() == PLAYTYPES[turns % 2])
						{
							//Parsing moveTo location
							int col = ((int)move[0]) - 'a';
							int row = 8 - (((int)move[2]) - '0');
							Point target = new Point(row, col);

							//Getting piece's moves
							List<Point> moves = p.GetMoves();


							//Searching through movelist for a point matching the moveTo coordinate
							bool found = moves.Exists((point) => point.Equals(target));

							//movepiece
							if (found)
							{

								bool moved;
								(moved, message) = MovePiece(p, target);

								//If piece was able to move
								if (moved)
								{

									if(p.GetType() == typeof(Pawn))
									{
										((Pawn) p).SetMoved(moved);

										//A pawn cannot move backwards, so the only time 
										//		any pawn can reach the edge of the field is when it
										//		hits the opposite edge, turn the piece into a queen
										if(p.GetLoc().X == 7 || p.GetLoc().X == 0)
										{
											Chess.GetBoard().SetPiece(new Queen(p.GetColor(), p.GetLoc()), p.GetLoc());
										}

									}

									if (message != null && message.Equals("Check mate!"))
									{
										return false;
									}
									validMove = true;
								}
								//Else if piece couldn't move (checking self)!
								//		move was not valid, try again
								else { }
							}
							else
							{
								message = "Cannot move there!"; 
								//return false;
							}
						}
						else
						{
							message = "Not your piece";
							//return false;
						}
					}
					//Invalid move location
					else
					{
						message = String.Format("Cannot move there!\n\t" +
													"Type '{0}' to view the moves of piece {0}", piece);
					}
				}
				//Else, printing moves of piece
				else
				{
					//Getting piece
					Piece piece = Chess.GetBoard().GetPiece(input);

					//Getting piece's moves
					List<Point> moves = piece.GetMoves();


					string str = Chess.GetBoard().ToString(moves);


					//Prining new board
					Console.Clear();
					Console.WriteLine(Chess.GetBoard().ToString(moves));
					Console.WriteLine("Type 'x' to remove x's!");
					input = Console.ReadLine();
					//Check for 'h' (or 'H')
					if (input.ToUpper().Equals("H"))
					{
						printH();
					}
					else if (input.ToUpper().Equals("X"))
					{
						//Printing cleared board
						Console.Clear();
						Console.WriteLine(Chess.GetBoard());
						Console.WriteLine("Continue!");
						input = Console.ReadLine();
					}
				}
			}
			while (!validMove);


			turns++;
			return true;
		}
		//Method to move valid piece to valid location
		//		returns (true, null) if move was successful and neither king was checked
		//		returns (true, " ... ") if enemy king was checked or checkmated
		private static (bool, string) MovePiece(Piece piece, Point target)
		{
			string message = null;
			Point oldPoint = piece.GetLoc();
			Piece overwritten = Chess.GetBoard().MovePiece(piece, target);

			bool blackChecked = blackKing.CheckCheck();
			bool whiteChecked = whiteKing.CheckCheck();

			Func<bool, char, char, bool> CheckSelf = (isChecked, mine, kings) => isChecked && (mine == kings);

			bool checkSelf = CheckSelf(blackChecked, piece.GetColor(), 'b') || CheckSelf(whiteChecked, piece.GetColor(), 'w');

			//Return false with error message 'That would check your own king!'
			//		return board to original state
			if(checkSelf)
			{
				//Restoring board to original position
				Chess.GetBoard().MovePiece(piece, oldPoint);
				Chess.GetBoard().MovePiece(overwritten, target);

				return (false, "That move would check your king!");
			}
			else
			{

				//Now that we know we are not checked, check if enemy is checkmated
				bool enemyChecked;
				King checkedKing;
				(enemyChecked, checkedKing) = (blackChecked) ? (true, blackKing) : (whiteChecked, whiteKing);

				//If the enemy is checked, check if they are also checkmated
				if(enemyChecked)
				{
					message = "Enemy king checked!";
					bool enemeyCheckMated = checkedKing.CheckMate();
					
					if(enemeyCheckMated)
					{
						message = "Check mate!";
						return (false, message);
					}

				}
			}
			return (true, message);
		}

		//board instance getter
		public static Board GetBoard()
		{
			return board;
		}
	}
}
