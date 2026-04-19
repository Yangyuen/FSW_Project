namespace AspnetCoreMvcFull.Models;

public sealed class System65KpiCard
{
  public string Title { get; init; } = string.Empty;
  public string Value { get; init; } = string.Empty;
  public string Caption { get; init; } = string.Empty;
  public string Tone { get; init; } = "secondary";
  public string Icon { get; init; } = "ri-bar-chart-box-line";
}

public sealed class System65ActionItem
{
  public string Label { get; init; } = string.Empty;
  public string Url { get; init; } = "#";
  public string Tone { get; init; } = "primary";
  public string Icon { get; init; } = "ri-arrow-right-line";
  public bool IsOutline { get; init; }
}

public sealed class System65AlertMessage
{
  public string Tone { get; init; } = "info";
  public string Title { get; init; } = string.Empty;
  public string Message { get; init; } = string.Empty;
}

public sealed class System65Chip
{
  public string Label { get; init; } = string.Empty;
  public string Value { get; init; } = string.Empty;
  public string Tone { get; init; } = "secondary";
  public bool IsActive { get; init; }
}

public sealed class System65FormField
{
  public string Label { get; init; } = string.Empty;
  public string Value { get; init; } = string.Empty;
  public string Type { get; init; } = "text";
  public string Placeholder { get; init; } = string.Empty;
  public string HelperText { get; init; } = string.Empty;
  public string Prefix { get; init; } = string.Empty;
  public string Suffix { get; init; } = string.Empty;
  public bool Required { get; init; }
  public int ColumnSpan { get; init; } = 6;
  public IReadOnlyList<string> Options { get; init; } = Array.Empty<string>();
}

public sealed class System65FormSection
{
  public string Title { get; init; } = string.Empty;
  public string Description { get; init; } = string.Empty;
  public IReadOnlyList<System65FormField> Fields { get; init; } = Array.Empty<System65FormField>();
}

public sealed class System65DetailItem
{
  public string Label { get; init; } = string.Empty;
  public string Value { get; init; } = string.Empty;
  public string Tone { get; init; } = "secondary";
  public string HelpText { get; init; } = string.Empty;
}

public sealed class System65DetailPanel
{
  public string Title { get; init; } = string.Empty;
  public string Description { get; init; } = string.Empty;
  public string Tone { get; init; } = "secondary";
  public IReadOnlyList<System65DetailItem> Items { get; init; } = Array.Empty<System65DetailItem>();
  public IReadOnlyList<string> Notes { get; init; } = Array.Empty<string>();
  public int? ProgressValue { get; init; }
  public string ProgressLabel { get; init; } = string.Empty;
}

public sealed class System65TableCell
{
  public string Text { get; init; } = string.Empty;
  public string Tone { get; init; } = "secondary";
  public bool IsBadge { get; init; }
  public string Note { get; init; } = string.Empty;
  public string Icon { get; init; } = string.Empty;
}

public sealed class System65TableRow
{
  public IReadOnlyList<System65TableCell> Cells { get; init; } = Array.Empty<System65TableCell>();
}

public sealed class System65TableSection
{
  public string Title { get; init; } = string.Empty;
  public string Description { get; init; } = string.Empty;
  public IReadOnlyList<string> Headers { get; init; } = Array.Empty<string>();
  public IReadOnlyList<System65TableRow> Rows { get; init; } = Array.Empty<System65TableRow>();
  public IReadOnlyList<string> Filters { get; init; } = Array.Empty<string>();
  public IReadOnlyList<string> ExportFormats { get; init; } = Array.Empty<string>();
  public string PaginationSummary { get; init; } = string.Empty;
  public bool SupportsSort { get; init; }
  public bool SupportsFilter { get; init; }
  public bool SupportsSearch { get; init; }
  public bool SupportsPagination { get; init; }
}

public sealed class System65TimelineEvent
{
  public string Title { get; init; } = string.Empty;
  public string Description { get; init; } = string.Empty;
  public string Actor { get; init; } = string.Empty;
  public string Time { get; init; } = string.Empty;
  public string Tone { get; init; } = "primary";
  public IReadOnlyList<string> Tags { get; init; } = Array.Empty<string>();
}

public sealed class System65TraceNode
{
  public string Title { get; init; } = string.Empty;
  public string Subtitle { get; init; } = string.Empty;
  public string Tone { get; init; } = "secondary";
  public IReadOnlyList<System65TraceNode> Children { get; init; } = Array.Empty<System65TraceNode>();
}

public sealed class System65ScreenPageViewModel
{
  public System65ScreenDefinition Current { get; init; } = new();
  public IReadOnlyList<System65ScreenDefinition> RoleScreens { get; init; } = Array.Empty<System65ScreenDefinition>();
  public string CurrentStatus { get; init; } = string.Empty;
  public string PageKicker { get; init; } = string.Empty;
  public string PageSummary { get; init; } = string.Empty;
  public bool IsFallback { get; init; }
  public IReadOnlyList<System65KpiCard> KpiCards { get; init; } = Array.Empty<System65KpiCard>();
  public IReadOnlyList<System65AlertMessage> Alerts { get; init; } = Array.Empty<System65AlertMessage>();
  public IReadOnlyList<System65Chip> WorkflowChips { get; init; } = Array.Empty<System65Chip>();
  public IReadOnlyList<System65ActionItem> PrimaryActions { get; init; } = Array.Empty<System65ActionItem>();
  public IReadOnlyList<System65FormSection> FormSections { get; init; } = Array.Empty<System65FormSection>();
  public IReadOnlyList<System65DetailPanel> DetailPanels { get; init; } = Array.Empty<System65DetailPanel>();
  public IReadOnlyList<System65TableSection> TableSections { get; init; } = Array.Empty<System65TableSection>();
  public IReadOnlyList<System65TimelineEvent> Timeline { get; init; } = Array.Empty<System65TimelineEvent>();
  public IReadOnlyList<System65TraceNode> TraceNodes { get; init; } = Array.Empty<System65TraceNode>();
  public IReadOnlyList<string> PageNotes { get; init; } = Array.Empty<string>();
}