using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;


namespace MoreItemInfo {
	partial class MoreItemInfoItem : GlobalItem {
		public static ISet<int> GetRecipesCraftedByItem( int itemType ) {
			var mymod = MoreItemInfoMod.Instance;
			
			if( !mymod.ItemCraftedFrom.ContainsKey(itemType) ) {
				mymod.ItemCraftedFrom[itemType] = new HashSet<int>(
					Main.recipe.Where(
						r => r.requiredItem.Any(
							i => i?.active == true && i.type == itemType
						)
					).Select( r => r.createItem.type )
				);
			}

			return mymod.ItemCraftedFrom[itemType];
		}



		////////////////

		public void AddCraftsIntoListTip( ISet<int> itemTypes, List<TooltipLine> tooltips ) {
			if( itemTypes.Count == 0 ) {
				return;
			}

			var config = ModContent.GetInstance<MoreItemInfoConfig>();
			if( !config.ShowRecipesCraftingIntoItem ) {
				return;
			}

			int codesPerLine = config.RecipesPerLine;
			int maxCodes = Math.Min( itemTypes.Count, config.MaxRecipeResultsToList );
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
				var tip = new TooltipLine( this.mod, "MoreItemInfoCraftsInto_" + i, text );

				tooltips.Add( tip );
			}

			if( maxCodes < itemTypes.Count ) {
				int remaining = itemTypes.Count - maxCodes;
				var tip = new TooltipLine( this.mod, "MoreItemInfoCraftsInto_Post", "...and "+remaining+" more" );

				tooltips.Add( tip );
			}
		}
	}
}