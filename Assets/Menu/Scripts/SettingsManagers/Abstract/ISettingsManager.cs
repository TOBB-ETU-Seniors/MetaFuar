
namespace SettingsManagers.Abstract
{
    public interface ISettingsManager
    {
        public void GetAndSetInitialValues();
        public void SetPlayerPrefs();
        public void ResetSettings();
    }
}