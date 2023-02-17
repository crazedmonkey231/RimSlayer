using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;

namespace RimSlayer
{

    [HarmonyPatch(typeof(Pawn), nameof(Pawn.PreApplyDamage))]
    public static class Patch
    {
        public static void Postfix(Pawn __instance, ref DamageInfo dinfo, out bool absorbed)
        {
            if (__instance.genes.HasXenogene(GeneDefOf.Bloodfeeder) 
                && dinfo.Instigator is Pawn pawn && pawn != null 
                && pawn.equipment.Primary is ThingWithComps_Stake)
            {
                Random rand = new Random();
                float critChance = rand.Next(0, 100);
                float damage = 1.0f;
                Log.Message("Crit chance = " + critChance);
                if(critChance < BaseMod.settings.GetModifiedCritChance())
                {
                    MoteMaker.ThrowText(__instance.Position.ToVector3(), __instance.Map, "Critical Strike!");
                    damage = BaseMod.settings.damageMultiplier;
                }
                float newAmount = dinfo.Amount * damage;
                dinfo.SetAmount(newAmount);
            }
            absorbed = false;
        }
    }
}
