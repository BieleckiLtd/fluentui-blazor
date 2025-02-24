﻿using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Utilities;

namespace Microsoft.FluentUI.AspNetCore.Components;

public partial class FluentSplitter : FluentComponentBase
{
    protected string _direction = "row";

    /// <summary />
    protected virtual MarkupString InlineStyleValue => new InlineStyleBuilder()
        .AddStyle($"#{Id}", "--first-size", Panel1Size, !string.IsNullOrEmpty(Panel1Size))
        .AddStyle($"#{Id}", "--second-size", Panel2Size, !string.IsNullOrEmpty(Panel2Size))
        .BuildMarkupString();

    /// <summary />
    protected string? ClassValue => new CssBuilder(Class)
        .AddClass("fluent-splitter")
        .Build();

    /// <summary />
    protected string? StyleValue => new StyleBuilder(Style)
        .Build();

    /// <summary>
    /// Gets or sets the orientation.
    /// Default is horizontal (i.e a vertical splitter bar)
    /// </summary>
    [Parameter]
    public Orientation Orientation { get; set; } = Orientation.Horizontal;

    /// <summary>
    /// Content for the top/left panel
    /// </summary>
    [Parameter]
    public RenderFragment? Panel1 { get; set; }


    /// <summary>
    /// Content for the bottom/right panel
    /// </summary>
    [Parameter]
    public RenderFragment? Panel2 { get; set; }

    /// <summary>
    /// Size for the left/top panel. 
    /// Needs to be a valid css size like '50%' or '250px'
    /// </summary>
    [Parameter]
    public string? Panel1Size { get; set; }

    /// <summary>
    /// Size for the right/bottom panel. Needs to be a valid css size like '50%' or '250px'
    /// Uses grid-template-rows/columns with max-content to determine end width. 
    /// See <see href="https://developer.mozilla.org/en-US/docs/Web/CSS/grid-template-columns">mdn web docs</see> for more information 
    /// </summary>
    [Parameter]
    public string? Panel2Size { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the splitter is collapsed.
    /// If set to true, Panel1 will take up all the space and Panel2 as well as the splitter bar will be hidden.
    /// </summary>
    [Parameter]
    public bool Collapsed { get; set; }

    [Parameter]
    public EventCallback<bool> OnCollapsed { get; set; }

    [Parameter]
    public EventCallback<bool> OnExpanded { get; set; }

    [Parameter]
    public EventCallback<SplitterResizedEventArgs> OnResized { get; set; }

    public FluentSplitter()
    {
        Id = Identifier.NewId();
    }

    protected override void OnParametersSet()
    {
        _direction = (Orientation == Orientation.Horizontal) ? "row" : "column";
    }

    private void OnCollapsedHandler(SplitterCollapsedEventArgs args)
    {
        bool status = args.Collapsed;

        if (OnCollapsed.HasDelegate)
        {
            OnCollapsed.InvokeAsync(status);
        }

        if (OnExpanded.HasDelegate)
        {
            OnExpanded.InvokeAsync(!status);
        }
    }

    private void OnResizedHandler(SplitterResizedEventArgs args)
    {
        if (OnResized.HasDelegate)
        {
            OnResized.InvokeAsync(args);
        }
    }
}
