﻿using BetterDialogue.UI.DefaultDialogueStyles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace BetterDialogue.UI
{
	/// <summary>
	/// The <see cref="ModType"/> used to define and add dialogue styles.<br/>
	/// In this class, you will do almost everything relevant to your dialogue style, including defining all textures used, altering<br/>
	/// dialogue box tile drawing, changing the location of the portrait box, and even changing what text is used for dialogue.<br/>
	/// </summary>
	public abstract partial class DialogueStyle : ModType
	{
		protected sealed override void Register()
		{
			ModTypeLookup<DialogueStyle>.Register(this);

			DialogueStyleLoader.DialogueStyles.Add(this);
		}

		/// <summary>
		/// The vanilla dialogue style.<br/>
		/// A simple main backdrop and no button texture.<br/>
		/// <b>Selected by default.</b><br/>
		/// </summary>
		public static Classic Classic { get; private set; } = new Classic();
		/// <summary>
		/// A polished-up variant of the vanilla dialogue style.<br/>
		/// Still doesn't use a chat button texture, but uses a slightly fancier version of vanilla's main dialogue backdrop.<br/>
		/// </summary>
		public static ClassicRefurbished ClassicRefurbished { get; private set; } = new ClassicRefurbished();

		/// <summary>
		/// The display name of this dialogue style.
		/// </summary>
		public abstract string DisplayName { get; }

		/// <summary>
		/// The Texture2D to be used as the sheet in which the tiles used to construct the dialogue box reside.<br/>
		/// This is a 3x3 sheet of 15x15 (30x30 once upscaled) tiles which represent the corners, edges, and center of the dialogue box.<br/>
		/// Please refer to the default dialogue styles to see examples.
		/// </summary>
		public abstract Texture2D DialogueBoxTileSheet { get; }

		/// <summary>
		/// The Texture2D to be used as the texture for buttons pressed within the dialogue menu.<br/>
		/// Is <see langword="null"/> by default, meaning there is no back texture for buttons (this is vanilla behavior).<br/>
		/// </summary>
		public virtual Texture2D ChatButtonTexture => null;

		/// <summary>
		/// The main text color to be used when drawing chat buttons.<br/>
		/// Defaults to using the vanilla calculation for button colors (which results in the yellow coloration you know and love/hate from vanilla).<br/>
		/// To emulate vanilla behavior more closely (read: to mimic the gradual fade between darker and lighter shades of your color), use <see cref="Main.mouseTextColor"/> as a reference instead of <see cref="Color.White"/>.<br/>
		/// </summary>
		public virtual Color ChatButtonColor
		{
			get
			{
				int mouseTextColor = Main.mouseTextColor;
				return new Color(mouseTextColor, (int)((double)mouseTextColor / 1.1), mouseTextColor / 2, mouseTextColor);
			}
		}

		/// <summary>
		/// The font to be used for dialogue with this dialogue style.<br/>
		/// By default, uses Andy, the font used by Terraria for pretty much everything (specifically, defaults to the variant used for <see cref="FontAssets.MouseText"/>).<br/>
		/// </summary>
		public virtual DynamicSpriteFont DialogueFont => FontAssets.MouseText.Value;
		/// <summary>
		/// The font to be used for buttons the player can click in the dialogue menu with this dialogue style.<br/>
		/// By default, uses whatever <see cref="DialogueFont"/> is set to.<br/>
		/// </summary>
		public virtual DynamicSpriteFont ChatButtonFont => DialogueFont;

		/// <summary>
		/// Can be used to draw your dialogue box style in a special way that precedes, and can overrule, the normal draw method for dialogue styles.<br/>
		/// Called every frame that a dialogue box is active.<br/>
		/// Returns <see langword="true"/> by default, return <see langword="false"/> to prevent the normal draw method from running.<br/>
		/// Does not prevent <see cref="PostDraw(NPC, Player, List{ChatButton})"/> from running.<br/>
		/// </summary>
		/// <param name="npc">
		/// The NPC currently speaking with the given player.<br/>
		/// </param>
		/// <param name="player">
		/// The player currently speaking with the given NPC.<br/>
		/// </param>
		/// <param name="activeChatButtons">
		/// A list of all currently-active chat buttons at the time of drawing.<br/>
		/// </param>
		public virtual bool PreDraw(NPC npc, Player player, List<ChatButton> activeChatButtons) => true;

		/// <summary>
		/// Can be used to draw extra things on your dialogue box style regardless of the return value of <see cref="PreDraw(NPC, Player, List{ChatButton})"/>.<br/>
		/// Called every frame that a dialogue box is active.<br/>
		/// </summary>
		/// <param name="npc">
		/// The NPC currently speaking with the given player.<br/>
		/// </param>
		/// <param name="player">
		/// The player currently speaking with the given NPC.<br/>
		/// </param>
		/// <param name="activeChatButtons">
		/// A list of all currently-active chat buttons at the time of drawing.<br/>
		/// </param>
		public virtual void PostDraw(NPC npc, Player player, List<ChatButton> activeChatButtons) { }

		/// <summary>
		/// Not recommended for public use, but left public just in case you know what you're doing and want to use it for some reason.<br/>
		/// Performs all code related to drawing dialogue styles. In order...<br/>
		/// 1.) Checks to ensure that the local player is talking to an NPC.<br/>
		/// 1.) Calls <see cref="PreDraw(NPC, Player, List{ChatButton})"/>. If <see cref="PreDraw(NPC, Player, List{ChatButton})"/> returns <see langword="false"/>, skips step 3.<br/>
		/// 2.) Calls <see cref="DrawClassic(NPC, Player, List{ChatButton})(NPC, Player, List{ChatButton})"/>.<br/>
		/// 3.) Calls <see cref="PostDraw(NPC, Player, List{ChatButton})"/>.<br/>
		/// </summary>
		public void DrawDialogueStyle() {
			Player player = Main.LocalPlayer;
			if (player.TalkNPC is null)
				return;
			List<ChatButton> activeChatButtons = new List<ChatButton>();
			ChatButtonLoader.VerifyExitButtonPosition();
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
			if (!PreDraw(player.TalkNPC, player, activeChatButtons))
				goto PostDraw;

			DrawClassic(player.TalkNPC, player, activeChatButtons);

			PostDraw:
			PostDraw(player.TalkNPC, player, activeChatButtons);
		}
	}
}