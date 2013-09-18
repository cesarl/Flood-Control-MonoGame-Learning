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

		public void FillFromAbove(int x, int y)
		{
			int row = y - 1;
			while (row >= 0)
			{
				if (GetSquare(x, row) != "Empty")
				{
					SetSquare(x, y, GetSquare(x, row));
					SetSquare(x, row, "Empty");
					row = -1;
				}
				--row;
			}
		}

		public void Generate(bool dropSquares)
		{
			if (dropSquares)
			{
				for (int x = 0; x < w; ++x)
				{
					for (int y = h - 1; y >= 0; --y)
					{
						if (GetSquare(x, y) == "Empty")
						{
							FillFromAbove(x, y);
						}
					}
				}
			}
			for (int y = 0; y < h; ++y)
			{
				for (int x = 0; x < w; ++x)
				{
					if (GetSquare(x, y) == "Empty")
					{
						RandomPiece(x, y);
					}
				}
			}
		}

		public void ResetWater()
		{
			for (int y = 0; y < h; ++y)
			{
				for (int x = 0; x < w; ++x)
				{
					pieces_[x, y].RemoveSuffix("W");
				}
			}
		}

		public void FillPiece(int x, int y)
		{
			pieces_[x, y].AddSuffix("W");
		}

		public void PropagateWater(int x, int y, string fromDirecton)
		{
			if ((y >= 0) && (y < h) && (x >= 0) && (x < w))
			{
				if (pieces_[x, y].HasConnector(fromDirecton)
				    && !pieces_[x, y].Suffix.Contains("W"))
				{
					FillPiece(x, y);
					waterTracker.Add(new Vector2(x, y));
					foreach(string end in pieces_[x, y].GetOtherEnds(fromDirecton))
					{
						switch (end)
						{
						case "Left":
							PropagateWater(x - 1, y, "Right");
							break;
						case "Right":
							PropagateWater(x + 1, y, "Left");
							break;
						case "Top":
							PropagateWater(x, y - 1, "Bottom");
							break;
						case "Bottom":
							PropagateWater(x, y + 1, "Top");
							break;
						}
					}
				}
			}
		}

		public List<Vector2> GetWaterChain(int y)
		{
			waterTracker.Clear();
			PropagateWater(0, y, "Left");
			return waterTracker;
		}
	}
}

