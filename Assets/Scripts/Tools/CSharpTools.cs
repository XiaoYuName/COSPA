using System;
using System.Collections;
using System.Collections.Generic;

using Microsoft.CSharp;
using System.Reflection;
using System.Reflection.Emit;

namespace ARPG
{
    public static class CSharpTools
    {
       

        public static void RunTimeCreatEnum(string EnumName,List<string> EnumProp)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            AssemblyName aName = new AssemblyName("ARPG");
            AssemblyBuilder ab = AssemblyBuilder.DefineDynamicAssembly(aName, AssemblyBuilderAccess.RunAndCollect);
            ModuleBuilder mb = ab.DefineDynamicModule(aName.Name+".dll");
            EnumBuilder eb = mb.DefineEnum(EnumName, TypeAttributes.Public, typeof(int));
            for (int i = 0; i < EnumProp.Count; i++)
            {
                eb.DefineLiteral(EnumProp[i], i);
            }
            Type finished = eb.CreateTypeInfo();
        }
    }
    }
