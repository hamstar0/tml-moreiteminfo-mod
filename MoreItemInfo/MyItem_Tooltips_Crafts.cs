using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using MoreItemInfo.Libraries.Items;


namespace MoreItemInfo {
	partial class MoreItemInfoItem : GlobalItem {
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
	}
}