# One Day Intro to XAML - Instructor Outline

The purpose of this document is to provide a the details necessary to guide a classroom of students through a step-by-step lab that creates a UWP app that interacts with a GitHub repository.

## Course Outline

The course is broken into a number of sections:

1. Getting started
    * Discuss
        * Discuss XAML & UWP
        * Where does Xamarin fit in?
            * Code sharing patterns
    * Code
        * Create basic app
1. Implementing Navigation
    * Discuss
        * Design - Fluent
        * Adaptive
        * Styles & Resources
        * OAuth
    * Code
        * Create Shell
        * Update App to launch shell
        * Add nuget:
            * `NavViewEx`
        * Implement Simple Pages
            * `HomePage`
            * `CodePage`
            * `IssuesPage`
            * `SettingsPage`
        * Implement basic navigation with `NavViewEx`
            > We have services here before we discuss them. May need to think about this some more...
        * Add Services
            * `GitHubService`
        * Implement login in code-behind `HomePage.xaml.cs`
            * Copy in and explain UI
            * Copy in and explain code-behind
1. Implementing MVVM, Settings, etc.
    * Discuss
        * MVVM & INPC
        * Services
    * Code
        * Add nuget:
            * `XamlU.Demo.GitHubLibrary`
            * `MVVMLightLibs`
                * Explain differences between the MVVMLight packages
        * Add Services
            * `SettingsService`
            * `GitHubService`
        * Add `HomePageViewModel`
        * Add view model binding to `HomePage.xaml.cs`
        * Move Auth into `HomePageViewModel`
1. Issues, Master/Detail, Pivot for Issues
    * Discuss
        * Libraries
        * Microsoft UWP Community Toolkit
    * Code
        * Complete `IssuesPage`
        * Complete `IssuesPageViewModel`
1. Messaging and Commanding
    * Discuss
        * Messaging
        * Commanding
    * Code
        * Use message to enable/disable navigation when not logged in
        * Context menu / slide control for an issue
            > Is this still relevant given the changes?
1. Data entry and field/form validation
    * Discuss
        * What is validation and why do we need it?
        * What support for validation ships in UWP?
        * JSON, REST and WebClient
            > Show the source for the `GitHubClient`?
    * Code
        * Add `CreateCommentPage`
        * Add `CreateCommentPageViewModel`
        * Add button to `IssuesPage` to launch the `CreateCommentPage` and hook up navigation
            > Discuss the system back button...
1. Additional topics
    * Telemetry
    * Deployment
        * Regular Store
        * Business Store
        * Side-loading
        * Beta distribution
    * Inking
    * Composition UI
    * Desktop Bridge
    * Controls
    * Template Studio
