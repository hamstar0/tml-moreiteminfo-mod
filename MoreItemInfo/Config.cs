using System;
using System.ComponentModel;
using Terraria;
using Terraria.ModLoader.Config;


namespace MoreItemInfo {
	public class MoreItemInfoConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ClientSide;



		////

		[Range( 1, 100 )]
		[DefaultValue( 10 )]
		public int RecipesPerLine { get; set; } = 10;


		[Range( 1, 200 )]
		[DefaultValue( 50 )]
		public int MaxRecipesToList { get; set; } = 50;

		////

		[DefaultValue( true )]
		public bool ShowRecipesCraftingIntoItem { get; set; } = true;

		[DefaultValue( true )]
		public bool ShowRecipesCraftedByItem { get; set; } = true;

		[DefaultValue( true )]
		public bool ShowPricePerItem { get; set; } = true;
	}
}