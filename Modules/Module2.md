# Module 2

### Task 1: get your app's callback URI

1. Place the following code in the constructor of `App.xaml.cs`.

    > This snippet is the easiest way to get your callback URI, and once you have it, you can remove this one-time code from your app. 

    ```csharp
    var callbackUri = Windows.Security.Authentication.Web.WebAuthenticationBroker.GetCurrentApplicationCallbackUri(); 
    ```

1. Add a breakpoint (hit `F9`) on the new line.  
1. Run your app and look at the reulting value.

    > Note: the debug value will have curly braces at the start and the end `{ms-app://value}`, be sure and remove them so your URI is the correct syntax, like this `ms-app://value`. 

    ![ImageLabel](./Images/MOD02_2017-10-26 12_04_35.png

1. Copy your `Callback URI` to somewhere handy

### Task 2: Setup your app in Github

1. Open the following url: [https://github.com/settings/applications/new](https://github.com/settings/applications/new)

    1. You only need to provide four values
        1. **Application name** Naming convention XU_Oredev2017_\<YourName\>
        1. **Homepage URL** http://xamlu.com/XU_Oredev2017_\<YourName\>
        1. **Application description** "Sample app for Oredev"
        1. **Authorization Callback URL** Use the result from Task 1 above

        ![ImageLabel](./Images/MOD02_2017-10-26 12_17_46.png

    1. Click `Register application`.
    1. Copy *your* `Client Id` and `Client Secret` to somewhere handy

    ![ImageLabel](./Images/MOD02_2017-10-26 12_20_30.png

### Task 3: Add services to your app

1. Add a reference to the `Xamlu.Demo.GitHubLibrary` NuGet package 

    1. Open the NuGet Package Manager Console
    
    ![ImageLabel](./Images/MOD02_2017-10-26 12_26_42.png

    2. Type the following into the console: `Install-Package XamlU.Demo.GithubLibrary`

    ![ImageLabel](./Images/MOD02_2017-10-26 12_28_32.png

    > TODO: show the source code

    > TODO: discuss HTTP Client, show diagnostics

1. In Visual Studio, add a folder `Services` at the root level of your project.
1. Copy `SettingsService.cs` and `GithubService.cs` to the `Services` folder.
    
    > Find both files in `Module2/Files`

1. Update values in `SettingsService.cs`

    ![ImageLabel](./Images/MOD02_2017-10-26 12_35_11.png

    1. Replace `<your client id>` with your client id (from above)
    2. Replace `<your secret>' with your secret (from above)

    ![ImageLabel](./Images/MOD02_2017-10-26 12_36_17.png

### Task 4: Add authentication UI

1. Open `homePage.xaml`
1. On the root `Grid`, add the attribute `Padding="32"`
1. Paste the following `WaitUI` XAML inside the grid

    > This block of XAML will show while the user logs in. 

    ```xml
    <StackPanel x:Name="WaitUI" Visibility="{x:Bind ShowWaitUI, Mode=OneWay}">
        <ProgressBar IsIndeterminate="True" />
        <TextBlock Style="{StaticResource TitleTextBlockStyle}" Text="Please wait" />
    </StackPanel> 
    ```

1. Paste the following `LoggedOutUI` XAML below the `WaitUI` just added.

    > This block of XAML will show when the suer is logged out. 

    ```xml
    <StackPanel x:Name="LoggedOutUI" Visibility="{x:Bind ShowLoggedOutUI, Mode=OneWay}">
        <TextBlock Style="{StaticResource TitleTextBlockStyle}" Text="Authentication is required" />
        <Button Content="Login now" Click="{x:Bind LoginAsync}" />
    </StackPanel> 
    ```
1. Paste the following `LoggedInUI` XAML below the `LoggedOutUI` just added.

    > This block of XAML will show when the user is logged in. 

    ```xml
    <StackPanel x:Name="LoggedInUI" Visibility="{x:Bind ShowLoggedInUI, Mode=OneWay}">
        <TextBlock Style="{StaticResource TitleTextBlockStyle}" x:Name="UserNameTextBlock" Text="{x:Bind User.name, Mode=OneWay}" />
    </StackPanel> 
    ```
    > TODO: describe (binding) (INPC) (Boolean converter)

1. Open `HomePage.xaml.cs`

1. Implement `INotifyPropertyChanged`

    > HomePage inherits from page. We also want to add the declaration of the INotifyPropertyChanged interface to our HomePage and implement it.

    1. Add `INotifyPropertyChanged` to the class declaration

    > Note: you can use the refactoring tool by hitting `CTRL+.`

    ![ImageLabel](./Images/MOD02_2017-10-26 12_56_22.png

    1. Implement the interface. 
    
    ![ImageLabel](./Images/MOD02_2017-10-26 12_57_35.png

1. Copy the following code into `HomePage.xaml.cs`.

    > Note: alternatively, this snippet can be also found in resources `Module2/Files/HomePage_Auth_Snippet.txt`

    ![ImageLabel](./Images/MOD02_2017-10-26 13_22_41.png

1. Fix the namespaces

    ![ImageLabel](./Images/MOD02_2017-10-26 13_30_40.png

    Add the following:

    ```csharp
    using System.Threading.Tasks; 
    ```

    > Note: you can use the refactoring tool by hitting `CTRL+.`

1. Test your app, hit `F5`

    1. Click the `Login now` button
    1. Congratulations.

    ![ImageLabel](./Images/MOD02_2017-10-26 13_25_36.png

    ![ImageLabel](./Images/MOD02_2017-10-26 13_31_48.png

    ![ImageLabel](./Images/MOD02_2017-10-26 13_32_01.png

    ![ImageLabel](./Images/MOD02_2017-10-26 13_32_59.png

### Task 5: Add automatic authentication 

1. Add a `Loaded` handler to HomePage.xaml.cs

    > Note: you can use the refactoring tool by hitting `CTRL+.`

    ```csharp
    public HomePage()
    {
        this.InitializeComponent();
        this.Loaded += this.HomePage_Loaded;
    }

    private async void HomePage_Loaded(object sender, RoutedEventArgs e)
    {
        await this.LoginAsync();
    } 
    ```
    Note that the handler is an asynchronous method, decorated with the `async` modifier so `LoginAsync` can be awaited. In addition, asynchronous methods, as a rule of thumb, do not return void, but `event` handlers do not need to comply to this rule, since they can't. 

    ![ImageLabel](./Images/MOD02_2017-10-26 13_35_50.png

// end