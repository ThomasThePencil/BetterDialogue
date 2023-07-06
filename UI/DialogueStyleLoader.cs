using BetterDialogue.UI.DefaultDialogueStyles;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;

namespace BetterDialogue.UI
{
	/// <summary>
	/// The class used to store existing and newly-added dialogue styles.<br/>
	/// The only thing you will need to do with this class, for the most part, is call Register.<br/>
	/// </summary>
	public static class DialogueStyleLoader
	{
		internal static List<DialogueStyle> DialogueStyles = new List<DialogueStyle>();
		internal static List<GlobalDialogueStyle> DialogueStyleGlobals = new List<GlobalDialogueStyle>();
		public static List<string> DialogueStyleDisplayNames {
			get {
				List<string> nameList = new List<string>();
				if (DialogueStyles is null || DialogueStyles.Count <= 0)
					return null;

				foreach (DialogueStyle style in DialogueStyles)
				{
					nameList.Add(style.DisplayName);
				}
				return nameList;
			}
		}

		internal static void Load()
		{
			DialogueStyles = new List<DialogueStyle>() {
				DialogueStyle.Classic,
				DialogueStyle.ClassicRefurbished
			};
			DialogueStyleGlobals = new List<GlobalDialogueStyle>();
		}

		/// <summary>
		/// Not recommended for public use, but left public just in case you know what you're doing and want to use it for some reason.<br/>
		/// Performs all code related to drawing dialogue styles. In order...<br/>
		/// 1.) Verifies that the Exit button is in the last button space, and moves it there if it is not.
		/// 1.) Calls <see cref="GlobalDialogueStyle.PreDraw"/> for all applicable global styles. If any of them return <see langword="false"/>, skip to step 4.
		/// 2.) Calls <see cref="DialogueStyle.PreDraw"/> for the active dialogue style. If this returns <see langword="false"/>, skip step 3.<br/>
		/// 3.) Calls <see cref="DialogueStyle.Draw"/> for the active dialogue style.<br/>
		/// 4.) Calls <see cref="DialogueStyle.PostDraw"/> for the active dialogue style.<br/>
		/// 5.) Calls <see cref="GlobalDialogueStyle.PostDraw"/> for all applicable global styles.<br/>
		/// </summary>
		public static void DrawActiveDialogueStyle()
		{
			DialogueStyle activeStyle = BetterDialogue.CurrentActiveStyle;
			Player player = Main.LocalPlayer;
			
			List<ChatButton> activeChatButtons = new List<ChatButton>();
			ChatButtonLoader.VerifyExitAndHappinessButtonPositions();
			foreach (ChatButton button in ChatButtonLoader.ChatButtons)
			{
				bool active = button.IsActive(player.TalkNPC, player);
				foreach (GlobalChatButton global in ChatButtonLoader.ChatButtonGlobals)
				{
					bool? activeFromGlobal = global.IsActive(button, player.TalkNPC, player);
					if (activeFromGlobal.HasValue)
						active &= activeFromGlobal.Value;
				}
				if (active)
					activeChatButtons.Add(button);
			}
			foreach (GlobalDialogueStyle global in DialogueStyleGlobals)
			{
				if (!global.PreDraw(activeStyle, player.TalkNPC, player, activeChatButtons))
					goto PostDraw;
			}
			if (!activeStyle.PreDraw(player.TalkNPC, player, activeChatButtons))
				goto PostDraw;

			activeStyle.Draw(player.TalkNPC, player, activeChatButtons);

			PostDraw:
			activeStyle.PostDraw(player.TalkNPC, player, activeChatButtons);
			foreach (GlobalDialogueStyle global in DialogueStyleGlobals)
			{
				global.PostDraw(activeStyle, player.TalkNPC, player, activeChatButtons);
			}

		}

		internal static void Unload()
		{
			DialogueStyles = null;
			DialogueStyleGlobals = null;
		}
	}
}
