﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.UI.Chat;
using Terraria.GameContent.UI.States;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.UI.Gamepad;

namespace BetterDialogue.UI
{
	/// <summary>
	/// The class used to define and add dialogue styles.<br/>
	/// In this class, you will do almost everything relevant to your dialogue style, including defining all textures used, altering<br/>
	/// dialogue box tile drawing, changing the location of the portrait box, and even changing what text is used for dialogue.<br/>
	/// </summary>
	public abstract partial class DialogueStyle
	{
		public static TextDisplayCachePlus Cache { get; } = new TextDisplayCachePlus();

		private static void DrawClassic(NPC npc, Player localPlayer, List<ChatButton> chatButtons)
		{
			bool playerIsTalkingToSomeone = npc is not null;
			bool playerIsUsingASign = localPlayer.sign != -1;
			if (!playerIsTalkingToSomeone && !playerIsUsingASign)
			{
				Main.npcChatText = "";
				return;
			}

			// at a cursory glance, this looks unfathomably dumb, but if I don't do this, the dialogue window adds unnecessary space between tiles on non-100% scales
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null);

			Color someShadeOfGray = new Color(200, 200, 200, 200);
			int mouseTextColor = (Main.mouseTextColor * 2 + 255) / 3;
			Color baseTextColor = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
			bool skipButtonDraw = Main.InGameUI.CurrentState is UIVirtualKeyboard && PlayerInput.UsingGamepad;
			string chatText = Main.npcChatText;
			if (npc is not null)
			{
				bool doYouHaveTheFuckingCokeIOrdered = Main.CanDryadPlayStardewAnimation(Main.LocalPlayer, npc);
				if (npc.ai[0] == 24f && NPC.RerollDryadText == 2)
					NPC.RerollDryadText = 1;

				if (doYouHaveTheFuckingCokeIOrdered && NPC.RerollDryadText == 1 && npc.ai[0] != 24f && npc.type == NPCID.Dryad)
				{
					NPC.RerollDryadText = 0;
					Main.npcChatText = npc.GetChat();
					NPC.PreventJojaColaDialog = true;
				}

				if (doYouHaveTheFuckingCokeIOrdered && !NPC.PreventJojaColaDialog)
					chatText = Language.GetTextValue("StardewTalk.PlayerHasColaAndIsHoldingIt");
			}

			Cache.PrepareCache(chatText, baseTextColor);
			List<List<TextSnippet>> textLines = Cache.TextLines;
			int amountOfLines = Cache.AmountOfLines;

			bool flag3 = false;
			if (Main.editSign)
			{
				Main.instance.textBlinkerCount++;
				if (Main.instance.textBlinkerCount >= 20)
				{
					if (Main.instance.textBlinkerState == 0)
						Main.instance.textBlinkerState = 1;
					else
						Main.instance.textBlinkerState = 0;

					Main.instance.textBlinkerCount = 0;
				}

				if (Main.instance.textBlinkerState == 1)
				{
					flag3 = true;

					textLines[amountOfLines - 1].Add(new TextSnippet("|", Color.White, 1f));
				}

				Main.instance.DrawWindowsIMEPanel(new Vector2(Main.screenWidth / 2, 90f), 0.5f);
			}

			BetterDialogueConfig config = ModContent.GetInstance<BetterDialogueConfig>();

