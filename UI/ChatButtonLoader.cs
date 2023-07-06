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

		internal static void VerifyExitAndHappinessButtonPositions()
		{
			ChatButtons.Remove(ChatButton.Exit);
			ChatButtons.Add(ChatButton.Exit);
			ChatButtons.Remove(ChatButton.TownNPCHappiness);
			ChatButtons.Add(ChatButton.TownNPCHappiness);
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
			return;
		}
	}
}
