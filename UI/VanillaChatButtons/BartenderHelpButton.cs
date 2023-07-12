using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class BartenderHelpButton : ChatButton
	{
		public override string Text(NPC npc, Player player) => Language.GetTextValue("UI.BartenderHelp");

		public override double Priority => 15.0;

		public override string Description(NPC npc, Player player) => "Perhaps " + npc.GivenName + " knows a thing or two about strange invaders from another realm?";

		public override bool IsActive(NPC npc, Player player) => npc.type == NPCID.DD2Bartender;

		public override void OnClick(NPC npc, Player player)
		{
			SoundEngine.PlaySound(SoundID.MenuTick);
			Main.npcChatText = Lang.BartenderHelpText(npc);
		}
	}
}
