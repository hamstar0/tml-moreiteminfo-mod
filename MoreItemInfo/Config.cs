using System;
using System.ComponentModel;
using Terraria;
using Terraria.ModLoader.Config;


namespace MoreItemInfo {
	public class MoreItemInfoConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ClientSide;



		////

		[DefaultValue( 10 )]
		public int RecipesPerLine { get; set; } = 10;


		[DefaultValue( 100 )]
		public int MaxRecipesToList { get; set; } = 100;


		[DefaultValue( true )]
		public bool ShowPricePerItem { get; set; } = true;
	}
}