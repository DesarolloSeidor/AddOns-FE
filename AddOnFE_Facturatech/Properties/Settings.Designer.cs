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
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("FE_DIAN_LOG")]
        public string FileLog {
            get {
                return ((string)(this["FileLog"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://ws.facturatech.co/v2/demo/index.php?wsdl")]
        public string AddOnFE_Facturatech_FacturatechWS_SERVICES_FACTURATECH {
            get {
                return ((string)(this["AddOnFE_Facturatech_FacturatechWS_SERVICES_FACTURATECH"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ws-dse.facturatech.co/v1/demo/?wsdl")]
        public string AddOnFE_Facturatech_facturatechWSdse_DOCUMENTO_SOPORTE_FACTURATECH {
            get {
                return ((string)(this["AddOnFE_Facturatech_facturatechWSdse_DOCUMENTO_SOPORTE_FACTURATECH"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://ws-nomina.facturatech.co/v1/demo/index.php?wsdl")]
        public string AddOnFE_Facturatech_FacturatechWSNC_SERVICES_NOMINA_FACTURATECH {
            get {
                return ((string)(this["AddOnFE_Facturatech_FacturatechWSNC_SERVICES_NOMINA_FACTURATECH"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("60000")]
        public double TimerStatus {
            get {
                return ((double)(this["TimerStatus"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("300000")]
        public double TimerResend {
            get {
                return ((double)(this["TimerResend"]));
            }
        }
    }
}
