using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace RimSlayer
{
    // Add references for UnityEngine, UnityEngin.CoreModule, Assembly-Csharp
    // These can be found for example in <Drive>\Steam\SteamApps\common\RimWorld\RimWorldWin64_Data\Managed
    // Right click on References and Add Reference then navigate to the folder to add the reference.
    // Add Harmony reference by navigating to the mod folder and in Assemblies, add the assembly.
    // Be sure to turn off Copy Local in the Properties window of the referenced assembly.
    // Lastly, right click on ModAssembly->Properties->Build->Output Path->Browse->*Set to assemblies folder in this mod*->Save
    // Then you can Build->Build Solution
    public class BaseMod : Mod
    {
        // Static reference for settings
        // Use BaseMod.settings.<setting_name> to get value in other parts of code.
        public static BaseModSettings settings;

        // Uncomment out to use harmony, be sure to add the reference assembly
        public static Harmony harmony;
        

        // Constructor to load your settings
        public BaseMod(ModContentPack content) : base(content)
        {

            // Uncomment below to use harmony.
            // Be sure to add the reference assembly.
            // This will run all patches marked with [HarmonyPatch]

            harmony = new Harmony(Content.Name);
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            settings = GetSettings<BaseModSettings>();

            
        }

        // Page for mod settings
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.Gap();
            listingStandard.Label("----- Welcome -----");
            listingStandard.Gap();
            listingStandard.Label("Critical Strike Chance: " + settings.critChance);
            settings.critChance = (int)listingStandard.Slider(settings.critChance, 0, 100);
            listingStandard.Label("Critical Strike Damage Multiplier: " + settings.damageMultiplier);
            settings.damageMultiplier = (int)listingStandard.Slider(settings.damageMultiplier, 1, 100);
            listingStandard.Gap();
            listingStandard.End();
        }

        // Name of your settings
        public override string SettingsCategory()
        {
            return Content.Name;
        }
    }
}
