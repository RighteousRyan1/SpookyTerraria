using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;

namespace SpookyTerraria.ModIntances
{
	public class BlackSky : CustomSky
	{
		private readonly Random _random = new Random();
		private bool _isActive;

		public override void OnLoad()
		{
		}

		public override void Update(GameTime gameTime)
		{
		}

		private float GetIntensity()
		{
			return 1f - Utils.SmoothStep(0f, 0f, 0f);
		}

		public override Color OnTileColor(Color inColor)
		{
			inColor = Color.Black;
			float intensity = GetIntensity();
			return new Color(Vector4.Lerp(new Vector4(0.0f, 0.0f, 0f, 0f), inColor.ToVector4(), intensity));
		}

		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			float intensity = GetIntensity();
			spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black);
		}

		public override float GetCloudAlpha()
		{
			return 0f;
		}

		public override void Activate(Vector2 position, params object[] args)
		{
			_isActive = true;
		}

		public override void Deactivate(params object[] args)
		{
			_isActive = false;
		}

		public override void Reset()
		{
			_isActive = false;
		}

		public override bool IsActive()
		{
			return _isActive;
		}
	}
}
