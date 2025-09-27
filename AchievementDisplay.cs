using System;
using System.Collections.Generic;

namespace AchievementFixer
{
    /// <summary>
    /// Formatting helper only. No game/UI references.
    /// Converts internal names like "EverythingTheLightTouches", "HappytobeofService"
    /// to friendly display text by INSERTING SPACES ONLY (no case changes).
    /// </summary>
    internal static class AchievementDisplay
    {
        // Optional: purely stylistic fix (curly apostrophe). Remove if you don't want it.
        private static readonly Dictionary<string, string> s_Overrides =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["NowTheyreAllAshTrees"] = "Now They’re All Ash Trees",
            };

        // Short connector words that may appear glued in lowercase bridges.
        private static readonly HashSet<string> s_Conn =
            new(StringComparer.Ordinal)
            { "to", "be", "of", "for", "a", "the", "and", "in", "on", "or", "with" };

        private const int ConnMaxLen = 5;

        /// <summary>Public entry point.</summary>
        public static string Get(string internalNameOrId)
        {
            if (string.IsNullOrWhiteSpace(internalNameOrId))
                return internalNameOrId ?? string.Empty;

            if (s_Overrides.TryGetValue(internalNameOrId, out var ov))
                return ov;

            return InsertSpacesPreserveCase(internalNameOrId);
        }

        /// <summary>
        /// Core algorithm:
        /// - TitleCase words: split before an Upper+lower sequence.
        /// - ACRONYMS: keep contiguous UPPER runs together (e.g., TLC, USB).
        /// - Digits: split at letter↔digit boundaries.
        /// - Lowercase bridges between TitleCase words: segment into connector words
        ///   (e.g., "tobeof" -> "to be of", "ofthe" -> "of the", "fora" -> "for a").
        /// </summary>
        private static string InsertSpacesPreserveCase(string s)
        {
            var parts = new List<string>(8);
            int i = 0, n = s.Length;

            while (i < n)
            {
                char c = s[i];

                // Numbers
                if (char.IsDigit(c))
                {
                    int j = i + 1;
                    while (j < n && char.IsDigit(s[j])) j++;
                    parts.Add(s.Substring(i, j - i));
                    i = j;
                    continue;
                }

                // TitleCase or ACRONYM starting with Upper
                if (char.IsUpper(c))
                {
                    // TitleCase word (Upper + lower…)
                    if (i + 1 < n && char.IsLower(s[i + 1]))
                    {
                        int j = i + 2;
                        while (j < n && char.IsLower(s[j])) j++;

                        string lowerRun = s.Substring(i + 1, j - (i + 1));
                        bool nextIsUpper = (j < n && char.IsUpper(s[j]));

                        string prefix = lowerRun;
                        List<string>? tails = null;

                        // Only peel connector words when this lowerRun is followed by another Capitalized word.
                        // (Prevents "Decisi on", "Sp in", "Inspect or", "Emissi on".)
                        if (nextIsUpper)
                            PeelSuffixConnectors(lowerRun, out prefix, out tails);

                        // Emit the base TitleCase word (capital + remaining prefix)
                        parts.Add(c + prefix);

                        // Then any peeled connectors ("to be of", "of the", "for a", …)
                        if (tails is { Count: > 0 }) parts.AddRange(tails);

                        i = j;
                        continue;
                    }
                    else
                    {
                        // ACRONYM or single-letter word (e.g., 'A' in MakingAMark)
                        int j = i;
                        while (j + 1 < n && char.IsUpper(s[j + 1]) &&
                               !(j + 2 < n && char.IsLower(s[j + 2])))
                        {
                            j++;
                        }
                        parts.Add(s.Substring(i, j - i + 1));
                        i = j + 1;
                        continue;
                    }
                }

                // Standalone lowercase run (e.g., "the" in CallingtheShots, or "fora" in OutforaSpin)
                if (char.IsLower(c))
                {
                    int j = i + 1;
                    while (j < n && char.IsLower(s[j])) j++;
                    string run = s.Substring(i, j - i);

                    // If it's between Capitalized words, try to segment entirely into connectors.
                    // If segmentation fails, keep as-is.
                    bool betweenCaps = (i > 0 && char.IsUpper(s[i - 1])) || (j < n && char.IsUpper(s[j]));
                    if (betweenCaps)
                    {
                        var seg = SegmentAllConnectors(run);
                        parts.AddRange(seg);
                    }
                    else
                    {
                        parts.Add(run);
                    }

                    i = j;
                    continue;
                }

                // Any other character (rare): keep as-is
                parts.Add(s[i].ToString());
                i++;
            }

            // Join with single spaces
            return string.Join(" ", parts).Replace("  ", " ").Trim();
        }

        /// <summary>
        /// Try to segment a full lowercase run into connector words (left→right).
        /// If any part doesn't match, return the original run as a single token.
        /// </summary>
        private static List<string> SegmentAllConnectors(string run)
        {
            var pieces = new List<string>();
            int k = 0;
            while (k < run.Length)
            {
                bool matched = false;
                int maxLen = Math.Min(ConnMaxLen, run.Length - k);
                for (int len = maxLen; len >= 1; len--)
                {
                    string sub = run.Substring(k, len);
                    if (s_Conn.Contains(sub))
                    {
                        pieces.Add(sub);
                        k += len;
                        matched = true;
                        break;
                    }
                }
                if (!matched)
                {
                    // Not fully segmentable → return original
                    pieces.Clear();
                    pieces.Add(run);
                    return pieces;
                }
            }
            return pieces;
        }

        /// <summary>
        /// From a lowercase run after the initial capital of a TitleCase word,
        /// peel connector words from the RIGHT edge. Return the remaining prefix
        /// (stays attached to the capital) and the list of peeled connectors
        /// in left→right order.
        /// </summary>
        private static void PeelSuffixConnectors(string run, out string prefix, out List<string> tails)
        {
            tails = new List<string>(3);
            int k = run.Length;

            while (k > 0)
            {
                bool matched = false;
                int maxLen = Math.Min(ConnMaxLen, k);
                for (int len = maxLen; len >= 1; len--)
                {
                    string sub = run.Substring(k - len, len);
                    if (s_Conn.Contains(sub))
                    {
                        tails.Add(sub);     // collected right→left
                        k -= len;
                        matched = true;
                        break;
                    }
                }
                if (!matched) break;
            }

            prefix = run.Substring(0, k);
            if (tails.Count > 1) tails.Reverse();   // make connectors left→right
        }
    }
}
