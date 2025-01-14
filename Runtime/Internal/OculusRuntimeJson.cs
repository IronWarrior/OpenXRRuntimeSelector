// Copyright 2021 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.IO;
using Microsoft.Win32;

namespace OpenXRRuntimeJsons.Internal
{
    internal class OculusRuntimeJson : IOpenXRRuntimeJson
    {
        private const string InstallLocKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Oculus";
        private const string InstallLocValue = "InstallLocation";
        private const string JsonName = @"Support\oculus-runtime\oculus_openxr_64.json";

        public OpenXRRuntimeType Name => OpenXRRuntimeType.Oculus;

        public bool TryGetJsonPath(out string jsonPath)
        {
            var oculusPathValue = OpenXRRuntimeJson.RegistryGetValue(InstallLocKey, InstallLocValue, string.Empty);
            if (oculusPathValue is string oculusPath && !string.IsNullOrWhiteSpace(oculusPath))
            {
                var path = Path.Combine(oculusPath, JsonName);
                if (File.Exists(path))
                {
                    jsonPath = Path.GetFullPath(path);
                    return true;
                }
            }

            jsonPath = default;
            return false;
        }
    }
}