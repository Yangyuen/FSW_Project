using System.Text.RegularExpressions;

namespace AspnetCoreMvcFull.Models;

public sealed class System65ScreenDefinition
{
  public string Role { get; init; } = string.Empty;
  public string RoleLabel { get; init; } = string.Empty;
  public string ScreenId { get; init; } = string.Empty;
  public string Title { get; init; } = string.Empty;
  public string Route { get; init; } = string.Empty;
  public string RequirementCoverage { get; init; } = string.Empty;
  public string Goal { get; init; } = string.Empty;
  public IReadOnlyList<string> Components { get; init; } = Array.Empty<string>();
}

public sealed class System65RoleGroup
{
  public string Role { get; init; } = string.Empty;
  public string RoleLabel { get; init; } = string.Empty;
  public IReadOnlyList<System65ScreenDefinition> Screens { get; init; } = Array.Empty<System65ScreenDefinition>();
}

public sealed class System65IndexViewModel
{
  public int TotalScreens { get; init; }
  public IReadOnlyList<System65RoleGroup> Roles { get; init; } = Array.Empty<System65RoleGroup>();
}

public sealed class System65MockRecord
{
  public string RefNo { get; init; } = string.Empty;
  public string ItemName { get; init; } = string.Empty;
  public string Status { get; init; } = string.Empty;
  public string UpdatedBy { get; init; } = string.Empty;
  public string UpdatedAt { get; init; } = string.Empty;
}

public sealed class System65ScreenViewModel
{
  public System65ScreenDefinition Current { get; init; } = new();
  public IReadOnlyList<System65ScreenDefinition> RoleScreens { get; init; } = Array.Empty<System65ScreenDefinition>();
  public IReadOnlyList<System65MockRecord> MockRecords { get; init; } = Array.Empty<System65MockRecord>();
}

public static class System65ScreenCatalog
{
  private static readonly Regex RouteRegex = new(
    @"^\|\s*(?<id>[A-Z]{2}-\d+)\s*\|\s*(?<route>/System_6_5/[^|]+)\|",
    RegexOptions.Compiled);

  private static readonly Regex HeaderRegex = new(
    @"^###\s*(?<id>[A-Z]{2}-\d+)\s*:\s*(?<title>.+)$",
    RegexOptions.Compiled);

  private static readonly IReadOnlyDictionary<string, string> RoleFileSuffix =
    new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
      ["Operator"] = "ผู้ประกอบการ",
      ["Officer"] = "เจ้าหน้าที่",
      ["Admin"] = "ผู้ดูแลระบบ"
    };

  private static readonly IReadOnlyDictionary<string, string> RoleLabel =
    new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
      ["Operator"] = "ผู้ประกอบการ",
      ["Officer"] = "เจ้าหน้าที่",
      ["Admin"] = "ผู้ดูแลระบบ"
    };

  public static IReadOnlyList<System65ScreenDefinition> Load(string requirementsDirectory)
  {
    var all = new List<System65ScreenDefinition>();

    foreach (var role in RoleFileSuffix.Keys)
    {
      var fileSuffix = RoleFileSuffix[role];
      var path = Path.Combine(requirementsDirectory, $"screen-requirements-{fileSuffix}.md");

      if (!File.Exists(path))
      {
        continue;
      }

      all.AddRange(ParseRoleFile(role, RoleLabel[role], path));
    }

    return all
      .OrderBy(s => RoleOrder(s.Role))
      .ThenBy(s => ParseScreenNumber(s.ScreenId))
      .ToList();
  }

  private static IEnumerable<System65ScreenDefinition> ParseRoleFile(string role, string roleLabel, string filePath)
  {
    var lines = File.ReadAllLines(filePath);
    var routeById = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    foreach (var line in lines)
    {
      var trimmed = line.Trim();
      var routeMatch = RouteRegex.Match(trimmed);
      if (!routeMatch.Success)
      {
        continue;
      }

      var id = routeMatch.Groups["id"].Value.Trim();
      var route = routeMatch.Groups["route"].Value.Trim();
      routeById[id] = route;
    }

    var results = new List<System65ScreenDefinition>();
    MutableScreen? current = null;
    var collectingComponents = false;

    foreach (var line in lines)
    {
      var trimmed = line.Trim();
      var headerMatch = HeaderRegex.Match(trimmed);

      if (headerMatch.Success)
      {
        TryCommitCurrent();
        current = new MutableScreen
        {
          ScreenId = headerMatch.Groups["id"].Value.Trim(),
          Title = headerMatch.Groups["title"].Value.Trim()
        };
        collectingComponents = false;
        continue;
      }

      if (current is null)
      {
        continue;
      }

      if (trimmed.StartsWith("- ครอบคลุมข้อกำหนด:", StringComparison.Ordinal))
      {
        current.RequirementCoverage = trimmed["- ครอบคลุมข้อกำหนด:".Length..].Trim();
        collectingComponents = false;
        continue;
      }

      if (trimmed.StartsWith("- เป้าหมาย:", StringComparison.Ordinal))
      {
        current.Goal = trimmed["- เป้าหมาย:".Length..].Trim();
        collectingComponents = false;
        continue;
      }

      if (trimmed.StartsWith("- Components จาก Views:", StringComparison.Ordinal))
      {
        collectingComponents = true;
        continue;
      }

      if (collectingComponents)
      {
        if (trimmed.StartsWith("- ", StringComparison.Ordinal))
        {
          var component = trimmed[2..].Trim();
          if (component.StartsWith("Views/", StringComparison.OrdinalIgnoreCase))
          {
            current.Components.Add(component);
            continue;
          }
        }

        if (trimmed.Length == 0)
        {
          continue;
        }

        collectingComponents = false;
      }
    }

    TryCommitCurrent();
    return results;

    void TryCommitCurrent()
    {
      if (current is null)
      {
        return;
      }

      if (routeById.TryGetValue(current.ScreenId, out var route) && !string.IsNullOrWhiteSpace(route))
      {
        results.Add(new System65ScreenDefinition
        {
          Role = role,
          RoleLabel = roleLabel,
          ScreenId = current.ScreenId,
          Title = current.Title,
          Route = route,
          RequirementCoverage = current.RequirementCoverage,
          Goal = current.Goal,
          Components = current.Components
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList()
        });
      }

      current = null;
    }
  }

  private static int RoleOrder(string role) => role switch
  {
    "Operator" => 1,
    "Officer" => 2,
    "Admin" => 3,
    _ => 99
  };

  private static int ParseScreenNumber(string screenId)
  {
    var parts = screenId.Split('-', StringSplitOptions.RemoveEmptyEntries);
    return parts.Length == 2 && int.TryParse(parts[1], out var n) ? n : int.MaxValue;
  }

  private sealed class MutableScreen
  {
    public string ScreenId { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string RequirementCoverage { get; set; } = string.Empty;
    public string Goal { get; set; } = string.Empty;
    public List<string> Components { get; } = new();
  }
}