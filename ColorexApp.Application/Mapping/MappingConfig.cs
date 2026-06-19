using Mapster;

namespace ColorexApp.Application.Mapping;

public static class MappingConfig
{
    public static void Configure()
    {
        TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
    }
}
