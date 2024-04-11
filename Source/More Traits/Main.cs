using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Garthor_More_Traits.Compatibility;
using HarmonyLib;
using Verse;

namespace Garthor_More_Traits;

[StaticConstructorOnStartup]
internal class Main
{
    static Main()
    {
        new Harmony("Garthor.More_Traits").PatchAll();
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
                        dictionary.Add(method, new PatchProcessor(new Harmony("Garthor.More_Traits"), method));
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