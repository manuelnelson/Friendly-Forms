﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessLogic.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BusinessLogic.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Emmanuel@FriendlyForm.com.
        /// </summary>
        internal static string FromEmail {
            get {
                return ResourceManager.GetString("FromEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ManitoRamez24.
        /// </summary>
        internal static string FromPassword {
            get {
                return ResourceManager.GetString("FromPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Only alpha-numeric characters and [.,_-&apos;] are allowed..
        /// </summary>
        internal static string InvalidInputCharacter {
            get {
                return ResourceManager.GetString("InvalidInputCharacter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to smtp.gmail.com.
        /// </summary>
        internal static string MailServerName {
            get {
                return ResourceManager.GetString("MailServerName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(?!.*--)[A-Za-z0-9\.,&apos;_ \-\s]*$.
        /// </summary>
        internal static string TextLineInputValidatorRegEx {
            get {
                return ResourceManager.GetString("TextLineInputValidatorRegEx", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to OpenID Url.
        /// </summary>
        internal static string UserAuthorizationIdLabelText {
            get {
                return ResourceManager.GetString("UserAuthorizationIdLabelText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name must be less than 15 characters.
        /// </summary>
        internal static string UserDisplayNameStringLengthValidationError {
            get {
                return ResourceManager.GetString("UserDisplayNameStringLengthValidationError", resourceCulture);
            }
        }
    }
}
