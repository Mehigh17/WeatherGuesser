﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeatherGuesser.Wpf.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WeatherGuesser.Wpf.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Directory not found.
        /// </summary>
        internal static string DirectoryNotFoundTitle {
            get {
                return ResourceManager.GetString("DirectoryNotFoundTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The folder &apos;{0}&apos; could not be found, please make sure it exists..
        /// </summary>
        internal static string DirectoryNotFoundWarning {
            get {
                return ResourceManager.GetString("DirectoryNotFoundWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Machine loaded successfully.
        /// </summary>
        internal static string LoadSuccessfull {
            get {
                return ResourceManager.GetString("LoadSuccessfull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Machine ready.
        /// </summary>
        internal static string MachineReady {
            get {
                return ResourceManager.GetString("MachineReady", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Normalizing images.
        /// </summary>
        internal static string NormalizingImages {
            get {
                return ResourceManager.GetString("NormalizingImages", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ready to train.
        /// </summary>
        internal static string ReadyToTrain {
            get {
                return ResourceManager.GetString("ReadyToTrain", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Training.
        /// </summary>
        internal static string Training {
            get {
                return ResourceManager.GetString("Training", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Training complete.
        /// </summary>
        internal static string TrainingComplete {
            get {
                return ResourceManager.GetString("TrainingComplete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please select the location where the root of the training data is located..
        /// </summary>
        internal static string TrainingDataBrowserTip {
            get {
                return ResourceManager.GetString("TrainingDataBrowserTip", resourceCulture);
            }
        }
    }
}
