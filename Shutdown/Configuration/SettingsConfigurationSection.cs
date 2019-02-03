using System;
using System.Configuration;
namespace Shutdown.Configuration
{
    public sealed class SettingsConfigurationSection : ConfigurationSection
    {
        public static SettingsConfigurationSection Get()
        {
            return ConfigurationManager.GetSection("settings") as SettingsConfigurationSection ??
                new SettingsConfigurationSection();
        }

        public static void Save(int hoursInitial, int hoursJumpSize, int minutesInitial, int minutesJumpSize)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = Get();
            if (config.Sections["settings"] == null)
                config.Sections.Add("settings", section);

            section.HoursInitialValue= hoursInitial;
            section.HoursJumpSize= hoursJumpSize;
            section.MinutesInitialValue = minutesInitial;
            section.MinutesJumpSize = minutesJumpSize;
            section.SectionInformation.ForceSave = true;
            config.Save();
            ConfigurationManager.RefreshSection("settings");
        }

        [ConfigurationProperty("hoursInitialValue", DefaultValue = 1)]
        public int HoursInitialValue
        {
            get { return Convert.ToInt32(this["hoursInitialValue"]); }
            set { this["hoursInitialValue"] = value; }
        }

        [ConfigurationProperty("hoursJumpSize", DefaultValue = 1)]
        public int HoursJumpSize
        {
            get { return Convert.ToInt32(this["hoursJumpSize"]); }
            set { this["hoursJumpSize"] = value; }
        }

        [ConfigurationProperty("minutesInitialValue", DefaultValue = 0)]
        public int MinutesInitialValue
        {
            get { return Convert.ToInt32(this["minutesInitialValue"]); }
            set { this["minutesInitialValue"] = value; }
        }
        
        [ConfigurationProperty("minutesJumpSize", DefaultValue = 10)]
        public int MinutesJumpSize
        {
            get { return Convert.ToInt32(this["minutesJumpSize"]); }
            set { this["minutesJumpSize"] = value; }
        }
    }
}