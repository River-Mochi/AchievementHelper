using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AchievementFixer
{
    /// <summary>
    /// Formatting helper only. No game/UI references.
    /// Converts internal name/ID => returns a nice display title.
    /// </summary>
    internal static class AchievementDisplay
    {
        // Hard-fixes a few problematic titles; everything else gets simple split
        private static readonly Dictionary<string, string> s_Overrides =
            new(StringComparer.OrdinalIgnoreCase)
            {
                // problem titles (glued lowercase bridges etc.)
                ["ALittleBitofTLC"] = "A Little Bit of TLC",
                ["CallingtheShots"] = "Calling the Shots",
                ["HappytobeofService"] = "Happy to be of Service",
                ["OneofEverything"] = "One of Everything",
                ["OutforaSpin"] = "Out for a Spin",
                ["TopoftheClass"] = "Top of the Class",
                ["WelcomeOneandAll"] = "Welcome One and All",
                ["NowTheyreAllAshTrees"] = "Now They’re All Ash Trees",
            };

        // Splitter: insert spaces before Capital letters only (no digit logic)
        // This avoids mid-word splits like "Emissi on" that came from more aggressive patterns.
        private static readonly Regex s_SimpleSplit =
            new(@"(?<!^)(?=[A-Z])", RegexOptions.Compiled);

        private static readonly HashSet<string> s_MidLower =
            new(StringComparer.OrdinalIgnoreCase)
            { "of", "the", "to", "and", "in", "on", "for", "a", "an", "be", "or", "with" };

        // Formatting only
        public static string Get(string internalNameOrId)
        {
            if (string.IsNullOrWhiteSpace(internalNameOrId))
                return string.Empty;

            // If we have the name, use it.
            if (s_Overrides.TryGetValue(internalNameOrId, out var ov))
                return ov;

            // Otherwise, simple CamelCase split (preserve acronyms)
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
