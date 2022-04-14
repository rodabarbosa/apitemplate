using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Infra.Data.Extensions;

public static class ModelBuilderExtension
{
    /// <summary>
    ///     Get and Apply all configuration to context
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void ApplyAllConfigurations(this ModelBuilder modelBuilder)
    {
        MethodInfo applyConfigurationMethodInfo = modelBuilder
            .GetType()
            .GetMethods(BindingFlags.Instance | BindingFlags.Public)
            .First(m => m.Name.Equals("ApplyConfiguration", StringComparison.OrdinalIgnoreCase));

        _ = typeof(ApiTemplateContext).Assembly
            .GetTypes()
            .Select(t => (t, i: Array.Find(t.GetInterfaces(), i => i.Name.Equals(typeof(IEntityTypeConfiguration<>).Name, StringComparison.Ordinal))))
            .Where(it => it.i != null)
            .Select(it => (et: it.i?.GetGenericArguments()[0], cfgObj: Activator.CreateInstance(it.t)))
            .Select(it => applyConfigurationMethodInfo.MakeGenericMethod(it.et).Invoke(modelBuilder, new[] {it.cfgObj}))
            .ToList();
    }
}