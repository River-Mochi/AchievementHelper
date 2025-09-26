using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AchievementFixer
{
    internal static class AchievementDisplay
    {
        // Hard-code the few that have format issues below. Everything else gets a simple split.
        private static readonly Dictionary<string, string> s_Overrides =
            new(StringComparer.OrdinalIgnoreCase)
            {
                // problem titles
                ["ALittleBitofTLC"] = "A Little Bit of TLC",
                ["CallingtheShots"] = "Calling the Shots",
                ["HappytobeofService"] = "Happy to be of Service",
                ["OneofEverything"] = "One of Everything",
                ["OutforaSpin"] = "Out for a Spin",
                ["TopoftheClass"] = "Top of the Class",
                ["WelcomeOneandAll"] = "Welcome One and All",
                ["NowTheyreAllAshTrees"] = "Now They’re All Ash Trees",
            };

        // simple splitter: inserts spaces between CamelCase and digits.
        private static readonly Regex s_SimpleSplit =
            new(@"(?<!^)(?=[A-Z0-9])", RegexOptions.Compiled);

        private static readonly HashSet<string> s_MidLower =
            new(StringComparer.OrdinalIgnoreCase) { "of", "the", "to", "and", "in", "on", "for", "a", "an", "be", "or", "with" };

        public static string Get(string internalNameOrId)
        {
            if (string.IsNullOrWhiteSpace(internalNameOrId)) return internalNameOrId ?? "";

            //  If we have the name, use it.
            if (s_Overrides.TryGetValue(internalNameOrId, out var ov)) return ov;

            // Otherwise, simple CamelCase split
            var parts = s_SimpleSplit.Split(internalNameOrId);

            // Title-case each word, except keep short words lowercased when in the middle
            for (int i = 0; i < parts.Length; i++)
            {
                var p = parts[i];

                // keep acronyms (ALLCAPS) as-is
                bool isAcronym = p.Any(char.IsLetter) && p.ToUpperInvariant() == p;
                if (isAcronym) continue;

                if (i > 0 && i < parts.Length - 1 && s_MidLower.Contains(p))
                    parts[i] = p.ToLowerInvariant();
                else
                    parts[i] = char.ToUpper(p[0]) + (p.Length > 1 ? p.Substring(1).ToLowerInvariant() : "");
            }

            return string.Join(" ", parts);
        }
    }
}
