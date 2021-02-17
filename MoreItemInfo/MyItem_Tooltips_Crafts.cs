using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;


namespace MoreItemInfo {
	partial class MoreItemInfoItem : GlobalItem {
		public static IDictionary<int, bool> GetRecipesCraftedByItem( int itemType ) {
			var mymod = MoreItemInfoMod.Instance;
			if( mymod.ItemCraftedFrom.ContainsKey( itemType ) ) {
				return mymod.ItemCraftedFrom[itemType];
			}

			IEnumerable<(int recipeItemType, bool)> recipes = Main.recipe.Where(
				r => r.requiredItem.Any(
					i => i?.active == true && i.type == itemType
				)
			).Select( r => (r.createItem.type, false) );

			mymod.ItemCraftedFrom[itemType] = recipes.ToDictionary(
				kv => kv.recipeItemType,
				kv => false
			);

			IEnumerable<RecipeGroup> grpsOfItem = RecipeGroup.recipeGroups.Values
				.Where( grp => grp.ContainsItem(itemType) );

			foreach( RecipeGroup rg in grpsOfItem ) {
				int rgItemType = rg.IconicItemIndex;

				IEnumerable<int> rgCraftedFrom = Main.recipe.Where(
					r => r.requiredItem.Any(
						i => i?.active == true && i.type == rgItemType
					)
				).Select( r => r.createItem.type );

				foreach( int recipeItemType in rgCraftedFrom ) {
					mymod.ItemCraftedFrom[itemType][recipeItemType] = true;
				}
			}

			return mymod.ItemCraftedFrom[itemType];
		}



		////////////////

		public void AddCraftsIntoListTip( ISet<(int recipeItemType, bool isRecipeGroup)> itemTypes, List<TooltipLine> tooltips ) {
			if( itemTypes.Count == 0 ) {
				return;
			}

			var config = ModContent.GetInstance<MoreItemInfoConfig>();
			if( !config.ShowRecipesCraftingIntoItem ) {
				return;
			}

			int codesPerLine = config.RecipesPerLine;
			int maxCodes = Math.Min( itemTypes.Count, config.MaxRecipeResultsToList );
			(int recipeItemType, bool isRecipeGroup)[] itemCodes = itemTypes.ToArray();

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
					if( itemCodes[idx].isRecipeGroup ) {
						craftsLines[line] += "Any [i/s1:" + itemCodes[idx] + "] ";
					} else {
						craftsLines[line] += "[i/s1:" + itemCodes[idx] + "] ";
					}
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