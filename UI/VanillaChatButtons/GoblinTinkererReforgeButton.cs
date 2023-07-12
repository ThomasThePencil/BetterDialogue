using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI.Gamepad;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class GoblinTinkererReforgeButton : ChatButton
	{
		public override string Text(NPC npc, Player player) => Lang.inter[19].Value;

		public override double Priority => 10.0;

		public override string Description(NPC npc, Player player) => npc.GivenName + " can reforge your items for a price, making them stronger or weaker depending on whether or not he messes it up.";

		public override bool IsActive(NPC npc, Player player) => npc.type == NPCID.GoblinTinkerer;

		public override void OnClick(NPC npc, Player player)
		{
			Main.npcChatCornerItem = 0;
			SoundEngine.PlaySound(SoundID.MenuTick);
			Main.playerInventory = true;
			Main.npcChatText = "";
			Main.InReforgeMenu = true;
			UILinkPointNavigator.GoToDefaultPage();
		}
	}
}
