using System.Collections.Generic;
using Colossal;

namespace AchievementFixer
{
    /// <summary>
    /// Korean locale (ko-KR)
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
                // Options menu entry
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Settings.MainTab),     "메인"   },
                { m_Setting.GetOptionTabLocaleID(Settings.AdvancedTab), "고급"   },

                // Groups (Main tab)
                { m_Setting.GetOptionGroupLocaleID(Settings.MainInfoGroup), "정보" },
                { m_Setting.GetOptionGroupLocaleID(Settings.ButtonGroup),   "링크" },
                { m_Setting.GetOptionGroupLocaleID(Settings.NotesGroup),    "메모" },

                // Groups (Advanced tab)
                { m_Setting.GetOptionGroupLocaleID(Settings.AdvRowActions), "작업" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AdvRowDebug),   "DEBUG" },

                // Main >> Info
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)),    "모드" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),     "모드 표시 이름." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "버전" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "현재 모드 버전." },

                // Main >> Links
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.OpenAchievementsWikiButton)), "도전과제 위키" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.OpenAchievementsWikiButton)),
                  "브라우저에서 위키 열기." },

                // Main >> Notes
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.MainNotes)), "도전과제가 활성화되었습니다. 필요한 작업을 완료하면 자연스럽게 달성됩니다.\n즐기세요! :)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.MainNotes)), "참고: 조건을 달성해도 게임을 재시작할 때까지 표시되지 않을 수 있습니다." },

                // --- Advanced tab ---
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.SelectedAchievement)), "도전과제 선택" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.SelectedAchievement)), "작업할 도전과제를 선택하세요." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.UnlockSelectedAchievement)), "선택 항목 해제(언락)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.UnlockSelectedAchievement)), "**선택한 도전과제를 해제하고 완료 처리합니다.**" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ClearSelectedAchievement)), "선택 항목 초기화" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ClearSelectedAchievement)), "선택한 도전과제를 **미완료**로 표시합니다." },
                { m_Setting.GetOptionWarningLocaleID(nameof(Settings.ClearSelectedAchievement)), "이 도전과제를 삭제/초기화합니다.\n\n계속할까요?" },

                // Advanced >> advisory text
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.AdvancedAdvisory)), "이 모드는 이미 기본값으로 도전과제를 활성화합니다. [고급] 탭의 버튼을 사용할 필요가 없습니다.\n더 빨리 진행하고 싶다면 [선택 항목 해제] 버튼을 사용하세요." },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.AdvancedAdvisory)), "[모두 초기화] 버튼 사용 시 주의하세요. 실수했다면 [선택 항목 해제]로 복구할 수 있습니다." },

                // Advanced >> DEBUG (Clear All)
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ResetAllAchievements)), "모든 도전과제 초기화" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ResetAllAchievements)), "**경고**: 모든 도전과제를 삭제/초기화합니다(테스트용).\n실수했다면 [선택 항목 해제]로 되돌릴 수 있습니다." },
                { m_Setting.GetOptionWarningLocaleID(nameof(Settings.ResetAllAchievements)), "모든 도전과제를 **미완료** 초기 상태로 되돌립니다. 계속할까요?" },
            };
        }

        public void Unload() { }
    }
}
