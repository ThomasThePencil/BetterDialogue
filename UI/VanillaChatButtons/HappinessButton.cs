using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class HappinessButton : ChatButton
	{
		public override string Text(NPC npc, Player player) => Language.GetTextValue("UI.NPCCheckHappiness");

		public override double Priority => 100.0;

		public override string Description(NPC npc, Player player) => "How happy might " + npc.GivenName + " be with their current living arrangements? Only one way to find out...";

		public override bool IsActive(NPC npc, Player player)
		{
			if (NPC.CanShowHomelessText(Main.LocalPlayer.talkNPC))
				return false;

			return base.LocalPlayer.currentShoppingSettings.HappinessReport != "";
		}

		public override void OnClick(NPC npc, Player player)
		{
			SoundEngine.PlaySound(SoundID.MenuTick);
			Main.npcChatCornerItem = 0;
			Main.npcChatText = player.currentShoppingSettings.HappinessReport;
		}
	}
}
