﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataMover.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=PA-IMI-VAL-01\\MSSQLSRVR_2008R2;Initial Catalog=PSH_BOR1_003_1200_131_" +
            "DEV;Integrated Security=True")]
        public string PSH_BOR1_003_1200_131_DEVConnectionString {
            get {
                return ((string)(this["PSH_BOR1_003_1200_131_DEVConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=PA-IMI-VAL-01\\MSSQLSRVR_2008R2;Initial Catalog=PSH_BOR1_003_1200_131_" +
            "PROD_1;Integrated Security=True")]
        public string PSH_BOR1_003_1200_131_PROD_1ConnectionString {
            get {
                return ((string)(this["PSH_BOR1_003_1200_131_PROD_1ConnectionString"]));
            }
        }
    }
}