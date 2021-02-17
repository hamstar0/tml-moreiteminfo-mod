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

		internal IDictionary<int, IDictionary<int, bool>> ItemCraftedFrom { get; private set; } = new Dictionary<int, IDictionary<int, bool>>();

		internal IDictionary<int, ISet<Recipe>> ItemCraftsInto { get; private set; } = new Dictionary<int, ISet<Recipe>>();



		////////////////

		public MoreItemInfoMod() {
			MoreItemInfoMod.Instance = this;
		}

		public override void Load() {
			MoreItemInfoMod.Instance = this;
		}

		public override void Unload() {
			MoreItemInfoMod.Instance = null;
		}
	}
}