using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace floodControl
{
	public class GameBoard
	{
		Random rand = new Random();
		public const int w = 8;
		public const int h = 10;
		private GamePiece[,] pieces_ = new GamePiece[w,h];
		private List<Vector2> waterTracker = new List<Vector2>();

		public GameBoard ()
		{
			ClearBoard();
		}

		public void ClearBoard()
		{
			for (int x = 0; x < w; ++x)
			{
				for (int y = 0; y < h; ++y)
				{
					pieces_[x, y] = new GamePiece("Empty");
				}
			}
		}

		public void RotatePiece(int x, int y, bool clockwise)
		{
			pieces_[x, y].RotatePiece(clockwise);
		}

		public Rectangle GetRect(int x, int y)
		{
			return pieces_[x, y].GetRect();
		}

		public string GetSquare(int x, int y)
		{
			return pieces_[x, y].Type;
		}

		public void SetSquare(int x, int y, string type)
		{
			pieces_[x, y].SetPiece(type);
		}

		public bool HasConnector(int x, int y, string direction)
		{
			return pieces_[x, y].HasConnector(direction);
		}

		public void RandomPiece(int x, int y)
		{
			pieces_[x, y].SetPiece(GamePiece.pieceTypes[rand.Next(0, GamePiece.playableIndex + 1)]);
		}
	}

}

