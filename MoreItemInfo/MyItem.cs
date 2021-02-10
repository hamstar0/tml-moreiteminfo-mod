using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using MoreItemInfo.Libraries.Items;


namespace MoreItemInfo {
	partial class MoreItemInfoItem : GlobalItem {
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			this.AddPriceTooltip( item, tooltips );
			this.AddIngredientsListTip( MoreItemInfoItem.GetRecipeResults( item.type ), tooltips );
		}
	}
}