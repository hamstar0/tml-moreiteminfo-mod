using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using MoreItemInfo.Libraries.Items;


namespace MoreItemInfo {
	partial class MoreItemInfoItem : GlobalItem {
		private void AddPriceTooltip( Item item, List<TooltipLine> tooltips ) {
			if( !ModContent.GetInstance<MoreItemInfoConfig>().ShowPricePerItem ) {
				return;
			}
			if( Main.npcShop != 0 ) {
				return;
			}
			if( item.value == 0 ) {
				return;
			}

			long unitSellValue = item.value / 5;
			long stackSellValue = unitSellValue * item.stack;

			string[] renderedSellValueDenoms = ItemMoneyHelpers.RenderMoneyDenominations( stackSellValue, true, true );
			string renderedSellValue = string.Join( ", ", renderedSellValueDenoms );

			string tipText = "Sells for " + renderedSellValue;

			if( item.stack > 1 ) {
				string[] renderedUnitSellValueDenoms = ItemMoneyHelpers.RenderMoneyDenominations( unitSellValue, true, true );
				for( int i = 0; i < renderedUnitSellValueDenoms.Length; i++ ) {
					string[] segs = renderedUnitSellValueDenoms[i].Split( ' ' );
					renderedUnitSellValueDenoms[i] = segs[0] + segs[1][0] + "]";
				}

				string renderedUnitSellValue = string.Join( ", ", renderedUnitSellValueDenoms );

				tipText += " (" + renderedUnitSellValue + " each)";
			}

			var tip = new TooltipLine( this.mod, "MoreItemInfoValue", tipText );
			tooltips.Add( tip );
		}
	}
}