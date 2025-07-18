#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

public class WebOptimizer
{
    [MenuItem("WebGL/Optimize for Build")]
    public static void Optimize()
    {
        var namedBuildTarget = NamedBuildTarget.WebGL;

        // Set release build
        EditorUserBuildSettings.development = false;
        
        // Set IL2CPP code generation to Optimize Size 
        PlayerSettings.SetIl2CppCodeGeneration(namedBuildTarget,         
            Il2CppCodeGeneration.OptimizeSize);

        // Set the Managed Stripping Level to High
        PlayerSettings.SetManagedStrippingLevel(namedBuildTarget,     
            ManagedStrippingLevel.High);  

        // Strip unused mesh components           
        PlayerSettings.stripUnusedMeshComponents = true;

        // Enable data caching
        PlayerSettings.WebGL.dataCaching = true;

        // Set the compression format to Brotli
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Brotli;

        // Deactivate exceptions
        //PlayerSettings.WebGL.exceptionSupport = WebGLExceptionSupport.None;

        // Deactivate debug symbols
        PlayerSettings.WebGL.debugSymbolMode = WebGLDebugSymbolMode.Off;

        //Enable WebAssembly 2023 features
        PlayerSettings.WebGL.wasm2023 = true;           

        // Set Code Optimization to 'Disk Size with LTO'
        EditorUserBuildSettings.SetPlatformSettings(
            BuildTarget.WebGL.ToString(),
            "CodeOptimization",
            "DiskSizeLTO"
        );
    }
}
#endif