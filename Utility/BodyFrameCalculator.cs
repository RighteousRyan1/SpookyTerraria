using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace SpookyTerraria.Utilities
{
	public static class BodyFrameCalculator // Thanks, ambrose
	{
		public static int GetBodyFrameFromRotation(float rotation)
		{
			int bodyFrame = 3;
			if (rotation < -0.7853982f && rotation > -2.3561945f)
			{
				bodyFrame = 2;
			}
			if (rotation > 0.7853982f && rotation < 2.3561945f)
			{
				bodyFrame = 4;
			}
			return bodyFrame;
		}
	}
}
