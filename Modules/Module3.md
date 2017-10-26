# Module 3

### Task 1: add a reference to the MVVMLight NuGet package

1. Add a reference to the `MVVM Light` NuGet package 

    > This library introduces a basic Model View View-Model (MVVM) framework allowing your app to implement `INotifyPropertyChanged` with a common `ObservableObject` base class, use a concrete `RelayCommand` implementing `ICommand`, and providing a Messenger service (used in a later module).

    1. Open the NuGet Package Manager Console
    
    > (reusing image from module 2) C:\Users\jnixon\git.repos\one-day-intro\Modules\Images\MOD02_2017-10-26 12_26_42.png

    2. Type the following into the console: `Install-Package MvvmLightLibs`

    > C:\Users\jnixon\git.repos\one-day-intro\Modules\Images\MOD03_2017-10-26 13_55_24.png

### Task 2: move `HomePage.xaml.cs` code-behind to `HomePageViewModel.cs` 

1. In Visual Studio, add a folder `ViewModels` at the root level of your project.

1. Copy `HomePageViewModel.cs` to the `ViewModels` folder.

    > Find the file in `Module3/Files`

1. Add a reference to `HomePageViewModel` in the `HomePage.xaml` markup, by setting the value of `Page.DataContext`, shown below. Ensure it has the name `ViewModel`.

    ```xml
    <Page.DataContext>
        <viewmodels:HomePageViewModel x:Name="ViewModel"/>
    </Page.DataContext> 
    ```

    The XAML above will require a new xml namespace declaration to your  ViewModels namespace, as shown in the image below.

    > C:\Users\jnixon\git.repos\one-day-intro\Modules\Images\MOD03_2017-10-26 14_06_27.png

1. Update your XAML bindings to point to the view-model instead of the code-behind. Note that `{x:Bind ShowWaitUI}`, as an example, is now `{x:Bind ViewModel.ShowWaitUI}`.

    > You can replace the entire body of `HomePage` with the following XAML.

    ```xml
    <StackPanel x:Name="WaitUI" Visibility="{x:Bind ViewModel.ShowWaitUI, Mode=OneWay}">
        <ProgressBar IsIndeterminate="True" />
        <TextBlock Style="{StaticResource TitleTextBlockStyle}" Text="Please wait" />
    </StackPanel>

    <StackPanel x:Name="LoggedOutUI" Visibility="{x:Bind ViewModel.ShowLoggedOutUI, Mode=OneWay}">
        <TextBlock Style="{StaticResource TitleTextBlockStyle}" Text="Authentication is required" />
        <Button Content="Login now" Click="{x:Bind ViewModel.LoginAsync}" />
    </StackPanel>

    <StackPanel x:Name="LoggedInUI" Visibility="{x:Bind ViewModel.ShowLoggedInUI, Mode=OneWay}">
        <TextBlock Style="{StaticResource TitleTextBlockStyle}" x:Name="UserNameTextBlock" Text="{x:Bind ViewModel.User.name, Mode=OneWay}" />
    </StackPanel> 
    ```

    > C:\Users\jnixon\git.repos\one-day-intro\Modules\Images\MOD03_2017-10-26 14_10_56.png

1. We can now remove the unused code in the `HomePage.xaml.cs` code-behind file. 

    1. Open `HonmePage.xaml.cs`
    1. Remove `INotifyPropertyChanged` form the class declaration.
    1. Modify the `HomePage_Loaded` handler to call `ViewModel.LoginAsync()`
    1. Delete the `PropertyChanged` event.
    1. Delete all the properties.
    1. Delete `LoginAsync()`

    > C:\Users\jnixon\git.repos\one-day-intro\Modules\Images\MOD03_2017-10-26 14_15_23.png

1. Run the app, hit `F5`.

    > TODO: walk-through view-model
    > TODO: talk about seperation of concerns

### Task 3: implement Settings

1. 