using System.Collections.Generic;
using Colossal;

namespace AchievementFixer
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
                { m_Setting.GetSettingsLocaleID(), "Achievement Fixer" },

                { m_Setting.GetOptionTabLocaleID(Settings.Section),      "Cài đặt" },
                { m_Setting.GetOptionTabLocaleID(Settings.AboutSection), "Giới thiệu" },
                { m_Setting.GetOptionTabLocaleID(Settings.DebugSection), "Gỡ lỗi" },

                { m_Setting.GetOptionGroupLocaleID(Settings.MainGroup),        "Cài đặt" },
                { m_Setting.GetOptionGroupLocaleID(Settings.NotCompleteGroup), "Chưa hoàn thành" },
                { m_Setting.GetOptionGroupLocaleID(Settings.CompletedGroup),   "Đã hoàn thành" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "Bật thành tích" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "BẬT (mặc định) cho phép đạt thành tích khi dùng mod và giữ trạng thái khi tải." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NotCompleteList)), "Chưa hoàn thành" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NotCompleteList)),  "Thành tích còn có thể đạt được." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.CompletedList)), "Đã hoàn thành" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.CompletedList)),  "Thành tích đã mở khóa." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)),    "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),     "Tên hiển thị của mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Phiên bản" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "Phiên bản hiện tại của mod." },

            { m_Setting.GetOptionLabelLocaleID(nameof(Settings.OpenAchievementsWikiButton)), "Achievements wiki" },
{ m_Setting.GetOptionDescLocaleID(nameof(Settings.OpenAchievementsWikiButton)),  "Open the CS2 achievements wiki in your browser." },


                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.DebugNote)), "Ghi chú" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.DebugNote)),  "Công cụ phát triển sẽ được thêm sau." },
            };
        }

        public void Unload() { }
    }
}
