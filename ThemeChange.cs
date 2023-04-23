using Terraria;
using Terraria.ModLoader;
using System;

namespace PrimordialUnexceptancy
{
    public class ThemeChange : ModSceneEffect
    {
        public int phase = 1;
        public bool enabled = false;
        public override SceneEffectPriority Priority
        {
            get
            {
                return SceneEffectPriority.BossHigh;
            }
        }
        public override bool IsSceneEffectActive(Player player)
        {
            ModLoader.TryGetMod("ThoriumMod", out Mod Thorium);
            ModNPC Aquaius = Thorium.Find<ModNPC>("Aquaius");
            ModNPC Omnicide = Thorium.Find<ModNPC>("Omnicide");
            ModNPC SlagFury = Thorium.Find<ModNPC>("SlagFury");
            ModNPC DreamEater = Thorium.Find<ModNPC>("RealityBreaker");

            bool AquaiusAlive = NPC.AnyNPCs(Aquaius.Type); 
            bool OmnicideAlive = NPC.AnyNPCs(Omnicide.Type); 
            bool SlagFuryAlive = NPC.AnyNPCs(SlagFury.Type);

            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active)
                {
                    if (npc.type == Aquaius.Type || npc.type == Omnicide.Type || npc.type == SlagFury.Type)
                    {
                        if (AquaiusAlive && OmnicideAlive && SlagFuryAlive)
                        {
                            phase = 1;
                            return true;
                        }
                        else if (!AquaiusAlive || !OmnicideAlive || !SlagFuryAlive)
                        {
                            phase = 2;
                            return true;
                        }
                    }
                    else if (npc.type == DreamEater.Type)
                    {
                        if ((double)npc.GetLifePercent() > 0.4)
                        {
                            phase = 3;
                            return true;
                        }
                        else if ((double)npc.GetLifePercent() < 0.4)
                        {
                            phase = 4;
                            return true;
                        }
                    }

                }
            }
            return false;
        }
        public override int Music
        {
            get
            {
                return MusicLoader.GetMusicSlot(Mod, "Assets/Sounds/Music/part" + phase as string);
            }
        }
        public override float GetWeight(Player player)
        {
            return 1f;
        }

    }
}
