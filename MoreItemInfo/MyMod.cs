using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace MoreItemInfo {
	public class MoreItemInfoMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-moreiteminfo-mod";


		////////////////

		public static MoreItemInfoMod Instance { get; private set; }



		////////////////

		internal IDictionary<int, ISet<int>> ItemCraftedFrom { get; private set; } = new Dictionary<int, ISet<int>>();

		internal IDictionary<int, ISet<Recipe>> ItemCraftsInto { get; private set; } = new Dictionary<int, ISet<Recipe>>();

		internal ModHotKey ToggleItemInfo { get; private set; }



		////////////////

		public MoreItemInfoMod() {
			MoreItemInfoMod.Instance = this;
		}

		public override void Load() {
			MoreItemInfoMod.Instance = this;

			this.ToggleItemInfo = this.RegisterHotKey( "Toggle Item Info", "I" );
		}

		public override void Unload() {
			MoreItemInfoMod.Instance = null;
		}
	}
}