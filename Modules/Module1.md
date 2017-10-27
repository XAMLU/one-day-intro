# Module 1

## Task 1: Orientation

> TODO: describe

1. Create GitHub account [https://github.com/join](https://github.com/join)
1. Ensure environment
    1. Windows 10 (any edition) Fall Creators Update
    1. Visual Studio 2017 (any edition) 15.4 or later
1. TODO: Copy resources locally

## Task 2: Introduce ShellPage

> TODO: describe

1. Create Blank UWP app.

    ![ImageLabel](./Images/MOD01_2017-10-26_10_27_11.png)

2. Name it `GitHubBrowser`
3. Select Fall Creators Update as both Target and Minimum Version.

    ![ImageLabel](./Images/MOD01_2017-10-26_10_27_54.png)

4. Open Solution Explorer. Select `View/Solution Explorer` 
5. Delete `MainPage.xaml` folder;
6. Create `Views` folder;
7. Create a new XAML page called `ShellPage` in the `Views` folder.

    > Right-click the `Views` folder, select `Blank Page` in the `Add New item` dialog.

    ![ImageLabel](./Images/MOD01_2017-10-26_10_31_56.png)

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

    ![ImageLabel](./Images/MOD01_2017-10-26_10_43_54.png)

12. Add the using statement to Views.

    1. Method 1: use CTRL+. to open the `Quick Actions and Refactorings` dropdown. Select `using GitHubBrowser.Views;`
    2. Method 2: Manually type `using GitHubBrowser.Views;` at the top of the class file.

13. Test your app.

    1. Build your app by hitting `F6`
    2. Run your app by hitting `F5` 

    ![Blank_Light](./Images/MOD01_2017-10-26_10_54_10.png)

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

    ![Blank_Dark](./Images/MOD01_2017-10-26_10_55_33.png)

## Task 3: Add a NavigationView

> TODO: describe

1. Add a folder called `Controls` to your solution
1. Copy `NavViewEx.cs` into your `Controls` folder

    > This file is located in resources `Module1/Files`

    ![ImageLabel](./Images/MOD01_2017-10-26_11_07_53.png)

    > TODO: describe

1. Add new pages in the `Views` folder

    1. `HomePage.xaml`
    1. `CodePage.xaml`
    1. `IssuesPage.xaml`
    1. `PullPage.xaml`
    1. `SettingsPage.xaml`

    ![ImageLabel](./Images/MOD01_2017-10-26_11_12_30.png)

1. Add the following namespace and property **to each new page**. Be sure to change the value of the `Header` property to match the name of the xaml page, as below: 

    > TODO: describe

    ```xml
    xmlns:controls="using:GitHubBrowser.Controls"
    controls:NavProperties.Header="Home page" 
    ```

    ![ImageLabel](./Images/MOD01_2017-10-26_11_17_08.png)

1. Open `ShellPage.xaml`

1. Add the following namespaces and `x:Name` directive to `ShellPage`

    > TODO: describe

    ```xml
    xmlns:controls="using:GitHubBrowser.Controls"
    xmlns:views="using:GitHubBrowser.Views" 
    x:Name="ThisPage"
    ```

    ![ImageLabel](./Images/MOD01_2017-10-26_11_20_45.png)

1. Copy the `Images` folder from resources into your solution

    Find the folder in `Module1/Files/Images`. Copy the folder with all the files, not the files individually. These images will be used as the icons in the Hamburger menu.

    ![ImageLabel](./Images/MOD01_2017-10-26_11_32_11.png)

1. Find the default `Grid` and replace it with the Extended Navigation View code below:

    > Note: alternatively, this snippet can be also found in resources `Module1/Files/ShellPage_NavViewEx_Snippet.txt`

    Find:  

    ```xml
    <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <TextBlock>Hello, World!</TextBlock>
    </Grid> 
    ```

    Replace with:

    ```xml
    <controls:NavViewEx x:Name="MainNavigationView" x:FieldModifier="Public"
                        SettingsPageType="views:SettingsPage">

        <NavigationView.HeaderTemplate>
            <DataTemplate>
                <CommandBar>
                    <CommandBar.Content>
                        <Grid Margin="12,5,0,11" VerticalAlignment="Stretch">
                            <TextBlock Text="{Binding}" Style="{StaticResource TitleTextBlockStyle}" TextWrapping="NoWrap" VerticalAlignment="Bottom"/>
                        </Grid>
                    </CommandBar.Content>
                </CommandBar>
            </DataTemplate>
        </NavigationView.HeaderTemplate>

        <NavigationView.MenuItems>
            <NavigationViewItemHeader Content="Repository" />
            <NavigationViewItem controls:NavProperties.PageType="views:HomePage" 
                                controls:NavProperties.IsStartPage="True" 
                                Icon="Home" Content="Home">
            </NavigationViewItem>
            <NavigationViewItem controls:NavProperties.PageType="views:CodePage"  
                                Content="Code">
                <NavigationViewItem.Icon>
                    <BitmapIcon UriSource="/Images/git-code-menu.png" />
                </NavigationViewItem.Icon>
            </NavigationViewItem>
            <NavigationViewItem controls:NavProperties.PageType="views:IssuesPage"  
                                Content="Issues">
                <NavigationViewItem.Icon>
                    <BitmapIcon UriSource="/Images/git-issue-menu.png" />
                </NavigationViewItem.Icon>
            </NavigationViewItem>
            <NavigationViewItem controls:NavProperties.PageType="views:PullPage"  
                                Content="Pull Requests">
                <NavigationViewItem.Icon>
                    <BitmapIcon UriSource="/Images/git-pull-request-menu.png" />
                </NavigationViewItem.Icon>
            </NavigationViewItem>
        </NavigationView.MenuItems>

    </controls:NavViewEx>
    ```

1. Run your app to test it; hit `F5`

    1. Click on the buttons to navigate
    1. Notice the page Header matches your settings
    1. Hide and show the menu with the hamburger button
    1. Notice the menu's Acrylic background brush
    1. Change your app's width
    1. Notice how the menu auto-adapts

    ![ImageLabel](./Images/MOD01_2017-10-26_11_36_00.png)