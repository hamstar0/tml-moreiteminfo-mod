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

		internal IDictionary<int, ISet<int>> ItemRecipeResults { get; private set; } = new Dictionary<int, ISet<int>>();



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