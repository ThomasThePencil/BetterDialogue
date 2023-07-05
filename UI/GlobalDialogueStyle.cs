using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace BetterDialogue.UI
{
	/// <summary>
	/// Can be used to modify existing chat buttons.
	/// </summary>
	public abstract class GlobalDialogueStyle : ModType
	{
		protected sealed override void Register()
		{
			ModTypeLookup<GlobalDialogueStyle>.Register(this);

			DialogueStyleLoader.DialogueStyleGlobals.Add(this);
		}

		/// <summary>
		/// Can be used to draw a given dialogue box style in a special way that precedes, and can overrule, the normal draw method for dialogue styles.<br/>
		/// Called every frame that a dialogue box is active.<br/>
		/// Returns <see langword="true"/> by default, return <see langword="false"/> to prevent <see cref="Draw"/> from running.<br/>
		/// Does not prevent <see cref="PostDraw"/> from running.<br/>
		/// </summary>
		/// <param name="activeStyle">
		/// The NPC currently speaking with the given player.<br/>
		/// </param>
		/// <param name="npc">
		/// The NPC currently speaking with the given player.<br/>
		/// </param>
		/// <param name="player">
		/// The player currently speaking with the given NPC.<br/>
		/// </param>
		/// <param name="activeChatButtons">
		/// A list of all currently-active chat buttons at the time of drawing.<br/>
		/// </param>
		public virtual bool PreDraw(DialogueStyle activeStyle, NPC npc, Player player, List<ChatButton> activeChatButtons) => true;

		/// <summary>
		/// Can be used to draw extra things on your dialogue box style.<br/>
		/// Called every frame that a dialogue window is active, regardless of the return value of <see cref="PreDraw"/> or <see cref="DialogueStyle.PreDraw"/>.<br/>
		/// </summary>
		/// <param name="activeStyle">
		/// The NPC currently speaking with the given player.<br/>
		/// </param>
		/// <param name="npc">
		/// The NPC currently speaking with the given player.<br/>
		/// </param>
		/// <param name="player">
		/// The player currently speaking with the given NPC.<br/>
		/// </param>
		/// <param name="activeChatButtons">
		/// A list of all currently-active chat buttons at the time of drawing.<br/>
		/// </param>
		public virtual void PostDraw(DialogueStyle activeStyle, NPC npc, Player player, List<ChatButton> activeChatButtons) { }
	}
}
