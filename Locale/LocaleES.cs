using System.Collections.Generic;
using Colossal;

namespace AchievementFixer
{
    /// <summary>
    /// Spanish locale (es-ES)
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
                // Options menu entry
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Settings.MainTab),     "Principal" },
                { m_Setting.GetOptionTabLocaleID(Settings.AdvancedTab), "Avanzado"  },

                // Groups (Main tab)
                { m_Setting.GetOptionGroupLocaleID(Settings.MainInfoGroup), "Info"    },
                { m_Setting.GetOptionGroupLocaleID(Settings.ButtonGroup),   "Enlaces" },
                { m_Setting.GetOptionGroupLocaleID(Settings.NotesGroup),    "Notas"   },

                // Groups (Advanced tab)
                { m_Setting.GetOptionGroupLocaleID(Settings.AdvRowActions), "Acciones" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AdvRowDebug),   "DEBUG"    },

                // Main >> Info
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)),    "Mod"     },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),     "Nombre visible del mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Versión" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "Versión actual del mod." },

                // Main >> Links
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.OpenAchievementsWikiButton)), "Wiki de logros" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.OpenAchievementsWikiButton)),
                  "Abrir el wiki de logros en el navegador." },

               // Main >> Notes
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.MainNotes)), "Los logros ya están activados; completa las tareas requeridas para conseguirlos de forma natural.\n¡Disfruta! :)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.MainNotes)), "Nota: a veces, tras cumplir los requisitos, el logro puede no aparecer hasta que reinicies el juego." },

                // --- Advanced tab ---
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.SelectedAchievement)), "Seleccionar logro" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.SelectedAchievement)), "Elige un logro sobre el que operar." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.UnlockSelectedAchievement)), "Desbloquear seleccionado" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.UnlockSelectedAchievement)), "**Desbloquea y completa** el logro seleccionado." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ClearSelectedAchievement)), "Borrar seleccionado" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ClearSelectedAchievement)), "Marca el logro seleccionado como **no completado**." },
                { m_Setting.GetOptionWarningLocaleID(nameof(Settings.ClearSelectedAchievement)), "BORRAR / REINICIAR este logro.\n\n¿Continuar?" },

                // Advanced >> advisory text
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.AdvancedAdvisory)), "Este mod ya habilita los logros (por defecto) sin usar los botones de la pestaña Avanzado.\nSi quieres algo más rápido, usa [Desbloquear seleccionado]." },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.AdvancedAdvisory)), "CUIDADO con [Restablecer todo]. Si lo usas por accidente, puedes recuperar logros con [Desbloquear seleccionado]." },

                // Advanced >> DEBUG (Clear All)
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ResetAllAchievements)), "RESTABLECER TODOS LOS LOGROS" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ResetAllAchievements)), "**ADVERTENCIA**: borra/restablece TODOS los logros. Útil para pruebas.\nSi te equivocas, recupera logros con [Desbloquear seleccionado]." },
                { m_Setting.GetOptionWarningLocaleID(nameof(Settings.ResetAllAchievements)), "RESTABLECER / BORRAR todos los logros al estado ORIGINAL (no completado). ¿Continuar?" },
            };
        }

        public void Unload() { }
    }
}
