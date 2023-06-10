using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI.Gamepad;

namespace BetterDialogue.UI.ExampleChatButtonChanges
{
	public class GuideCrafting : GlobalChatButton
	{
		/// <summary>
		/// Checks if the target player has at least one item tagged as a material in their inventory.<br/>
		/// </summary>
		/// <param name="player">
		/// The player whose inventory should be searched for material items.<br/>
		/// </param>
		/// <returns>
		/// <see langword="true"/> if a material is found; <see langword="false"/> otherwise.<br/>
		/// </returns>
		private static bool PlayerHasMaterialItem(Player player)
		{
			bool materialFound = false;
			for (int i = 0; i < 58; i++)
			{
				Item item = player.inventory[i];
				if (item.IsAir || item.stack <= 0)
					continue;

				if (item.material)
				{
					materialFound = true;
					break;
				}
			}
			return materialFound;
		}

		public override void OverrideColor(ChatButton chatButton, NPC npc, Player player, ref Color buttonTextColor)
		{
			if (chatButton != ChatButton.GuideCraftHelp)
				return;

			if (!PlayerHasMaterialItem(player))
				buttonTextColor = Color.Gray;
		}

		public override bool PreClick(ChatButton chatButton, NPC npc, Player player)
		{
			if (chatButton != ChatButton.GuideCraftHelp)
				return true;

			return PlayerHasMaterialItem(player);
		}
	}
}
