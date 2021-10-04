using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;


namespace MoreItemInfo {
	partial class MoreItemInfoItem : GlobalItem {
		public static ISet<int> GetRecipesCraftedByItem( int itemType ) {
			var mymod = MoreItemInfoMod.Instance;
			if( mymod.ItemCraftedFrom.ContainsKey( itemType ) ) {
				return mymod.ItemCraftedFrom[itemType];
			}

			//

			IEnumerable<int> recipes = Main.recipe.Where(
				r => {
					if( !RecipeHooks.RecipeAvailable(r) ) {
						return false;
					}
					return r.requiredItem.Any(
						i => i?.active == true && i.type == itemType
					);
				}
			).Select( r => r.createItem.type );

			mymod.ItemCraftedFrom[itemType] = new HashSet<int>( recipes );

			//

			IEnumerable<RecipeGroup> grpsOfItem = RecipeGroup.recipeGroups.Values
				.Where( grp => grp.ContainsItem(itemType) );
//MoreItemInfoMod.Instance.Logger.Info( "grps of "+ItemID.GetUniqueKey(itemType)+": "
//	+string.Join(", ", grpsOfItem.Select(g=>ItemID.GetUniqueKey(rg.ValidItems[g.IconicItemIndex])) ) );

			//

			foreach( RecipeGroup rg in grpsOfItem ) {
				int rgItemType = rg.ValidItems[ rg.IconicItemIndex ];

				IEnumerable<int> rgCraftedFrom = Main.recipe.Where(
					r => {
						if( !RecipeHooks.RecipeAvailable(r) ) {
							return false;
						}
						return r.requiredItem.Any(
							i => i?.active == true && i.type == rgItemType
						);
					}
				).Select( r => r.createItem.type );

				mymod.ItemCraftedFrom[itemType]
					.UnionWith( rgCraftedFrom );
			}

			//

			return mymod.ItemCraftedFrom[itemType];
		}



		////////////////

		public void AddCraftsIntoListTip( ISet<int> recipeItemTypes, List<TooltipLine> tooltips ) {
			int recipeCount = recipeItemTypes.Count;
			if( recipeCount == 0 ) {
				return;
			}

			var config = ModContent.GetInstance<MoreItemInfoConfig>();
			if( !config.ShowRecipesCraftingIntoItem ) {
				return;
			}

			int codesPerLine = config.RecipesPerLine;
			int maxCodes = Math.Min( recipeCount, config.MaxRecipeResultsToList );
			int[] itemCodes = recipeItemTypes.ToArray();

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

			if( maxCodes < recipeCount ) {
				int remaining = recipeCount - maxCodes;
				var tip = new TooltipLine( this.mod, "MoreItemInfoCraftsInto_Post", "...and "+remaining+" more" );

				tooltips.Add( tip );
			}
		}
	}
}