			int tileDrawSize = (int)Math.Round(30f * Main.UIScale);
			float chatScale = tileDrawSize / 30f;
			int chatBackdropWidth = (config.DialogueBoxWidth + BetterDialogue.CurrentActiveStyle.BoxWidthModifier + 2) * tileDrawSize;
			int chatBackdropHeight = (amountOfLines + 2) * tileDrawSize;
			int chatBackdropXOffset = ((int)Math.Round(Main.screenWidth * chatScale) - chatBackdropWidth) / 2;
			int chatBackdropYOffset = (int)Math.Round(100f * chatScale);
			#region Draw the backdrop of the dialogue style
			for (int y = 0; y <= amountOfLines + 1; y++)
			{
				int widthInTiles = config.DialogueBoxWidth + BetterDialogue.CurrentActiveStyle.BoxWidthModifier + 1;
				for (int x = 0; x <= widthInTiles; x++)
				{
					int targetSheetTileX = x == 0 ? 0 : (x < widthInTiles ? 32 : 64);
					int targetSheetTileY = y == 0 ? 0 : (y < amountOfLines + 1 ? 32 : 64);
					Main.spriteBatch.Draw(
						BetterDialogue.CurrentActiveStyle.DialogueBoxTileSheet,
						new Vector2(
							chatBackdropXOffset + (tileDrawSize * x),
							chatBackdropYOffset + (tileDrawSize * y)
						),
						new Rectangle(
							targetSheetTileX,
							targetSheetTileY,
							30,
							30
						),
						someShadeOfGray,
						0f,
						default,
						chatScale,
						SpriteEffects.None,
						0f
					);
				}
			}
			#endregion

			TextSnippet hoveredSnippet = null;
			for (int i = 0; i < amountOfLines; i++)
			{
				List<TextSnippet> text = textLines[i];
				ChatManager.DrawColorCodedStringWithShadow(
					Main.spriteBatch,
					FontAssets.MouseText.Value,
					text.ToArray(),
					new Vector2(
						(int)(20 * chatScale) + chatBackdropXOffset,
						(int)(120 * chatScale) + (tileDrawSize * i)
					),
					0f,
					baseTextColor,
					Color.Black,
					Vector2.Zero,
					Vector2.One * chatScale,
					out int hoveredSnippetNum
				);

				if (hoveredSnippetNum > -1)
					hoveredSnippet = text[hoveredSnippetNum];
			}

			if (flag3)
				textLines[amountOfLines - 1].RemoveAt(textLines[amountOfLines - 1].Count - 1);

			// do these even do anything?
			if (hoveredSnippet is not null)
			{
				hoveredSnippet.OnHover();

				if (Main.mouseLeft && Main.mouseLeftRelease)
					hoveredSnippet.OnClick();
			}

			Rectangle interfaceRectangle = new Rectangle(
				chatBackdropXOffset,
				chatBackdropYOffset,
				chatBackdropWidth,
				chatBackdropHeight
			);
			int num3 = 120 + amountOfLines * 30 + 30;
			num3 -= 235;
			num3 = (int)Math.Round(num3 * chatScale);
			UIVirtualKeyboard.ShouldHideText = !PlayerInput.SettingsForUI.ShowGamepadHints;
			if (!PlayerInput.UsingGamepad)
				num3 = 9999;

			UIVirtualKeyboard.OffsetDown = num3;
			if (Main.npcChatCornerItem != 0)
			{
				Vector2 position = new Vector2(
					chatBackdropXOffset,
					chatBackdropYOffset + chatBackdropHeight
				);
				position -= Vector2.One * 8f;
				position.Y *= chatScale;
				Item item = new Item();
				item.netDefaults(Main.npcChatCornerItem);
				float itemDrawScale = chatScale;
				Main.GetItemDrawFrame(item.type, out var itemTexture, out var rectangle2);
				if (rectangle2.Width > 32 || rectangle2.Height > 32)
					itemDrawScale = ((rectangle2.Width <= rectangle2.Height) ? (32f / (float)rectangle2.Height) : (32f / (float)rectangle2.Width)) * chatScale;

				Main.spriteBatch.Draw(itemTexture, position, rectangle2, item.GetAlpha(Color.White), 0f, rectangle2.Size(), itemDrawScale, SpriteEffects.None, 0f);
				if (item.color != default)
					Main.spriteBatch.Draw(itemTexture, position, rectangle2, item.GetColor(item.color), 0f, rectangle2.Size(), itemDrawScale, SpriteEffects.None, 0f);

				Rectangle itemHoverRectangle = new Rectangle(
					(int)position.X - (int)(rectangle2.Width * itemDrawScale),
					(int)position.Y - (int)(rectangle2.Height * itemDrawScale),
					(int)(rectangle2.Width * itemDrawScale),
					(int)(rectangle2.Height * itemDrawScale));
				if (itemHoverRectangle.Contains(new Point(Main.mouseX, Main.mouseY)))
				{
					Main.cursorOverride = 2;
					if (Main.mouseLeftRelease && Main.mouseLeft)
					{
						if (!Main.drawingPlayerChat)
							Main.OpenPlayerChat();

						if (ChatManager.AddChatText(FontAssets.MouseText.Value, ItemTagHandler.GenerateTag(item), Vector2.One))
							SoundEngine.PlaySound(SoundID.MenuTick);
					}

					Main.instance.MouseText(item.Name, -11, 0);
				}
			}

