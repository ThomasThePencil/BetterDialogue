﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace BetterDialogue.UI
{
	/// <summary>
	/// Can be used to modify existing chat buttons.
	/// </summary>
	public abstract class GlobalChatButton : ModType
	{
		protected sealed override void Register()
		{
			ModTypeLookup<GlobalChatButton>.Register(this);

			ChatButtonLoader.ChatButtonGlobals.Add(this);
		}

		/// <summary>
		/// Allows you to modify the text on an existing chat button.<br/>
		/// </summary>
		/// <param name="chatButton">The chat button to modify the text of.</param>
		/// <param name="npc">The NPC the given player is talking to.</param>
		/// <param name="player">The player talking to the given NPC.</param>
		/// <param name="buttonText">
		/// The text to be used for the button, sourced from <see cref="ChatButton.Text(NPC, Player)"/><br/>
		/// and any previous <see cref="ModifyText(ChatButton, NPC, Player, ref string)"/> calls.<br/>
		/// </param>
		public virtual void ModifyText(ChatButton chatButton, NPC npc, Player player, ref string buttonText) { }

		/// <summary>
		/// Allows you to modify the color of the text on an existing chat button.<br/>
		/// </summary>
		/// <param name="chatButton">The chat button to add new on-hover behavior to.</param>
		/// <param name="npc">The NPC the given player is talking to.</param>
		/// <param name="player">The player talking to the given NPC.</param>
		/// <param name="buttonTextColor">
		/// The color to be used for the button, sourced from <see cref="DialogueStyle.ChatButtonColor"/>, <see cref="ChatButton.OverrideColor(NPC, Player)"/>,<br/>
		/// and any previous <see cref="ModifyColor(ChatButton, NPC, Player, ref Color)"/> calls.<br/>
		/// </param>
		public virtual void ModifyColor(ChatButton chatButton, NPC npc, Player player, ref Color buttonTextColor) { }

		/// <summary>
		/// Allows you to modify the description for an existing chat button.<br/>
		/// </summary>
		/// <param name="chatButton">The chat button to add new on-hover behavior to.</param>
		/// <param name="npc">The NPC the given player is talking to.</param>
		/// <param name="player">The player talking to the given NPC.</param>
		/// <param name="descriptionText">
		/// The text to be used for the description of the button, sourced from <see cref="ChatButton.Description(NPC, Player)"/><br/>
		/// and any previous <see cref="ModifyDescription(ChatButton, NPC, Player, ref string)"/> calls.<br/>
		/// </param>
		public virtual void ModifyDescription(ChatButton chatButton, NPC npc, Player player, ref string descriptionText) { }

		/// <summary>
		/// Allows you to decide if you'd like to change the active status of the given chat button.<br/>
		/// Useful for giving additional active conditions to vanilla buttons in particular.<br/>
		/// </summary>
		/// <param name="chatButton">The chat button to modify active status for.</param>
		/// <param name="npc">The NPC the given player is talking to.</param>
		/// <param name="player">The player talking to the given NPC.</param>
		/// <returns>
		/// <see langword="null"/> by default, and if the active status of the button should not be changed.<br/>
		/// <see langword="true"/> to force the button to be active. <b>Overrules all <see langword="null"/> returns from previous calls.</b><br/>
		/// <see langword="false"/> to force the button to be inactive. <b>Overrules all <see langword="null"/> and <see langword="true"/> returns from previous calls.</b><br/>
		/// </returns>
		public virtual bool? IsActive(ChatButton chatButton, NPC npc, Player player) => null;

		/// <summary>
		/// Allows you to make chat buttons do additional things when hovered over.
		/// </summary>
		/// <param name="chatButton">The chat button to add new on-hover behavior to.</param>
		/// <param name="npc">The NPC the given player is talking to.</param>
		/// <param name="player">The player talking to the given NPC.</param>
		public virtual void OnHover(ChatButton chatButton, NPC npc, Player player) { }

		/// <summary>
		/// Allows you to override the on-click behavior for chat buttons.<br/>
		/// Be careful when using this.<br/>
		/// </summary>
		/// <param name="chatButton">The chat button to potentially override the on-click behavior of.</param>
		/// <param name="npc">The NPC the given player is talking to.</param>
		/// <param name="player">The player talking to the given NPC.</param>
		/// <returns></returns>
		public virtual bool PreClick(ChatButton chatButton, NPC npc, Player player) => true;

		/// <summary>
		/// Allows you to make chat buttons do additional things when clicked on!<br/>
		/// </summary>
		/// <param name="chatButton">The chat button to add new on-click behavior to.</param>
		/// <param name="npc">The NPC the given player is talking to.</param>
		/// <param name="player">The player talking to the given NPC.</param>
		public virtual void OnClick(ChatButton chatButton, NPC npc, Player player) { }
	}
}
