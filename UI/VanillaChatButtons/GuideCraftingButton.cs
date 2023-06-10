using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI.Gamepad;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class GuideCraftingButton : ChatButton
	{
		public override string Text(NPC npc, Player player) => Lang.inter[25].Value;

		public override string Description(NPC npc, Player player) => npc.GivenName + " can recall every (standard) crafting recipe in the known universe! Concerning as that may be, he can help you learn what things are used for. Just give him a \"Material\" item, and he'll do the rest.";

		public override bool IsActive(NPC npc, Player player) => npc.type == NPCID.Guide;

		public override void OnClick(NPC npc, Player player)
		{
			SoundEngine.PlaySound(SoundID.MenuTick);
			Main.playerInventory = true;
			Main.npcChatText = "";
			Main.InGuideCraftMenu = true;
			UILinkPointNavigator.GoToDefaultPage();
		}
	}
}
