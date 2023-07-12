using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Achievements;
using Terraria.ID;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class DryadWorldStatusButton : ChatButton
	{
		public override string Text(NPC npc, Player player) => Lang.inter[49].Value;

		public override double Priority => 8.0;

		public override string Description(NPC npc, Player player) => "With " + npc.GivenName + "'s unusually-accurate evil detection abilities, see the amount of your stretch of Terraria which is infected by any of a number of evils.";

		public override bool IsActive(NPC npc, Player player) => npc.type == NPCID.Dryad;

		public override void OnClick(NPC npc, Player player)
		{
			SoundEngine.PlaySound(SoundID.MenuTick);
			Main.npcChatText = Lang.GetDryadWorldStatusDialog(out var worldIsEntirelyPure);
			if (worldIsEntirelyPure)
				AchievementsHelper.HandleSpecialEvent(player, 27);
		}
	}
}
