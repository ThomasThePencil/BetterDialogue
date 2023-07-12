using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class PainterDecorShopButton : ChatButton
	{
		public override string Text(NPC npc, Player player) => Language.GetTextValue("GameUI.PainterDecor");

		public override double Priority => 11.0;

		public override string Description(NPC npc, Player player) => "See what " + npc.GivenName + " has for sale.\n...again.";

		public override bool IsActive(NPC npc, Player player) => npc.type == NPCID.Painter;

		public override void OnClick(NPC npc, Player player)
		{
			SoundEngine.PlaySound(SoundID.MenuTick);
			Main.playerInventory = true;
			Main.stackSplit = 9999;
			Main.npcChatText = "";
			Main.SetNPCShopIndex(1);
			Main.instance.shop[Main.npcShop].SetupShop(25);
		}
	}
}
