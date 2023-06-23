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

		// We want to make the Guide's crafting button not allow clicking on it if the player doesn't have something in their inventory which can be used as a material.

		public override void ModifyColor(ChatButton chatButton, NPC npc, Player player, ref Color buttonTextColor)
		{
			// When modifying a given chat button, it is virtually necessary to check if the button is the target button before doing anything else.
			// Failing to do this may have unintended consequences on buttons which you didn't mean to modify.
			if (chatButton != ChatButton.GuideCraftHelp)
				return;

			// Here, we make the button colored gray if PlayerHasMaterialItem returns false, so that it's clear that it shouldn't work.
			if (!PlayerHasMaterialItem(player))
				buttonTextColor = Color.Gray;
		}

		public override bool PreClick(ChatButton chatButton, NPC npc, Player player)
		{
			if (chatButton != ChatButton.GuideCraftHelp)
				return true;

			// Here, we return whatever PlayerHasMaterialItem returns --- meaning the button can't be (successfully) clicked if a material item isn't in the player's inventory.
			return PlayerHasMaterialItem(player);
		}
	}
}
