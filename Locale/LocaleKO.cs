using System.Collections.Generic;
using Colossal;

namespace AchievementFixer
{
    /// <summary>
    /// Korean locale entries (ko-KR)
    /// </summary>
    public class LocaleKO : IDictionarySource
    {
        private readonly Settings m_Setting;
        public LocaleKO(Settings setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "Achievement Fixer" },

                { m_Setting.GetOptionTabLocaleID(Settings.Section),      "설정" },
                { m_Setting.GetOptionTabLocaleID(Settings.AboutSection), "정보" },
                { m_Setting.GetOptionTabLocaleID(Settings.DebugSection), "디버그" },

                { m_Setting.GetOptionGroupLocaleID(Settings.MainGroup),        "설정" },
                { m_Setting.GetOptionGroupLocaleID(Settings.NotCompleteGroup), "미달성" },
                { m_Setting.GetOptionGroupLocaleID(Settings.CompletedGroup),   "달성됨" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "도전과제 활성화" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "기본값 ON일 때, 모드를 사용해도 도전과제가 가능하며 로딩 중에도 유지합니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NotCompleteList)), "미달성" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NotCompleteList)),  "아직 달성 가능한 도전과제." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.CompletedList)), "달성됨" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.CompletedList)),  "이미 해금된 도전과제." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)),    "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),     "모드 표시 이름." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "버전" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "현재 모드 버전." },

         { m_Setting.GetOptionLabelLocaleID(nameof(Settings.OpenAchievementsWikiButton)), "Achievements wiki" },
{ m_Setting.GetOptionDescLocaleID(nameof(Settings.OpenAchievementsWikiButton)),  "Open the CS2 achievements wiki in your browser." },


                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.DebugNote)), "메모" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.DebugNote)),  "개발 도구는 추후 추가됩니다." },
            };
        }

        public void Unload() { }
    }
}
