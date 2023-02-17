using Verse;

namespace RimSlayer
{
    public class BaseModSettings : ModSettings
    {
        // A setting
        public float critChance = 50.0f;
        public float damageMultiplier = 2.0f;

        public float GetModifiedCritChance() { 
            return critChance * 100; 
        }

        public override void ExposeData()
        {
            // Save settings
            base.ExposeData();
            Scribe_Values.Look(ref critChance, "critChance");
            Scribe_Values.Look(ref damageMultiplier, "damageMultiplier");
        }
    }
}