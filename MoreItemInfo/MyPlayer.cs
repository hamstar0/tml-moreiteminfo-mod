using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;


namespace MoreItemInfo {
	class MoreItemInfoPlayer : ModPlayer {
		public bool DisplayItemInfo { get; private set; } = true;




		////////////////

		public override void ProcessTriggers( TriggersSet triggersSet ) {
			if( MoreItemInfoMod.Instance.ToggleItemInfo.JustPressed ) {
				this.DisplayItemInfo = !this.DisplayItemInfo;
			}
		}
	}
}