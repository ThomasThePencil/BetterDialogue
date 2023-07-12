using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class AnglerQuestButton : ChatButton
	{
		public override string Text(NPC npc, Player player) => Lang.inter[64].Value;

		public override double Priority => 6.0;

		public override string Description(NPC npc, Player player) => "See what " + npc.GivenName + " needs you to go fetch for him today.";

		public override bool IsActive(NPC npc, Player player) => npc.type == NPCID.Angler;

		public override void OnClick(NPC npc, Player player)
		{
			SoundEngine.PlaySound(SoundID.MenuTick);
			Main.npcChatCornerItem = 0;
			bool flag4 = false;
			if (!Main.anglerQuestFinished && !Main.anglerWhoFinishedToday.Contains(player.name))
			{
				int num20 = player.FindItem(Main.anglerQuestItemNetIDs[Main.anglerQuest]);
				if (num20 != -1)
				{
					player.inventory[num20].stack--;
					if (player.inventory[num20].stack <= 0)
						player.inventory[num20] = new Item();

					flag4 = true;
					SoundEngine.PlaySound(SoundID.Chat);
					player.anglerQuestsFinished++;
					player.GetAnglerReward(npc, Main.anglerQuestItemNetIDs[Main.anglerQuest]);
				}
			}

			Main.npcChatText = Lang.AnglerQuestChat(flag4);
			if (flag4)
			{
				Main.anglerQuestFinished = true;
				if (Main.netMode == NetmodeID.MultiplayerClient)
					NetMessage.SendData(MessageID.AnglerQuestFinished);
				else
					Main.anglerWhoFinishedToday.Add(player.name);

				AchievementsHelper.HandleAnglerService();
			}
		}
	}
}
