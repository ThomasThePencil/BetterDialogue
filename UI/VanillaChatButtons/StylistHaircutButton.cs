using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class StylistHaircutButton : ChatButton
	{
		public override string Text(NPC npc, Player player) => Language.GetTextValue("GameUI.HairStyle");

		public override string Description(NPC npc, Player player) => "Bored with your scalp? Need a new \'do? " + npc.GivenName + "\'s the snazzy hair stylist for you!";

		public override bool IsActive(NPC npc, Player player) => npc.type == NPCID.Stylist;

		public override void OnClick(NPC npc, Player player)
		{
			Main.OpenHairWindow();
		}
	}
}
