using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace BetterDialogue.UI.DefaultDialogueStyles
{
	public class Classic : DialogueStyle
	{
		public override string DisplayName => "Classic";

		public override Texture2D DialogueBoxTileSheet => ModContent.Request<Texture2D>("BetterDialogue/UI/DefaultDialogueStyles/Classic_MainBox", AssetRequestMode.ImmediateLoad).Value;
	}
}
