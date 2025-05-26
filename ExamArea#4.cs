/*
Why MudBlazor

Enables developers to build interactive web user interfaces using C#  and razor Syntax
In this project, I used Blazor WebAssembly which runs entirely in browser via WebAssembly,
thus everything renders client-side, offering fats and responsive experience .
One of Blazor's powerfuly feature is that it allows C# code and HTML to coexist in the same
.razor file. 
*/




/* 
Project Setup

You can inject dependicies. They decouple Blazor components more modular by decoupling them
from the implementation details. 

Dependencies are the external objects, services, or resources that a class relies on 
to do its work—such as databases, APIs, or other helper classes.
*/
@page "/"
@inject HttpClinet Http

await Http.GetFromJsonAsync<User>("api/user");




/* 
Using MudBlazor for UI

MudBlazor is a Material Design Component library for Blazor
Have components like MudText and MudGrid

*/
< MudTextField @bind-Value="searchString" Placeholder="Search" />





/* Authentication with Local Storage

Blaozr has a feature call Blazored.LocalStorage which securly stores token in the browser 
which can be used to check auth. 

When a page is loaded in the app, it goes through the OnInitilzedAsunc() method which
runs once whne the component is initialized. Use to load data from API and real local storage
This place is where the auth is checked

*/
protected override async Task OnInitializedAsync()
{
    await CheckAuthentication();
}



/* 
Loading Data from the API

HttpClient is used to enable client side data fetching in C#
Here is an example of blazor syntax which allows logic and markup in the same file

*/
var result = await Http.GetFromJsonAsync<List<GetAllCompaniesResponse>>("/api/company/get");
Companies = result;

@foreach (var company in FilteredCompanies)
{
    <MudCard>
        <MudText Typo="Typo.h6">@company.companyName</MudText>
        <MudButton OnClick="@(() => NavigateToCompany(company.companyID))">View Details</MudButton>
    </MudCard>
}

/*
Navigation and Routing

Used NavigationManager to go to company details
This navigation is handeled on the client-side, giving a smooth, fast experience, 
no full-page refresh. You can also have dynamic urls

*/

Navigation.NavigateTo($"/company/{companyID}");


/*
@bind-value in Blazor

Data binding directive in Blazor. Allows you to bind a C# variable to a UI element,
such as <input>. 
This creates a two-way binding, meaning:
    - When the suer types or changes the value in the UI, the C# variable updates
    - When the C# variabel changes, the UI updates

*/

<MudTextField @bind-Value="userName" Label="Name" />

/* 
Modal Dialog for Creating Companies

Controlled by _isCreateCompanyOpen boolean

*/

< MudOverlay Visible="_isCreateCompanyOpen">
    <MudTextField @bind-Value="_newCompanyName" />
    <MudButton OnClick="CreateCompany" > Create </ MudButton >
</ MudOverlay >

/*
Error handling

MudBlazor has snackbar to prompt any successes, warnings, and errors
This enhances user experience,
*/


Snackbar.Add("Company created successfully!", Severity.Success);


/* Pros and Cons

Pros:
    - Can write C# and HTML code in same file
    - Component-Based Architecture
    - Built in dependency injections
    - Rich Ecosystem with libraries

Cons:
    - Large Initial Load; have a large download size due to .NET runtime
    - Fewer frontend packages and integrations than React ecosystems
    - Blazor Servers have latency

*/




