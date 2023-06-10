using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI.Gamepad;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class OldManBreakCurseButton : ChatButton
	{
		public override string Text(NPC npc, Player player) => Lang.inter[50].Value;

		public override string Description(NPC npc, Player player) => "This aged fellow appears to be cursed. Why not see if you can snap that curse in half?";

		public override bool IsActive(NPC npc, Player player) => npc.type == NPCID.OldMan && !Main.IsItDay();

		public override void OnClick(NPC npc, Player player)
		{
			if (Main.netMode == NetmodeID.SinglePlayer)
				NPC.SpawnSkeletron(Main.myPlayer);
			else
				NetMessage.SendData(MessageID.MiscDataSync, -1, -1, null, Main.myPlayer, 1f);

			Main.npcChatText = "";
		}
	}
}
