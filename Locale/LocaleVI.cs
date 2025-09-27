using System.Collections.Generic;
using Colossal;

namespace AchievementFixer
{
    /// <summary>
    /// Vietnamese locale (vi-VN)
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
                // Options menu entry
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Settings.MainTab),     "Chính"   },
                { m_Setting.GetOptionTabLocaleID(Settings.AdvancedTab), "Nâng cao" },

                // Groups (Main tab)
                { m_Setting.GetOptionGroupLocaleID(Settings.MainInfoGroup), "Thông tin" },
                { m_Setting.GetOptionGroupLocaleID(Settings.ButtonGroup),   "Liên kết"  },
                { m_Setting.GetOptionGroupLocaleID(Settings.NotesGroup),    "Ghi chú"   },

                // Groups (Advanced tab)
                { m_Setting.GetOptionGroupLocaleID(Settings.AdvRowActions), "Thao tác" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AdvRowDebug),   "DEBUG"    },

                // Main >> Info
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)),    "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),     "Tên hiển thị của mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Phiên bản" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "Phiên bản hiện tại của mod." },

                // Main >> Links
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.OpenAchievementsWikiButton)), "Wiki thành tựu" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.OpenAchievementsWikiButton)),
                  "Mở wiki thành tựu trong trình duyệt." },

                // Main >> Notes
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.MainNotes)), "Thành tựu đã được bật; chỉ cần làm các nhiệm vụ cần thiết để mở khóa tự nhiên.\nChơi vui nhé! :)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.MainNotes)), "Lưu ý: đôi khi sau khi hoàn thành yêu cầu, thành tựu có thể chỉ hiện sau khi khởi động lại game." },

                // --- Advanced tab ---
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.SelectedAchievement)), "Chọn thành tựu" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.SelectedAchievement)), "Chọn thành tựu để thao tác." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.UnlockSelectedAchievement)), "Mở khóa đã chọn" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.UnlockSelectedAchievement)), "**Mở khóa & đánh dấu hoàn thành** thành tựu đã chọn." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ClearSelectedAchievement)), "Xóa đã chọn" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ClearSelectedAchievement)), "Đánh dấu thành tựu đã chọn là **chưa hoàn thành**." },
                { m_Setting.GetOptionWarningLocaleID(nameof(Settings.ClearSelectedAchievement)), "XÓA / ĐẶT LẠI thành tựu này.\n\nTiếp tục?" },

                // Advanced >> advisory text
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.AdvancedAdvisory)), "Mod này mặc định đã bật thành tựu—không cần dùng nút trong tab Nâng cao.\nNếu muốn nhanh hơn, dùng nút [Mở khóa đã chọn]." },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.AdvancedAdvisory)), "CẨN THẬN với nút [Đặt lại Tất cả]. Nếu lỡ bấm, có thể khôi phục bằng [Mở khóa đã chọn]." },

                // Advanced >> DEBUG (Clear All)
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ResetAllAchievements)), "ĐẶT LẠI TẤT CẢ THÀNH TỰU" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ResetAllAchievements)), "**CẢNH BÁO**: xóa/đặt lại TẤT CẢ thành tựu (hữu ích khi thử nghiệm).\nNếu bấm nhầm, có thể lấy lại bằng [Mở khóa đã chọn]." },
                { m_Setting.GetOptionWarningLocaleID(nameof(Settings.ResetAllAchievements)), "ĐẶT LẠI / XÓA tất cả thành tựu về trạng thái BAN ĐẦU (chưa hoàn thành). Tiếp tục?" },
            };
        }

        public void Unload() { }
    }
}
