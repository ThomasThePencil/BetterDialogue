using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class TheOneSignButton : ChatButton
	{
		public override string Text(NPC npc, Player player) => Main.editSign ? Lang.inter[47].Value : Lang.inter[48].Value;

		public override double Priority => 0.0;

		public override string Description(NPC npc, Player player) => Main.editSign ? "Confirm your changes to this sign." : "Edit what this sign says.";

		public override bool IsActive(NPC npc, Player player) => player.sign != -1;

		public override void OnClick(NPC npc, Player player)
		{
			SoundEngine.PlaySound(SoundID.MenuTick);

			if (Main.editSign)
				Main.SubmitSignText();
			else
				IngameFancyUI.OpenVirtualKeyboard(1);
		}
	}
}
