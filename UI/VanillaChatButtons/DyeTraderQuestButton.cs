using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class DyeTraderQuestButton : ChatButton
	{
		public override string Text(NPC npc, Player player) => Lang.inter[107].Value;

		public override string Description(NPC npc, Player player) => npc.GivenName + " is seeking rare plants! Why not exchange them with him for special dyes not found anywhere else?";

		public override bool IsActive(NPC npc, Player player) => npc.type == NPCID.DyeTrader;

		public override void OnClick(NPC npc, Player player)
		{
			Main.npcChatCornerItem = 0;
			SoundEngine.PlaySound(SoundID.MenuTick);
			bool gotDye = false;
			int num29 = player.FindItem(ItemID.Sets.ExoticPlantsForDyeTrade);
			if (num29 != -1)
			{
				player.inventory[num29].stack--;
				if (player.inventory[num29].stack <= 0)
					player.inventory[num29] = new Item();

				gotDye = true;
				SoundEngine.PlaySound(SoundID.Chat);
				player.GetDyeTraderReward(npc);
			}

			Main.npcChatText = Lang.DyeTraderQuestChat(gotDye);
		}
	}
}
