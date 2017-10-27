# One Day Intro to XAML - Instructor Outline

The purpose of this document is to provide a the details necessary to guide a classroom of students through a step-by-step lab that creates a UWP app that interacts with a GitHub repository.

## Course Outline

The course is broken into a number of sections:

1. Getting started & Basic Navigation
    * Discuss
        * Discuss XAML & UWP
        * Where does Xamarin fit in?
            * Code sharing patterns
    * Module
        * Course Orientation
        * Introduce Shell Page
        * Add a Navigation View
1. Implementing Authentication
    * Discuss
        * Design - Fluent
        * Adaptive
        * Web Auth Broker
        * OAuth
        * Services
    * Module
        * App Callback
        * Setup app in GitHub
        * Add some services to App
        * Add Authentication UI
        * Add auto-authentication to App
1. Implementing MVVM, Settings, etc.
    * Discuss
        * MVVM & INPC
            * Talk about MVVMLight. Where does T10 fit in?
    * Module
        * Add MVVMLight
        * Add a ViewModel
        * Implement Repository Search
        * Implement Settings
1. Messaging and Commanding
    * Discuss
        * Messaging
        * Commanding
    * Module
        * Implement messaging & commands
1. Issues, Master/Detail, Pivot for Issues
    * Discuss
        * Libraries
        * Microsoft UWP Community Toolkit
        * ObservableCollection
        * Item ViewModels
        * LINQ
        * Styles & Resources
    * Module
        * UWP Toolkit NuGet Package
        * Update Issues Logic
        * Update Issues UI
1. Data entry and field/form validation
    * Discuss
        * What is validation and why do we need it?
        * What support for validation ships in UWP?
        * JSON, REST and WebClient
            > Show the source for the `GitHubClient`?
        * Global back button & back stack
    * Module
        * Add NavigationSerivce
        * Add Validation Property Helper
        * Add create issue button to global nav bar
        * Add Create Issue Page
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
