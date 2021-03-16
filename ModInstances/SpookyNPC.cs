using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework;
using SpookyTerraria.NPCs;
using SpookyTerraria.OtherItems;
using SpookyTerraria.TownNPCSpawnBooks;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using SpookyTerraria.ModIntances;
using SpookyTerraria.Utilities;

namespace SpookyTerraria
{
    public class SpookyNPCs : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;

        public int heartBeatTimer;

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            switch (type)
            {
                case NPCID.Merchant:
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<Battery>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 10, 0);
                    nextSlot++;
                    break;
                case NPCID.TravellingMerchant:
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<Battery>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 10, 0);
                    nextSlot++;
                    break;
                case NPCID.Demolitionist:
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<Battery>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 10, 0);
                    nextSlot++;
                    break;
            }
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            var player = Main.player[Main.myPlayer];
            if (ModContent.GetInstance<SpookyTerraria>().beatGame ||
                Main.worldName == SpookyTerrariaUtils.slenderWorldName)
                pool.Clear(); // No more enemy spawns!.. Because you beat the game, numnut

            if (!ModContent.GetInstance<SpookyConfigServer>().excludeAllNPCsBesideCreepyTerrariaOnesFromSpawnPool) return;

            if (ModContent.GetInstance<SpookyConfigServer>().excludeAllNPCsBesideCreepyTerrariaOnesFromSpawnPool && !ModContent.GetInstance<SpookyTerraria>().beatGame)
            {
                pool.Clear();
                if (!player.GetModPlayer<SpookyPlayer>().stalkerConditionMet && Main.worldName != SpookyTerrariaUtils.slenderWorldName)
                    foreach (var SpookyNPCs in Lists_SpookyNPCs.SpookyNPCs) pool.Add(SpookyNPCs, 1f);
            }
        }

        public override void NPCLoot(NPC npc)
        {
            var downedBossAny = NPC.downedBoss1 || NPC.downedBoss2 || NPC.downedBoss3 || NPC.downedSlimeKing;
            if (Main.rand.NextFloat() < 0.02f)
                Item.NewItem(npc.getRect(), ModContent.ItemType<GuideBook>());
            else if (Main.rand.NextFloat() < 0.02f)
                Item.NewItem(npc.getRect(), ModContent.ItemType<MerchantBook>());
            else if (Main.rand.NextFloat() < 0.02f)
                Item.NewItem(npc.getRect(), ModContent.ItemType<NurseBook>());
            else if (Main.rand.NextFloat() < 0.02f)
                Item.NewItem(npc.getRect(), ModContent.ItemType<ArmsDealBook>());
            else if (Main.rand.NextFloat() < 0.02f)
                Item.NewItem(npc.getRect(), ModContent.ItemType<DyeTraderBook>());
            else if (Main.rand.NextFloat() < 0.02f)
                Item.NewItem(npc.getRect(), ModContent.ItemType<PainterBook>());
            else if (Main.rand.NextFloat() < 0.02f) Item.NewItem(npc.getRect(), ModContent.ItemType<DemoBook>());

            if (downedBossAny)
            {
                if (Main.rand.NextFloat() < 0.02f)
                    Item.NewItem(npc.getRect(), ModContent.ItemType<DryadBook>());
                else if (Main.rand.NextFloat() < 0.02f)
                    Item.NewItem(npc.getRect(), ModContent.ItemType<StylistBook>());
                else if (Main.rand.NextFloat() < 0.02f)
                    Item.NewItem(npc.getRect(), ModContent.ItemType<AnglerBook>());
                else if (Main.rand.NextFloat() < 0.02f)
                    Item.NewItem(npc.getRect(), ModContent.ItemType<TaxCollectBook>());
                else if (Main.rand.NextFloat() < 0.02f)
                    Item.NewItem(npc.getRect(), ModContent.ItemType<TravMerchBook>());
                else if (Main.rand.NextFloat() < 0.02f)
                    Item.NewItem(npc.getRect(), ModContent.ItemType<WitDocBook>());
                else if (Main.rand.NextFloat() < 0.02f)
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PartyGirlBook>());
                else if (Main.rand.NextFloat() < 0.02f) Item.NewItem(npc.getRect(), ModContent.ItemType<ClothierBook>());
            }
        }

        public override void GetChat(NPC npc, ref string chat)
        {
            var player = Main.player[Main.myPlayer];
            if (npc.type == NPCID.Painter)
                switch (Main.rand.Next(5))
                {
                    case 1:
                        chat = "Painting in black isn't the best idea nowadays...";
                        break;
                    case 2 when SpookyPlayer.Pages >= 1:
                        chat = "My paint is going to fall over! Stop this!";
                        break;
                    case 3:
                        chat =
                            $"Wanna know a nerd fact?\nThe exact date is {System.DateTime.Now}. Don't ask why I know this.";
                        break;
                    case 4:
                        chat = "The crickets and the sounds of nature are nice to listen to while painting.";
                        break;
                }

            if (npc.type == NPCID.ArmsDealer)
                switch (Main.rand.Next(5))
                {
                    case 1:
                        chat = "Why in the world is it so damn dark? Good thing I got guns with me.";
                        break;
                    case 2 when SpookyPlayer.Pages >= 1:
                        chat = "What is this rumbling? I'm getting creeped out...";
                        break;
                    case 3:
                        chat = "Why did the music stop?";
                        break;
                    case 4:
                        chat = "Just buy my guns. That's all I gotta say.";
                        break;
                }

            if (npc.type == NPCID.Dryad)
            {
                var armsDealerIndex = NPC.FindFirstNPC(NPCID.ArmsDealer);
                switch (Main.rand.Next(5))
                {
                    case 1:
                        chat = "The sounds of nature are calming, but the immense darkness frightens me...";
                        break;
                    case 2 when SpookyPlayer.Pages >= 1:
                        chat = "Is this what goes bump in the night?";
                        break;
                    case 3:
                        chat = "Nature can sometimes get freaky.";
                        break;
                    case 4 when armsDealerIndex > -1:
                        chat = $"{Main.npc[armsDealerIndex].GivenName}? Pfft. What a clown. I hate him.";
                        break;
                }
            }

            if (npc.type == NPCID.Merchant)
                switch (Main.rand.Next(5))
                {
                    case 1:
                        chat = "The sun is low! My prices are not.";
                        break;
                    case 2 when SpookyPlayer.Pages >= 1:
                        chat = "What is this feeling? It's Like I'm being watched.";
                        break;
                    case 3:
                        chat = "My venilated armor is feeling quite hot right now!";
                        break;
                    case 4:
                        chat = $"Hey {player.name}, buy these arrows and kill whatever is lurking around here.";
                        break;
                }

            if (npc.type == NPCID.Nurse)
                switch (Main.rand.Next(5))
                {
                    case 1:
                        chat = "Yeah, yeah. Let me heal you. Pay up first.";
                        break;
                    case 2 when SpookyPlayer.Pages >= 1:
                        chat = "Oh? Baby want his milk? Is the rumbling too scary-wary for you?";
                        break;
                    case 3:
                        chat = "Ugh.. What now?";
                        break;
                    case 4:
                        chat = "This darkness makes me feel at home.";
                        break;
                }

            if (npc.type == NPCID.Guide)
                switch (Main.rand.Next(5))
                {
                    case 1:
                        chat =
                            "This darkness is not for the light hearted. For what I know, I have heard things that lurk around in the darkness here.";
                        break;
                    case 2 when SpookyPlayer.Pages >= 1:
                        chat = "When you hear rumbling, you know that you are being watched.";
                        break;
                    case 3:
                        chat =
                            "This ambience of crickets and wind blowing against the trees is calming, but the dark sky immediately revokes all of that.";
                        break;
                    case 4:
                        chat = $"What's up, {player.name}! I'm just sitting around thinking about how we might die.";
                        break;
                }

            if (npc.type == NPCID.Demolitionist)
                switch (Main.rand.Next(5))
                {
                    case 1:
                        chat =
                            "It's kinda hard to see when it is this dark. Good thing the fuses on my explosives can change that!";
                        break;
                    case 2 when SpookyPlayer.Pages >= 1:
                        chat = "I'm pretty sure this rumbling isn't from one of my babies.";
                        break;
                    case 3:
                        chat = "I gotta say, while caving I have never witnessed such darkness.";
                        break;
                    case 4:
                        chat = "Is it just me or do these trees look more slender to you?";
                        break;
                }

            if (npc.type == NPCID.Wizard)
                switch (Main.rand.Next(5))
                {
                    case 1:
                        chat = "Wanna die? No? Ok.";
                        break;
                    case 2 when SpookyPlayer.Pages >= 1:
                        chat = "Want me to stop this rumbling? Yes? Too bad.";
                        break;
                    case 3:
                        chat = "My pure wizardry is too much for this world to handle.";
                        break;
                    case 4:
                        chat = "My magic ball can read when you all die!";
                        break;
                }

            if (npc.type == NPCID.Mechanic)
                switch (Main.rand.Next(5))
                {
                    case 1:
                        chat = "Why is all the wifi down?";
                        break;
                    case 2 when SpookyPlayer.Pages >= 1:
                        chat =
                            "Engineering gets more and more depressing every day, especially when you feel like rumbles will just tear it all down.";
                        break;
                    case 3:
                        chat = "Damn these crickets! They are so annoying when someone is trying to work!";
                        break;
                    case 4 when player.GetModPlayer<SpookyPlayer>().heartRate >= 100:
                        chat =
                            $"Hurry it up {player.name}. I mean, I can already tell you are in a hurry because your heartrate is {player.GetModPlayer<SpookyPlayer>().heartRate}";
                        break;
                }

            if (npc.type == NPCID.Stylist)
                switch (Main.rand.Next(5))
                {
                    case 1:
                        chat = "These scissors can come handy in may ways...";
                        break;
                    case 2 when SpookyPlayer.Pages >= 1:
                        chat = "This is a very uncertain feeling...";
                        break;
                    case 3:
                        chat = "You want a haircut? Are you sure you can trust me?";
                        break;
                    case 4:
                        chat = "I can see it in the headlines: 'Local Hairstylist Murders Patient'.";
                        break;
                }
        }

        public static int slenderFailsafeTimer;

        public override void PostAI(NPC npc)
        {
            var player = Main.player[Main.myPlayer];
            var distanceToStalker = npc.Distance(player.Center);
            if (npc.type == ModContent.NPCType<Stalker>())
                if (!player.dead)
                {
                    heartBeatTimer++;
                    if (heartBeatTimer == 1)
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/HeartBeat"), npc.Center);

                    if (heartBeatTimer >= 50 && distanceToStalker >= 750f)
                    {
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/HeartBeat"), npc.Center);
                        heartBeatTimer = 2;
                    }

                    if (heartBeatTimer >= 40 && distanceToStalker < 750f && distanceToStalker >= 300f)
                    {
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/HeartBeat"), npc.Center);
                        heartBeatTimer = 2;
                    }

                    if (heartBeatTimer >= 30 && distanceToStalker < 300f && distanceToStalker >= 100f)
                    {
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/HeartBeat"), npc.Center);
                        heartBeatTimer = 2;
                    }

                    if (heartBeatTimer >= 25 && distanceToStalker < 100f)
                    {
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/HeartBeat"), npc.Center);
                        heartBeatTimer = 2;
                    }
                }

            if (SpookyPlayer.Pages >= 1)
            {
                slenderFailsafeTimer++;

                if (slenderFailsafeTimer >= 500)
                    if (!npc.active && npc.type == ModContent.NPCType<Slenderman>())
                    {
                        mod.Logger.Debug("Slender was not found! Spawning a new slender!");
                        var randChoice = Main.rand.Next(0, 2);
                        NPC.NewNPC(randChoice == 0 ? (int) player.Center.X - 2500 : (int) player.Center.X + 2500, (int) player.Center.Y + Main.rand.Next(-100, 100), ModContent.NPCType<Slenderman>());
                    }
            }
        }
    }
}