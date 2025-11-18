using System;
using System.Drawing;

namespace Plugin.Core.Utility
{
	public class ColorUtil
	{
		public static Color White;

		public static Color Black;

		public static Color Red;

		public static Color Green;

		public static Color Blue;

		public static Color Yellow;

		public static Color Fuchsia;

		public static Color Cyan;

		public static Color Silver;

		public static Color LightGrey;

		static ColorUtil()
		{
			ColorUtil.White = Color.FromArgb(255, ColorTranslator.FromHtml("#FFFFFF"));
			ColorUtil.Black = Color.FromArgb(255, ColorTranslator.FromHtml("#000000"));
			ColorUtil.Red = Color.FromArgb(255, ColorTranslator.FromHtml("#FF0000"));
			ColorUtil.Green = Color.FromArgb(255, ColorTranslator.FromHtml("#00FF00"));
			ColorUtil.Blue = Color.FromArgb(255, ColorTranslator.FromHtml("#0000FF"));
			ColorUtil.Yellow = Color.FromArgb(255, ColorTranslator.FromHtml("#FFFF00"));
			ColorUtil.Fuchsia = Color.FromArgb(255, ColorTranslator.FromHtml("#FF00FF"));
			ColorUtil.Cyan = Color.FromArgb(255, ColorTranslator.FromHtml("#00FFFF"));
			ColorUtil.Silver = Color.FromArgb(255, ColorTranslator.FromHtml("#C0C0C0"));
			ColorUtil.LightGrey = Color.FromArgb(255, ColorTranslator.FromHtml("#D3D3D3"));
		}

		public ColorUtil()
		{
		}
	}
}