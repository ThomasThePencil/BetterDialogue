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
				ChatButton.TownNPCHappiness,
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
			};
			ChatButtonGlobals = new List<GlobalChatButton>();
		}

		internal static void Unload() {
			ChatButtons = null;
			ChatButtonGlobals = null;
		}

		internal static void VerifyExitButtonPosition()
		{
			if (ChatButtons.Last() != ChatButton.Exit)
			{
				ChatButtons.Remove(ChatButton.Exit);
				ChatButtons.Add(ChatButton.Exit);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="chatButton"></param>
		/// <param name="npc"></param>
		/// <param name="player"></param>
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
