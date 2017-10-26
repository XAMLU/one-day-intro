# Sorting

This Lab Exercise will focus on sorting. Sorting with `Entity Framework Core` alters the generated `SQL` statement and performs the sort on the database server. In this lab, you will learn how to sort on multiple fields.

## Download Initial Files

1. Download the Lab exercise files **here**

    >**Note:** You can either download the Lab exercise files, or continue with the code from the previous module. We will be picking up where we left off.

2. Extract the files to a directory
3. Open the directory with `Visual Studio Code`

## Add Helper Methods

As with Module 1, we will use a Helper Class for this module. In this exercise you will add all the methods required for this Lab.

1. Locate the `Module2Helper` class. Add the following methods for this lab:

    ```csharp
    public static void Sort()
    {
        Console.WriteLine(nameof(Sort));
    }
    public static void SortDescending()
    {
        Console.WriteLine(nameof(SortDescending));
    }
    public static void Skip()
    {
        Console.WriteLine(nameof(Skip));
    }
    public static void Take()
    {
        Console.WriteLine(nameof(Take));
    }
    public static void Paging()
    {
        Console.WriteLine(nameof(Paging));
    }
    ```

## Launch Environment

Now that the methods for this lab are in place, it is time to call the `Sort` method on the `Module2Helper` static class.

1. Locate the `Program` class. Modify the `Main` method to call the `Sort` method on the `Module2Helper` static class.

    Your code should look like the following:

    ```csharp
    static void Main(string[] args)
    {
        Module2Helper.Sort();
    }
    ```

2. Run the application. Your output should look like the following:

    ![Initial Sort Execution](../../../Linked_Image_Files/M02-01-01-sort-initial.png)

## Sort Actor

For this first sorting exercise, we want to sort the actors by last name.

1. Open the `Module2Helper` class. Locate the `Sort` method. Replace the body with the following code.

    ```csharp
    var actors = MoviesContext.Instance.Actors
                    .OrderBy(a => a.LastName)
                    .Select(a => a.Copy<Actor, ActorModel>());
    ConsoleTable.From(actors).Write();
    ```

    >**Note:** This code is very similar to what you have already seen. The only difference is the call to `OrderBy`. In this example, the code orders by `LastName`.

2. Add any missing using statements. Below are the using statements that were added:

    ```csharp
    using System.Linq;
    using ConsoleTables;
    using MovieApp.Entities;
    using MovieApp.Extensions;
    using MovieApp.Models;
    ```

3. Run the app. The output should be similar to the following:

    ![Sort Actors](../../../Linked_Image_Files/M02-01-01-sort-actors.png)

The actors are now sorted by last name in ascending order.

## Sort Film by Rating

In this exercise, you will apply a sort to the films. We will start by sorting the films by rating.

1. Continue in the `Sort` method. Below the code you added from above, add the following code:

    ```csharp
    var films = MoviesContext.Instance.Films
                    .OrderBy(f => f.Rating)
                    .Select(f => f.Copy<Film, FilmModel>());
    ConsoleTable.From(films).Write();
    ```

Notice that the films are now sorted by the film rating, then by `FilmId`.

Your output should be similar to the following:

![Sort Films by Rating](../../../Linked_Image_Files/M02-01-01-sort-films-by-rating.png)

>**Note:** In the list of films, each film is sorted by the rating, then the primary key. In `MySQl`, data is stored in each table physically sorted by the primary key. If no `ORDER BY` clause is provided, this is the default sort order. In our example, there are multiple films for each rating, in which case the `FilmId` is used as the secondary sort criteria.

## Sort Film by Year

Sorting by rating is helping us group the films together. In this exercise, you will add a sort by year.

1. After the `OrderBy` method call, add a call to `ThenBy` to order by the films by `ReleaseYear`.

    ```csharp
    var films = MoviesContext.Instance.Films
                    .OrderBy(f => f.Rating)
                    .ThenBy(f => f.ReleaseYear)
                    .Select(f => f.Copy<Film, FilmModel>());
    ConsoleTable.From(films).Write();
    ```

![Sort Films by Rating](../../../Linked_Image_Files/M02-01-01-sort-films-by-rating-and-year.png)

>**Note:** Always use the `ThenBy` method for subsequent sort criteria. Using multiple `OrderBy` calls, will effectively ignore all but the last sort column.

## Sort Film by Title

For this final exercise, you will add a sort by title. This will eliminate the default sort by `FilmId`.

1. Add a third column for sorting, this time, the `Title`. Add a call to the `ThenBy` method as in the previous example, with `f => f.Title` as the parameter.

    The code for querying the films should now look like the following:

    ```csharp
    var films = MoviesContext.Instance.Films
                    .OrderBy(f => f.Rating)
                    .ThenBy(f => f.ReleaseYear)
                    .ThenBy(f => f.Title)
                    .Select(f => f.Copy<Film, FilmModel>());
    ConsoleTable.From(films).Write();
    ```

2. Run the application
3. The output should look like the following:

    ![Sort Films by Rating](../../../Linked_Image_Files/M02-01-01-sort-films-by-rating-year-and-title.png)

    Notice that the films are no longer sorted by the `FilmId` column, but are in the order we have specified.

    The `Sort` method should look like the following:

    ```csharp
    public static void Sort()
    {
        var actors = MoviesContext.Instance.Actors
                        .OrderBy(a => a.LastName)
                        .Select(a => a.Copy<Actor, ActorModel>());
        ConsoleTable.From(actors).Write();

        var films = MoviesContext.Instance.Films
                        .OrderBy(f => f.Rating)
                        .ThenBy(f => f.ReleaseYear)
                        .ThenBy(f => f.Title)
                        .Select(f => f.Copy<Film, FilmModel>());
        ConsoleTable.From(films).Write();
    }
    ```

## Summary

In this lab exercise, you have learned the basics of sorting data. You have learned how to sort by a single field, and also how to sort by multiple fields. In the next lab, you will learn how to sort in descending order.