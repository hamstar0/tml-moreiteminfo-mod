using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;


namespace MoreItemInfo {
	partial class MoreItemInfoItem : GlobalItem {
		public static ISet<Recipe> GetRecipesCraftingIntoItem( int itemType ) {
			var mymod = MoreItemInfoMod.Instance;
			if( mymod.ItemCraftsInto.ContainsKey(itemType) ) {
				return mymod.ItemCraftsInto[itemType];
			}

			mymod.ItemCraftsInto[ itemType ] = new HashSet<Recipe>(
				Main.recipe.Where( r => r.createItem.type == itemType )
			);

			return mymod.ItemCraftsInto[itemType];
		}



		////////////////

		public void AddCraftedByListTip( ISet<Recipe> craftsMe, List<TooltipLine> tooltips ) {
			if( craftsMe.Count == 0 ) {
				return;
			}

			var config = ModContent.GetInstance<MoreItemInfoConfig>();
			if( !config.ShowRecipesCraftedByItem ) {
				return;
			}

			var singles = new List<string>();
			var multis = new List<string>();

			foreach( Recipe recipe in craftsMe ) {
				IEnumerable<string> recipeIngredientsEach = recipe.requiredItem
					.Where( i => !i.IsAir )
					.Select( i => "[i/s"+i.stack+":"+i.type+"] " );
				string recipeIngredients = string.Join( "", recipeIngredientsEach );

				if( recipeIngredientsEach.Count() > 1 ) {
					multis.Add( recipeIngredients );
				} else {
					singles.Add( recipeIngredients );
				}
			}

			string prefix = "Crafted from: ";

			if( singles.Count > 0 ) {
				do {
					int range = Math.Min( config.RecipesPerLine, singles.Count );
					List<string> singlesChunk = singles.GetRange( 0, range );
					singles.RemoveRange( 0, range );

					var tip = new TooltipLine( this.mod, "MoreItemInfoCraftsMe_Singles", prefix + string.Join( "or", singlesChunk ) );
					tooltips.Add( tip );

					prefix = "  ";
				} while( singles.Count > 0 );
			}

			int multiCount = multis.Count;
			if( multiCount > config.MaxRecipesToList ) {
				multis = multis.GetRange( 0, config.MaxRecipesToList - 1 );
			}

			int line = 0;
			foreach( string recipe in multis ) {
				var tip = new TooltipLine( this.mod, "MoreItemInfoCraftsMe_" + line, prefix + recipe );
				tooltips.Add( tip );

				prefix = "  ";
				line++;
			}

			if( multiCount > multis.Count ) {
				int remaining = multiCount - (config.MaxRecipesToList - 1);
				var tip = new TooltipLine( this.mod, "MoreItemInfoCraftsMe_TooMany", prefix + "...and "+remaining+" more" );
				tooltips.Add( tip );
			}
		}
	}
}