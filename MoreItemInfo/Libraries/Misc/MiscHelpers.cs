using System;
using Microsoft.Xna.Framework;


namespace MoreItemInfo.Libraries.Misc {
	class MiscHelpers {
		/// <summary>
		/// Renders a color as a hex code string.
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static string RenderColorHex( Color color ) {
			string r = ( (int)color.R ).ToString( "X2" );
			string g = ( (int)color.G ).ToString( "X2" );
			string b = ( (int)color.B ).ToString( "X2" );
			return r + g + b;
		}
	}
}
