using System.Collections.Generic;
using Colossal;

namespace AchievementHelper
{
    /// <summary>
    /// Vietnamese locale entries (vi-VN)
    /// </summary>
    public class LocaleVI : IDictionarySource
    {
        private readonly Settings m_Setting;
        public LocaleVI(Settings setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod name in Options menu
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // One section/tab
                { m_Setting.GetOptionTabLocaleID(Settings.Section), "Chính" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(Settings.MainGroup),  "Cài đặt" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AboutGroup), "Giới thiệu" },

                // Toggle
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "Bật thành tựu" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "Khi BẬT (mặc định), kích hoạt lại thành tựu khi dùng mod và bảo vệ trạng thái trong lúc tải." },

                // About fields
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),  "Tên hiển thị của mod này." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Phiên bản" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)), "Phiên bản mod hiện tại." },
            };
        }

        public void Unload() { }
    }
}
