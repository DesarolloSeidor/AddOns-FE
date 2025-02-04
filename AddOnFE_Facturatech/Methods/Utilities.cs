﻿using System;
using System.Runtime.InteropServices;

namespace AddOnFE_Facturatech.Methods
{
    internal class Utilities
    {
        public static void Release(params object[] objects)
        {
            foreach (var obj in objects)
            {
                ReleaseOne(obj);
            }
        }

        private static bool NotComObj(object o)
        {
            return !"System.__ComObject".Equals(o.GetType().ToString());
        }

        private static void ReleaseOne(object o)
        {
            if (o == null || NotComObj(o))
            {
                return;
            }

            Marshal.ReleaseComObject(o);
            GC.Collect();
        }

    }
}
