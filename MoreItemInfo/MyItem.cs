using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace MoreItemInfo {
	partial class MoreItemInfoItem : GlobalItem {
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<MoreItemInfoPlayer>();
			if( !myplayer.DisplayItemInfo ) {
				return;
			}

			this.AddPriceTooltip( item, tooltips );
			this.AddCraftsIntoListTip( MoreItemInfoItem.GetRecipesCraftedByItem(item.type), tooltips );
			this.AddCraftedByListTip( MoreItemInfoItem.GetRecipesCraftingIntoItem(item.type), tooltips );
		}
	}
}