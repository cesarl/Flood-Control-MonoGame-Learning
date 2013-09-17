using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace floodControl
{
	public class GamePiece
	{

		public static string[] pieceTypes = 
		{
			"Left,Right",
			"Top,Bottom",
			"Left,Top",
			"Top,Right",
			"Right,Bottom",
			"Bottom,Left",
			"Empty"
		};
		public const int h = 40;
		public const int w = 40;
		public const int playableIndex = 5;
		public const int emptyIndex = 6;
		public const int offsetX = 1;
		public const int offsetY = 1;
		public const int paddingX = 1;
		public const int paddingY = 1;
		private string type_ = "";
		private string suffix_ = "";

		public string Type
		{
			get{return type_;}
		}

		public string Suffix
		{
			get {return suffix_;}
		}

		public GamePiece (string type, string suffix)
		{
			type_ = type;
			suffix_ = suffix;
		}

		public GamePiece(string type)
		{
			type_ = type;
			suffix_ = "";
		}

		public void SetPiece(string type, string suffix)
		{
			type_ = type;
			suffix_ = suffix;
		}

		public void SetPiece(string type)
		{
			SetPiece(type, "");
		}

		public void AddSuffix(string suffix)
		{
			if (!suffix_.Contains(suffix))
				suffix_ += suffix;
		}

		public void RemoveSuffix(string suffix)
		{
			suffix_ = suffix_.Replace(suffix, "");
		}

		public void RotatePiece(bool clockwise)
		{
			switch (type_)
			{
			case "Left,Right":
				type_ = "Top,Bottom";
				break;
			case "Top,Bottom":
				type_ = "Left,Right";
				break;
			case "Left,Top":
				if (clockwise)
				{
					type_ = "Top,Right";
				}
				else
				{
					type_ = "Bottom,Left";
				}
				break;
			case "Top,Right":
				if (clockwise)
				{
					type_ = "Right,Bottom";
				}
				else
				{
					type_ = "Left,Top";
				}
				break;
			case "Right,Bottom":
				if (clockwise)
				{
					type_ = "Bottom,Left";
				}
				else
				{
					type_ = "Top,Right";
				}
				break;
			case "Bottom,Left":
				if (clockwise)
				{
					type_ = "Left,Top";
				}
				else
				{
					type_ = "Right,Bottom";
				}
				break;
			case "Empty":
				break;
			}
		}

		public string[] GetOtherEnds(string startingEnd)
		{
			List<string> opposites = new List<string>();
			foreach (string end in type_.Split(','))
			{
				if (end != startingEnd)
					opposites.Add(end);
			}
			return opposites.ToArray(); 
		}

		public bool HasConnector(string direction)
		{
			return type_.Contains(direction);
		}

		public Rectangle GetRect()
		{
			int x = offsetX;
			int y = offsetY;
			if (suffix_.Contains("W"))
				x += w + paddingX;
			y += (Array.IndexOf(pieceTypes, type_) * h + paddingY);
			return new Rectangle(x, y, w, h); 
		}
	}
}

