using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.GameContent.UI.Chat;
using Terraria.GameContent.UI.States;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace BetterDialogue.UI
{
	public class UISystem : ModSystem
	{
		public UserInterface DialogueCycleButtonLayer;
		public DialogueCycleButtonUI DialogueCycleButton;

		public override void OnWorldLoad()
		{
			DialogueCycleButtonLayer = new UserInterface();
			DialogueCycleButton = new DialogueCycleButtonUI();
			DialogueCycleButton.Activate();
			DialogueCycleButtonLayer.SetState(DialogueCycleButton);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int vanillaDialogueBoxIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: NPC / Sign Dialog"));
			if (vanillaDialogueBoxIndex != -1)
				AddInterfaceLayer(layers, DialogueCycleButtonLayer, DialogueCycleButton, vanillaDialogueBoxIndex, "Dialogue Cycle Button");
		}

		public static void AddInterfaceLayer(List<GameInterfaceLayer> layers, UserInterface userInterface, UIState state, int index, string customName = null)
		{
			string name;
			if (customName == null)
				name = state.ToString();
			else
				name = customName;

			layers.Insert(index, new LegacyGameInterfaceLayer("Dialect: " + name,
				delegate
				{
					userInterface.Update(Main._drawInterfaceGameTime);
					state.Draw(Main.spriteBatch);
					return true;
				}, InterfaceScaleType.UI
			));
		}
	}

	public class DialogueCycleButtonUI : UIState
	{
		public static string ActiveDialogueMod { get; internal set; }

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (Main.editChest)
				return;

			Player localPlayer = Main.LocalPlayer;
			NPC npc = localPlayer.TalkNPC;
			bool playerIsTalkingToSomeone = npc is not null;
			bool playerIsUsingASign = localPlayer.sign != -1;
			if (!playerIsTalkingToSomeone && !playerIsUsingASign)
			{
				Main.npcChatText = "";
				ActiveDialogueMod = "Dialect";
				return;
			}

			if (playerIsTalkingToSomeone && !BetterDialogue.SupportedNPCs.Contains(npc.type))
				return;

			BetterDialogueConfig config = ModContent.GetInstance<BetterDialogueConfig>();

			Vector2 interfacePoint = Vector2.Zero;
			switch (ActiveDialogueMod)
			{
				case "Vanilla":
					int vanillaChatBackdropXOffset = (Main.screenWidth - TextureAssets.ChatBack.Value.Width) / 2;
					interfacePoint.X = vanillaChatBackdropXOffset - 48f;
					interfacePoint.Y = 100f;
					break;
				case "Dialect":
					int tileDrawSize = 30;
					int chatBackdropWidth = (config.DialogueBoxWidth + BetterDialogue.CurrentActiveStyle.BoxWidthModifier + 2) * tileDrawSize;
					int chatBackdropXOffset = (Main.screenWidth - chatBackdropWidth) / 2;
					interfacePoint.X = chatBackdropXOffset - 48f;
					interfacePoint.Y = 100f;
					break;
				case "DPR":
					int DPRChatBackdropXOffset = (Main.screenWidth - TextureAssets.ChatBack.Value.Width) / 2;
					interfacePoint.X = DPRChatBackdropXOffset - 48f;
					interfacePoint.Y = 100f;
					break;
				default:
					ActiveDialogueMod = "Dialect";
					goto case "Dialect";
			}

			Rectangle interfaceRectangle = new Rectangle((int)interfacePoint.X, (int)interfacePoint.Y, 40, 40);
			Rectangle textureRectangle = new Rectangle(0, 0, 40, 40);
			if (interfaceRectangle.Contains(Main.MouseScreen.ToPoint()))
				textureRectangle.X += 40;
			Main.spriteBatch.Draw(
				ModContent.Request<Texture2D>("BetterDialogue/UI/DialogueCycleButton", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
				interfacePoint,
				textureRectangle,
				Color.White,
				0f,
				default,
				1f,
				SpriteEffects.None,
				0f
			);

			if (PlayerInput.IgnoreMouseInterface)
				return;

			if (interfaceRectangle.Contains(Main.MouseScreen.ToPoint()))
			{
				localPlayer.mouseInterface = true;
				UICommon.TooltipMouseText(
					Language.GetTextValueWith(
						"Mods.BetterDialogue.UI.SwapDialectButtonDesc." + (ModLoader.TryGetMod("DialogueTweak", out Mod DPR) ? "DPR" : "NoDPR"),
						new
						{
							DialectSortColor = (ActiveDialogueMod == "Dialect" ? new Color(0, 255, 255) : new Color(127, 127, 127)).Hex3(),
							VanillaSortColor = (ActiveDialogueMod == "Vanilla" ? new Color(0, 255, 255) : new Color(127, 127, 127)).Hex3(),
							DPRSortColor = (ActiveDialogueMod == "DPR" ? new Color(0, 255, 255) : new Color(127, 127, 127)).Hex3(),
						}
					)
				);
			}

			if (!localPlayer.mouseInterface)
				return;


			switch (ActiveDialogueMod)
			{
				case "Vanilla":
					if (Main.mouseLeft && Main.mouseLeftRelease)
					{
						Main.mouseLeftRelease = false;
						localPlayer.releaseUseItem = false;
						localPlayer.mouseInterface = true;
						ActiveDialogueMod = ModLoader.TryGetMod("DialogueTweak", out Mod DPR) ? "DPR" : "Dialect";
					}
					else if (Main.mouseRight && Main.mouseRightRelease)
					{
						Main.mouseRightRelease = false;
						localPlayer.releaseUseItem = false;
						localPlayer.mouseInterface = true;
						ActiveDialogueMod = "Dialect";
					}
					break;
				case "Dialect":
					if (Main.mouseLeft && Main.mouseLeftRelease)
					{
						Main.mouseLeftRelease = false;
						localPlayer.releaseUseItem = false;
						localPlayer.mouseInterface = true;
						ActiveDialogueMod = "Vanilla";
					}
					else if (Main.mouseRight && Main.mouseRightRelease)
					{
						Main.mouseRightRelease = false;
						localPlayer.releaseUseItem = false;
						localPlayer.mouseInterface = true;
						ActiveDialogueMod = ModLoader.TryGetMod("DialogueTweak", out Mod DPR) ? "DPR" : "Vanilla";
					}
					break;
				case "DPR":
					if (Main.mouseLeft && Main.mouseLeftRelease)
					{
						Main.mouseLeftRelease = false;
						localPlayer.releaseUseItem = false;
						localPlayer.mouseInterface = true;
						ActiveDialogueMod = "Dialect";
					}
					else if (Main.mouseRight && Main.mouseRightRelease)
					{
						Main.mouseRightRelease = false;
						localPlayer.releaseUseItem = false;
						localPlayer.mouseInterface = true;
						ActiveDialogueMod = "Vanilla";
					}
					break;
			}
		}
	}
}
