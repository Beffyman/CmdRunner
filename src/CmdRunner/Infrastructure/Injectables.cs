using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmdRunner.Infrastructure
{
	/// <summary>
	/// Magic service registration
	/// </summary>
	public interface IInjectable { }
	/// <summary>
	/// Magically register this service with a singleton lifecycle
	/// </summary>
	public interface ISingletonInjectable : IInjectable { }
	/// <summary>
	/// Magically register this service with a transient lifecycle
	/// </summary>
	public interface ITransientInjectable : IInjectable { }
	/// <summary>
	/// Magically register this service with a scoped lifecycle
	/// </summary>
	public interface IScopedInjectable : IInjectable { }


	/// <summary>
	/// Extensions to enhance the IApplicationBuilder
	/// </summary>
	public static class ApplicationExtensions
	{
		internal static void Register<InjectionType>(IServiceCollection container, Type interfaceType, IList<Type> lifestypeTypes) where InjectionType : IInjectable
		{
			var implementationTypes = lifestypeTypes.Where(x => x.IsClass && interfaceType.IsAssignableFrom(x)).ToList();
			if (implementationTypes.Count > 1)
			{
				return;
			}

			var implementationType = implementationTypes.SingleOrDefault();
			if (implementationType == null)
			{
				throw new Exception($"{interfaceType.Name} does not have a class that implements it.");
			}


			if (typeof(InjectionType) == typeof(ISingletonInjectable))
			{
				container.AddSingleton(interfaceType, implementationType);
			}
			else if (typeof(InjectionType) == typeof(ITransientInjectable))
			{
				container.AddTransient(interfaceType, implementationType);
			}
			else if (typeof(InjectionType) == typeof(IScopedInjectable))
			{
				container.AddScoped(interfaceType, implementationType);
			}
			else
			{
				throw new Exception($"Unsupported {nameof(IInjectable)} type.");
			}
		}

		/// <summary>
		/// Configures the builder to lookup all IInjectable and IInstallable classes so they are auto-registered
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection UseInjectables(this IServiceCollection services)
		{
			var types = ReflectionLoader.Types.Where(x => typeof(IInjectable).IsAssignableFrom(x)
																							&& x != typeof(ISingletonInjectable)
																							&& x != typeof(ITransientInjectable)
																							&& x != typeof(IScopedInjectable)).ToList();

			var singletons = types.Where(x => x.IsInterface && typeof(ISingletonInjectable).IsAssignableFrom(x)).ToList();
			var transients = types.Where(x => x.IsInterface && typeof(ITransientInjectable).IsAssignableFrom(x)).ToList();
			var scoped = types.Where(x => x.IsInterface && typeof(IScopedInjectable).IsAssignableFrom(x)).ToList();

			foreach (var type in singletons)
			{
				Register<ISingletonInjectable>(services, type, types);
			}

			foreach (var type in transients)
			{
				Register<ITransientInjectable>(services, type, types);
			}

			foreach (var type in scoped)
			{
				Register<IScopedInjectable>(services, type, types);
			}

			return services;
		}

	}
}
