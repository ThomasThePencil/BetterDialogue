using Terraria;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class ExitButton : ChatButton
	{
		public override string Text(NPC npc, Player player) => Lang.inter[52].Value;

		public override string Description(NPC npc, Player player) => "Close the chat menu.";

		public override bool IsActive(NPC npc, Player player) => true;

		public override void OnClick(NPC npc, Player player)
		{
			Main.CloseNPCChatOrSign();
		}
	}
}
