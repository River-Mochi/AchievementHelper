using System.Collections.Generic;
using Colossal;

namespace AchievementFixer
{
    /// <summary>
    /// Spanish locale entries (es-ES)
    /// </summary>
    public class LocaleES : IDictionarySource
    {
        private readonly Settings m_Setting;
        public LocaleES(Settings setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "Achievement Fixer" },

                { m_Setting.GetOptionTabLocaleID(Settings.Section),      "Ajustes" },
                { m_Setting.GetOptionTabLocaleID(Settings.AboutSection), "Acerca de" },
                { m_Setting.GetOptionTabLocaleID(Settings.DebugSection), "Depuración" },

                { m_Setting.GetOptionGroupLocaleID(Settings.MainGroup),        "Ajustes" },
                { m_Setting.GetOptionGroupLocaleID(Settings.NotCompleteGroup), "Sin completar" },
                { m_Setting.GetOptionGroupLocaleID(Settings.CompletedGroup),   "Completados" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "Activar logros" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "Cuando está ACTIVADO (por defecto), permite logros con mods y lo mantiene durante la carga." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NotCompleteList)), "Sin completar" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NotCompleteList)),  "Logros que aún puedes conseguir." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.CompletedList)), "Completados" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.CompletedList)),  "Logros ya desbloqueados." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)),    "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),     "Nombre del mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Versión" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "Versión actual del mod." },


                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.OpenAchievementsWikiButton)), "Achievements wiki" },
{ m_Setting.GetOptionDescLocaleID(nameof(Settings.OpenAchievementsWikiButton)),  "Open the CS2 achievements wiki in your browser." },


                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.DebugNote)), "Notas" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.DebugNote)),  "Se añadirán herramientas de desarrollo." },
            };
        }

        public void Unload() { }
    }
}
