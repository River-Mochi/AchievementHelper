using System.Collections.Generic;
using Colossal;

namespace AchievementFixer
{
    /// <summary>
    /// Japanese locale (ja-JP)
    /// </summary>
    public class LocaleJA : IDictionarySource
    {
        private readonly Settings m_Setting;
        public LocaleJA(Settings setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Options menu entry
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Settings.MainTab),     "メイン" },
                { m_Setting.GetOptionTabLocaleID(Settings.AdvancedTab), "詳細"   },

                // Groups (Main tab)
                { m_Setting.GetOptionGroupLocaleID(Settings.MainInfoGroup), "情報" },
                { m_Setting.GetOptionGroupLocaleID(Settings.ButtonGroup),   "リンク" },
                { m_Setting.GetOptionGroupLocaleID(Settings.NotesGroup),    "メモ"   },

                // Groups (Advanced tab)
                { m_Setting.GetOptionGroupLocaleID(Settings.AdvRowActions), "操作" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AdvRowDebug),   "DEBUG" },

                // Main >> Info
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)),    "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),     "このModの表示名。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "バージョン" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "現在のModバージョン。" },

                // Main >> Links
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.OpenAchievementsWikiButton)), "実績Wiki" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.OpenAchievementsWikiButton)),
                  "ブラウザーで実績Wikiを開きます。" },

                 // Main >> Notes
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.MainNotes)), "実績は有効になっています。必要な条件を満たせば自然に解除されます。\n楽しんで！ :)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.MainNotes)), "注意：条件達成後でも、ゲームを再起動するまで実績が表示されない場合があります。" },

                // --- Advanced tab ---
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.SelectedAchievement)), "実績を選択" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.SelectedAchievement)), "操作する実績を選んでください。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.UnlockSelectedAchievement)), "選択をアンロック" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.UnlockSelectedAchievement)), "**選択した実績を解除して達成済みにします。**" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ClearSelectedAchievement)), "選択をクリア" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ClearSelectedAchievement)), "選択した実績を**未達成**にします。" },
                { m_Setting.GetOptionWarningLocaleID(nameof(Settings.ClearSelectedAchievement)), "この実績をクリア/リセットします。\n\n続行しますか？" },

                // Advanced >> advisory text
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.AdvancedAdvisory)), "このMODは（既定で）実績を有効にします。［詳細］タブのボタンを使う必要はありません。\n急ぐ場合は［選択をアンロック］を使ってください。" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.AdvancedAdvisory)), "［すべてリセット］の使用には注意してください。誤って押した場合は、［選択をアンロック］で取り戻せます。" },

                // Advanced >> DEBUG (Clear All)
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ResetAllAchievements)), "すべての実績をリセット" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ResetAllAchievements)), "**警告**：すべての実績を消去/リセットします（テスト用途）。\n誤操作した場合は［選択をアンロック］で復旧できます。" },
                { m_Setting.GetOptionWarningLocaleID(nameof(Settings.ResetAllAchievements)), "すべての実績を**未達成**の初期状態に戻します。続行しますか？" },
            };
        }

        public void Unload() { }
    }
}
