﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Garthor_More_Traits.Compatibility;
using HarmonyLib;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x02000027 RID: 39
    [StaticConstructorOnStartup]
    internal class Main
    {
        // Token: 0x06000043 RID: 67 RVA: 0x00003A78 File Offset: 0x00001C78
        static Main()
        {
            var harmony = new Harmony("Garthor.More_Traits");
            harmony.PatchAll();
            var enumerable = from x in Assembly.GetExecutingAssembly().GetTypes()
                where x.IsClass && x.Namespace == "Garthor_More_Traits.Compatibility"
                select x;
            foreach (var type in enumerable)
            {
                var customAttribute = type.GetCustomAttribute<PatchIfModAttribute>();
                if (customAttribute == null || !customAttribute.IsModLoaded())
                {
                    continue;
                }

                var dictionary = new Dictionary<MethodBase, PatchProcessor>();
                foreach (var methodInfo in type.GetMethods())
                {
                    foreach (var harmonyPatch in methodInfo.GetCustomAttributes<HarmonyPatch>())
                    {
                        MethodBase method = harmonyPatch.info.declaringType.GetMethod(harmonyPatch.info.methodName);
                        if (method is not null && !dictionary.ContainsKey(method))
                        {
                            dictionary.Add(method, new PatchProcessor(harmony, method));
                        }

                        if (method is null)
                        {
                            continue;
                        }

                        var patchProcessor = dictionary[method];
                        if (methodInfo.GetCustomAttributes<HarmonyPrefix>().Any())
                        {
                            patchProcessor.AddPrefix(new HarmonyMethod(methodInfo));
                        }

                        if (methodInfo.GetCustomAttributes<HarmonyPostfix>().Any())
                        {
                            patchProcessor.AddPostfix(new HarmonyMethod(methodInfo));
                        }

                        if (methodInfo.GetCustomAttributes<HarmonyTranspiler>().Any())
                        {
                            patchProcessor.AddTranspiler(new HarmonyMethod(methodInfo));
                        }

                        if (methodInfo.GetCustomAttributes<HarmonyFinalizer>().Any())
                        {
                            patchProcessor.AddFinalizer(new HarmonyMethod(methodInfo));
                        }
                    }
                }

                foreach (var patchProcessor2 in dictionary.Values)
                {
                    patchProcessor2.Patch();
                }
            }
        }
    }
}