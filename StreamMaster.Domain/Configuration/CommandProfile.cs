using System.Text.Json.Serialization;

namespace StreamMaster.Domain.Configuration;

[TsInterface(AutoI = false, IncludeNamespace = false, FlattenHierarchy = true, AutoExportMethods = false)]
public class CommandProfile
{
    public bool IsReadOnly { get; set; } = false;
    public string Command { get; set; } = "ffmpeg";
    public string Parameters { get; set; } = "";
}

[TsInterface(AutoI = false, IncludeNamespace = false, FlattenHierarchy = true, AutoExportMethods = false)]
public class CommandProfileDto : CommandProfile
{
    public string ProfileName { get; set; } = "";
}

public class CommandProfileDict
{
    [JsonPropertyName("CommandProfiles")]
    public Dictionary<string, CommandProfile> CommandProfiles { get; set; } = [];
    public CommandProfile? GetProfile(string CommandProfileName)
    {
        return CommandProfiles.TryGetValue(CommandProfileName, out CommandProfile? existingProfile)
            ? existingProfile
            : null;
    }

    public bool HasProfile(string CommandProfileName)
    {
        return CommandProfiles.TryGetValue(CommandProfileName, out _);
    }

    public CommandProfileDto GetProfileDto(string CommandProfileName)
    {
        return GetDefaultProfileDto(CommandProfileName);
    }

    public CommandProfileDto GetDefaultProfileDto(string defaultName = "Default")
    {

        CommandProfile? defaultProfile = GetProfile(defaultName);
        return defaultProfile == null
            ? throw new Exception($"Command Profile {defaultName} not found")
            : GetProfileDtoFromProfile(defaultProfile, defaultName);
    }

    public CommandProfileDto GetProfileDtoFromProfile(CommandProfile commandProfile, string ProfileName)
    {
        return new CommandProfileDto
        {
            Command = commandProfile.Command,
            ProfileName = ProfileName,
            IsReadOnly = commandProfile.IsReadOnly,
            Parameters = commandProfile.Parameters,
        };
    }

    public List<CommandProfileDto> GetProfilesDto()
    {
        List<CommandProfileDto> ret = [];

        foreach (string key in CommandProfiles.Keys)
        {
            if (CommandProfiles.TryGetValue(key, out CommandProfile? profile))
            {
                ret.Add(GetProfileDtoFromProfile(profile, key));
            }
        }
        return ret;
    }

    public List<CommandProfile> GetProfiles()
    {
        List<CommandProfile> ret = [];

        foreach (string key in CommandProfiles.Keys)
        {
            if (CommandProfiles.TryGetValue(key, out CommandProfile? profile))
            {
                ret.Add(profile);
            }
        }
        return ret;
    }

}