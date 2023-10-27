using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace BetterDialogue.UI
{
	public static class ChatButtonLoader
	{
		internal static List<ChatButton> ChatButtons = new List<ChatButton>();
		internal static List<GlobalChatButton> ChatButtonGlobals = new List<GlobalChatButton>();

		internal static void Load() {
			ChatButtons = new List<ChatButton>() {
				ChatButton.Sign,
				ChatButton.Pet,
				ChatButton.Shop,
				ChatButton.GuideProgressHelp,
				ChatButton.GuideCraftHelp,
				ChatButton.NurseHeal,
				ChatButton.AnglerQuestBecauseTheLittleShitTriscuitWontShutUp,
				ChatButton.DyeTraderStrangePlants,
				ChatButton.DryadWorldStatus,
				ChatButton.DryadPurifySoda,
				ChatButton.GoblinTinkererScamButton,
				ChatButton.PainterDecorShop,
				ChatButton.StyistHaircut,
				ChatButton.PartyGirlMusic,
				ChatButton.TaxCollectorNeedsYOUToTakeYourDamnTaxesAlready,
				ChatButton.DD2HelpButton,
				ChatButton.OldManCurse,
				ChatButton.Exit,
				ChatButton.TownNPCHappiness,
			};
			ChatButtonGlobals = new List<GlobalChatButton>();
		}

		internal static void Unload() {
			ChatButtons = null;
			ChatButtonGlobals = null;
		}

		/// <summary>
		/// Fetches the display text of the given chat button, accounting for all global adjustments.<br/>
		/// </summary>
		/// <param name="chatButton">The chat button to fetch the display text of.</param>
		/// <param name="npc">The NPC the given player is talking to.</param>
		/// <param name="player">The player talking to the given NPC.</param>
		/// <returns>
		/// The text that this <see cref="ChatButton"/> should display.<br/>
		/// </returns>
		public static string GetText(ChatButton chatButton, NPC npc, Player player)
		{
			string buttonText = chatButton.Text(npc, player);
			foreach (GlobalChatButton global in ChatButtonGlobals)
			{
				global.ModifyText(chatButton, npc, player, ref buttonText);
			}
			return buttonText;
		}

		/// <summary>
		/// Fetches the description of the given chat button, accounting for all global adjustments.<br/>
		/// </summary>
		/// <param name="chatButton">The chat button to fetch the description of.</param>
		/// <param name="npc">The NPC the given player is talking to.</param>
		/// <param name="player">The player talking to the given NPC.</param>
		/// <returns>
		/// The description that this <see cref="ChatButton"/> should have.<br/>
		/// </returns>
		public static string GetDescription(ChatButton chatButton, NPC npc, Player player)
		{
			string descriptionText = chatButton.Description(npc, player);
			foreach (GlobalChatButton global in ChatButtonGlobals)
			{
				global.ModifyDescription(chatButton, npc, player, ref descriptionText);
			}
			return descriptionText;
		}

		/// <summary>
		/// Fetches the intended color of the given chat button, accounting for all global adjustments and the active style's base chat button color.<br/>
		/// </summary>
		/// <param name="chatButton">The chat button to fetch the description of.</param>
		/// <param name="npc">The NPC the given player is talking to.</param>
		/// <param name="player">The player talking to the given NPC.</param>
		/// <returns>
		/// The description that this <see cref="ChatButton"/> should have.<br/>
		/// </returns>
		public static Color GetColor(ChatButton chatButton, NPC npc, Player player)
		{
			Color buttonColor = BetterDialogue.CurrentActiveStyle.ChatButtonColor;
			Color? overrideColor = chatButton.OverrideColor(npc, player);
			if (overrideColor.HasValue)
				buttonColor = overrideColor.Value;
			foreach (GlobalChatButton global in ChatButtonGlobals)
			{
				global.ModifyColor(chatButton, npc, player, ref buttonColor);
			}
			return buttonColor;
		}

		/// <summary>
		/// Fetches the intended origin position of the given chat button, accounting for all global adjustments.<br/>
		/// </summary>
		/// <param name="chatButton">The chat button to fetch the origin position of.</param>
		/// <param name="npc">The NPC the given player is talking to.</param>
		/// <param name="player">The player talking to the given NPC.</param>
		/// <returns>
		/// The description that this <see cref="ChatButton"/> should have.<br/>
		/// </returns>
		public static void ModifyPosition(ChatButton chatButton, NPC npc, Player player, ref Vector2 position)
		{
			foreach (GlobalChatButton global in ChatButtonGlobals)
			{
				global.ModifyPosition(chatButton, npc, player, ref position);
			}
			chatButton.ModifyPosition(npc, player, ref position);
		}

		/// <summary>
		/// Performs all possible on-hover actions for the given chat button.<br/>
		/// </summary>
		/// <param name="chatButton">The chat button to perform all on-hover actions of.</param>
		/// <param name="npc">The NPC the given player is talking to.</param>
		/// <param name="player">The player talking to the given NPC.</param>
		public static void OnHover(ChatButton chatButton, NPC npc, Player player)
		{
			foreach (GlobalChatButton global in ChatButtonGlobals)
			{
				bool preHover = global.PreHover(chatButton, npc, player);
				if (!preHover)
					return;
			}
			chatButton.OnHover(npc, player);
			foreach (GlobalChatButton global in ChatButtonGlobals)
			{
				global.OnHover(chatButton, npc, player);
			}
		}

		/// <summary>
		/// Performs all possible on-click actions for the given chat button.<br/>
		/// </summary>
		/// <param name="chatButton">The chat button to perform all on-click actions of.</param>
		/// <param name="npc">The NPC the given player is talking to.</param>
		/// <param name="player">The player talking to the given NPC.</param>
		public static void OnClick(ChatButton chatButton, NPC npc, Player player)
		{
			foreach (GlobalChatButton global in ChatButtonGlobals)
			{
				bool preClick = global.PreClick(chatButton, npc, player);
				if (!preClick)
					return;
			}
			chatButton.OnClick(npc, player);
			foreach (GlobalChatButton global in ChatButtonGlobals)
			{
				global.OnClick(chatButton, npc, player);
			}
		}
	}
}
