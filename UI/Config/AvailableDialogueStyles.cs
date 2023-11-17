using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.ModLoader.UI;
using Terraria.UI;
using Terraria.UI.Chat;

namespace BetterDialogue.UI.Config
{
	public class AvailableDialogueStyles : ConfigElement
	{
		private static Action<ModConfig> ConfigManagerSave;

		public static void SaveModConfig(ModConfig config)
		{
			(ConfigManagerSave ??= CreateConfigManagerSave())(config);

			static Action<ModConfig> CreateConfigManagerSave()
			{
				MethodInfo configManagerSaveMethod = typeof(ConfigManager).GetMethod("Save", BindingFlags.Static | BindingFlags.NonPublic, new[] { typeof(ModConfig) })
					?? throw new InvalidOperationException("Cannot get 'Terraria.ModLoader.Config.ConfigManager.Save' method.");
				ParameterExpression modConfigParameter = Expression.Parameter(typeof(ModConfig));
				return Expression.Lambda<Action<ModConfig>>(Expression.Call(configManagerSaveMethod, modConfigParameter), modConfigParameter).Compile();
			}
		}

		public Texture2D Backdrop { get; set; }
		public Texture2D ActiveOption { get; set; }
		public Texture2D InactiveOption { get; set; }
		public override void OnBind()
		{
			base.OnBind();
			Backdrop = ModContent.Request<Texture2D>("BetterDialogue/UI/Config/StyleOption", AssetRequestMode.ImmediateLoad).Value;
			ActiveOption = ModContent.Request<Texture2D>("BetterDialogue/UI/Config/StyleSelected", AssetRequestMode.ImmediateLoad).Value;
			InactiveOption = ModContent.Request<Texture2D>("BetterDialogue/UI/Config/StyleNotSelected", AssetRequestMode.ImmediateLoad).Value;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			CalculatedStyle dimensions = GetDimensions();
			List<DialogueStyle> selectableStyles = DialogueStyleLoader.DialogueStyles.FindAll(x => x.CanBeSelected());
			for (int i = 1; i <= selectableStyles.Count; i++)
			{
				DialogueStyle style = selectableStyles[i - 1];
				string styleName = style.DisplayName;
				Rectangle destRect = new Rectangle(
					(int)dimensions.X + 25,
					(int)dimensions.Y + ((Backdrop.Height + 3) * i) - 6,
					Backdrop.Width,
					Backdrop.Height
				);
				bool hover = destRect.Contains(Main.MouseScreen.ToPoint());
				bool click = Main.mouseLeft && Main.mouseLeftRelease;
				spriteBatch.Draw(
					Backdrop,
					destRect,
					hover
					? Color.White
					: new Color(55, 55, 95, 95)
				);
				if (hover)
				{
					UICommon.TooltipMouseText(style.Description);
					Main.mouseText = true;
					if (click)
					{
						SoundEngine.PlaySound(SoundID.MenuTick);
						ModContent.GetInstance<BetterDialogueConfig>().DialogueStyle = styleName;
						SaveModConfig(ModContent.GetInstance<BetterDialogueConfig>());
					}
				}
				Vector2 textScale = new Vector2(0.8f);
				Vector2 textSize = ChatManager.GetStringSize(FontAssets.MouseText.Value, styleName, textScale);
				ChatManager.DrawColorCodedStringWithShadow(
					spriteBatch,
					FontAssets.MouseText.Value,
					styleName,
					new Vector2(destRect.X + 12.0f, destRect.Y + 20f),
					Color.White,
					hover ? Color.Brown : Color.Black,
					0f,
					new Vector2(0, textSize.Y * 0.5f),
					textScale
				);

				destRect.X += 466;
				destRect.Y += 7;
				destRect.Width = 22;
				destRect.Height = 22;
				if (ModContent.GetInstance<BetterDialogueConfig>().DialogueStyle == styleName)
				{
					spriteBatch.Draw(
						ActiveOption,
						destRect,
						Color.White
					);
				}
				else
				{
					spriteBatch.Draw(
						InactiveOption,
						destRect,
						Color.White
					);
				}
			}
		}
	}
}
