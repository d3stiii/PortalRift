using System.IO;
using TowerDefence.Common.Extensions;
using TowerDefence.Data.Persistent;
using TowerDefence.Services.Data;
using UnityEngine;

namespace TowerDefence.Services.SaveLoad
{
  public class SaveLoadService
  {
    private readonly string _path = Application.persistentDataPath + "\\save.json";
    private readonly PersistentDataProvider _persistentDataProvider;

    public SaveLoadService(PersistentDataProvider persistentDataProvider)
    {
      _persistentDataProvider = persistentDataProvider;
      Application.quitting += Save;
    }

    public void Save()
    {
      using var writer = new StreamWriter(_path);
      writer.Write(_persistentDataProvider.PersistentData.ToJson());
    }

    public void Load()
    {
      if (!File.Exists(_path))
        File.Create(_path).Close();

      using var reader = new StreamReader(_path);
      _persistentDataProvider.PersistentData = reader.ReadToEnd().ToDeserialized<PersistentData>() ?? CreateNew();
    }

    private PersistentData CreateNew()
    {
      return new PersistentData
      {
        CompletedLevelData = new CompletedLevelData
        {
          LastCompletedLevel = -1
        },
        SoundSettingsData = new SoundSettingsData
        {
          MusicEnabled = true,
          MusicVolume = 1,
          SfxEnabled = true,
          SfxVolume = 1
        }
      };
    }
  }
}