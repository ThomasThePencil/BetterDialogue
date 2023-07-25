using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
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

			Color someShadeOfGray = new Color(200, 200, 200, 200);
			int mouseTextColor = (Main.mouseTextColor * 2 + 255) / 3;
			Color baseTextColor = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
			bool flag = Main.InGameUI.CurrentState is UIVirtualKeyboard && PlayerInput.UsingGamepad;
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
			int chatBackdropWidth = (config.DialogueBoxWidth + BetterDialogue.CurrentActiveStyle.BoxWidthModifier + 2) * 30;
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
							Main.screenWidth / 2 - chatBackdropWidth / 2 + (30 * x),
							100f + (y * 30)
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
						1f,
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
						20 + ((Main.screenWidth - chatBackdropWidth) / 2),
						120 + i * 30
					),
					0f,
					baseTextColor,
					Color.Black,
					Vector2.Zero,
					Vector2.One,
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

			Rectangle rectangle = new Rectangle(
				Main.screenWidth / 2 - chatBackdropWidth / 2,
				100,
				chatBackdropWidth,
				(amountOfLines + 2) * 30
			);
			int num3 = 120 + amountOfLines * 30 + 30;
			num3 -= 235;
			UIVirtualKeyboard.ShouldHideText = !PlayerInput.SettingsForUI.ShowGamepadHints;
			if (!PlayerInput.UsingGamepad)
				num3 = 9999;

			UIVirtualKeyboard.OffsetDown = num3;
			if (Main.npcChatCornerItem != 0)
			{
				Vector2 position = new Vector2(
					Main.screenWidth / 2 + chatBackdropWidth / 2,
					100 + (amountOfLines + 1) * 30 + 30
				);
				position -= Vector2.One * 8f;
				Item item = new Item();
				item.netDefaults(Main.npcChatCornerItem);
				float num4 = 1f;
				Main.GetItemDrawFrame(item.type, out var itemTexture, out var rectangle2);
				if (rectangle2.Width > 32 || rectangle2.Height > 32)
					num4 = ((rectangle2.Width <= rectangle2.Height) ? (32f / (float)rectangle2.Height) : (32f / (float)rectangle2.Width));

				Main.spriteBatch.Draw(itemTexture, position, rectangle2, item.GetAlpha(Color.White), 0f, rectangle2.Size(), num4, SpriteEffects.None, 0f);
				if (item.color != default(Color))
					Main.spriteBatch.Draw(itemTexture, position, rectangle2, item.GetColor(item.color), 0f, rectangle2.Size(), num4, SpriteEffects.None, 0f);

				if (new Rectangle((int)position.X - (int)((float)rectangle2.Width * num4), (int)position.Y - (int)((float)rectangle2.Height * num4), (int)((float)rectangle2.Width * num4), (int)((float)rectangle2.Height * num4)).Contains(new Point(Main.mouseX, Main.mouseY)))
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

			mouseTextColor = Main.mouseTextColor;
			baseTextColor = new Color(mouseTextColor, (int)((double)mouseTextColor / 1.1), mouseTextColor / 2, mouseTextColor);

			bool drawMoney = false;
			if (npc is not null)
				drawMoney = chatButtons.FirstOrDefault(x => x == ChatButton.TaxCollectorNeedsYOUToTakeYourDamnTaxesAlready && x.IsActive(npc, localPlayer)) != null;

			if (!flag)
			{
				DrawClassicButtons(mouseTextColor, baseTextColor, amountOfLines, chatButtons);
				if (drawMoney)
				{
					string focusText = Lang.inter[89].Value;
					string text2 = "";
					for (int k = 0; k < focusText.Length; k++)
					{
						text2 += " ";
					}
					float num18 = 130 + amountOfLines * 30;
					float num19 = Main.screenWidth / 2 - chatBackdropWidth / 2 + 30;
					num19 += ChatManager.GetStringSize(FontAssets.MouseText.Value, text2, new Vector2(0.9f)).X - 20f;
					int taxMoney2 = localPlayer.taxMoney;
					taxMoney2 = (int)((double)taxMoney2 / localPlayer.currentShoppingSettings.PriceAdjustment);
					ItemSlot.DrawMoney(Main.spriteBatch, "", num19, num18 - 40f, Utils.CoinsSplit(taxMoney2), horizontal: true);
				}
			}

			if (PlayerInput.IgnoreMouseInterface)
				return;

			if (rectangle.Contains(Main.MouseScreen.ToPoint()))
				localPlayer.mouseInterface = true;

			if (!Main.mouseLeft || !Main.mouseLeftRelease || !localPlayer.mouseInterface)
				return;

			Main.mouseLeftRelease = false;
			localPlayer.releaseUseItem = false;
			localPlayer.mouseInterface = true;
			foreach (ChatButton button in chatButtons)
			{
				if (!button.IsHovered)
					continue;

				ChatButtonLoader.OnClick(button, npc, localPlayer);
			}
		}

		private static void DrawClassicButtons(int superColor, Color chatColor, int numLines, List<ChatButton> chatButtons)
		{
			float y = 130 + numLines * 30;
			BetterDialogueConfig config = ModContent.GetInstance<BetterDialogueConfig>();
			int chatBackdropWidth = (config.DialogueBoxWidth + BetterDialogue.CurrentActiveStyle.BoxWidthModifier + 2) * 30;
			int num = Main.screenWidth / 2 - chatBackdropWidth / 2 + 30;
			Player localPlayer = Main.player[Main.myPlayer];
			NPC talkNPC = localPlayer.TalkNPC;
			Vector2 originalButtonOrigin = new Vector2(num, y);
			for (int i = 0; i < chatButtons.Count; i++)
			{
				ChatButton button = chatButtons[i];
				string buttonText = ChatButtonLoader.GetText(button, talkNPC, localPlayer);
				DynamicSpriteFont buttonTextFont = BetterDialogue.CurrentActiveStyle.ChatButtonFont;
				Vector2 buttonTextScale = new Vector2(0.9f);
				Vector2 buttonTextSize = ChatManager.GetStringSize(buttonTextFont, buttonText, buttonTextScale);
				Color baseColor = ChatButtonLoader.GetColor(button, talkNPC, localPlayer);
				Color black = Color.Black;
				Vector2 vector4 = new Vector2(1f);
				float hoverScaleModifier = 1.2f;
				if (buttonTextSize.X > 260f)
					vector4.X *= 260f / buttonTextSize.X;

				Vector2 modifiedButtonOrigin = originalButtonOrigin;
				ChatButtonLoader.ModifyPosition(button, talkNPC, localPlayer, ref modifiedButtonOrigin);

				button.HoverBox = new Rectangle(
					(int)modifiedButtonOrigin.X,
					(int)modifiedButtonOrigin.Y,
					(int)(buttonTextSize * buttonTextScale * vector4.X).X,
					(int)(buttonTextSize * buttonTextScale * vector4.X).Y
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
					modifiedButtonOrigin + buttonTextSize * vector4 * 0.5f,
					baseColor,
					button.IsHovered ? Color.Brown : Color.Black,
					0f,
					buttonTextSize * 0.5f,
					buttonTextScale * vector4
				);

				// Prepare the origin of the next button before the next cycle begins, since it's based on the end of the previous button's text.
				originalButtonOrigin.X += buttonTextSize.X * vector4.X + 30f;
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

					int textWidth = ((Config.DialogueBoxWidth + BetterDialogue.CurrentActiveStyle.BoxWidthModifier + 2) * 30) - 40;
					TextLines = Utils.WordwrapStringSmart(text, baseColor, FontAssets.MouseText.Value, textWidth, Config.DialogueBoxMaximumLines);
					AmountOfLines = TextLines.Count;
				}
			}
		}
	}
}
