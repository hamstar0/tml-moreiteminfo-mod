using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using MoreItemInfo.Libraries.Items;


namespace MoreItemInfo {
	class MoreItemInfoItem : GlobalItem {
		public static ISet<int> GetRecipeResults( int itemType ) {
			var mymod = MoreItemInfoMod.Instance;
			
			if( !mymod.ItemRecipeResults.ContainsKey(itemType) ) {
				mymod.ItemRecipeResults[itemType] = new HashSet<int>(
					Main.recipe.Where(
						r => r.requiredItem.Any(
							i => i?.active == true && i.type == itemType
						)
					).Select( r => r.createItem.type )
				);
			}

			return mymod.ItemRecipeResults[itemType];
		}



		////////////////

		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			this.AddPriceTooltip( item, tooltips );
			this.AddIngredientsListTip( MoreItemInfoItem.GetRecipeResults( item.type ), tooltips );
		}


		////

		public void AddIngredientsListTip( ISet<int> itemTypes, List<TooltipLine> tooltips ) {
			if( itemTypes.Count == 0 ) {
				return;
			}

			var config = ModContent.GetInstance<MoreItemInfoConfig>();
			int codesPerLine = config.RecipesPerLine;
			int maxCodes = Math.Min( itemTypes.Count, config.MaxRecipesToList );
			int[] itemCodes = itemTypes.ToArray();

			int lineCount = (int)Math.Ceiling( (double)(maxCodes+2) / (double)codesPerLine );
			string[] craftsLines = new string[ lineCount ];

			int idx = 0;

			for( int line=0; line<lineCount; line++ ) {
				int max = line == 0
					? codesPerLine - 2
					: codesPerLine;
				if( (idx + max) > maxCodes ) {
					max = maxCodes - idx;
				}

				craftsLines[line] = "";

				for( int i=0; i<max; i++ ) {
					craftsLines[line] += "[i/s1:" + itemCodes[idx] + "] ";
					idx++;
				}
			}

			for( int i=0; i<craftsLines.Length; i++ ) {
				string text = i == 0
					? "Crafts: "+craftsLines[i]
					: craftsLines[i];
				var tip = new TooltipLine( this.mod, "MoreItemInfoTip_" + i, text );

				tooltips.Add( tip );
			}

			if( maxCodes < itemTypes.Count ) {
				int remaining = itemTypes.Count - maxCodes;
				var tip = new TooltipLine( this.mod, "MoreItemInfoTip_Post", "...and "+remaining+" more" );

				tooltips.Add( tip );
			}
		}


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