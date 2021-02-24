using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace MoreItemInfo {
	partial class MoreItemInfoItem : GlobalItem {
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<MoreItemInfoPlayer>();
			if( !myplayer.DisplayItemInfo ) {
				return;
			}

			switch( item.type ) {
			case ItemID.CopperCoin:
			case ItemID.SilverCoin:
			case ItemID.GoldCoin:
			case ItemID.PlatinumCoin:
				break;
			default:
				this.AddPriceTooltip( item, tooltips );
				break;
			}
			this.AddCraftsIntoListTip( MoreItemInfoItem.GetRecipesCraftedByItem(item.type), tooltips );
			this.AddCraftedByListTip( MoreItemInfoItem.GetRecipesCraftingIntoItem(item.type), tooltips );
		}
	}
}