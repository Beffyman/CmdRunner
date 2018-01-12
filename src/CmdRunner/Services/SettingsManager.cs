using CmdRunner.Dtos.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using CmdRunner.Infrastructure;
using System.Runtime.InteropServices;

namespace CmdRunner.Services
{

	public interface ISettingsManager : ISingletonInjectable
	{
		ScriptLocationConfiguration CurrentConfiguration { get; }
		Guid? CurrentConfigurationId { get; set; }
		IList<ScriptLocationConfiguration> Configurations { get; set; }
		IList<ScriptApplicationConfiguration> Applications { get; set; }

		Task<SettingsManager> LoadFileAsync();
		Task LoadAsync();
		Task SaveAsync();
	}

	public class SettingsManager : ISettingsManager
	{
		public SettingsManager()
		{
			this.LoadAsync().GetAwaiter().GetResult();
		}

		[JsonIgnore]
		private readonly JsonSerializerSettings _JsonSettings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.All,
			Formatting = Formatting.Indented,
			NullValueHandling = NullValueHandling.Include
		};

		[JsonIgnore]
		private string FullPath => Path.GetFullPath($"{GetDefaultLocation()}/{FileName}");

		[JsonIgnore]
		const string FileName = "settings.json";


		public ScriptLocationConfiguration CurrentConfiguration => Configurations.SingleOrDefault(x => x.Id == CurrentConfigurationId);
		public Guid? CurrentConfigurationId { get; set; }
		public IList<ScriptLocationConfiguration> Configurations { get; set; } = new List<ScriptLocationConfiguration>();
		public IList<ScriptApplicationConfiguration> Applications { get; set; } = new List<ScriptApplicationConfiguration>();

		private static string GetDefaultLocation()
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				string path = $"{Environment.GetEnvironmentVariable("LOCALAPPDATA")}/{typeof(Program).Namespace}/Settings/V1/";
				path = Path.GetFullPath(path);
				return path;
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
				|| RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				string path = $"{Environment.GetEnvironmentVariable("Home")}/{typeof(Program).Namespace}/Settings/V1/";
				path = Path.GetFullPath(path);
				return path;

			}
			else
			{
				throw new PlatformNotSupportedException();
			}
		}


		public async Task LoadAsync()
		{
			if (!File.Exists(FullPath))
			{
				return;
			}
			var data = await File.ReadAllTextAsync(FullPath);
			JsonConvert.PopulateObject(data, this, _JsonSettings);
		}

		public async Task<SettingsManager> LoadFileAsync()
		{
			if (!File.Exists(FullPath))
			{
				return null;
			}
			var data = await File.ReadAllTextAsync(FullPath);
			return JsonConvert.DeserializeObject<SettingsManager>(data, _JsonSettings);
		}

		public async Task SaveAsync()
		{
			if (!Directory.Exists(Path.GetDirectoryName(FullPath)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(FullPath));
			}
			var data = JsonConvert.SerializeObject(this, _JsonSettings);
			await File.WriteAllTextAsync(FullPath, data);
		}
	}
}
