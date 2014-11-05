﻿using Serenity.ComponentModel;
using Serenity.Extensibility;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Serenity.Web
{
    public class FormScriptRegistration
    {
        public static void RegisterFormScripts()
        {
            var assemblies = ExtensibilityHelper.SelfAssemblies;

            var scripts = new List<Func<string>>();

            foreach (var assembly in assemblies)
                foreach (var type in assembly.GetTypes())
                {
                    var attr = type.GetCustomAttribute<FormScriptAttribute>();
                    if (attr != null)
                        scripts.Add(new FormScript(attr.Key, type).GetScript);
                }

            DynamicScriptManager.Register("FormBundle", new ConcatenatedScript(scripts));
        }
    }
}
