# Module 5
**TOC**
1. [UWP Toolkit NuGet package](#uwptoolkit)
1. [Update Logic](#updatelogic)
1. [Update UI](#updateui)

## Task 1: Add a reference to the UWP Toolkit NuGet package<a name="uwptoolkit"></a>

1. Add a reference to the `UWP Toolkit` NuGet package 

    > This library introduces a set of controls built by Microsoft and the XAML community. The library includes the `MasterDetailView` which looks a little like the Mail app, and we will use later in this module. 

    > `MasterDetailView` online documentation: https://docs.microsoft.com/en-us/windows/uwpcommunitytoolkit/controls/masterdetailsview 

    1. Open the NuGet Package Manager Console
    
    > (reusing image from module 2) C:\Users\jnixon\git.repos\one-day-intro\Modules\Images\MOD02_2017-10-26_12_26_42.png)

    2. Type the following into the console: `Install-Package Microsoft.Toolkit.Uwp.UI.Controls`

    ![ImageLabel](./Images/MOD05_2017-10-26_17_10_02.png)

## Task 2: Update Logic<a name="updatelogic"></a>

1. Copy `IssueViewModel.cs` and `IssuesPageViewModel.cs` into the `ViewModels` folder.

    > Find both `cs` files in `Module5/Files`

    ![ImageLabel](./Images/MOD05_2017-10-26_17_16_07.png)

    > TODO discuss ObservableCollection

    > TODO discuss item view-models

    > TODO discuss LINQ

1. Add a reference to `IssuesPageViewModel` in the `IssuesPage.xaml` markup, by setting the value of `Page.DataContext`, shown below. Ensure it has the name `ViewModel`.

    ```xml
    <Page.DataContext>
        <viewmodels:IssuesPageViewModel x:Name="ViewModel"/>
    </Page.DataContext> 
    ```

    The XAML above will require a new xml namespace declaration to your ViewModels namespace, as shown in the image below.

    ![ImageLabel](./Images/MOD05_2017-10-26_17_13_57.png)

1. Find the default `Grid` in `IssuesPage` and replace it with the XAML below:

    > Note: alternatively, this snippet can be also found in resources `Module5/Files/IssuesPage_Pivot_Snippet.txt`

    Find:  

    ```xml
    <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
      
    </Grid> 
    ```

    Replace with:

    ```xml
    <Pivot>

        <PivotItem Header="Open Issues">
            <TextBlock>Open</TextBlock>
        </PivotItem>

        <PivotItem Header="Closed Issues">
            <TextBlock>Closed</TextBlock>
        </PivotItem>

    </Pivot> 
    ```

1. Test your app, hit `F5`

    1. Click on Issues in the navigation view
    2. Pivot the Issues closed and open
    3. Notice your `TextBlocks` visibility changing

    ![ImageLabel](./Images/MOD05_2017-10-26_17_22_08.png)

## Task 3: Update UI<a name="updateui"></a>

1. Make a control-level resources available inside the `Pivot`, but adding `ListItemTemplate` and `ItemDetailTemplate` in the `Resources` property. This is quite a bit of XAML, so copy it from the snippet file.

    A DataTemplate is a XAML type used to define the visual for a specific data type. In this case, we are defining the visual for an Issue while in a list and an Issue when displayed in detail. 

    > This snippet can be  found in resources `Module5/Files/IssuesPage_PivotResources_Snippet.txt`

    > TODO: discuss resources

1. Inside the first `PivotItem` with its `Header` set to 'Open Issues" we will replace the entire block with a block (below) containing a `MasterDetailView`, from the UWP Toolkit, which we will use to display issues and issue detail. 

    ```xml
    <PivotItem Header="Open Issues">

        <toolkit:MasterDetailsView
            SelectedItem="{x:Bind ViewModel.SelectedIssue, Mode=TwoWay}"
            ItemsSource="{x:Bind ViewModel.OpenIssues}" 
            ItemTemplate="{StaticResource ListItemTemplate}"
            DetailsTemplate="{StaticResource ItemDetailTemplate}"/>

    </PivotItem>
    ```

    We do not have the namespace for the UWP Toolkit on our page yet. Use the refactoring tools to add the XAML namespace to the UWP toolkit.

    ![ImageLabel](./Images/MOD05_2017-10-26_17_29_26.png)

1. Just like the previous step: Inside the second `PivotItem` with its `Header` set to 'Closed Issues" we will replace the entire block with a block (below) containing a `MasterDetailView`, from the UWP Toolkit, which we will use to display issues and issue detail. 

    ```xml
    <PivotItem Header="Closed Issues">

        <toolkit:MasterDetailsView
            SelectedItem="{x:Bind ViewModel.SelectedIssue, Mode=TwoWay}"
            ItemsSource="{x:Bind ViewModel.ClosedIssues}" 
            ItemTemplate="{StaticResource ListItemTemplate}"
            DetailsTemplate="{StaticResource ItemDetailTemplate}"/>

    </PivotItem>
    ```

// end
