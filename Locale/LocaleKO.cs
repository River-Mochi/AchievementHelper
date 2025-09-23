using System.Collections.Generic;
using Colossal;

namespace AchievementHelper
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
                // Mod name in Options menu
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // One section/tab
                { m_Setting.GetOptionTabLocaleID(Settings.Section), "메인" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(Settings.MainGroup),  "설정" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AboutGroup), "정보" },

                // Toggle
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "도전과제 활성화" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "기본적으로 켜짐. 모드 사용 시에도 도전과제를 다시 활성화하고 로딩 중 상태를 보호합니다." },

                // About fields
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)), "모드" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),  "이 모드의 표시 이름." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "버전" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)), "현재 모드 버전." },
            };
        }

        public void Unload() { }
    }
}
