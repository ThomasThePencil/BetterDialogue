using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class DryadPurifyButton : ChatButton
	{
		public override string Text(NPC npc, Player player) => Lang.inter[49].Value;

		public override double Priority => 9.0;

		public override string Description(NPC npc, Player player) => "...it's still full of soda. She could just say she's thirsty.";

		public override bool IsActive(NPC npc, Player player) => npc.type == NPCID.Dryad;

		public override void OnClick(NPC npc, Player player)
		{
			SoundEngine.PlaySound(SoundID.MenuTick);
			NPC.PreventJojaColaDialog = true;
			NPC.RerollDryadText = 2;
			Main.LocalPlayer.ConsumeItem(5275, reverseOrder: true);
			if (Main.netMode == NetmodeID.MultiplayerClient)
				NetMessage.SendData(144);
			else
				NPC.HaveDryadDoStardewAnimation();

			Main.npcChatText = Language.GetTextValue("StardewTalk.PlayerGivesCola");
		}
	}
}
