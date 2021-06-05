﻿using HarmonyLib;
using System;

namespace IndustriesSunsetHarborMerged.IndustriesSunsetHarborMerged
{
	[HarmonyPatch(typeof(PowerPlantAI), "GetLocalizedStats")]
    public static class PowerPlantSourceAI
    {
		[HarmonyPrefix]
		public static bool Prefix()
        {
			return false;
        }

		[HarmonyPostfix]
        public static void Postfix(PowerPlantAI __instance, ushort buildingID, ref Building data, ref string __result)
		{
			int electricityRate = __instance.GetElectricityRate(buildingID, ref data);
			string text = LocaleFormatter.FormatGeneric("AIINFO_ELECTRICITY_PRODUCTION", new object[]
			{
				(electricityRate * 16 + 500) / 1000
			});
			text += Environment.NewLine;
			string name = __instance.m_resourceType.ToString();
			name = name.Replace("Grain", "Crops");
			name = name.Replace("Flours", "Flour");
			name = name.Replace("Metals", "Metal");
			int resourceDuration = __instance.GetResourceDuration(buildingID, ref data);
			text += name + " stored for " + resourceDuration + " weeks";
			__result = text;
		}
    }
}