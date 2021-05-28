﻿using HarmonyLib;
using System;

namespace FishIndustryEnhanced
{
	[HarmonyPatch(typeof(PowerPlantAI), "GetLocalizedStats")]
    public static class AlgaeBioreactorAI
    {
		[HarmonyPostfix]
        public static void Postfix(PowerPlantAI __instance, ushort buildingID, ref Building data, ref string __result)
		{
			int electricityRate = __instance.GetElectricityRate(buildingID, ref data);
			string text = LocaleFormatter.FormatGeneric("AIINFO_ELECTRICITY_PRODUCTION", new object[]
			{
				(electricityRate * 16 + 500) / 1000
			});
			if (__instance.m_resourceType == TransferManager.TransferReason.Coal)
			{
				text += Environment.NewLine;
				int resourceDuration = __instance.GetResourceDuration(buildingID, ref data);
				text += LocaleFormatter.FormatGeneric("AIINFO_COAL_STORED", new object[]
				{
					resourceDuration
				});
			}
			else if (__instance.m_resourceType == TransferManager.TransferReason.Petrol)
			{
				text += Environment.NewLine;
				int resourceDuration2 = __instance.GetResourceDuration(buildingID, ref data);
				text += LocaleFormatter.FormatGeneric("AIINFO_OIL_STORED", new object[]
				{
					resourceDuration2
				});
			}
			else if (__instance.m_resourceType == TransferManager.TransferReason.Grain)
			{
				text += Environment.NewLine;
				int resourceDuration3 = __instance.GetResourceDuration(buildingID, ref data);
				text += "Crops stored for " + resourceDuration3 + "weeks";
			}
			__result = text;
		}
    }
}
