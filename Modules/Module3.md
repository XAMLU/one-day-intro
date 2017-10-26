# Module 3

### Task 1: add a reference to the MVVMLight NuGet package

1. Add a reference to the `Xamu.Demo.GitHubLibrary` NuGet package 

    > This library introduces a basic Model View View-Model (MVVM) framework allowing your app to implement `INotifyPropertyChanged` with a common `ObservableObject` base class, use a concrete `RelayCommand` implementing `ICommand`, and providing a Messenger service (used in a later module).

    1. Open the NuGet Package Manager Console
    
    > (reusing image from module 2) C:\Users\jnixon\git.repos\one-day-intro\Modules\Images\MOD02_2017-10-26 12_26_42.png

    2. Type the following into the console: `Install-Package MvvmLightLibs`

### Task 2: move `HomePage.xaml.cs` code-behind to `HomePageViewModel.cs` 



### Task 3: implement Settings