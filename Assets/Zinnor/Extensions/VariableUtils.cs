using System;

namespace Zinnor.Extensions
{
    public static class VariableUtils
    {
        public static bool HasEnvironmentVariable(string variable)
        {
            return GetEnvironmentVariable(variable) != null;
        }

        public static string GetEnvironmentVariable(string variable)
        {
            return Environment.GetEnvironmentVariable(variable);
        }

        public static void SetEnvironmentVariable(string variable, string value)
        {
            Environment.SetEnvironmentVariable(variable, value);
        }

        public static void DeleteEnvironmentVariable(string variable)
        {
            Environment.SetEnvironmentVariable(variable, null);
        }
    }
}