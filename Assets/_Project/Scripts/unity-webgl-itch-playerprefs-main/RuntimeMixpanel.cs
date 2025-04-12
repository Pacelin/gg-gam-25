using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using mixpanel;
using TSS.Core;

namespace Jammer
{
    [RuntimeOrder(ERuntimeOrder.SubsystemRegistration)]
    [UsedImplicitly]
    public class RuntimeMixpanel : IPreferences, IRuntimeLoader
    {
        public void DeleteKey(string key) => UserDataManager.DeleteKey(key);

        public int GetInt(string key) => UserDataManager.GetInt(key);
        public int GetInt(string key, int defaultValue) => UserDataManager.GetInt(key, defaultValue);

        public string GetString(string key) => UserDataManager.GetString(key);
        public string GetString(string key, string defaultValue) => UserDataManager.GetString(key, defaultValue);

        public bool HasKey(string key) => UserDataManager.HasKey(key);

        public void SetInt(string key, int value) => UserDataManager.SetInt(key, value);
        public void SetString(string key, string value) => UserDataManager.SetString(key, value);
    
        public UniTask Initialize(CancellationToken cancellationToken)
        {
            try
            {
                Mixpanel.SetPreferencesSource(this);
                Mixpanel.Init();
                Mixpanel.Track("@session_start");
                Mixpanel.Flush();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
                Mixpanel.Disable();
            }
            return UniTask.CompletedTask;
        }

        public void Dispose() { }
    }
}