			bool drawTaxMoney = false;
			if (npc is not null)
				drawTaxMoney = chatButtons.FirstOrDefault(x => x == ChatButton.TaxCollectorNeedsYOUToTakeYourDamnTaxesAlready && x.IsActive(npc, localPlayer)) != null;

			if (!skipButtonDraw)
			{
				DrawClassicButtons(amountOfLines, chatButtons);
				if (drawTaxMoney)
				{
					string focusText = Lang.inter[89].Value;
					string text2 = "";
					for (int k = 0; k < focusText.Length; k++)
					{
						text2 += " ";
					}
					float shopX = chatBackdropYOffset + chatBackdropHeight - tileDrawSize;
					float shopButtonX = chatBackdropXOffset + tileDrawSize;
					shopButtonX += (ChatManager.GetStringSize(FontAssets.MouseText.Value, text2, new Vector2(0.9f)).X - 20f) * chatScale;
					int taxMoney2 = localPlayer.taxMoney;
					taxMoney2 = (int)((double)taxMoney2 / localPlayer.currentShoppingSettings.PriceAdjustment);
					ItemSlot.DrawMoney(Main.spriteBatch, "", shopButtonX, shopX - (40f * chatScale), Utils.CoinsSplit(taxMoney2), horizontal: true);
				}
			}

			if (PlayerInput.IgnoreMouseInterface)
				goto RestartVanillaSpriteBatch;

			if (interfaceRectangle.Contains(Main.MouseScreen.ToPoint()))
				localPlayer.mouseInterface = true;

			if (!Main.mouseLeft || !Main.mouseLeftRelease || !localPlayer.mouseInterface)
				goto RestartVanillaSpriteBatch;

			Main.mouseLeftRelease = false;
			localPlayer.releaseUseItem = false;
			localPlayer.mouseInterface = true;
			foreach (ChatButton button in chatButtons)
			{
				if (!button.IsHovered)
					continue;

				ChatButtonLoader.OnClick(button, npc, localPlayer);
			}

			RestartVanillaSpriteBatch:
			// now we can let the rest of the vanilla stuff happen as it would normally
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
		}

