using CmdRunner.Dtos.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using CmdRunner.Infrastructure;

namespace CmdRunner.Services
{

	public interface ISettings : ISingletonInjectable
	{
		string Location { get; set; }
		ScriptLocationConfiguration CurrentConfiguration { get; }
		Guid? CurrentConfigurationId { get; set; }
		IList<ScriptLocationConfiguration> Configurations { get; set; }
		IList<ScriptApplicationConfiguration> Applications { get; set; }

		Task<Settings> LoadFile();
		Task Load();
		Task Save();
	}

	public class Settings : ISettings
	{
		public Settings()
		{
			this.Load().GetAwaiter().GetResult();
		}

		[JsonIgnore]
		private readonly JsonSerializerSettings _JsonSettings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.All,
			Formatting = Formatting.Indented,
			NullValueHandling = NullValueHandling.Include
		};

		[JsonIgnore]
		private string FullPath => Path.GetFullPath($"{Location}/{FileName}");

		[JsonIgnore]
		const string FileName = "settings.json";


		public ScriptLocationConfiguration CurrentConfiguration => Configurations.SingleOrDefault(x => x.Id == CurrentConfigurationId);
		public string Location { get; set; } = "";
		public Guid? CurrentConfigurationId { get; set; }
		public IList<ScriptLocationConfiguration> Configurations { get; set; } = new List<ScriptLocationConfiguration>();
		public IList<ScriptApplicationConfiguration> Applications { get; set; } = new List<ScriptApplicationConfiguration>();



		public async Task Load()
		{
			if (!File.Exists(FullPath))
			{
				return;
			}
			var data = await File.ReadAllTextAsync(FullPath);
			JsonConvert.PopulateObject(data, this, _JsonSettings);
		}

		public async Task<Settings> LoadFile()
		{
			if (!File.Exists(FullPath))
			{
				return null;
			}
			var data = await File.ReadAllTextAsync(FullPath);
			return JsonConvert.DeserializeObject<Settings>(data, _JsonSettings);
		}

		public async Task Save()
		{
			if (!Directory.Exists(Path.GetDirectoryName(Location)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(Location));
			}
			var data = JsonConvert.SerializeObject(this, _JsonSettings);
			await File.WriteAllTextAsync(FullPath, data);
		}
	}
}
