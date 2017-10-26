# Module 1

## Lab 1

### Task 1: Orientation

> TODO: describe

1. Create GitHub account https://github.com/join
1. Ensure environment
    1. Windows 10 (any edition) Fall Creators Update
    1. Visual Studio 2017 (any edition) 15.4 or later

### Task 2: Introduce ShellPage

> TODO: describe

1. Create Blank UWP app 

> C:\Users\jnixon\git.repos\one-day-intro\Modules\Images\MOD01_2017-10-26 10_27_11.png

2. Name it `GitHubBrowser`
3. Select Fall Creators Update as both Target and Minimum Version.

    > C:\Users\jnixon\git.repos\one-day-intro\Modules\Images\MOD01_2017-10-26 10_27_54.png

4. Open Solution Explorer. Select `View/Solution Explorer` 
5. Delete `MainPage.xaml` folder;
6. Create `Views` folder;
7. Create a newXAML page called `ShellPage` in the `Views` folder.

    > Right-click the `Views` folder, select `Blank Page` in the `Add New item` dialog.

    > C:\Users\jnixon\git.repos\one-day-intro\Modules\Images\MOD01_2017-10-26 10_31_56.png

8. Replace the default grid with the following XAML code:

    ```xml
    <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <TextBlock>Hello, World!</TextBlock>
    </Grid> 
    ```

9. Open `App.xaml.cs` in the root.

    > Expand `App.xaml` to see its `App.xaml.cs` code-behind file

10. Replace the entire `App` class with the following C# code:

    ```csharp
    sealed partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            var shell = new ShellPage();
            Window.Current.Content = shell;
            Window.Current.Activate();
        }
    } 
    ```

11. Note the error under ShellPage. This is the C# language service indicating it cannot resolve ShellPage because its namespace is not in scope.

> C:\Users\jnixon\git.repos\one-day-intro\Modules\Images\MOD01_2017-10-26 10_43_54.png

12. Add the using statement to Views.

    1. Method 1: use CTRL+. to open the `Quick Actions and Refactorings` dropdown. Select `using GitHubBrowser.Views;`
    2. Method 2: Manually type `using GitHubBrowser.Views;` at the top of the class file.

13. Test your app.

    1. Build your app by hitting `F6`
    2. Run your app by hitting `F5` 

    > C:\Users\jnixon\git.repos\one-day-intro\Modules\Images\MOD01_2017-10-26 10_54_10.png

14. Change the app theme to `Dark`

    1. Open `App.xaml`
    2. Replace the `RequestedTheme` default value of `Light` with `Dark`

    ```xml
    <Application
    x:Class="GitHubBrowser.App"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:GitHubBrowser"
    RequestedTheme="Dark">
    </Application>      
    ```

    3. Test your app again by running it.

    > C:\Users\jnixon\git.repos\one-day-intro\Modules\Images\MOD01_2017-10-26 10_55_33.png

You have reached the end.