		private static void DrawClassicButtons(int lineCount, List<ChatButton> activeChatButtons)
		{
			BetterDialogueConfig config = ModContent.GetInstance<BetterDialogueConfig>();
			int tileDrawSize = (int)Math.Round(30f * Main.UIScale);
			float chatScale = tileDrawSize / 30f;
			float chatBackdropWidth = (config.DialogueBoxWidth + BetterDialogue.CurrentActiveStyle.BoxWidthModifier + 2) * tileDrawSize;
			float buttonX = (((int)Math.Round(Main.screenWidth * chatScale) - chatBackdropWidth) / 2) + tileDrawSize;
			float buttonY = (130 + lineCount * 30) * chatScale;
			Player localPlayer = Main.player[Main.myPlayer];
			NPC talkNPC = localPlayer.TalkNPC;
			Vector2 originalButtonOrigin = new Vector2(buttonX, buttonY);
			for (int i = 0; i < activeChatButtons.Count; i++)
			{
				ChatButton button = activeChatButtons[i];
				string buttonText = ChatButtonLoader.GetText(button, talkNPC, localPlayer);
				DynamicSpriteFont buttonTextFont = BetterDialogue.CurrentActiveStyle.ChatButtonFont;
				Vector2 buttonTextScale = new Vector2(0.9f * chatScale);
				Vector2 buttonTextSize = ChatManager.GetStringSize(buttonTextFont, buttonText, buttonTextScale);
				Color baseColor = ChatButtonLoader.GetColor(button, talkNPC, localPlayer);
				Color black = Color.Black;
				Vector2 mysteriousVector = new Vector2(1f);
				float hoverScaleModifier = 1.2f;
				if (buttonTextSize.X > 260f)
					mysteriousVector.X *= 260f / buttonTextSize.X;

				Vector2 modifiedButtonOrigin = originalButtonOrigin;
				ChatButtonLoader.ModifyPosition(button, talkNPC, localPlayer, ref modifiedButtonOrigin);

				button.HoverBox = new Rectangle(
					(int)modifiedButtonOrigin.X,
					(int)modifiedButtonOrigin.Y,
					(int)(buttonTextSize * buttonTextScale * mysteriousVector.X).X,
					(int)(buttonTextSize * buttonTextScale * mysteriousVector.X).Y
				);
				if (button.IsHovered && !PlayerInput.IgnoreMouseInterface)
				{
					localPlayer.mouseInterface = true;
					localPlayer.releaseUseItem = false;
					buttonTextScale *= hoverScaleModifier;
					if (button.HoverChanged.HasValue && button.HoverChanged.Value)
						SoundEngine.PlaySound(SoundID.MenuTick);
				}
				else
				{
					if (button.HoverChanged.HasValue && !button.HoverChanged.Value)
						SoundEngine.PlaySound(SoundID.MenuTick);
				}
				button.WasHovered = button.IsHovered;

				ChatManager.DrawColorCodedStringWithShadow(
					Main.spriteBatch,
					buttonTextFont,
					buttonText,
					modifiedButtonOrigin + (buttonTextSize * mysteriousVector * 0.5f),
					baseColor,
					button.IsHovered ? Color.Brown : Color.Black,
					0f,
					buttonTextSize * mysteriousVector * 0.5f,
					buttonTextScale * mysteriousVector
				);

				// Prepare the origin of the next button before the next cycle begins, since it's based on the end of the previous button's text.
				originalButtonOrigin.X += buttonTextSize.X * mysteriousVector.X + (30f * chatScale);
			}
		}

		public class TextDisplayCachePlus
		{
			private string _originalText;
			private Color _originalColor;
			private int _lastScreenWidth;
			private int _lastScreenHeight;
			private InputMode _lastInputMode;
			private int _lastBoxWidth;
			private int _lastBoxMaxLines;

			private static BetterDialogueConfig Config => ModContent.GetInstance<BetterDialogueConfig>();

			public List<List<TextSnippet>> TextLines { get; private set; } // Changed from string[]

			public int AmountOfLines { get; private set; }

			public void PrepareCache(string text, Color baseColor)
			{
				if ((Main.screenWidth != _lastScreenWidth)
				  | (Main.screenHeight != _lastScreenHeight)
				  | (_originalText != text)
				  | (PlayerInput.CurrentInputMode != _lastInputMode)
				  | (_originalColor != baseColor)
				  | (_lastBoxWidth != Config.DialogueBoxWidth + BetterDialogue.CurrentActiveStyle.BoxWidthModifier)
				  | (_lastBoxMaxLines != Config.DialogueBoxMaximumLines))
				{
					_originalText = text;
					_originalColor = baseColor;
					_lastScreenWidth = Main.screenWidth;
					_lastScreenHeight = Main.screenHeight;
					_lastInputMode = PlayerInput.CurrentInputMode;
					_lastBoxWidth = Config.DialogueBoxWidth + BetterDialogue.CurrentActiveStyle.BoxWidthModifier;
					_lastBoxMaxLines = Config.DialogueBoxMaximumLines;
					text = Lang.SupportGlyphs(text);

					int tileDrawSize = (int)Math.Round(30f * Main.UIScale);
					float chatScale = tileDrawSize / 30f;
					int textWidth = ((Config.DialogueBoxWidth + BetterDialogue.CurrentActiveStyle.BoxWidthModifier + 2) * 30) - 40;
					TextLines = Utils.WordwrapStringSmart(text, baseColor, FontAssets.MouseText.Value, textWidth, Config.DialogueBoxMaximumLines);
					AmountOfLines = TextLines.Count;
				}
			}
		}
	}
}
