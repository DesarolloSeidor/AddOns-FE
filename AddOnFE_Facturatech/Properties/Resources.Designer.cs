﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AddOnFE_Facturatech.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AddOnFE_Facturatech.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select &quot;SeriesName&quot; From NNM1 Where &quot;Series&quot; =  $[@FEDIAN_NUMAUTORI.Code].
        /// </summary>
        internal static string DescNume {
            get {
                return ResourceManager.GetString("DescNume", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select &quot;Name&quot; From &quot;@FEDIAN_RESPONSA&quot; Where &quot;Code&quot; =  $[@FEDIAN_SNRES.U_Codigo].
        /// </summary>
        internal static string DescRespon {
            get {
                return ResourceManager.GetString("DescRespon", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select &quot;Name&quot; From &quot;@FEDIAN_TRIBU&quot; Where &quot;Code&quot; =  $[@FEDIAN_SNTRI.U_Codigo].
        /// </summary>
        internal static string DescTribu {
            get {
                return ResourceManager.GetString("DescTribu", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select &quot;Code&quot;, &quot;Name&quot; 
        ///From &quot;@FEDIAN_CODDOC&quot;.
        /// </summary>
        internal static string ListaDocDIAN {
            get {
                return ResourceManager.GetString("ListaDocDIAN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select &quot;Series&quot;, &quot;SeriesName&quot;, &quot;InitialNum&quot;, &quot;LastNum&quot;, &quot;DocSubType&quot;,
        ///Case 
        ///When &quot;ObjectCode&quot; = &apos;14&apos; Then &apos;Nota de Cerdito&apos;
        ///When &quot;ObjectCode&quot; = &apos;13&apos; And &quot;DocSubType&quot; = &apos;--&apos; Then &apos;Factura de Venta&apos; 
        ///When &quot;ObjectCode&quot; = &apos;13&apos; And &quot;DocSubType&quot; = &apos;DN&apos; Then &apos;Nota de Debito&apos; 
        ///When &quot;ObjectCode&quot; = &apos;13&apos; And &quot;DocSubType&quot; = &apos;IX&apos; Then &apos;Factura de Exportacion&apos; 
        ///When &quot;ObjectCode&quot; = &apos;13&apos; And &quot;DocSubType&quot; = &apos;RI&apos; Then &apos;Factura de Reserva&apos; 
        ///When &quot;ObjectCode&quot; = &apos;18&apos; And &quot;DocSubType&quot; = &apos;--&apos; Then &apos;Factura de Proveedores&apos;  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ListaNume {
            get {
                return ResourceManager.GetString("ListaNume", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select * from &quot;@FEDIAN_RESPONSA&quot;
        ///Order By &quot;Code&quot;.
        /// </summary>
        internal static string ListaRespon {
            get {
                return ResourceManager.GetString("ListaRespon", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select * from &quot;@FEDIAN_TRIBU&quot;
        ///Order By &quot;Code&quot;.
        /// </summary>
        internal static string ListaTribu {
            get {
                return ResourceManager.GetString("ListaTribu", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select &quot;Code&quot;, &quot;U_Descripcion&quot; 
        ///From &quot;@FEDIAN_UM&quot;.
        /// </summary>
        internal static string ListaUM {
            get {
                return ResourceManager.GetString("ListaUM", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select &quot;CardName&quot; From &quot;OCRD&quot; Where &quot;CardCode&quot; =  $[@FEDIAN_SN.Code].
        /// </summary>
        internal static string NombreSN {
            get {
                return ResourceManager.GetString("NombreSN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select * from &quot;@HBT_TIPODOC&quot;.
        /// </summary>
        internal static string TipoDoc {
            get {
                return ResourceManager.GetString("TipoDoc", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select 
        ///Case 
        ///When $[$38.1470002145.0] = &apos;Manual&apos; Then (SELECT T0.&quot;U_DIAN_UM&quot; FROM &quot;@FEDIAN_HOMOL_UM&quot; T0 WHERE T0.&quot;U_SAP_UM&quot; = $[$38.212.0])
        ///Else (SELECT T0.&quot;U_DIAN_UM&quot; FROM &quot;@FEDIAN_HOMOL_UM&quot; T0 WHERE T0.&quot;U_SAP_UM&quot; = $[$38.1470002145.0])
        ///End 
        ///From &quot;OADM&quot;.
        /// </summary>
        internal static string UM_DIAN {
            get {
                return ResourceManager.GetString("UM_DIAN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select C0.&quot;Unidad&quot;, C0.&quot;Descr&quot;
        ///From(
        ///	Select  
        ///	Case When &quot;UomCode&quot; = &apos;Manual&apos; Then &quot;unitMsr&quot; Else &quot;UomCode&quot; End &quot;Unidad&quot;, 
        ///	&quot;unitMsr&quot; as &quot;Descr&quot; 
        ///	From INV1
        ///	Group By &quot;UomCode&quot;, &quot;unitMsr&quot;
        ///)C0 
        ///Where IsNull(&quot;Unidad&quot;,&apos;&apos;) != &apos;&apos; And C0.&quot;Unidad&quot; Not In (Select &quot;U_SAP_UM&quot; From &quot;@FEDIAN_HOMOL_UM&quot; Where IsNull(&quot;U_DIAN_UM&quot;,&apos;&apos;) != &apos;&apos;)
        ///Group By C0.&quot;Unidad&quot;, C0.&quot;Descr&quot;.
        /// </summary>
        internal static string UM_Pendientes {
            get {
                return ResourceManager.GetString("UM_Pendientes", resourceCulture);
            }
        }
    }
}
