using Microsoft.Xna.Framework.Graphics;
using MonoMod.RuntimeDetour.HookGen;
using System;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpookyTerraria.Utilities
{
	// Thank you, Steviegt6
    public static class Hooks
    {
		public delegate void Orig_AddMenuButtons(Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, ref int offY, ref int spacing, ref int buttonIndex, ref int numButtons);

		public delegate void Hook_AddMenuButtons(Orig_AddMenuButtons orig, Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, ref int offY, ref int spacing, ref int buttonIndex, ref int numButtons);

		public static event Hook_AddMenuButtons On_AddMenuButtons
		{
			add
			{
				HookEndpointManager.Add<Hook_AddMenuButtons>(typeof(Mod).Assembly.GetType("Terraria.ModLoader.UI.Interface").GetMethod("AddMenuButtons", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic), value);
			}
			remove
			{
				HookEndpointManager.Remove<Hook_AddMenuButtons>(typeof(Mod).Assembly.GetType("Terraria.ModLoader.UI.Interface").GetMethod("AddMenuButtons", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic), value);
			}
		}
		public delegate void Orig_ModLoaderMenus(Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, int[] buttonVerticalSpacing, ref int offY, ref int spacing, ref int numButtons, ref bool backButtonDown);

		public delegate void Hook_ModLoaderMenus(Orig_ModLoaderMenus orig, Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, int[] buttonVerticalSpacing, ref int offY, ref int spacing, ref int numButtons, ref bool backButtonDown);

		public static event Hook_ModLoaderMenus On_ModLoaderMenus
		{
			add
			{
				HookEndpointManager.Add<Hook_ModLoaderMenus>(typeof(Mod).Assembly.GetType("Terraria.ModLoader.UI.Interface").GetMethod("ModLoaderMenus", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic), value);
			}
			remove
			{
				HookEndpointManager.Remove<Hook_ModLoaderMenus>(typeof(Mod).Assembly.GetType("Terraria.ModLoader.UI.Interface").GetMethod("ModLoaderMenus", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic), value);
			}
		}
	}


	public static class MenuHelper
	{
		public static void AddButton(string text, Action act, int selectedMenu, string[] buttonNames, ref int buttonIndex, ref int numButtons)
		{
			buttonNames[buttonIndex] = text;

			if (selectedMenu == buttonIndex)
			{
				Main.PlaySound(SoundID.MenuOpen);
				act();
			}

			buttonIndex++;
			numButtons++;
		}
	}
}